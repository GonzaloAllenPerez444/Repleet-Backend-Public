namespace Repleet.Models
{
    public class SkillQuestionPair
    {
        public SkillLevel? SkillLevel { get; set; }
        public QuestionDifficulty QuestionDifficulty { get; set; }

        public SkillQuestionPair(SkillLevel? skillLevel, QuestionDifficulty questionDifficulty)
        {
            SkillLevel = skillLevel;
            QuestionDifficulty = questionDifficulty;
        }
    };
}
