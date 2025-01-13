using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Queries.SkillQueries.GetAllSkill
{
    public class GetAllSkillsQueryHandler : IRequestHandler<GetAllSkillsQuery, ResultViewModel<List<SkillItemViewModel>>>
    {
        public readonly DevFreelaDbContext _context;

        public GetAllSkillsQueryHandler(DevFreelaDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel<List<SkillItemViewModel>>> Handle(GetAllSkillsQuery request, CancellationToken cancellationToken)
        {
            var skills = await _context.Skills.ToListAsync();

            var model = skills.Select(SkillItemViewModel.FromEntity).ToList();

            return ResultViewModel<List<SkillItemViewModel>>.Success(model);
        }
    }
}
