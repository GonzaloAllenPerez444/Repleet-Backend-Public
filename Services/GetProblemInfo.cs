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
    public class ProblemInfo
    {
        public ProblemInfo(Problem p) {
        
            this.Title = p.Title;
            this.Url = p.Url;
            this.IsCompleted = p.IsCompleted;
            this.CompletionDate = p.CompletionDate;
            this.Difficulty = p.Difficulty;
            this.SkillLevel = p.SkillLevel;
            this.CategoryName = p.Category.Name;
        
        }
        public string Title { get; set; }
        public string Url { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? CompletionDate { get; set; }
        public QuestionDifficulty Difficulty { get; set; }
        public SkillLevel SkillLevel { get; set; }
        public string CategoryName { get; set; }
    }
    public class GetProblemInfo
    {
        public static ProblemInfo GetProblemInfoFromProblem(Problem problem)
        {
            return new ProblemInfo(problem);
        }
    }
}
