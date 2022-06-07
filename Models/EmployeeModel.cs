using KnowledgeControl.Entities;

namespace KnowledgeControl.Models
{
    public class EmployeeModel
    {
        public EmployeeModel(User user) 
        {
            Id = user.Id;
            Name = user.Name;
            IsEmployer = user.CompanyId == null;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsEmployer { get; set; }
    }
}