using DevFreela.Application.Models;
using DevFreela.Core.Repository;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.ProjectQueries.GetAllProjects
{
    public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, ResultViewModel<List<ProjectItemViewModel>>>
    {
        public GetAllProjectsQueryHandler(IProjectRepository repository)
        {
            _repository = repository;
        }

        private readonly IProjectRepository _repository;

        public async Task<ResultViewModel<List<ProjectItemViewModel>>> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {
            var projects = await _repository.GetAll();

            var model = projects.Select(ProjectItemViewModel.FromEntity).ToList();

            return ResultViewModel<List<ProjectItemViewModel>>.Success(model);
        }
    }
}
