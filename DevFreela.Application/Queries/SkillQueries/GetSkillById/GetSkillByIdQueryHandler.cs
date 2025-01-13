using Azure.Core;
using DevFreela.Application.Models;
using DevFreela.Application.Queries.ProjectQueries.GetProjectById;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Queries.SkillQueries.GetSkillByIdQuery
{
    public class GetAllSkillsQueryHandler : IRequestHandler<GetProjectByIdQuery, ResultViewModel<SkillItemViewModel>>
    {
        public readonly DevFreelaDbContext _context;

        public GetAllSkillsQueryHandler(DevFreelaDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel<SkillItemViewModel>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            var skill = await _context.Skills.SingleOrDefaultAsync(p => p.Id == request.Id);
            var model = SkillItemViewModel.FromEntity(skill);

            return ResultViewModel<SkillItemViewModel>.Success(model);
        }
    }
}
