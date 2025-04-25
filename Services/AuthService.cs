using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repleet.Contracts;
using Repleet.Data;
using Repleet.Models.Entities;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Repleet.Services
{
    public class AuthService(ApplicationDbContext _dbContext, IConfiguration configuration): IAuthService
    {
        ApplicationDbContext dbContext = _dbContext;
        


        public String GenerateToken(ApplicationUser user) { 
        
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("Appsettings:Token")!));

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), //specific id for each token
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new(JwtRegisteredClaimNames.Name,user.Username),
                new Claim(ClaimTypes.Role,user.Role),
                //new(JwtRegisteredClaimNames.Email, user.Email)

            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {

                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(60),
                Issuer = configuration.GetValue<string>("Appsettings:Issuer"), //idk what to put here tbh (project name?)
                Audience = configuration.GetValue<string>("Appsettings:Audience"),
                
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        
        }
        public async Task<string> GenerateAndSaveRefreshTokenAsync(ApplicationUser user)
        {

            var refreshToken = GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await dbContext.SaveChangesAsync();

            return refreshToken;


        }

        public async Task<TokenResponseDTO> CreateTokenResponse(ApplicationUser? user) 
        
        {
            return new TokenResponseDTO
            {
                AccessToken = GenerateToken(user),
                RefreshToken = await GenerateAndSaveRefreshTokenAsync(user)
            };

        }
        public async Task<TokenResponseDTO?> RefreshTokensAsync( RefreshTokenRequestDTO dto) {

            var user = await ValidateRefreshTokenAsync(dbContext, dto.userID, dto.refreshToken);
            if (user == null) {return null;}


            return await CreateTokenResponse(user);
            

        }
        public string GenerateRefreshToken() {
            
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }

        public async Task<ApplicationUser?> ValidateRefreshTokenAsync(ApplicationDbContext context, Guid userID, string refreshToken) 
        { 
        
            var user = await context.Users.FindAsync(userID);

            if (user == null || user.RefreshToken != refreshToken
                || user.RefreshTokenExpiryTime <= DateTime.UtcNow) {

                return null;
            };

            return user;
        }

        public async Task<TokenResponseDTO> LoginAsync(ApplicationUserDTO request)
        {
            ApplicationUser user = await dbContext.Users.FirstOrDefaultAsync(u => u.Username == request.Username);

            if (user is null) { return null; };

            if (user.Username != request.Username) { return null; };

            if (new PasswordHasher<ApplicationUser>().VerifyHashedPassword(user, user.PasswordHash, request.Password)
                == PasswordVerificationResult.Failed)
            {

                return null;

            }

            var response = new TokenResponseDTO
            {
                AccessToken = GenerateToken(user),
                RefreshToken = await GenerateAndSaveRefreshTokenAsync(user),
            };

            return response;
        }

        public async Task<ApplicationUser?> RegisterAsync(ApplicationUserDTO request)
        {
            if (await dbContext.Users.AnyAsync(u => u.Username == request.Username))
            {
                return null;
            }

            var user = new ApplicationUser();

            var hashedPassword = new PasswordHasher<ApplicationUser>()
                .HashPassword(user, request.Password);

            user.Username = request.Username;
            user.PasswordHash = hashedPassword;
            //user.Email = "default";


            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();

            return user;
        }

        

     }
}


