using System.Collections.Generic;
using System.Linq;
using KnowledgeControl.Entities;
using KnowledgeControl.Interfaces;
using KnowledgeControl.Models;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeControl.Services
{
    public class TestService
    {
        private readonly KCDbContext _db;
        private readonly IAuthService _authService;

        public TestService(
            KCDbContext db,
            IAuthService authService)
        {
            _db = db;
            _authService = authService;
        }
        
        public IEnumerable<TestViewModel> GetTests() {
            var user = _authService.GetCurrentUser();

            return _db.Tests.Include(_ => _.User)
                .Where((test) => test.UserId == (user.CompanyId ?? user.Id))
                .Select(test => new TestViewModel(test));
        }
    }
}