using DevFreela.Application.Models;
using DevFreela.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Queries.UserQueries.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, ResultViewModel<List<UserItemViewModel>>>
    {
        private readonly DevFreelaDbContext _context;

        public GetAllUsersQueryHandler(DevFreelaDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel<List<UserItemViewModel>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
        var users = await _context.Users
            .Include(u => u.Skills)
            .ThenInclude(u => u.Skill)
            .Where(u => !u.IsDeleted)
            .ToListAsync();

        var model = users.Select(UserItemViewModel.FromEntity).ToList();

        return ResultViewModel<List<UserItemViewModel>>.Success(model);
        }
    }
}
