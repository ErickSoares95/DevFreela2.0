using DevFreela.Application.Commands.ProjectCommands.InsertProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repository;
using NSubstitute;

namespace DevFreela.UnitTests.Application;

public class InsertProjectHandlerTests
{
    [Fact]
    public async Task InputDataOk_Insert_Success()
    {
        // Arrange
        const int ID = 1;
        var repository = Substitute.For<IProjectRepository>();
        repository.Add(Arg.Any<Project>()).Returns(Task.FromResult(ID));

        var command = new InsertProjectCommand
        {
            Title = "Projeto A",
            Description = "Descricao do Projeto",
            IdClient = 1,
            IdFreelancer = 2,
            TotalCost = 20000
        };
        
        var handler = new InsertProjectCommandHandler(repository);
        
        //Act
        var result = await handler.Handle(command, new CancellationToken());
        
        //Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(1, result.Data);
        await repository.Received(1).Add(Arg.Any<Project>());
    }
}