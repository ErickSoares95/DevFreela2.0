using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Commands.ProjectCommands.InsertComment
{
    public class InsertCommentCommandHandler : IRequestHandler<InsertCommentCommand, ResultViewModel>
    {
        public InsertCommentCommandHandler(DevFreelaDbContext context)
        {
            _context = context;
        }

        private readonly DevFreelaDbContext _context;


        public async Task<ResultViewModel> Handle(InsertCommentCommand request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects.SingleOrDefaultAsync(p => p.Id == request.IdProject);

            if (project is null)
            {
                return ResultViewModel.Error("Projeto não existe");
            }
            var comment = new ProjectComment(request.Content, request.IdProject, request.IdUser);
            await _context.ProjectComments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return ResultViewModel.Success();

        }
    }
}
