namespace KnowledgeControl.Models
{
    public class RegistrationModel
    {
      public string Name { get; set; }
      public string UserName { get; set; }
      public string Email { get; set; }
      public string Password { get; set; }
      public int? CompanyId { get; set; }
    }
}