﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Repleet.Data;
using Repleet.Models;
using Repleet.Models.Entities;
using System.Diagnostics;
using Repleet.Services;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Linq;
using Repleet.Contracts;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Repleet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProblemsAPIController : Controller
    {
        private readonly ApplicationDbContext dbContext; 
        private readonly ILogger<ProblemsAPIController> _logger;
        public ProblemsAPIController(ApplicationDbContext dbContext, ILogger<ProblemsAPIController> logger)
        {
            this.dbContext = dbContext;
            _logger = logger;
        }

        [Authorize]
        [HttpPost("submitratings")]


        //Take in a list of 18 ints seperated by strings, then create a New ProblemSet in the database
        //Return the ID of that ProblemSet
        public async Task<IActionResult> SubmitRatings(RatingRequestDTO sliderValuesRequest)

        {

            //Get User that's pinging the endpoint
            Guid userIdBefore = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            
            


            // Fetch the user from the database
            var userBefore = await dbContext.Users
                .Include(u => u.ProblemSet) // Include the ProblemSet to avoid a second query
                .FirstOrDefaultAsync(u => u.Id == userIdBefore);


            int? problemSetIDBefore = userBefore.ProblemSetId; // Get the current user's problemSetID to see if they've created one yet

            if (problemSetIDBefore != null) { return new JsonResult(Ok("Problem Already Exists For User")); }


            //ok now that we've confirmed they don't already have a PS, let's make them one.

            string sliderValues = sliderValuesRequest.RatingList;

            

            System.Collections.Generic.List<int> ratings = sliderValues.Split(',')
                                            .Select(int.Parse)
                                            .ToList();


            ProblemSet defaultProblemSet = DefaultData.GetDefaultProblemSet(); 

            for (int i = 0; i < defaultProblemSet.Categories.Count; i++)
            {
                defaultProblemSet.Categories[i].CurrentSkill = (SkillLevel)ratings[i];
                foreach (Problem problem in defaultProblemSet.Categories[i].Problems)
                {
                    problem.SkillLevel = (SkillLevel)ratings[i];
                }
            };

            //Each new creation in the DB saves the new ID.
            await dbContext.ProblemSets.AddAsync(defaultProblemSet);


            await dbContext.SaveChangesAsync();

            

            // Find the user and update their ProblemSetID
            var userFromDB = await dbContext.Users.FindAsync(userIdBefore);

            userFromDB.ProblemSetId = defaultProblemSet.ProblemSetId;

            dbContext.Users.Update(userFromDB);
            await dbContext.SaveChangesAsync();

            return new JsonResult(Ok(defaultProblemSet.ProblemSetId));

        }
        [Authorize]
        [HttpGet("getnextproblem")]
        /* 
         * This function takes in nothing, loads the users problemset and and calculates the 
         * next problem with the problemPickerService. returns the info the front end needs to display problem on a card.
         */
        public async Task<IActionResult> GetNextProblem()

        {

            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);


            // Fetch the user from the database
            

            var user = await dbContext.Users
                .Include(u => u.ProblemSet) // Include the ProblemSet to avoid a second query
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null) { return new JsonResult(Ok("User Not Found")); }

            int? problemSetID = user.ProblemSetId; // Get the current user's problemSetID

            if (problemSetID == null)
            {
                return  new JsonResult(Ok("Problem Set Not Found"));

            }

            //just grab problemSet from DB with both it's categories and problems
            var MyProblemSet = await dbContext.ProblemSets
            .Include(ps => ps.Categories)
            .ThenInclude(c => c.Problems)
            .SingleOrDefaultAsync(ps => ps.ProblemSetId == problemSetID);

            if (MyProblemSet == null) { return new JsonResult(Ok("Problem Set Not Found")); }

            //In this Problem Set, call the PickNextCategory and PickNextProblem Service to give us the problem to show to the User.
            var ProblemPicker = new ProblemPicker(MyProblemSet);

            var NextCategory = ProblemPicker.PickNextCategory();



            var NextProblem = ProblemPicker.PickProblemFromCategory(NextCategory);
            //Note that we turn this problem into the problemInfo DTO for a standard between front end and back end.
            var NextProblemDTO = NextProblem.AsProblemInfoDTO();

            return new JsonResult(Ok(NextProblemDTO));

        }
        [Authorize]
        [HttpPost("submitproblem")]
        /*
         * This function takes in the Problem Name, Category Name, and report of how well a user did on the problem.
         * With that, it recalculates the New Skill Levels for the problem and category and adds it back to the problemset in the DB.
         */
        public async Task<IActionResult> SubmitProblem(SubmitProblemRequestDTO SPR)
        {

            Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);


            // Fetch the user from the database
            var user = await dbContext.Users
                .Include(u => u.ProblemSet) // Include the ProblemSet to avoid a second query
                .FirstOrDefaultAsync(u => u.Id == userId);


            int? problemSetID = user.ProblemSetId; // Get the current user's problemSetID

            string ProblemName = SPR.ProblemName;

            string CategoryName = SPR.CategoryName;

            SkillLevel Report = SPR.Report;

            //just grab problemSet from DB with both it's categories and problems
            var MyProblemSet = await dbContext.ProblemSets
            .Include(ps => ps.Categories)
            .ThenInclude(c => c.Problems)
            .SingleOrDefaultAsync(ps => ps.ProblemSetId == problemSetID);
            if (MyProblemSet == null) { return new JsonResult(Ok("Problem Set Not Found")); }


            var ProblemPicker = new ProblemPicker(MyProblemSet);

            var UpdatedProblemSet = ProblemPicker.SubmitProblem(ProblemName, CategoryName, Report);

            //this updates the categories in the database thanks to EF Core, should update both Categories and Problem Details
            MyProblemSet.Categories = UpdatedProblemSet.Categories;

            await dbContext.SaveChangesAsync();


            Category matchingCategory = MyProblemSet.Categories.Where(c => c.Name == CategoryName).FirstOrDefault(); //returns null if not found

            Problem matchingProblem = matchingCategory.Problems.Where(p => p.Title == ProblemName).FirstOrDefault(); //should be reference to object, not value 

            var matchingProblemDTO = matchingProblem.AsProblemInfoDTO();

            return new JsonResult(Ok(matchingProblemDTO));




        }
        [Authorize]
        [HttpGet("getcategoryprogress")]
        /* 
         * This endpoint loads the user's problemSet, and based on the progress for each problem, gives it a score 1-100 of how close they are to finishing it.
         * 
         */
        public async Task<IActionResult> GetCategoryProgress()
        {
            Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);


            // Fetch the user from the database
            var user = await dbContext.Users
                .Include(u => u.ProblemSet) // Include the ProblemSet to avoid a second query
                .FirstOrDefaultAsync(u => u.Id == userId);


            int? problemSetID = user.ProblemSetId; // Get the current user's problemSetID

            if (problemSetID == null)
            {
                return new JsonResult(Ok("Problem Set Not Found"));

            }


            //just grab problemSet from DB with both it's categories and problems
            var MyProblemSet = await dbContext.ProblemSets
            .Include(ps => ps.Categories)
            .ThenInclude(c => c.Problems)
            .SingleOrDefaultAsync(ps => ps.ProblemSetId == problemSetID);

            if (MyProblemSet == null) { return new JsonResult(Ok("Problem Set Not Found")); }

            Dictionary<string, int> userProgress = GetProgressOfProblemSet.GetProgressOfProblemSetService(MyProblemSet);

            ProblemSetProgressResponseDTO response = new ProblemSetProgressResponseDTO(userProgress);

            return new JsonResult(response);

        }
    }
}
