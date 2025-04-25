using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repleet.Models.Entities
{
    public class Problem
    {
        [Key]
        public int ProblemId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? CompletionDate { get; set; }
        public QuestionDifficulty Difficulty { get; set; }
        public SkillLevel SkillLevel { get; set; }
        

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        

    }

    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public List<Problem> Problems { get; set; } //used to be ICollection
        public SkillLevel CurrentSkill { get; set; }
    }

    public class ProblemSet
    {
        [Key]
        public int ProblemSetId { get; set; }

        public List<Category> Categories { get; set; } //used to be ICollection


    }
    
}
