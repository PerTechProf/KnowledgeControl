using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KnowledgeControl.Entities;
using KnowledgeControl.Models;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeControl.Services.Interfaces
{
    public interface IAuthService
    {
        int CurrentUserId { get; }
        User GetCurrentUser();
        User GetCurrentUser(Func<IQueryable<User>, IEnumerable<User>> withAction);
        string CurrentUserName { get; }
        bool IsAuthorized { get; }
        bool IsEmployer();
        void RequireAsEmployer();
        Task<AuthModel> Register(RegistrationModel model);
        Task RegisterEmployee(RegistrationModel model);
        Task EditUser(EditEmployeeModel model);
        Task DeleteUser(int id);
        Task<AuthModel> Authenticate(string userName, string password);
        IEnumerable<EmployeeModel> GetEmployees();
        Task ChangePassword(string currentPassword, string password);
    }
}