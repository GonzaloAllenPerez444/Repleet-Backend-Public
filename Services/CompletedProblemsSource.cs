using Repleet.Models;
using Xunit;

namespace Repleet.Services
{





    public class CompletedProblemsSource
    {
        

        public CompletedProblemsSource()
        { }

        //This helper skill level applies a score to each completed problem and returns the avg score.
        //Used for re-calculating Category Skill level after problem submission
        //Input: List of completed problems for a category (len >= 1), output: avg SkillLevel based on completed problem skillLevel and Difficulty.
        public static SkillLevel GetAvgSkillLevel(List<SkillQuestionPair> SQP)
        {
            Assert.NotEmpty(SQP);

            double totalScore = 0;
            int count = SQP.Count;

            foreach (var pair in SQP)
            {
                int skillLevel = (int)pair.SkillLevel;

                switch (pair.QuestionDifficulty)
                {
                    case QuestionDifficulty.Easy:
                        totalScore += skillLevel - 1;
                        break;
                    case QuestionDifficulty.Medium:
                        totalScore += skillLevel;
                        break;
                    case QuestionDifficulty.Hard:
                        totalScore += skillLevel + 1;
                        break;
                }
            }

            double averageScore = totalScore / count;

            // Clamp the averageScore to be within the valid range of SkillLevel
            averageScore = Math.Max(1, Math.Min(averageScore, 5));

            // Round to the nearest integer and cast to SkillLevel
            SkillLevel averageSkillLevel = (SkillLevel)Math.Round(averageScore);

            return averageSkillLevel;
        }


    }

}







