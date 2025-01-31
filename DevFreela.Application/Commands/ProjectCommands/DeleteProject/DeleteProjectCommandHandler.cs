using DevFreela.Application.Models;
using DevFreela.Core.Repository;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.ProjectCommands.DeleteProject
{
    public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, ResultViewModel>
    {
        public const string PROJECT_NOT_FOUND_MESSAGE = "Projeto não existe.";
        public DeleteProjectCommandHandler(IProjectRepository repository)
        {
            _repository = repository;
        }

        private readonly IProjectRepository _repository;

        public async Task<ResultViewModel> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _repository.GetById(request.Id);

            if (project is null)
            {
                return ResultViewModel.Error(PROJECT_NOT_FOUND_MESSAGE);
            }

            project.SetAsDeleted();
            
            await _repository.Update(project);

            return ResultViewModel.Success();
        }
    }
}
