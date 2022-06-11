using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KnowledgeControl.Entities;
using KnowledgeControl.Models;

namespace KnowledgeControl.Services.Interfaces
{
    public interface IAuthService
    {
        int CurrentUserId { get; }
        User GetCurrentUser();
        string CurrentUserNameOrNull { get; }
        bool IsAuthorized { get; }
        bool IsEmployer();
        void RequireAsEmployer();
        Task<AuthModel> Register(RegistrationModel model);
        Task RegisterEmployee(RegistrationModel model);
        Task EditUser(EditEmployeeModel model);
        Task<AuthModel> Authenticate(string userName, string password);
        IEnumerable<EmployeeModel> GetEmployees();
        Task ChangePassword(string currentPassword, string password);
    }
}