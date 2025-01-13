using DevFreela.Application.Models;
using DevFreela.Application.Queries.ProjectQueries.GetProjectById;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.SkillQueries.GetSkillById
{
    public class GetSkillByIdQueryHandler : IRequestHandler<GetSkillByIdQuery, ResultViewModel<SkillItemViewModel>>
    {
        public readonly DevFreelaDbContext _context;

        public GetSkillByIdQueryHandler(DevFreelaDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel<SkillItemViewModel>> Handle(GetSkillByIdQuery request, CancellationToken cancellationToken)
        {
            var skill = await _context.Skills.SingleOrDefaultAsync(p => p.Id == request.Id);
            var model = SkillItemViewModel.FromEntity(skill);

            return ResultViewModel<SkillItemViewModel>.Success(model);
        }
    }
}
