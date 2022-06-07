using KnowledgeControl.Entities;

namespace KnowledgeControl.Models
{
    public class AuthModel
    {
        public AuthModel(string token, User user)
        {
            Token = token;
            IsEmployer = user.CompanyId == null;
            UserId = user.Id;
        }
        public string Token { get; set; }
        public bool IsEmployer { get; set; }
        public int UserId { get; set; }
    }
}