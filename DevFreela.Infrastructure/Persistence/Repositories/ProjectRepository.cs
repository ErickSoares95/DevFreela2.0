using DevFreela.Core.Entities;
using DevFreela.Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositories;

public class ProjectRepository : IProjectRepository
{
    public ProjectRepository(DevFreelaDbContext context)
    {
        _context = context;
    }

    private readonly DevFreelaDbContext _context;
    
    public async Task<List<Project>> GetAll()
    {
        var projects = await _context.Projects
            .Include(p => p.Client)
            .Include(p => p.Freelancer)
            .Where(p => !p.IsDeleted)
            .ToListAsync();
        return projects;
    }

    public async Task<Project?> GetDetailsById(int id)
    {
        var project = await _context.Projects
            .Include(p => p.Client)
            .Include(p => p.Freelancer)
            .Include(p => p.Comments)
            .SingleOrDefaultAsync(p => p.Id == id);
        return project;
    }

    public async Task<Project?> GetById(int id)
    {
        return await _context.Projects.SingleOrDefaultAsync(p => p.Id == id);
    }

    public async Task<int> Add(Project project)
    {
        await _context.Projects.AddAsync(project);
        await _context.SaveChangesAsync();
        
        return project.Id;
    }

    public async Task Update(Project project)
    {
        _context.Projects.Update(project);
        await _context.SaveChangesAsync();
    }

    public async Task AddComment(ProjectComment comment)
    {
        await _context.ProjectComments.AddAsync(comment);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> Exists(int id)
    {
        return await _context.Projects.AnyAsync(x => x.Id == id);
    }
}