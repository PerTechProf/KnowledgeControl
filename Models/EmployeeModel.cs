using KnowledgeControl.Entities;

namespace KnowledgeControl.Models
{
    public class EmployeeModel
    {
        public EmployeeModel(User user) 
        {
            Id = user.Id;
            UserName = user.UserName;
            Name = user.Name;
            Email = user.Email;
            IsEmployer = user.CompanyId == null;
        }
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsEmployer { get; set; }
    }
}