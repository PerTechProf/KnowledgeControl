using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KnowledgeControl.Entities;
using KnowledgeControl.Services.Interfaces;
using KnowledgeControl.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeControl.Services
{
    public class TestService : ITestService
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

        public async Task CreateTest(TestModel test)
        {
            _authService.RequireAsEmployer();

            var userId = _authService.CurrentUserId;
            
            await _db.Tests.AddAsync(new Test()
            {
                Questions = test.Questions,
                Answers = test.Answers,
                Name = test.Name,
                UserId = userId
            });

            await _db.SaveChangesAsync();
        }

        public async Task EditTest(TestModel model)
        {
            _authService.RequireAsEmployer();

            var user = _authService.GetCurrentUser();
            
            var test = await _db.Tests.FindAsync(model.Id);

            if (test.UserId != user.Id)
                throw new ArgumentException("No such id");

            test.Name = model.Name;
            test.Questions = model.Questions;
            test.Answers = model.Answers;

            await _db.SaveChangesAsync();
        }

        public async Task DeleteTest(TestModel model)
        {
            _authService.RequireAsEmployer();

            var user = _authService.GetCurrentUser();
            
            var test = await _db.Tests.FindAsync(model.Id);

            if (test.UserId != user.Id)
                throw new ArgumentException("No such id");

            _db.Tests.Remove(test);

            await _db.SaveChangesAsync();
        }
    }
}