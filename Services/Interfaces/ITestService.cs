using System.Collections.Generic;
using System.Threading.Tasks;
using KnowledgeControl.Models;

namespace KnowledgeControl.Services.Interfaces
{
    public interface ITestService
    {
        IEnumerable<TestViewModel> GetTests();
        Task CreateTest(TestModel test);
        Task EditTest(TestModel model);
        Task DeleteTest(TestModel model);
    }
}