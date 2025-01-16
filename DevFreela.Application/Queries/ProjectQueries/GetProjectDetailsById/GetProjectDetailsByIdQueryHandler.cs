using DevFreela.Application.Models;
using DevFreela.Core.Repository;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.ProjectQueries.GetProjectDetailsById
{
    public class GetProjectDetailsByIdQueryHandler : IRequestHandler<GetProjectDetailsByIdQuery, ResultViewModel<ProjectViewModel>>
    {
        public GetProjectDetailsByIdQueryHandler(IProjectRepository repository)
        {
            _repository = repository;
        }

        private readonly IProjectRepository _repository;


        public async Task<ResultViewModel<ProjectViewModel>> Handle(GetProjectDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            var project = await _repository.GetDetailsById(request.Id);

            if (project is null)
            {
                return ResultViewModel<ProjectViewModel>.Error("Projeto não existe");
            }

            var model = ProjectViewModel.FromEntity(project);

            return ResultViewModel<ProjectViewModel>.Success(model);
        }
    }
}
