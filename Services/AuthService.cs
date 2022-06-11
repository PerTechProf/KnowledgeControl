using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using KnowledgeControl.Entities;
using KnowledgeControl.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using KnowledgeControl.Interfaces;

namespace KnowledgeControl.Services
{
    public class AuthService : IAuthService
    {
        private readonly KCDbContext _db;
        private readonly UserManager<User> _userManager;
        private readonly IHttpUserService _userService;
        
        public AuthService(
            KCDbContext db, 
            UserManager<User> userManager,
            IHttpUserService userService)
        {
            _db = db;
            _userManager = userManager;
            _userService = userService;
        }
        public User GetCurrentUser() =>
            _db.Users.First(user => user.UserName == CurrentUserName);
        
        public int CurrentUserId =>
            GetCurrentUser().Id;
        
        private string CurrentUserName =>
            CurrentUserNameOrNull ?? throw new InvalidOperationException("The current user is not set");

        public string CurrentUserNameOrNull => _userService.CurrentHttpUserName;

        public bool IsAuthorized => CurrentUserNameOrNull != null;

        public bool IsEmployer() => GetCurrentUser().CompanyId != null;

        public void RequireAsEmployer()
        {
            if (!IsEmployer())
            {
                throw new ("You are not supposed to do this");
            }
        }

        private async Task<User> GetUser(string userName, string password) {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null || !await _userManager.CheckPasswordAsync(user, password))
            {
                throw new ("Логин или пароль неверны.");
            }

            return user;
        }

        public async Task<AuthModel> Authenticate(string userName, string password)
        {
            var user = await GetUser(userName, password);

            return new AuthModel(await CreateToken(user), user);
        }

        private async Task<string> CreateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var credentials = new SigningCredentials(
                Options.SecurityKey, 
                SecurityAlgorithms.HmacSha256
            );

            var identity = new ClaimsIdentity(
                await _userManager.GetClaimsAsync(user), "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("UserName", user.UserName)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = credentials
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
  
            return tokenHandler.WriteToken(token);
        }

        public async Task RegisterEmployee(RegistrationModel model)
        {
            RequireAsEmployer();

            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                Name = model.Name,
                CompanyId = CurrentUserId
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                throw new ("Не удалось зарегестрировать пользователя");
            }

            await _userManager.AddClaimAsync(user, new Claim("UserName", user.UserName));
        }

        public async Task<AuthModel> Register(RegistrationModel model)
        {
            if (CurrentUserNameOrNull != null)
                throw new ("Не удалось зарегестрировать компанию");

            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                CompanyId = null
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                throw new ("Не удалось зарегестрировать компанию");
            }

            await _userManager.AddClaimAsync(user, new Claim("UserName", user.UserName));
            
            return await Authenticate(model.UserName, model.Password);
        }

        public async Task EditUser(EditEmployeeModel model) {
            RequireAsEmployer();

            var user = _db.Users.First(_ => _.Id == model.Id);

            await _userManager.RemoveClaimAsync(user, new Claim("UserName", user.UserName));

            user.Email = model.Email;
            user.UserName = model.UserName;
            user.Name = model.Name;

            await _userManager.AddClaimAsync(user, new Claim("UserName", user.UserName));

            await _db.SaveChangesAsync();
        }

        public IEnumerable<EmployeeModel> GetEmployees() {
            RequireAsEmployer();
            return _db.Users.Where(user => user.CompanyId == CurrentUserId).Select((user) => new EmployeeModel(user));
        }

        public async Task ChangePassword(string currentPassword, string password) =>
            await _userManager.ChangePasswordAsync(GetCurrentUser(), currentPassword, password);
    }
}