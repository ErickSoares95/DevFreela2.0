using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Queries.ProjectQueries.GetProjectDetailsById
{
    public class GetProjectDetailsByIdQuery : IRequest<ResultViewModel<ProjectViewModel>>
    {
        public GetProjectDetailsByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }

    }
}
