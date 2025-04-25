using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Repleet.Tests.IntegrationTests
{
    public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public TestAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {

        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {

            Guid userId;

            if (Request.Headers["X-Test-UserId"].FirstOrDefault() == null) { userId = Guid.Parse("00000000-0000-0000-0000-00000000abcd"); }

            else { userId = Guid.Parse(Request.Headers["X-Test-UserId"].FirstOrDefault()); };
            




            var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(ClaimTypes.Name, "testuser@example.com"),
            new Claim(ClaimTypes.Email, "testuser@example.com"),          
            
        };

            

            var identity = new ClaimsIdentity(claims,"Test");
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal,"TestScheme");

            var result = AuthenticateResult.Success(ticket);

            return Task.FromResult(result);
        }
    }
}
