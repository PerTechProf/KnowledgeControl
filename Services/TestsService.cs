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
    public class TestsService : ITestsService
    {
        private readonly KCDbContext _db;
        private readonly IAuthService _authService;

        public TestsService(
            KCDbContext db,
            IAuthService authService)
        {
            _db = db;
            _authService = authService;
        }

        public IEnumerable<TestViewModel> GetTests()
        {
            var user = _authService.GetCurrentUser();

            return _db.Tests.Include(_ => _.Company)
                .Include(_ => _.Solutions)
                .Where((test) => test.CompanyId == (user.CompanyId ?? user.Id))
                .Where((test) => test.Solutions.FirstOrDefault(_ => _.UserId == user.Id) == null || user.CompanyId == null)
                .Select(test => new TestViewModel(test));
        }

        public TestModel GetTest(int id)
        {
            var user = _authService.GetCurrentUser(_ => _
                .AsNoTracking()
                .Include(__ => __.Company)
                    .ThenInclude(_ => _.Tests)
                        .ThenInclude(_ => _.Solutions)
                .Include(__ => __.Tests)
                .AsParallel()
            );

            var tests = user.CompanyId == null ? user.Tests : user.Company.Tests;

            var test = tests.First(_ => _.Id == id);

            if (test.Solutions.FirstOrDefault(_ => _.UserId == user.Id) != null && !_authService.IsEmployer())
                throw new ArgumentException("Test already solved");

            if (!_authService.IsEmployer())
                test.Answers = "[]";

            return new TestModel(test);
        }

        public async Task<TestModel> CreateTest(TestModel model)
        {
            _authService.RequireAsEmployer();

            var userId = _authService.CurrentUserId;

            var test = await _db.Tests.AddAsync(new Test()
            {
                Questions = model.Questions,
                Answers = model.Answers,
                Name = model.Name,
                CompanyId = userId
            });

            await _db.SaveChangesAsync();

            return new TestModel(test.Entity);
        }

        public async Task EditTest(TestModel model)
        {
            _authService.RequireAsEmployer();

            var user = _authService.GetCurrentUser();

            var test = await _db.Tests.FindAsync(model.Id);

            if (test.CompanyId != user.Id)
                throw new ArgumentException("No such id");

            test.Name = model.Name;
            test.Questions = model.Questions;
            test.Answers = model.Answers;

            await _db.SaveChangesAsync();
        }

        public async Task DeleteTest(int id)
        {
            _authService.RequireAsEmployer();

            var user = _authService.GetCurrentUser();

            var test = await _db.Tests.FindAsync(id);

            if (test.CompanyId != user.Id)
                throw new ArgumentException("No such id");

            _db.Tests.Remove(test);

            await _db.SaveChangesAsync();
        }
    }
}