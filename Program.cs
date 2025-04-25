using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Repleet.Data;
using Repleet.Models.Entities;
using Swashbuckle.AspNetCore.Filters;
using System.Security.Claims;
using Npgsql;
using Repleet.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Repleet.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//This is FOR DOCKER COMPOSE SETUP ONLY
//var DBHost = Environment.GetEnvironmentVariable("DB_HOST");
//var DBName = Environment.GetEnvironmentVariable("DB_NAME");
//var DBPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");

//var connectionString = $"Data Source={DBHost};Initial Catalog={DBName};User ID=sa;Password={DBPassword};TrustServerCertificate=true" ?? throw new InvalidOperationException("Connection String Incorrect");

//AZURE SETUP STARTS HERE, this should inject proper variables for both publishing and development
// SQL SERVER (not using anymore) var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

String? connectionString = null;

//This should get all configuration from user secrets / environmental variables
builder.Services.Configure<AppConfiguration>(builder.Configuration);


// Only add Swagger in Development

if (builder.Environment.IsDevelopment())
{
    connectionString = builder.Configuration.GetConnectionString("PostgreSQLConnection");
    

    builder.Services.AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
        });

        options.SwaggerDoc("v1", new OpenApiInfo { Title = "Repleet API", Version = "v1" });
        options.OperationFilter<SecurityRequirementsOperationFilter>();
    });

    builder.Services.AddDatabaseDeveloperPageExceptionFilter();

    // Configure logging - only in development
    builder.Logging.ClearProviders();
    builder.Logging.AddConsole();
    builder.Logging.AddDebug();
}

//production features
else {

    //TODO - refactor connectionString to use environmental variable

   connectionString = builder.Configuration.GetConnectionString("PostgreSQLConnection");

    

}






builder.Services.AddDbContext<ApplicationDbContext>(options =>
    //previous sql server options.UseSqlServer(connectionString, sqlOptions

    options.UseNpgsql(connectionString, npgsqlOptions =>

    {
        npgsqlOptions.EnableRetryOnFailure(
                maxRetryCount: 10,   // Maximum retry attempts
                maxRetryDelay: TimeSpan.FromSeconds(5), // Delay between retries
                errorCodesToAdd: null // Optional: specify PostgreSQL error codes to handle
            );

    }
    ));




builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var config = builder.Configuration;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = config["AppSettings:Issuer"],
        ValidAudience = config["AppSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["AppSettings:Token"]!))
    };
});


builder.Services.AddAuthorization();


builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;  // Ensure HTTPS
    options.Cookie.SameSite = SameSiteMode.None;  // Allow cross-origin requests
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();


var allowedOrigins = builder.Configuration
    .GetSection("Cors:AllowedOrigins")
    .Get<string[]>();
if (allowedOrigins != null)
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowSpecificOrigins",
            policy =>
            {// Define allowed origins for both dev and prod -> could make into env vars later

                if (builder.Environment.IsDevelopment())
                {
                    policy.WithOrigins(allowedOrigins)
                          //policy.AllowAnyOrigin()               
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials(); // Allow Cookies or JWT
                }
                else
                {

                    //JWT TOKENS! TODO
                    //policy.WithOrigins(allowedOrigins)
                    //.WithHeaders("Content-Type", "Authorization")
                    //.WithMethods("GET", "POST");

                    //Current Policy (Cookies)
                    policy.WithOrigins(allowedOrigins)
                    .WithHeaders("Content-Type", "Authorization") // whitelist only necessary headers
                    .WithMethods("GET", "POST", "PUT", "DELETE")  // only what your API needs
                    .AllowCredentials(); // Only if you're using cookies/auth

                }
            });


    });
};//end cors







var app = builder.Build();
             
app.UseCors("AllowSpecificOrigins");

app.UseRouting();
app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline - onluy in development
if (app.Environment.IsDevelopment())
{

     app.UseMigrationsEndPoint();//check for migrarion errors in production
     app.UseSwagger();
    
     app.UseSwaggerUI(c =>
     {
         c.SwaggerEndpoint("/swagger/v1/swagger.json", "Repleet API V1");
         c.RoutePrefix = string.Empty;
     });
        

}
else
{
    
}








app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});


//This may be causing errors in prod - how else could i do migrations?
//using (var scope = app.Services.CreateScope())
//{
    //var dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();
  //  dbContext.Database.Migrate();
//}

app.Run();

public partial class Program { }