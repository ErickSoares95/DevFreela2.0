using DevFreela.Application.Commands.UserCommands.InsertUsers;
using DevFreela.Application.Models;
using DevFreela.Core.Repository;
using DevFreela.Infrastructure.Auth;
using DevFreela.Infrastructure.Persistence;
using MediatR;

namespace DevFreela.Application.Commands.UserCommands.InsertUser
{
    public class InsertUserCommandHandler : IRequestHandler<InsertUserCommand, ResultViewModel<int>>
    {


        private readonly IUserRepository _repository;
        private readonly IAuthService _authService;

        public InsertUserCommandHandler(IUserRepository repository, IAuthService authService)
        {
            _repository = repository;
            _authService = authService;
        }

        public async Task<ResultViewModel<int>> Handle(InsertUserCommand request, CancellationToken cancellationToken)
        {
            var hash = _authService.ComputeHash(request.password);
            request.password = hash;
            
            var user = request.ToEntity();

            _repository.Add(user);

            return ResultViewModel<int>.Success(user.Id);
        }
    }
}
