using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using KnowledgeControl.Entities;
using KnowledgeControl.Models;
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

        public CertificateModel GetCertificate(int testId)
        {
            var user = _authService.GetCurrentUser();

            var test = _db.Tests.First(_ => _.Id == testId);

            if (test.CompanyId != user.CompanyId)
                throw new ArgumentException("Wrong test id");

            var solution = _db.Solutions.First(_ => _.TestId == test.Id && _.UserId == user.Id);

            var result = _db.Results.First(_ => _.SolutionId == solution.Id);

            return new CertificateModel()
            {
                Id = result.Id,
                Percentage = (double)result.CorrectCount / result.Count,
                SolutionId = solution.Id,
                PersonName = user.Name,
                TestName = test.Name
            };
        }
    }
}