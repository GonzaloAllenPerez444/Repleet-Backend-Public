using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Repleet.Models.Entities
{
    public class ApplicationUser //: IdentityUser
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }

        public string? Email { get; set; }
        public int? ProblemSetId { get; set; }
        public ProblemSet ProblemSet { get; set; }

        public string Role {  get; set; } = string.Empty;

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }



    }
}
