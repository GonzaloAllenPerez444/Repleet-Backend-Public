using Azure;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Protocol;
using Repleet.Contracts;
using Repleet.Data;
using Repleet.Models;
using Repleet.Services;
using System.Net;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using JsonException = Newtonsoft.Json.JsonException;

namespace Repleet.Tests.IntegrationTests
{
    public class ControllerTests : IClassFixture<RepleetApplicationFactory<Program>>
    {
        //Test the Create, Read, Update, and Delete operations for each entity to ensure they interact correctly with the database.
        private readonly RepleetApplicationFactory<Program> _factory;
        private HttpClient _httpClient;

        public ControllerTests(RepleetApplicationFactory<Program> factory)
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
               // db.Database.Migrate();
               //this would apply all the migrations of the project, which isn't necessary since i just want it from the schema.
               

                RatingRequestDTO request = new RatingRequestDTO("5,3,3,3,3,1,1,1,1,1,2,2,2,2,2,4,4,5");



                var response = await _httpClient.PostAsJsonAsync("/api/ProblemsAPI/submitratings", request);

                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                
                using var jsonDoc = JsonDocument.Parse(responseContent);


                var root = jsonDoc.RootElement;
                Assert.True(root.TryGetProperty("value", out var valueProperty), "Value property should exist.");
                Assert.True(valueProperty.ValueKind == JsonValueKind.Number, "Value should be a number.");


                var value = valueProperty.GetInt32(); //change to be more specific if we swithc to UUIDs later.
                Assert.True(value >= 0, "Value should be a non-negative number.");


            };//end scope



        }
        [Fact]
        public async Task Improper_Ratings_Handles_Error() { }

        [Fact]
        public async Task Get_Next_Problem_Returns_Problem_in_DB() {

            //make sure that this next problem is correct, pretty straightforward getter.
            
            using (var scope = _factory.Services.CreateScope())
            {

                var scopedServices = scope.ServiceProvider;

                var db = scopedServices.GetRequiredService<ApplicationDbContext>();

                db.Database.EnsureCreated();
                
                SeedDatabase.InitializeTestDB(db);


                var response = await _httpClient.GetAsync("/api/ProblemsAPI/getnextproblem?problemSetID=2");

                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<ProblemInfoDTO>();

                //unit tests assert that the problem picker works, we just have to check that we get a valid problem back.
                Assert.NotNull(result);
                Assert.IsType<ProblemInfoDTO>(result);

            }

            }

        
        [Fact]
        public async Task Get_Next_Problem_With_Invalid_ID_Handles_Error() {

            using (var scope = _factory.Services.CreateScope())
            {

                var scopedServices = scope.ServiceProvider;

                var db = scopedServices.GetRequiredService<ApplicationDbContext>();

                db.Database.EnsureDeleted();
                db.Database.EnsureCreated(); //this should delete any existing data in the db and replace it with a blank db

                var response = await _httpClient.GetAsync("/api/ProblemsAPI/getnextproblem?problemSetID=2");

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
        public async Task Submit_Problem_Correctly_Updates_Database() {
            
            using (var scope = _factory.Services.CreateScope())
            {

                var scopedServices = scope.ServiceProvider;

                var db = scopedServices.GetRequiredService<ApplicationDbContext>();

                db.Database.EnsureCreated();

                SeedDatabase.InitializeTestDB(db);

                SubmitProblemRequestDTO request = new SubmitProblemRequestDTO( "Car Fleet", "Stack", SkillLevel.lacking);

                var response = await _httpClient.PostAsJsonAsync<SubmitProblemRequestDTO>("/api/ProblemsAPI/submitproblem", request);

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

                

            }


        }



    }
}
