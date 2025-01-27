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
using DevFreela.Core.Repository;

namespace DevFreela.Application.Queries.UserQueries.GetUserById
{
    public class GetUserByidQueryHandler : IRequestHandler<GetUserByidQuery, ResultViewModel<UserItemViewModel>>
    {
        public GetUserByidQueryHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        private readonly IUserRepository _repository;

        public async Task<ResultViewModel<UserItemViewModel>> Handle(GetUserByidQuery request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetById(request.Id);

            if (user is null)
            {
                return ResultViewModel<UserItemViewModel>.Error("Usuário não existe");
            }

            var model = UserItemViewModel.FromEntity(user);
            
            return ResultViewModel<UserItemViewModel>.Success(model);
        }
    }
}
