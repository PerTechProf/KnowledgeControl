using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KnowledgeControl.Entities
{
    public class Test
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        [ForeignKey(nameof(CompanyId))]
        public User Company { get; set; }
        public string Name { get; set;  }
        public string Questions { get; set; }
        public string Answers { get; set; }
        
        public List<Solution> Solutions { get; set; }
    }
}