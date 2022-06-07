using JetBrains.Annotations;

namespace KnowledgeControl.Interfaces
{
  public interface IHttpUserService
  {
    string CurrentHttpUserName { get; }
  }
}