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
using KnowledgeControl.Services.Interfaces;

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
            _db.Users
                .First(user => user.UserName == CurrentUserNameRequired);

        public User GetCurrentUser(Func<IQueryable<User>, IEnumerable<User>> withAction) =>
            withAction(_db.Users).First(user => user.UserName == CurrentUserNameRequired);
        
        public int CurrentUserId =>
            GetCurrentUser().Id;
        
        private string CurrentUserNameRequired =>
            CurrentUserName ?? throw new InvalidOperationException("The current user is not set");

        public string CurrentUserName => _userService.CurrentHttpUserName;

        public bool IsAuthorized => CurrentUserName != null;

        public bool IsEmployer() => GetCurrentUser().CompanyId == null;

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
                throw new ("?????????? ?????? ???????????? ??????????????.");
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
                throw new ("???? ?????????????? ???????????????????????????????? ????????????????????????");
            }

            await _userManager.AddClaimAsync(user, new Claim("UserName", user.UserName));
        }

        public async Task<AuthModel> Register(RegistrationModel model)
        {
            if (CurrentUserName != null)
                throw new ("???? ?????????????? ???????????????????????????????? ????????????????");

            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                CompanyId = null
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                throw new ("???? ?????????????? ???????????????????????????????? ????????????????");
            }

            await _userManager.AddClaimAsync(user, new Claim("UserName", user.UserName));
            
            return await Authenticate(model.UserName, model.Password);
        }

        public async Task EditUser(EditEmployeeModel model) {
            RequireAsEmployer();

            var user = _db.Users.First(_ => _.Id == model.Id);
            
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            
            await _userManager.ResetPasswordAsync(user, token, model.Password);

            await _userManager.RemoveClaimAsync(user, new Claim("UserName", user.UserName));

            user.Email = model.Email;
            user.UserName = model.UserName;
            user.Name = model.Name;

            await _userManager.AddClaimAsync(user, new Claim("UserName", user.UserName));

            await _db.SaveChangesAsync();
        }

        public async Task DeleteUser(int id)
        {
            RequireAsEmployer();

            var company = GetCurrentUser();
            var user = _db.Users.First(_ => _.Id == id);

            if (user.CompanyId != company.Id)
                throw new ArgumentException("You are not supposed to do that");
            
            _db.Solutions.RemoveRange(_db.Solutions.Where(_ => _.UserId == user.Id));

            await _userManager.DeleteAsync(user);
        }

        public IEnumerable<EmployeeModel> GetEmployees() {
            RequireAsEmployer();
            return _db.Users.Where(user => user.CompanyId == CurrentUserId).Select((user) => new EmployeeModel(user));
        }

        public async Task ChangePassword(string currentPassword, string password) =>
            await _userManager.ChangePasswordAsync(GetCurrentUser(), currentPassword, password);
    }
}