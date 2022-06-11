using System;
using KnowledgeControl.Entities;

namespace KnowledgeControl.Models
{
    public class TestViewModel
    {
        public TestViewModel(Test test)
        {
            Id = test.Id;
            Name = test.Name;
        }
        
        public int Id { get; set; }
        
        public string Name { get; set; }
    }
}