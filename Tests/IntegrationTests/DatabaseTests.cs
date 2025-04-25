﻿using Azure;
using Docker.DotNet.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Protocol;
using Repleet.Contracts;
using Repleet.Controllers;
using Repleet.Data;
using Repleet.Models;
using Repleet.Models.Entities;
using Repleet.Services;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using JsonException = Newtonsoft.Json.JsonException;

namespace Repleet.Tests.IntegrationTests
{
    public class ControllerTests : IClassFixture<RepleetApplicationFactory>
    {
        //Test the Create, Read, Update, and Delete operations for each entity to ensure they interact correctly with the database.
        private readonly RepleetApplicationFactory _factory;
        private HttpClient _httpClient;

        public ControllerTests(RepleetApplicationFactory factory)
        {
            _factory = factory;
            _httpClient = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });





        }

        [Fact]
        public async Task Submit_Ratings_Creates_New_ProblemSet() {

            using (var scope = _factory.Services.CreateScope())
            {

                var scopedServices = scope.ServiceProvider;

                var db = scopedServices.GetRequiredService<ApplicationDbContext>();
                
                db.Database.EnsureCreated();
                

                RatingRequestDTO request = new RatingRequestDTO("5,3,3,3,3,1,1,1,1,1,2,2,2,2,2,4,4,5");

                // Create a new user
                var testUser = new ApplicationUser
                {
                    Username = "testuser@example.com",
                    ProblemSetId = null,
                   
                };
                testUser.Id = Guid.Parse("00000000-0000-0000-0000-00000000abcd");

                
                var passwordHasher = new PasswordHasher<ApplicationUser>();
                testUser.PasswordHash = passwordHasher.HashPassword(testUser, "TestPassword123!");

                // Add the user to the context and save changes
                db.Users.Add(testUser);
                db.SaveChanges();



                //Add the initial user in the dbcontext 
                var response = await _httpClient.PostAsJsonAsync("/api/ProblemsAPI/submitratings", request);

                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                
                using var jsonDoc = JsonDocument.Parse(responseContent);


                var root = jsonDoc.RootElement;
                Assert.True(root.TryGetProperty("value", out var valueProperty), "Value property should exist.");
                Assert.True(valueProperty.ValueKind == JsonValueKind.Number, "Value should be a number.");


                var value = valueProperty.GetInt32(); //change to be more specific if we swithc to UUIDs later.
                Assert.True(value >= 0, "Value should be a non-negative number.");

                //clean up
                db.Database.EnsureDeleted();

            };//end scope



        }

        [Fact]
        public async Task Get_Next_Problem_Returns_Problem_in_DB() {
            int TEST_NUMBER = 0;

            //make sure that this next problem is correct, pretty straightforward getter.

            using (var scope = _factory.Services.CreateScope())
            {

                var scopedServices = scope.ServiceProvider;

                var db = scopedServices.GetRequiredService<ApplicationDbContext>();

                db.Database.EnsureCreated();
                
                SeedDatabase seeded = new SeedDatabase(db);
                await seeded.SeedAsync();

                var users = db.Users.ToList();
               
                var chosenUser = users[TEST_NUMBER];


                //Inject USERS in the request

                var request = new HttpRequestMessage(HttpMethod.Get, "/api/ProblemsAPI/getnextproblem");
                request.Headers.Add("X-Test-UserId", chosenUser.Id.ToString());


                var response = await _httpClient.SendAsync(request);

                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<ProblemInfoDTO>();

                //unit tests assert that the problem picker works, we just have to check that we get a valid problem back.
                Assert.NotNull(result);
                Assert.IsType<ProblemInfoDTO>(result);

            }

            }

        
        [Fact]
        public async Task Get_Next_Problem_With_Invalid_User_Handles_Error() {

            using (var scope = _factory.Services.CreateScope())
            {

                var scopedServices = scope.ServiceProvider;

                var db = scopedServices.GetRequiredService<ApplicationDbContext>();

                db.Database.EnsureDeleted();
                db.Database.EnsureCreated(); //this should delete any existing data in the db and replace it with a blank db
                

                var testUser = new ApplicationUser
                {
                    Username = "testuser@example.com",
                    Email = "testuser@example.com",
                    ProblemSetId = null, // this is meant to be an improper ID

                };
                testUser.Id = Guid.Parse("00000000-0000-0000-0000-00000000abcd");
                var passwordHasher = new PasswordHasher<ApplicationUser>();
                testUser.PasswordHash = passwordHasher.HashPassword(testUser, "Test123!");

                // Add the user to the context and save changes
                db.Users.Add(testUser);
                
                db.SaveChanges();

                var response = await _httpClient.GetAsync("/api/ProblemsAPI/getnextproblem");

                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                var result = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(responseString);

                // Check that the result contains the expected message
                var message = result.GetProperty("value").GetString();
                Assert.Equal("Problem Set Not Found", message);

            };
            }

        [Fact]
        public async Task Submit_Problem_With_Incorrect_ProblemSetID()
        {
            using (var scope = _factory.Services.CreateScope())
            {

                var scopedServices = scope.ServiceProvider;

                var db = scopedServices.GetRequiredService<ApplicationDbContext>();

                db.Database.EnsureDeleted();
                db.Database.EnsureCreated(); //this should delete any existing data in the db and replace it with a blank db

                SubmitProblemRequestDTO request = new SubmitProblemRequestDTO( "Car Fleet", "Stack", SkillLevel.lacking);

                // Create a new user
                var testUser = new ApplicationUser
                {
                    Username = "testuser@example.com",
                    Email = "testuser@example.com",
                    ProblemSetId = null, // Maybe needs to be a number, idk why though?

                };
                testUser.Id = Guid.Parse("00000000-0000-0000-0000-00000000abcd");
                
                var passwordHasher = new PasswordHasher<ApplicationUser>();
                testUser.PasswordHash = passwordHasher.HashPassword(testUser, "Test123!");

                // Add the user to the context and save changes
                db.Users.Add(testUser);
                db.SaveChanges();

                var response = await _httpClient.PostAsJsonAsync<SubmitProblemRequestDTO>("/api/ProblemsAPI/submitproblem",request);

                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                var result = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(responseString);

                // Check that the result contains the expected message
                var message = result.GetProperty("value").GetString();
                Assert.Equal("Problem Set Not Found", message);




            };

        }

        [Fact]
        public async Task Submit_Problem_Correctly_Updates_Database()
        {
            int TEST_NUMBER = 2;

            using (var scope = _factory.Services.CreateScope())
            {

                var scopedServices = scope.ServiceProvider;

                var db = scopedServices.GetRequiredService<ApplicationDbContext>();

                db.Database.EnsureCreated();

                SeedDatabase seeded = new SeedDatabase(db);
                await seeded.SeedAsync();

                var users = db.Users.ToList();
               
                var chosenUser = users[TEST_NUMBER];

                // Create the DTO
                SubmitProblemRequestDTO requestDTO = new SubmitProblemRequestDTO("Car Fleet", "Stack", SkillLevel.lacking);

                // Serialize the DTO
                var json = System.Text.Json.JsonSerializer.Serialize(requestDTO);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Create the request
                var request = new HttpRequestMessage(HttpMethod.Post, "/api/ProblemsAPI/submitproblem")
                {
                    Content = content
                };

                // Add custom header
                request.Headers.Add("X-Test-UserId", chosenUser.Id.ToString());
                // var response = await _httpClient.PostAsJsonAsync<SubmitProblemRequestDTO>("/api/ProblemsAPI/submitproblem", request);
                var response = await _httpClient.SendAsync(request);

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {


                    var responseString = await response.Content.ReadAsStringAsync();
                    JsonElement result = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(responseString);

                    // Check that the completion date is not null
                    var resultJSON = result.GetProperty("value").GetProperty("completionDate");

                    Assert.NotNull(resultJSON.ToString());
                    Assert.NotEmpty(resultJSON.ToString());





                }
                //clean up
                db.Database.EnsureDeleted();



            }


        }

        //TODO Tests for GetCategoryProgress

        
        



    }
}
