using System;
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
  }
}