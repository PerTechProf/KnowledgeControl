using System.Threading.Tasks;
using KnowledgeControl.Entities;

namespace KnowledgeControl.Services.Interfaces
{
    public interface IResultService
    {
        Task<Result> GetResult(int testId);
        Task AddResult(Solution model);
    }
}