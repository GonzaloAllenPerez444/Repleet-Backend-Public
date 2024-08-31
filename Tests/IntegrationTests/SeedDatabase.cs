using Repleet.Data;
using Repleet.Models;
using Repleet.Models.Entities;

namespace Repleet.Tests.IntegrationTests;

//Not Currently Used
public class SeedDatabase
{

    public static void InitializeTestDB(ApplicationDbContext context)

        
    {
        //add and save index if you switch to a real db for integration tests
        foreach (ProblemSet p in GenerateProblemSets()){

            //for now, I'll just create 1 test user attached to problem set 2.
            if (p.ProblemSetId == 2)
            {
                // Create a new user
                var testUser = new ApplicationUser
                {
                    UserName = "testuser@example.com",
                    Email = "testuser@example.com",
                    ProblemSetId = null, // Maybe needs to be a number, idk why though?

                };
                testUser.Id = "test-user-id";
                testUser.ProblemSet = p;
                testUser.ProblemSetId = 2;

                context.Add(testUser);
            
            
            }
            

            context.Add(p);
        };

        context.SaveChanges(); 
    }
    public static List<ProblemSet> GenerateProblemSets() {
    
        var problemSets = new List<ProblemSet>();
        var p1 = DefaultData.GetDefaultProblemSet();
        var p2 = DefaultData.GetDefaultProblemSet();
        var p3 = DefaultData.GetDefaultProblemSet();

        //populate each problem set with specific stuff

        List<int> ratings1 = new List<int> {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1};
        List<int> ratings2 = new List<int> { 3, 3, 3, 3, 3, 1, 2, 3, 4, 5, 5, 4, 3, 2, 1, 3, 3, 3 };
        List<int> ratings3 = new List<int> { 2, 4, 1, 3, 5, 2, 4, 1, 3, 5, 2, 4, 1, 3, 5, 2, 4, 1 };

        for (int i = 0; i < p1.Categories.Count; i++)
        {
            p1.Categories[i].CurrentSkill = (SkillLevel)ratings1[i];
            p2.Categories[i].CurrentSkill = (SkillLevel)ratings2[i];
            p3.Categories[i].CurrentSkill = (SkillLevel)ratings3[i];
        };

        problemSets.Add(p1);
        problemSets.Add(p2);
        problemSets.Add(p3);

        return problemSets;
    
    }
}
