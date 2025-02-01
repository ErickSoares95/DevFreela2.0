using DevFreela.Application.Commands.ProjectCommands.DeleteProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repository;
using FluentAssertions;
using Moq;
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
        result.IsSuccess.Should().BeTrue();
        
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
        result.IsSuccess.Should().BeFalse();
        Assert.Equal(DeleteProjectCommandHandler.PROJECT_NOT_FOUND_MESSAGE, result.Message);
    }
    
    
    [Fact]
    public async Task ProjectExists_Delete_Success_Moq()
    {
        //Arrange
        const int id = 1;
        var project = new Project("Projeto A", "Descrição do projeto", 1, 2, 20000);
        
        var repository = Mock.Of<IProjectRepository>( p =>
            p.GetById(It.IsAny<int>()) == Task.FromResult(project) &&
            p.Update(It.IsAny<Project>()) == Task.CompletedTask
            );
        
        var handler = new DeleteProjectCommandHandler(repository);
        
        var command = new DeleteProjectCommand(1);
        
        
        //Act
        var result = await handler.Handle(command, new CancellationToken());
        
        //Assert
        Assert.True(result.IsSuccess);
        result.IsSuccess.Should().BeTrue();
        Mock.Get(repository).Verify(p => p.GetById(id), Times.Once);
        Mock.Get(repository).Verify(p => p.Update(It.IsAny<Project>()), Times.Once);

    }

    [Fact]
    public async Task ProjectDoesNotExist_Delete_Error_Moq()
    {
        //Arrange
        var repository = Mock.Of<IProjectRepository>(p =>
            p.GetById(It.IsAny<int>()) == Task.FromResult((Project?)null)
        );
        
        var handler = new DeleteProjectCommandHandler(repository);
        
        var command = new DeleteProjectCommand(1);
        
        //Act
        var result = await handler.Handle(command, new CancellationToken());
        
        //Assert
        Assert.False(result.IsSuccess);
        result.IsSuccess.Should().BeFalse();
        
        Assert.Equal(DeleteProjectCommandHandler.PROJECT_NOT_FOUND_MESSAGE, result.Message);
        
        Mock.Get(repository).Verify(p => p.GetById(1), Times.Once);
        Mock.Get(repository).Verify(p => p.Update(It.IsAny<Project>()), Times.Never);
    }
}