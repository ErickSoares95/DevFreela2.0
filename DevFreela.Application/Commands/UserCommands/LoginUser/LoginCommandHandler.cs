using DevFreela.Application.Models;
using DevFreela.Core.Repository;
using DevFreela.Infrastructure.Auth;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace DevFreela.Application.Commands.UserCommands.LoginUser;

public class LoginCommandHandler : IRequestHandler<LoginCommand, ResultViewModel<LoginViewModel>>
{
    public LoginCommandHandler(IUserRepository repository, IAuthService authService)
    {
        _repository = repository;
        _authService = authService;
    }

    private readonly IUserRepository _repository;
    private readonly IAuthService _authService;
    
    public async Task<ResultViewModel<LoginViewModel>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var hash = _authService.ComputeHash(request.Password);
        
        var user = await _repository.GetByEmailAndHash(request.Email, hash);
        
        if (user is null)
        {
            ResultViewModel.Error("Erro de login");
        }
        
        var token = _authService.GenerateToken(user.Email, user.Role);
        
        var viewModel = new LoginViewModel(token);
        
        return  ResultViewModel<LoginViewModel>.Success(viewModel);
    }
}