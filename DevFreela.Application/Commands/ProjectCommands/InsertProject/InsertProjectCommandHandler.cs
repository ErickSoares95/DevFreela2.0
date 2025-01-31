using DevFreela.Application.Models;
using DevFreela.Application.Notification.Project.ProjectCreated;
using DevFreela.Core.Repository;
using MediatR;

namespace DevFreela.Application.Commands.ProjectCommands.InsertProject
{
    public class InsertProjectCommandHandler : IRequestHandler<InsertProjectCommand, ResultViewModel<int>>
    {
        private readonly IProjectRepository _repository;

        public InsertProjectCommandHandler(IProjectRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<ResultViewModel<int>> Handle(InsertProjectCommand request, CancellationToken cancellationToken)
        {
            var project = request.ToEntity();

            var id = await _repository.Add(project);

            return ResultViewModel<int>.Success(id);

        }
    }
}
