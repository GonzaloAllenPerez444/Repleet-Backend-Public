using Repleet.Contracts;

namespace Repleet.Models.Entities
{
    public static class EntityExtensions
    {
        public static ProblemInfoDTO AsProblemInfoDTO(this Problem p)
        {
            return new ProblemInfoDTO(

               p.Title,
               p.Url,
               p.IsCompleted,
               p.CompletionDate,
               p.Difficulty,
               p.SkillLevel,
               p.Category.Name
                );
        }
    }
}
