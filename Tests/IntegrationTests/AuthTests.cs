using Docker.DotNet.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Repleet.Contracts;
using Repleet.Controllers;
using Repleet.Data;
using Repleet.Models.Entities;
using Repleet.Services;
using Xunit;

namespace Repleet.Tests.IntegrationTests
{
    public class AuthTests : IClassFixture<RepleetApplicationFactoryAuth>
    {
        //Test the Create, Read, Update, and Delete operations for each entity to ensure they interact correctly with the database.
        private readonly RepleetApplicationFactoryAuth _factory;
        private HttpClient _httpClient;

        public AuthTests(RepleetApplicationFactoryAuth factory)
        {
            _factory = factory;
            factory.InitializeAsync();
            
            
            _httpClient = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

        }

        
        [Fact]
        public async Task Login_Authenticates_User_and_returns_valid_tokens_And_Refresh_Valid_Tokens_Returns_Valid_Token() {

            using (var scope = _factory.Services.CreateScope())
            {

                var scopedServices = scope.ServiceProvider;

                var db = scopedServices.GetRequiredService<ApplicationDbContext>();

                db.Database.EnsureCreated();

                // Create a new user
                var testUser = new ApplicationUser
                {
                    Username = "testuser-auth",
                    ProblemSetId = null,

                };
                testUser.Id = Guid.Parse("00000000-0000-0000-0000-00000000abcd");


                var passwordHasher = new PasswordHasher<ApplicationUser>();
                var userPassword = "TestPassword123!";
                testUser.PasswordHash = passwordHasher.HashPassword(testUser, userPassword);

                // Add the user to the context and save changes
                db.Users.Add(testUser);
                db.SaveChanges();

                //call Login with Username and password
                ApplicationUserDTO request = new ApplicationUserDTO();
                request.Username = testUser.Username;
                request.Password = userPassword;

                AuthService myAuthService = new AuthService(db, _factory.Services.GetService<IConfiguration>());
                TokenResponseDTO response = await myAuthService.LoginAsync(request);

                Assert.NotNull(response);

                //Make Sure we don't get a 401 from pingauth
                var myAuthController = new AuthController(myAuthService);

                var pingAuthResult = myAuthController.PingAuth();

                var okResult = Assert.IsType<OkObjectResult>(pingAuthResult);
                Assert.Equal(200, okResult.StatusCode);


                //See if we can validate the refresh Token from here
                RefreshTokenRequestDTO refreshRequest = new RefreshTokenRequestDTO()
                { refreshToken = response.RefreshToken , userID = testUser.Id};
                
                TokenResponseDTO response2 = await myAuthService.RefreshTokensAsync(refreshRequest);
                Assert.NotNull(response2);

                //Try to Ping Auth Again, first with original token then with new

                //Original token should be null here
                ApplicationUser? validation1 = await myAuthService.ValidateRefreshTokenAsync(db, testUser.Id, refreshRequest.refreshToken);
                Assert.Null(validation1);

                //New Token Should NOT be Null
                ApplicationUser? validation2 = await myAuthService.ValidateRefreshTokenAsync(db, testUser.Id, response2.RefreshToken);
                Assert.NotNull(validation2);


            }


            }


        //TEST FOR RegisterAsync
        [Fact]
        public async Task Register_User_Adds_User_to_DB() {
            using (var scope = _factory.Services.CreateScope())
            {

                var scopedServices = scope.ServiceProvider;

                var db = scopedServices.GetRequiredService<ApplicationDbContext>();

                db.Database.EnsureCreated();

                ApplicationUserDTO request = new ApplicationUserDTO();
                request.Username = "testuser-auth-2";
                request.Password = "TestPassword123!";

                IAuthService myAuthService = new AuthService(db, _factory.Services.GetService<IConfiguration>());

                ApplicationUser response = await myAuthService.RegisterAsync(request);
                Assert.NotNull(response.Username);
                
                
                var matchingUser = db.Users.Where(c => c.Username == response.Username).FirstOrDefault();
                Assert.NotNull(matchingUser);



            }


            }


        









        }

    }
