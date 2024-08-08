using Repleet.Models;
using Repleet.Models.Entities;
using Repleet.Services;
using Xunit;
using Xunit.Abstractions;
using System.Linq;

namespace Repleet.Tests.UnitTests
{
    public class ProblemPickerTests
    {
        private readonly ITestOutputHelper _output; //this is for development purposes only
        private readonly ProblemPicker _problemPicker;
        private readonly ProblemSet _problemSet;
        public ProblemPickerTests(ITestOutputHelper output)
        {
            _output = output;
            int NUMCATEGORIES = 18;

            //Set Up Custom ProblemSet
            Random random = new Random(44);
            ProblemSet PS = DefaultData.GetDefaultProblemSet();

            int[] set1 = {1, 2,3,4,5 };
            int index1 = random.Next(set1.Length);
            for (int i = 0; i < NUMCATEGORIES; i++)
            {
                int[] set2 = { 1, 2, 3, 4, 5 };
                int index2 = random.Next(set2.Length);
                SkillLevel tempSL = (SkillLevel)set2[index2];
                PS.Categories[i].CurrentSkill = tempSL;
               //_output.WriteLine($"skill level of {PS.Categories[i].Name} is {PS.Categories[i].CurrentSkill}");


                foreach (Problem p in PS.Categories[i].Problems) {
                    index2 = random.Next(set2.Length);
                    tempSL = (SkillLevel)set2[index2];
                    p.SkillLevel = tempSL;
                    //if (PS.Categories[i].Name == "Two Pointers")
                    //{
                       // _output.WriteLine($"skill level of {p.Title} is {p.SkillLevel} and difficulty is {p.Difficulty}");
                    //};
                }
            };
            /*  skill level of Arrays & Hashing is good
                skill level of Two Pointers is good
                skill level of Sliding Window is lacking
                skill level of Stack is alright
                skill level of Binary Search is Horrible
                skill level of Linked Lists is perfect
                skill level of Heap/Priority Queue is alright
                skill level of Trees is perfect
                skill level of Backtracking is Horrible
                skill level of Tries is perfect
                skill level of Graphs is Horrible
                skill level of Advanced Graphs is alright
                skill level of 1D Dynamic Programming is alright
                skill level of 2D Dynamic Programming is perfect
                skill level of Greedy is alright
                skill level of Intervals is alright
                skill level of Math & Geometry is lacking
                skill level of Bit Manipulation is perfect
             * 
             */

            /* 
             * Stack problems
             *skill level of Valid Parentheses is Horrible and difficulty is Easy
              skill level of Min Stack is perfect and difficulty is Medium
              skill level of Evaluate Reverse Polish Notation is good and difficulty is Medium
              skill level of Generate Parentheses is perfect and difficulty is Medium
              skill level of Daily Temperatures is good and difficulty is Medium
              skill level of Car Fleet is alright and difficulty is Medium
              skill level of Largest Rectangle in Histogram is alright and difficulty is Hard

            Backtracking problems:
              skill level of Subsets is good and difficulty is Medium
              skill level of Combination Sum is lacking and difficulty is Medium
              skill level of Permutations is Horrible and difficulty is Medium
              skill level of Subsets 2 is lacking and difficulty is Medium
              skill level of Combination Sum 2 is alright and difficulty is Medium
              skill level of Word Search is alright and difficulty is Medium
              skill level of Palindrome Partitioning is good and difficulty is Medium
              skill level of Letter Combinations of a Phone Number is alright and difficulty is Medium
              skill level of N Queens is good and difficulty is Hard
             *
             *
             *
             *
             * 
             */



            _problemSet = PS;
            _problemPicker = new ProblemPicker(_problemSet);

        }


        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(10)]
        public void PickNextCategoryTests(int Dummy) {
            // From Preset, Selected Category Should either be Binary Search or Stack
            Category SelectedCategory = _problemPicker.PickNextCategory();
            var allowedNames = new[] { "Backtracking", "Binary Search","Graphs" };
            Assert.NotNull( SelectedCategory );
            Assert.Contains(SelectedCategory.Name, allowedNames);
            
        }
        
        [Fact]
        public void PickNextProblemTests() {

            Category Stack = _problemSet.Categories[3];
            Category Backtracking = _problemSet.Categories[8];

            List<String> predictedStackProblems = new List<string>() { "Car Fleet" };
            List<String> predictedBacktrackingProblems = new List<String>() { "Permutations"};

            String ActualStackProblem = _problemPicker.PickProblemFromCategory(Stack).Title;
            String ActualBacktrackingProblem = _problemPicker.PickProblemFromCategory(Backtracking).Title;

            Assert.Contains(ActualStackProblem, predictedStackProblems);
            Assert.Contains(ActualBacktrackingProblem, predictedBacktrackingProblems);

        }

        [Fact]
        public void SubmitProblemTests()
        {
            //check that problem gets completed when submitted as Perfect, including isCompleted Changed and Completetion Date Not Null
            //since we check avgSkillLevel somewhere else, just verify that the correct category in the UserSet was changed correctly.

            ProblemSet Submitted = _problemSet;

            Assert.Null(Submitted.Categories[1].Problems[1].CompletionDate);
            Assert.False(Submitted.Categories[1].Problems[1].IsCompleted);
            Assert.Equal(SkillLevel.good, Submitted.Categories[1].CurrentSkill); //randomly set from constructor


            //Case where No problems have been submitted yet
            Submitted = _problemPicker.SubmitProblem("Two Sum 2 Input Array is Sorted", "Two Pointers", SkillLevel.lacking);
            Assert.NotNull(Submitted.Categories[1].Problems[1].CompletionDate);
            
            Assert.Equal(SkillLevel.lacking, Submitted.Categories[1].CurrentSkill);


            //Case where multiple problems have been submitted.
            Submitted = _problemPicker.SubmitProblem("3 Sum", "Two Pointers", SkillLevel.perfect);
            Assert.True(Submitted.Categories[1].Problems[2].IsCompleted);
            Assert.NotNull(Submitted.Categories[1].Problems[2].CompletionDate);
            Assert.Equal(SkillLevel.good, Submitted.Categories[1].CurrentSkill);

        }


    }
}
