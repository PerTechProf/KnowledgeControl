﻿using System.Linq;
using KnowledgeControl.Entities;

namespace KnowledgeControl.Models
{
    public class TestModel
    {
        public TestModel(Test test)
        {
            Id = test.Id;
            Name = test.Name;
            Questions = test.Questions;
            Answers = test.Answers;
        }
        
        public int? Id { get; set; }
        public string Name { get; set;  }
        public string Questions { get; set; }
        public string Answers { get; set; }
    }
}