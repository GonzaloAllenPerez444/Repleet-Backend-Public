using Microsoft.IdentityModel.Tokens;
using Repleet.Models.Entities;
using Repleet.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace Repleet.Tests.UnitTests
{
    public class AuthServiceTests
    {
        private readonly ITestOutputHelper testOutputHelper;

        public AuthServiceTests(ITestOutputHelper outputHelper)
        {
            testOutputHelper = outputHelper;
        }
        private AuthService CreateTokenService(out IConfiguration config)
        {
            var configValues = new Dictionary<string, string>
        {
            { "Appsettings:Token", "ThisIsASecretKeyForTestingPurposes!" },
            { "Appsettings:Issuer", "TestIssuer" },
            { "Appsettings:Audience", "TestAudience" }
        };

            config = new ConfigurationBuilder()
                .AddInMemoryCollection(configValues)
                .Build();

            return new AuthService(null,config);
        }

        //TEST FOR GENERATE TOKEN
        [Fact]
        public void GenerateToken_Returns_Valid_JWT_With_Expected_Claims()
        {
            // Arrange
            var testGUID = Guid.NewGuid();
            var tokenService = CreateTokenService(out var config);
            var user = new ApplicationUser
            {
                Id = testGUID,
                Username = "testuser",
                Role = "Admin"
            };

            // Act
            string tokenString = tokenService.GenerateToken(user);

            // Assert
            Assert.False(string.IsNullOrWhiteSpace(tokenString));

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(config["Appsettings:Token"]);

            tokenHandler.ValidateToken(tokenString, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = config["Appsettings:Issuer"],
                ValidAudience = config["Appsettings:Audience"],
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            
            Assert.Equal("testuser", jwtToken.Claims.First(c => c.Type == JwtRegisteredClaimNames.Name).Value);
            Assert.Equal("Admin", jwtToken.Claims.First(c => c.Type == "role").Value);
            Assert.Equal(testGUID.ToString(), jwtToken.Claims.First(c => c.Type == "nameid").Value);
            
        }

        //TESTS FOR GenerateRefreshToken
        [Fact]
        public void GenerateRefreshToken_Returns_NonEmpty_Base64String()
        {
            // Arrange
            var tokenService = new AuthService(null, null);

            // Act
            var refreshToken = tokenService.GenerateRefreshToken();

            // Assert
            Assert.False(string.IsNullOrWhiteSpace(refreshToken));

            // Validate it is a valid base64 string
            byte[] data;
            var isValidBase64 = Convert.TryFromBase64String(refreshToken, new Span<byte>(new byte[32]), out _);
            Assert.True(isValidBase64);
        }

        [Fact]
        public void GenerateRefreshToken_Returns_Unique_Strings()
        {
            // Arrange
            var tokenService = new AuthService(null, null);

            // Act
            var token1 = tokenService.GenerateRefreshToken();
            var token2 = tokenService.GenerateRefreshToken();

            // Assert
            Assert.NotEqual(token1, token2);

        }
    }
}
