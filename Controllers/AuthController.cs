using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using KnowledgeControl.Models;
using KnowledgeControl.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace KnowledgeControl.Controllers
{
  [ApiController]
  [Route("api/[controller]/[action]")]
  public class AuthController : ControllerBase
  {
    private readonly IAuthService _auth;

    public AuthController(IAuthService auth)
    {
      _auth = auth;
    }

    [HttpPost]
    public async Task Register(RegistrationModel model)
    {
      await _auth.Register(model);
    }

    [HttpPost]
    public async Task CreateEmployee(RegistrationModel model)
    {
      await _auth.RegisterEmployee(model);
    }

    [HttpPost]
    public async Task EditEmployee(EditEmployeeModel model)
    {
      await _auth.EditUser(model);
    }

    [HttpGet]
    public IEnumerable<EmployeeModel> GetEmployees() {
        return _auth.GetEmployees();
    }

    [HttpPost]
    public async Task<AuthModel> Login(LoginModel model) {
      var auth = await _auth.Authenticate(model.UserName, model.Password);
      
      Response.Cookies.Append(
        Options.AuthCookie, auth.Token, new CookieOptions { MaxAge = TimeSpan.FromDays(7) });
      Response.Cookies.Append(
        Options.IsEmployerCookie, 
        auth.IsEmployer ? "true" : "false", 
        new CookieOptions { MaxAge = TimeSpan.FromDays(7) });

      return auth;
    }
    
    [HttpPost]
    public StatusCodeResult LogOut()
    {
      Response.Cookies.Delete(Options.AuthCookie);
      Response.Cookies.Delete(Options.IsEmployerCookie);
      return Ok();
    }

    [HttpPost]
    public Task ChangePassword(EditPasswordModel passwords) =>
      _auth.ChangePassword(passwords.CurrentPassword, passwords.Password);
  }
}
