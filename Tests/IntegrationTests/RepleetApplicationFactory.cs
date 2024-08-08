using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Repleet.Data;
using System.Data.Common;

namespace Repleet.Tests.IntegrationTests
{
    public class RepleetApplicationFactory<IProgram> : WebApplicationFactory<IProgram> where IProgram : class
    {

        /* This commented out part is if I would like to switch to use an actual docker mysql db for the integration tests
         * Functionally, i don't think there is a significant enough reason to go this far for this application just yet.
         * 
         * protected override void ConfigureWebHost(IWebHostBuilder builder)
        {

            

            builder.UseContentRoot("D:/RepleetProject/Repleet");


            builder.ConfigureTestServices(services => {


                services.RemoveAll(typeof(DbContextOptions<ApplicationDbContext>));

                var ConnString = GetConnectionString();

                services.AddSqlServer<ApplicationDbContext>(ConnString);

                var MyDBContext = CreateDbContext(services);

                MyDBContext.Database.EnsureDeleted(); 
                

                

            });
            
        } */

        //using in memory db (note that it still uses same dbcontext)
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {

            base.ConfigureWebHost(builder);

            builder.UseContentRoot("D:/RepleetProject/Repleet");

            


            builder.ConfigureTestServices(services => {

                

                var dbContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));


                services.Remove(dbContextDescriptor);

                var dbConnectionDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbConnection));

                services.Remove(dbConnectionDescriptor);

                services.AddSingleton<DbConnection>(container =>
                {
                    var connection = new SqliteConnection("DataSource=:memory:");
                    connection.Open();
                    return connection;
                
                });

                services.AddDbContext<ApplicationDbContext>((container,options) =>
                { 
                    var connection = container.GetRequiredService<DbConnection>();
                    options.UseSqlite(connection);
                });


            });

            builder.UseEnvironment("Development");

        }

        //for use with actual db only
        /*
        private static string? GetConnectionString()
        {
            var configuration = new ConfigurationBuilder().AddUserSecrets<RepleetApplicationFactory>().Build();

            var ConnString = configuration.GetConnectionString("DefaultConnectionTests");
            return ConnString;
        }

        //for use with actual db only
        private static ApplicationDbContext CreateDbContext(IServiceCollection services) {
        
            var serviceProvider = services.BuildServiceProvider();
            var scope = serviceProvider.CreateScope();
            var MyDBContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            return MyDBContext;
        }*/
    }
    
}
