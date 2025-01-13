using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Queries.SkillQueries.GetSkillById
{
    public class GetSkillByIdQuery : IRequest<ResultViewModel<SkillItemViewModel>>
    {
        public GetSkillByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
