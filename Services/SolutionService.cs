using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KnowledgeControl.Entities;
using KnowledgeControl.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeControl.Services
{
    public class SolutionService
    {
        private readonly KCDbContext _db;
        private readonly IAuthService _authService;

        public SolutionService(
            KCDbContext db,
            IAuthService authService)
        {
            _db = db;
            _authService = authService;
        }

        public async Task PostSolution(Solution model)
        {
            var user = _authService.GetCurrentUser();
            var test = await _db.Tests.FindAsync(model.TestId);
            
            if (test.UserId != user.CompanyId) 
                throw new ArgumentException("Wrong test id");

            await _db.Solutions.AddAsync(new Solution()
            {
                Answers = model.Answers,
                TestId = model.TestId
            });
        }

        public IEnumerable<Solution> GetSolutions()
        {
            _authService.RequireAsEmployer();
            
            var user = _authService.GetCurrentUser();

            return _db.Solutions
                .Include(_ => _.Test)
                .AsNoTracking()
                .Where(_ => _.Test.UserId == user.Id);
        }
    }
}