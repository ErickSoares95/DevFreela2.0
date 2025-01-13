using DevFreela.Application.Commands.UserCommands.InsertUsers;
using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.UserCommands.InsertUser
{
    public class InsertUserCommandHanlder : IRequestHandler<InsertUserCommand, ResultViewModel<int>>
    {


        private readonly DevFreelaDbContext _context;

        public InsertUserCommandHanlder(DevFreelaDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel<int>> Handle(InsertUserCommand request, CancellationToken cancellationToken)
        {
            var user = request.ToEntity();

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return ResultViewModel<int>.Success(user.Id);
        }
    }
}
