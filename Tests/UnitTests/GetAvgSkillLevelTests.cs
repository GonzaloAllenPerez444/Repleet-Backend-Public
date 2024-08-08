using Repleet.Models;
using Repleet.Services;
using Xunit;
using Xunit.Abstractions;
namespace Repleet.Tests.UnitTests
{
    public class GetAvgSkillLevelTests
    {

        private readonly ITestOutputHelper _output; //this is for development purposes only
        public GetAvgSkillLevelTests(ITestOutputHelper output)
        {
           _output = output;

        }
        public static List<SkillQuestionPair> GenerateSkillQuestionsPairs (int number)
        {
            //generate a random set of SQP - set random with SEED to get the same ones each time.
            //int SEED = 420;
            Random random = new Random(420);

            List<SkillQuestionPair> result = new List<SkillQuestionPair>();
            for (int i = 0; i < number; i++) {

                int[] set1 = { 0, 1, 2 };
                int index1 = random.Next(set1.Length);
                QuestionDifficulty tempQD = (QuestionDifficulty) set1[index1];

                int[] set2 = { 1, 2, 3, 4, 5 };
                int index2 = random.Next(set2.Length);
                SkillLevel tempSL = (SkillLevel) set2[index2];

                result.Add(new SkillQuestionPair(tempSL, tempQD));

            };
            return result; 

        }


        [Fact] 
        public void No_completed_problems_should_error()
        {
            List<SkillQuestionPair> empty = new List<SkillQuestionPair>();
            Assert.Throws<Xunit.Sdk.NotEmptyException>(() => CompletedProblemsSource.GetAvgSkillLevel(empty));
        }
        [Fact]
        public void All_Easy_Questions() {
            List<SkillQuestionPair> AllEasy = new List<SkillQuestionPair>();
            AllEasy.Add(new SkillQuestionPair(SkillLevel.Horrible, QuestionDifficulty.Easy));

            SkillLevel Result = CompletedProblemsSource.GetAvgSkillLevel(AllEasy);
            Assert.Equal(SkillLevel.Horrible, Result);

            AllEasy.Add(new SkillQuestionPair(SkillLevel.good,QuestionDifficulty.Easy));

            Result = CompletedProblemsSource.GetAvgSkillLevel(AllEasy);
            Assert.Equal(SkillLevel.lacking, Result);

            AllEasy.Add(new SkillQuestionPair(SkillLevel.perfect,QuestionDifficulty.Easy));
            AllEasy.Add(new SkillQuestionPair(SkillLevel.perfect, QuestionDifficulty.Easy));

            Result = CompletedProblemsSource.GetAvgSkillLevel(AllEasy);

            Assert.Equal(SkillLevel.alright,Result);

        }

        [Fact]
        public void All_Hard_Questions() {
            List<SkillQuestionPair> AllHard = new List<SkillQuestionPair>();
            AllHard.Add(new SkillQuestionPair(SkillLevel.perfect,QuestionDifficulty.Hard)) ;

            SkillLevel Result = CompletedProblemsSource.GetAvgSkillLevel(AllHard);
            Assert.Equal(SkillLevel.perfect,Result);

            AllHard.Add(new SkillQuestionPair(SkillLevel.Horrible,QuestionDifficulty.Hard));

            Result = CompletedProblemsSource.GetAvgSkillLevel(AllHard) ;

            Assert.Equal(SkillLevel.good, Result);


        }

        [Fact]
        public void Works_With_Random_SQP()
        {
            List<SkillQuestionPair> randomProbs = GenerateSkillQuestionsPairs(10); //the avg skill level of this should be 22 / 10 = 2.2 = SkillLevel.Lacking
            SkillLevel Result = CompletedProblemsSource.GetAvgSkillLevel(randomProbs);

            Assert.Equal(SkillLevel.lacking, Result);


        }

    }
}
