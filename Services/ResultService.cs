using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using KnowledgeControl.Entities;
using KnowledgeControl.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeControl.Services
{
    public class ResultService : IResultService
    {
        private readonly KCDbContext _db;
        private readonly IAuthService _authService;

        public ResultService(
            KCDbContext db,
            IAuthService authService)
        {
            _db = db;
            _authService = authService;
        }

        public async Task<Result> GetResult(int testId)
        {
            return await _db.Results.AsNoTracking()
                .Include(_ => _.Solution)
                    .ThenInclude(_ => _.Test)
                .FirstAsync(_ => _.Solution.TestId == testId);
        }

        public async Task AddResult(Solution model)
        {
            var solution = await _db.Solutions
                .Include(_ => _.Test)
                .AsNoTracking()
                .FirstAsync(_ => _.Id == model.Id);

            var answers = JsonSerializer.Deserialize<List<string>>(solution.Test.Answers);
            var userAnswers = JsonSerializer.Deserialize<List<string>>(solution.Answers);
            
            var result = new Result()
            {
                SolutionId = solution.Id,
                Count = answers.Count(),
                CorrectCount = answers.Zip(userAnswers, (ans, uAns) => ans == uAns ? 1 : 0).Sum()
            };

            _db.Results.Add(result);

            await _db.SaveChangesAsync();
        }
    }
}