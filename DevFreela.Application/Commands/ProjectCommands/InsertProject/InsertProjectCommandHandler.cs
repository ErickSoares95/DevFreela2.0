using DevFreela.Application.Models;
using DevFreela.Application.Notification.Project.ProjectCreated;
using DevFreela.Core.Repository;
using MediatR;

namespace DevFreela.Application.Commands.ProjectCommands.InsertProject
{
    public class InsertProjectCommandHandler : IRequestHandler<InsertProjectCommand, ResultViewModel<int>>
    {
        
        private readonly IMediator _mediator;
        
        private readonly IProjectRepository _repository;

        public InsertProjectCommandHandler(IProjectRepository repository, IMediator mediator)
        {
            _mediator = mediator;
            _repository = repository;
        }
        
        public async Task<ResultViewModel<int>> Handle(InsertProjectCommand request, CancellationToken cancellationToken)
        {
            var project = request.ToEntity();

            await _repository.Add(project);

            var projectCreated = new ProjectCreatedNotification(project.Id, project.Title, project.TotalCost);

            await _mediator.Publish(projectCreated, cancellationToken);

            return ResultViewModel<int>.Success(project.Id);

        }
    }
}
