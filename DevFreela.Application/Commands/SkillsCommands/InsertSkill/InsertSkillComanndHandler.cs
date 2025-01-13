using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.SkillsCommands.InsertSkill
{
    public class InsertSkillComanndHandler : IRequestHandler<InsertSkillCommand, ResultViewModel<int>>
    {
        public readonly DevFreelaDbContext _context;

        public InsertSkillComanndHandler(DevFreelaDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel<int>> Handle(InsertSkillCommand request, CancellationToken cancellationToken)
        {
            var skill = request.ToEntity();
            await _context.AddAsync(skill);
            await _context.SaveChangesAsync();

            return ResultViewModel<int>.Success(skill.Id);
        }
    }
}
