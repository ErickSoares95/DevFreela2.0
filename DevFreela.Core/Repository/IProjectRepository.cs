using DevFreela.Core.Entities;

namespace DevFreela.Core.Repository;

public interface IProjectRepository
{
    Task<List<Project>> GetAll();
    Task<Project?> GetDetailsById(int id);
    Task<Project?> GetById(int id);
    Task<int> Add(Project project);
    Task Update(Project project);
    Task AddComment(ProjectComment projectComment );
    Task<bool> Exists(int id);
}