using System;
using System.Collections.Generic;
namespace KnowledgeControl.Entities
{
    public class Test
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string TestQuestions { get; set; }
        public string TestAsnwers { get; set; }
        public List<Solution> Solutions { get; set; }
    }
}