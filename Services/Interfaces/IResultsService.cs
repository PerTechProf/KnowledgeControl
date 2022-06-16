using System.Collections.Generic;
using System.Threading.Tasks;
using KnowledgeControl.Entities;

namespace KnowledgeControl.Services.Interfaces
{
    public interface IResultsService
    {
        IEnumerable<Result> GetResults();
    }
}