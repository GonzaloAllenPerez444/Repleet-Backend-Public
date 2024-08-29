using Newtonsoft.Json.Linq;
using Repleet.Contracts;
using Repleet.Models.Entities;

namespace Repleet.Services
{
    public static class GetProgressOfProblemSet
    {
        public static Dictionary<string,int> GetProgressOfProblemSetService(ProblemSet p)
        {
            Dictionary<string,int> result = new Dictionary<string, int>();



            foreach (Category category in p.Categories)
            {

                float currentTemp = 0;
                float idealSum = 5 * category.Problems.Count;
                foreach (Problem problem in category.Problems)
                {
                    currentTemp += (int) problem.SkillLevel;
                }
                float percentDone = (currentTemp / idealSum) * 100;
                int percentDoneInt = (int) Math.Clamp(percentDone, 1, 100);
                result[category.Name] = percentDoneInt;
                

            }
            return result;
        }
    }

}