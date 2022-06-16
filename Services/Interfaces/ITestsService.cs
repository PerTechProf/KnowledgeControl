using System.Collections.Generic;
using System.Threading.Tasks;
using KnowledgeControl.Models;

namespace KnowledgeControl.Services.Interfaces
{
    public interface ITestsService
    {
        IEnumerable<TestViewModel> GetTests();
        TestModel GetTest(int id);
        Task<TestModel> CreateTest(TestModel test);
        Task EditTest(TestModel model);
        Task DeleteTest(int id);
    }
}