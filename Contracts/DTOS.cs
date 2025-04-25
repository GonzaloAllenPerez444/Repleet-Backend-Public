using Repleet.Models;
using Repleet.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace Repleet.Contracts
{
    public record RatingRequestDTO(
        [Required][StringLength(35)] string RatingList
        );

    public class ApplicationUserDTO
    {
        
        public string Username { get; set; }
        public string Password { get; set; }

        //idk if we need the following in the UserDTO
        //public string ID { get; set; }
        //public string Email { get; set; }
        //public int? ProblemSetId { get; set; }
        //public ProblemSet ProblemSet { get; set; }

    }

    public class TokenResponseDTO
    {
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }

    };

    public class RefreshTokenRequestDTO {
        public Guid userID { get; set; }
        public required string refreshToken { get; set; }
    };
    public record ProblemInfoDTO(

        string Title,
        string Url,
        bool IsCompleted,
        DateTime? CompletionDate,
        QuestionDifficulty Difficulty,
        SkillLevel SkillLevel,
        string CategoryName 


        );
    public record SubmitProblemRequestDTO(
         string ProblemName, string CategoryName, SkillLevel Report
        );

    public record ProblemSetProgressResponseDTO(
        Dictionary<string, int> Data
        )
    {
        
    }
}
