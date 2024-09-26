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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//This is FOR DOCKER COMPOSE SETUP ONLY
//var DBHost = Environment.GetEnvironmentVariable("DB_HOST");
//var DBName = Environment.GetEnvironmentVariable("DB_NAME");
//var DBPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");

//var connectionString = $"Data Source={DBHost};Initial Catalog={DBName};User ID=sa;Password={DBPassword};TrustServerCertificate=true" ?? throw new InvalidOperationException("Connection String Incorrect");

//AZURE SETUP STARTS HERE, this should inject proper variables for both publishing and development
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");




builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString, sqlOptions =>

    {
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 10,   // Maximum retry attempts
            maxRetryDelay: TimeSpan.FromSeconds(5),  // Delay between retries
            errorNumbersToAdd: null // Optional: specify SQL error numbers to handle
        );
    }
    ));



builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<ApplicationUser>().AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;  // Ensure HTTPS
    options.Cookie.SameSite = SameSiteMode.None;  // Allow cross-origin requests
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();


// Only add Swagger in Development
/*if (builder.Environment.IsDevelopment())
{
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
} */

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        policy =>
        {// Define allowed origins for both dev and prod -> could make into env vars later
            policy.WithOrigins("http://127.0.0.1:4173", "https://127.0.0.1:4173", "https://repleetfrontend.onrender.com", "http://localhost:5173", "https://localhost:5173", "https://repleet-frontend.vercel.app", "https://repleet-frontend.vercel.app/")
                                
             //policy.AllowAnyOrigin()               
                  .AllowAnyHeader()  
                  .AllowAnyMethod()
                  .AllowCredentials(); // Allow Cookies or JWT
        });

    
});



// Configure logging - only in development
//builder.Logging.ClearProviders();
//builder.Logging.AddConsole();
//builder.Logging.AddDebug();



var app = builder.Build();
             
app.UseCors("AllowSpecificOrigins");

app.UseRouting();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapIdentityApi<ApplicationUser>();

app.MapPost("/logout", async (SignInManager<ApplicationUser> signInManager ) =>
{
    await signInManager.SignOutAsync();
    return Results.Ok();
}).RequireAuthorization();

app.MapGet("/pingauth", (ClaimsPrincipal user) =>
{
    var email = user.FindFirstValue(ClaimTypes.Email); //get user email from claim
    return Results.Json(new { Email = email }); //return that email as plain text response.
}).RequireAuthorization();

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline - onluy in development
if (app.Environment.IsDevelopment())
{

     //app.UseMigrationsEndPoint();//check for migrarion errors in production
   //app.UseSwagger();
    
   //app.UseSwaggerUI(c =>
   // {
         //  c.SwaggerEndpoint("/swagger/v1/swagger.json", "Repleet API V1");
         //  c.RoutePrefix = string.Empty;
        //});
        var a = 1;

}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
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