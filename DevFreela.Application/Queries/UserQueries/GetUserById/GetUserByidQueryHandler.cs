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

namespace DevFreela.Application.Queries.UserQueries.GetUserById
{
    public class GetUserByidQueryHandler : IRequestHandler<GetUserByidQuery, ResultViewModel<UserItemViewModel>>
    {


        private readonly DevFreelaDbContext _context;

        public GetUserByidQueryHandler(DevFreelaDbContext context)
        {
            _context = context;
        }

        public async Task<ResultViewModel<UserItemViewModel>> Handle(GetUserByidQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Include(p => p.Skills)
                .SingleAsync(p => p.Id == request.Id);

            if (user is null)
            {
                return ResultViewModel<UserItemViewModel>.Error("Usuário não existe");
            }

            var model = UserItemViewModel.FromEntity(user);
            return ResultViewModel<UserItemViewModel>.Success(model);
        }
    }
}
