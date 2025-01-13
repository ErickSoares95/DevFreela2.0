using Azure.Core;
using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.UserCommands.InsertSkills
{
    public class InsertSkillsCommandHandler : IRequestHandler<InsertSkillsCommand, ResultViewModel>
    {

        private readonly DevFreelaDbContext _context;

        public InsertSkillsCommandHandler(DevFreelaDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel> Handle(InsertSkillsCommand request, CancellationToken cancellationToken)
        {
            var userSkills = request.SkillIds.Select(s => new UserSkill(request.Id, s)).ToList();

            if (userSkills is null)
            {
                return ResultViewModel.Error("Lista de habilidades não existe");
            }

            await _context.UserSkills.AddRangeAsync(userSkills);
            await _context.SaveChangesAsync();

            return ResultViewModel.Success();
        }
    }
}
