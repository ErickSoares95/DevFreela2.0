using DevFreela.Application.Models;
using DevFreela.Core.Repository;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.ProjectCommands.UpdateProject
{
    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, ResultViewModel>
    {
        public UpdateProjectCommandHandler(IProjectRepository repository)
        {
            _repository = repository;
        }

        private readonly IProjectRepository _repository;
        
        public async Task<ResultViewModel> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _repository.GetById(request.IdProject);
            if (project is null)
            {
                return ResultViewModel.Error("Projeto não existe");
            }

            project.Update(request.Title, request.Description, request.TotalCost);
            
            await _repository.Update(project);

            return ResultViewModel.Success();

        }
    }
}
