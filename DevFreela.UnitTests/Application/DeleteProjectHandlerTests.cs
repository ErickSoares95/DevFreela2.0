using DevFreela.Application.Commands.ProjectCommands.DeleteProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repository;
using NSubstitute;

namespace DevFreela.UnitTests.Application;

public class DeleteProjectHandlerTests
{
    const string PROJECT_NOT_FOUND_MESSAGE = "Projeto não existe.";
    
    [Fact]
    public async Task ProjectExists_Delete_Success()
    {
        //Arrange
        var project = new Project("Projeto A", "Descrição do projeto", 1, 2, 20000);
        var repository = Substitute.For<IProjectRepository>();
        repository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Project?) project));
        repository.Update(Arg.Any<Project>()).Returns(Task.CompletedTask);
        
        var handler = new DeleteProjectCommandHandler(repository);
        
        var command = new DeleteProjectCommand(1);
        
        
        //Act
        var result = await handler.Handle(command, new CancellationToken());
        
        //Assert
        Assert.True(result.IsSuccess);
        await repository.Received(1).GetById(1);
        await repository.Received(1).Update(Arg.Any<Project>());

    }

    [Fact]
    public async Task ProjectDoesNotExist_Delete_Error_NSubstitute()
    {
        //Arrange
        var repository = Substitute.For<IProjectRepository>();
        repository.GetById(Arg.Any<int>()).Returns(Task.FromResult((Project?)null));
        
        var handler = new DeleteProjectCommandHandler(repository);
        
        var command = new DeleteProjectCommand(1);
        
        //Act
        var result = await handler.Handle(command, new CancellationToken());
        
        //Assert
        await repository.Received(1).GetById(Arg.Any<int>());
        await repository.DidNotReceive().Update(Arg.Any<Project>());
        Assert.False(result.IsSuccess);
        Assert.Equal(DeleteProjectCommandHandler.PROJECT_NOT_FOUND_MESSAGE, result.Message);
    }
}