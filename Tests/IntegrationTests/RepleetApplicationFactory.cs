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
    public class RepleetApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
        
    {

        private PostgreSqlContainer _postgresContainer = new PostgreSqlBuilder()
                .WithImage("postgres:latest")
                .WithDatabase("testdb")
                .WithUsername("testuser")
                .WithPassword("testpassword")
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

           var currentDirectory = Directory.GetCurrentDirectory();
           builder.UseContentRoot(Path.Combine(Directory.GetCurrentDirectory(), "../../../")); //TODO, see how to get around contentRoot
            // ../../../



            builder.ConfigureTestServices(services => {

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


                

                services.AddDbContext<ApplicationDbContext>((container,options) =>
                { 
                    
                    options.UseNpgsql(_postgresContainer.GetConnectionString());
                });
                
                services.AddAuthentication(
                    
                    options =>
                    {
                        options.DefaultAuthenticateScheme = "TestScheme";
                        options.DefaultChallengeScheme = "TestScheme";
                    }
                    
                    ).AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
                    "TestScheme", options => { });

            });

           // builder.UseEnvironment("Development");

        }

        
    }
    
}
