using DevFreela.Core.Entities;
using DevFreela.Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    public UserRepository(DevFreelaDbContext context)
    {
        _context = context;
    }

    private readonly DevFreelaDbContext _context;
    
    public async Task<List<User>> GetAll()
    {
        var users = await _context.Users
            .Include(u => u.Skills)
            .ThenInclude(u => u.Skill)
            .Where(u => !u.IsDeleted)
            .ToListAsync();
        
        return users;
    }

    public async Task<User?> GetById(int id)
    {
        var user = await _context.Users
            .Include(p => p.Skills)
            .SingleOrDefaultAsync(p => p.Id == id);
        
        return user;
    }
    public async Task<User?> GetByEmailAndHash(string email, string hash)
    {
        var user = await _context.Users
            .SingleOrDefaultAsync(p => p.Email == email && p.Password == hash);
        
        return user;
    }

    public async Task<int> Add(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return user.Id;
    }

    public Task Update(User project)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Exists(int id)
    {
        throw new NotImplementedException();
    }

    public Task AddSkills(UserSkill userSkill)
    {
        throw new NotImplementedException();
    }
}