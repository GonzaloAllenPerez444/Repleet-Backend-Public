using Repleet.Models.Entities;
using Repleet.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Repleet.Services
{

    /* This is a data structure that keeps track of completed problem scores and their difficulty*/

    /* The class is how you pick the next problem to work on a submit problems when you are done, and submit problems to adjust skill levels
     * of categories and problems accordignly.
     Input: a UserSet to pick problems from.
     */
    internal class ProblemPicker
    {
        ProblemSet UserSet { get; set; }
        


        public ProblemPicker(ProblemSet p)
        {
            UserSet = p;
        }



        /* Given the user's current categories, put all of the categories with the lowest skillLevel in a list and pick a random one*/
        public Category PickNextCategory()
        {
            Random random = new Random();

            SkillLevel? lowestSkillLevel = UserSet.Categories.Min(c => c.CurrentSkill);
            List<Category> lowestSkillCategories = UserSet.Categories
                                               .Where(c => c.CurrentSkill == lowestSkillLevel)
                                               .ToList();

            int randomIndex = random.Next(lowestSkillCategories.Count);

            return lowestSkillCategories[randomIndex];

        }

        //input: pass in category with valid skillLevel 
        //output: problem that matches user's progress the best, based on skillLevel on individual problems
        public Problem PickProblemFromCategory(Category category)
        {
            
            List<Problem> SubsetDifficulty;
            if (category.CurrentSkill == SkillLevel.Horrible) { SubsetDifficulty = category.Problems.Where(p => p.Difficulty == QuestionDifficulty.Easy).ToList(); }
            else if (category.CurrentSkill == SkillLevel.lacking) { SubsetDifficulty = category.Problems.Where(p => p.Difficulty == QuestionDifficulty.Easy || p.Difficulty == QuestionDifficulty.Medium).ToList(); }
            else if (category.CurrentSkill == SkillLevel.alright) { SubsetDifficulty = category.Problems.Where(p => p.Difficulty == QuestionDifficulty.Medium).ToList(); }
            else if (category.CurrentSkill == SkillLevel.good) { SubsetDifficulty = category.Problems.Where(p => p.Difficulty == QuestionDifficulty.Medium || p.Difficulty == QuestionDifficulty.Hard).ToList(); }
            else { SubsetDifficulty = category.Problems.Where(p => p.Difficulty == QuestionDifficulty.Medium || p.Difficulty == QuestionDifficulty.Hard).ToList(); }

            SubsetDifficulty = SubsetDifficulty.Where(c => c.IsCompleted != true).ToList(); //this makes it so problems can't be repeated once mastered, I think thats an ok design decision.

            //There could be a case where a person's skill level is horrible for a category with no easy problems
            //we adjust for that here.
            if (SubsetDifficulty.Count == 0) { SubsetDifficulty = category.Problems.Where(p => p.Difficulty == QuestionDifficulty.Medium).ToList(); }


            List<Problem> NotCompletedYet = SubsetDifficulty.Where(c => c.IsCompleted != true).ToList(); 

            if (NotCompletedYet.Count > 0) { SubsetDifficulty = NotCompletedYet; };
            



            //Pick the problem(s) with the lowest skill level.


            Random random = new Random();
            SkillLevel? lowestSkillLevel = SubsetDifficulty.Min(c => c.SkillLevel);
            List<Problem> lowestSkillProblems = SubsetDifficulty.Where(c => c.SkillLevel == lowestSkillLevel).ToList();

            int randomIndex = random.Next(lowestSkillProblems.Count);

            return lowestSkillProblems[randomIndex];

        }

        //input: Problem name, User Skill report, Category name that the Problem is from
        //output: we change UserSet of this class to reflect the changes and return it.
        public ProblemSet SubmitProblem(string ProblemName, string CategoryName, SkillLevel Report)

        {
            Category matchingCategory = UserSet.Categories.Where(c => c.Name == CategoryName).FirstOrDefault(); //returns null if not found
            Debug.Assert(matchingCategory != null, "Category Name does not exist");

            Problem matchingProblem = matchingCategory.Problems.Where(p => p.Title == ProblemName).FirstOrDefault(); //should be reference to object, not value 
            Debug.Assert(matchingProblem != null, "Problem Name does not match in category");

            matchingProblem.CompletionDate = DateTime.Now;



            // Once a user puts this as perfect, they won't see this problem again.
            if (Report == SkillLevel.perfect) { matchingProblem.IsCompleted = true; };
            matchingProblem.SkillLevel = Report;

            //alter the Category itself potentially if this new report changes the overall balance of skill.
            /*Process:
            get list of the skillLevelPair for all problems in this category you have attempted.
            if it's easy, you get skillLevel -1 points, medium = skillLevel, hard = skillLevel + 2
            get average of this score above, and set the category skill level to be that average. (have to make sure avg is inbetween 1 and 5)
            */
            List<Problem> CompletedProblems = matchingCategory.Problems.Where(p => p.CompletionDate != null).ToList();
            List<SkillQuestionPair> skillQuestionPairs = new List<SkillQuestionPair>();

            foreach (Problem p in CompletedProblems) {

                SkillQuestionPair s = new SkillQuestionPair(p.SkillLevel, p.Difficulty);
                skillQuestionPairs.Add(s);

            }
            //the problem we just did should have been added in the loop above
            

            //get average of score across all skillQuestionPairs
            SkillLevel AvgSkillLevel = CompletedProblemsSource.GetAvgSkillLevel(skillQuestionPairs);
            matchingCategory.CurrentSkill = AvgSkillLevel;


            return UserSet;

        }

    }
}
