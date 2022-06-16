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
    public class ResultsService : IResultsService
    {
        private readonly KCDbContext _db;
        private readonly IAuthService _authService;

        public ResultsService(
            KCDbContext db,
            IAuthService authService)
        {
            _db = db;
            _authService = authService;
        }

        public IEnumerable<Result> GetResults()
        {
            var user = _authService.GetCurrentUser();
            
            return _db.Results.AsNoTracking()
                .Include(_ => _.Solution)
                .Where(_ => _.Solution.UserId == user.Id);
        }
    }
}