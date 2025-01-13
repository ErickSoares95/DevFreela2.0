using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.ProjectCommands.UpdateProject
{
    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, ResultViewModel>
    {
        public UpdateProjectCommandHandler(DevFreelaDbContext context)
        {
            _context = context;
        }

        private readonly DevFreelaDbContext _context;


        public async Task<ResultViewModel> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects.SingleOrDefaultAsync(p => p.Id == request.IdProject);
            if (project is null)
            {
                return ResultViewModel.Error("Projeto não existe");
            }

            project.Update(request.Title, request.Description, request.TotalCost);
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();

            return ResultViewModel.Success();

        }
    }
}
