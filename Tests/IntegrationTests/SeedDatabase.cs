using Repleet.Data;
using Repleet.Models;
using Repleet.Models.Entities;
using Bogus;
using Xunit;
using Microsoft.AspNetCore.Identity;
using Azure.Identity;
using Bogus.DataSets;

namespace Repleet.Tests.IntegrationTests;

//Not Currently Used
public class SeedDatabase
{
    private readonly ApplicationDbContext _context;
    private readonly Faker _faker;


    public SeedDatabase(ApplicationDbContext context)
    {
        _context = context;
        _faker = new Faker();
    }

    public async Task SeedAsync() {

        await SeedUserWithProblemSet();

    }

    private async Task SeedUserWithProblemSet(int count = 6) {
        
        if (_context.Users.Any()) { return; };



        List<ProblemSet> psFakerList = new List<ProblemSet> { };

        for (int i = 0; i < count; i++) {

            ProblemSet psFaker = DefaultData.GetDefaultProblemSet();
            psFaker.ProblemSetId = i + 100;
            psFakerList.Add(psFaker);
        };
            
           
        
        await _context.ProblemSets.AddRangeAsync(psFakerList);
        await _context.SaveChangesAsync(); // Ensure it gets an ID

        var passwordHasher = new PasswordHasher<ApplicationUser>();
        var users = new List<ApplicationUser>();

        for (int i = 0; i < count; i++)
        {
            var ps = psFakerList[i];
            var user = new ApplicationUser
            {
                Username = _faker.Internet.UserName(),
                Email = _faker.Internet.Email(),
                ProblemSetId = ps.ProblemSetId,
                Id = Guid.NewGuid(),
            };

            user.PasswordHash = passwordHasher.HashPassword(user, "Test123!");
            users.Add(user);
        }

        await _context.Users.AddRangeAsync(users); //why does this just straight up remove problemSet and problemSetID lol
        await _context.SaveChangesAsync();


    }



    //public static void InitializeTestDB(ApplicationDbContext context)

        
    //{
    //    //add and save index if you switch to a real db for integration tests
    //    foreach (ProblemSet p in GenerateProblemSets()){

    //        //for now, I'll just create 1 test user attached to problem set 2.
    //        if (p.ProblemSetId == 2)
    //        {
    //            // Create a new user
    //            var testUser = new ApplicationUser
    //            {
    //                UserName = "testuser@example.com",
    //                Email = "testuser@example.com",
    //                ProblemSetId = null, // Maybe needs to be a number, idk why though?

    //            };
    //            testUser.Id = "test-user-id";
    //            testUser.ProblemSet = p;
    //            testUser.ProblemSetId = 2;

    //            context.Add(testUser);
            
            
    //        }
            

    //        context.Add(p);
    //    };

    //    context.SaveChanges(); 
    //}
    //public static List<ProblemSet> GenerateProblemSets() {
    
    //    var problemSets = new List<ProblemSet>();
    //    var p1 = DefaultData.GetDefaultProblemSet();
    //    var p2 = DefaultData.GetDefaultProblemSet();
    //    var p3 = DefaultData.GetDefaultProblemSet();

    //    //populate each problem set with specific stuff

    //    List<int> ratings1 = new List<int> {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1};
    //    List<int> ratings2 = new List<int> { 3, 3, 3, 3, 3, 1, 2, 3, 4, 5, 5, 4, 3, 2, 1, 3, 3, 3 };
    //    List<int> ratings3 = new List<int> { 2, 4, 1, 3, 5, 2, 4, 1, 3, 5, 2, 4, 1, 3, 5, 2, 4, 1 };

    //    for (int i = 0; i < p1.Categories.Count; i++)
    //    {
    //        p1.Categories[i].CurrentSkill = (SkillLevel)ratings1[i];
    //        p2.Categories[i].CurrentSkill = (SkillLevel)ratings2[i];
    //        p3.Categories[i].CurrentSkill = (SkillLevel)ratings3[i];
    //    };

    //    problemSets.Add(p1);
    //    problemSets.Add(p2);
    //    problemSets.Add(p3);

    //    return problemSets;
    
    //}
}
