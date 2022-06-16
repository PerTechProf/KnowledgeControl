using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace KnowledgeControl.Entities
{
    public class User : IdentityUser<int>
  {
    public string Name { get; set; }
    
    public int? CompanyId { get; set; }

    [ForeignKey(nameof(CompanyId))]
    public User Company { get; set; }
    
    public List<Test> Tests { get; set; }
    
    public List<User> Users { get; set; }
    public List<Solution> Solutions { get; set; }
  }
}