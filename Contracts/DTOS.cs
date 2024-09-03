using Repleet.Models;
using System.ComponentModel.DataAnnotations;

namespace Repleet.Contracts
{
    public record RatingRequestDTO(
        [Required][StringLength(35)] string RatingList
        );
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
