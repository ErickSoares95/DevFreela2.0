using DevFreela.Application.Models;
using DevFreela.Core.Repository;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.ProjectCommands.StartProject
{
    public class StartProjectCommandHandler : IRequestHandler<StartProjectCommand, ResultViewModel>
    {
        public StartProjectCommandHandler(IProjectRepository repository)
        {
            _repository = repository;
        }

        private readonly IProjectRepository _repository;

        public async Task<ResultViewModel> Handle(StartProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _repository.GetById(request.Id);

            if (project is null)
            {
                return ResultViewModel.Error("Projeto não existe");
            }

            project.Start();
            await _repository.Update(project);

            return ResultViewModel.Success();
        }
    }
}
