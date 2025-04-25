using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Repleet.Data;
using Testcontainers.PostgreSql;
using System.Data.Common;
using Xunit;

namespace Repleet.Tests.IntegrationTests
{
    public class RepleetApplicationFactoryAuth : WebApplicationFactory<Program>, IAsyncLifetime

    {

        private PostgreSqlContainer _postgresContainer = new PostgreSqlBuilder()
                .WithImage("postgres:latest")
                .WithDatabase("testdb-auth")
                .WithUsername("testuser-auth")
                .WithPassword("testpassword-auth")
                .WithCleanUp(true)
                .Build();
        

        public Task InitializeAsync() {
            return _postgresContainer.StartAsync();
        }

        public Task DisposeAsync()
        {
            return _postgresContainer.StopAsync();
        }



        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {

            base.ConfigureWebHost(builder);

            builder.UseContentRoot(Path.Combine(Directory.GetCurrentDirectory(), "../../../"));
            // builder.UseContentRoot("D:/RepleetProject/Repleet"); //TODO, see how to get around contentRoot




            builder.ConfigureTestServices(services =>
            {

                // Ensure container is initialized before configuring services
                if (_postgresContainer == null)
                {
                    throw new InvalidOperationException("PostgreSQL container has not been initialized.");
                }


                var dbContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));


                if (dbContextDescriptor != null)
                {
                    services.Remove(dbContextDescriptor);
                }

                var dbConnectionDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbConnection));

                if (dbConnectionDescriptor != null)
                {
                    services.Remove(dbConnectionDescriptor);
                }




                services.AddDbContext<ApplicationDbContext>((container, options) =>
                {
                    var c = _postgresContainer.GetConnectionString();
                    options.UseNpgsql(_postgresContainer.GetConnectionString());
                });

                // Create mock configuration
                var inMemorySettings = new Dictionary<string, string>
                {
                { "Appsettings:Token", "test-token-security-key-this-is-supposed-to-be-very-very-long-to-work-1234" },
                    {"Appsettings:Issuer", "test-issuer"},
                    { "Appsettings:Audience", "test-issuer" }

                };

                IConfiguration configuration = new ConfigurationBuilder()
                    .AddInMemoryCollection(inMemorySettings)
                    .Build();
               // _configuration = configuration;
                services.AddSingleton<IConfiguration>(configuration);

                //We Want to Test the actual Auth so we are commenting this out
                //    services.AddAuthentication(

                //        options =>
                //        {
                //            options.DefaultAuthenticateScheme = "TestScheme";
                //            options.DefaultChallengeScheme = "TestScheme";
                //        }

                //        ).AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
                //        "TestScheme", options => { });

                //});

                // builder.UseEnvironment("Development");

            });



    }

    }

}
