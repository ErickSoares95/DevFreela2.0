using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Core.Repository;
using MediatR;

namespace DevFreela.Application.Commands.ProjectCommands.InsertComment
{
    public class InsertCommentCommandHandler : IRequestHandler<InsertCommentCommand, ResultViewModel>
    {
        public InsertCommentCommandHandler(IProjectRepository repository)
        {
            _repository = repository;
        }

        private readonly IProjectRepository _repository;


        public async Task<ResultViewModel> Handle(InsertCommentCommand request, CancellationToken cancellationToken)
        {
            if (!await _repository.Exists(request.IdProject))
            {
                return ResultViewModel.Error("Projeto não existe");
            }
            
            var comment = new ProjectComment(request.Content, request.IdProject, request.IdUser);
            
            await _repository.AddComment(comment);
            
            return ResultViewModel.Success();

        }
    }
}
