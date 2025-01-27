using DevFreela.Core.Entities;

namespace DevFreela.Core.Repository;

public interface IUserRepository
{
    Task<List<User>> GetAll();
    Task<User?> GetById(int id);
    Task<int> Add(User user);
    Task Update(User project);
    Task<bool> Exists(int id);
    Task AddSkills(UserSkill userSkill);
}