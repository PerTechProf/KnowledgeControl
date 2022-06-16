using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using KnowledgeControl.Entities;
using KnowledgeControl.Models;
using KnowledgeControl.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeControl.Services
{
    public class SolutionsService : ISolutionsService
    {
        private readonly KCDbContext _db;
        private readonly IAuthService _authService;

        public SolutionsService(
            KCDbContext db,
            IAuthService authService)
        {
            _db = db;
            _authService = authService;
        }

        public async Task PostSolution(PostSolutionModel model)
        {
            var user = _authService.GetCurrentUser();
            var test = _db.Tests
                .Include(_ => _.Solutions)
                .First(_ => _.Id == model.TestId);
            
            if (test.CompanyId != user.CompanyId) 
                throw new ArgumentException("Wrong test id");

            if (test.Solutions?.FirstOrDefault(_ => _.UserId == user.Id) != null)
                throw new ArgumentException("Test already solved");

            var solution = await _db.Solutions.AddAsync(new Solution()
            {
                Answers = model.Answers,
                TestId = model.TestId,
                UserId = user.Id
            });

            var correctAnswers = JsonSerializer.Deserialize<List<string>>(test.Answers);
            var userAnswers = JsonSerializer.Deserialize<List<string>>(model.Answers);
            
            if (correctAnswers.Count != userAnswers.Count)
                throw new ArgumentException("Wrong number of answers");
            
            var answers = correctAnswers.Zip(userAnswers, Tuple.Create);

            var correctCount = answers
                .Aggregate(0, (sum, ans) => ans.Item1 == ans.Item2 ? sum+1 : sum);

            await _db.SaveChangesAsync();
            
            await _db.Results.AddAsync(new Result()
            {
                SolutionId = solution.Entity.Id,
                Count = userAnswers.Count,
                CorrectCount = correctCount
            });
            
            await _db.SaveChangesAsync();
        }

        public IEnumerable<Solution> GetSolutions(int testId)
        {
            _authService.RequireAsEmployer();
            
            var user = _authService.GetCurrentUser();

            return _db.Solutions
                .AsNoTracking()
                .Include(_ => _.Test)
                .Where(_ => _.Test.CompanyId == user.Id)
                .Where(_ => _.TestId == testId);
        }
    }
}