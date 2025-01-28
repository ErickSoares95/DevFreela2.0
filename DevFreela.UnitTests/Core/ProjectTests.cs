using DevFreela.Core.Entities;
using DevFreela.Core.Enums;

namespace DevFreela.UnitTests.Core;

public class ProjectTests
{
    [Fact]
    public void ProjectIsCreated_Start_Success()
    {
        // Arrange
        var project = new Project("Projeto A", "Descrição do projeto", 1, 2, 20000);
        
        //Act
        project.Start();
        
        //Assert
        Assert.Equal(ProjectStateEnum.InProgress, project.Status);
        Assert.NotNull(project.StartedAt);
        
        Assert.True(project.Status == ProjectStateEnum.InProgress);
        Assert.False(project.StartedAt is null);
    }

    [Fact]
    public void ProjectIsInvalidState_Start_ThrowssException()
    {
        //Arrange
        var project = new Project("Projeto A", "Descrição do projeto", 1, 2, 20000);
        project.Start();
        
        //Act + Assert
        Action? start = project.Start;
        
        var exception = Assert.Throws<InvalidOperationException>(start);
        Assert.Equal(Project.INVALID_STATE_MESSAGE, exception.Message);
    }

    [Fact]
    public void ProjectIsStarted_Complete_Success()
    {
        //Arrange
        var project = new Project("Projeto A", "Descrição do projeto", 1, 2, 20000);
        project.Start();
        project.SetPaymentPending();
        //Act
        project.Complete();
        
        //Assert
        Assert.Equal(ProjectStateEnum.Completed, project.Status);
        Assert.Equal(project.CompletedAt, project.CompletedAt);
    }

    [Fact]
    public void ProjectIsInvalidState_Complete_ThrowssException()
    {
        //Arrange
        var project = new Project("Projeto A", "Descrição do projeto", 1, 2, 20000);
        project.Start();
        
        //Act + Assert
        Action? complete = project.Complete;
        
        var exception = Assert.Throws<InvalidOperationException>(complete);
        Assert.Equal(Project.INVALID_STATE_MESSAGE, exception.Message);
    }

    [Fact]
    public void ProjectIsStated_SetPaymentPending_Success()
    {
        // Arrange
        var project = new Project("Projeto A", "Descrição do projeto", 1, 2, 20000);
        project.Start();
        
        //Act
        project.SetPaymentPending();
        
        //Assert
        Assert.NotNull(project.StartedAt);
        Assert.Equal(ProjectStateEnum.PaymentPending, project.Status);
    }

    [Fact]
    public void ProjectIsCreated_Update_Success()
    {
        //Arrange
        var project = new Project("Projeto A", "Descrição do projeto", 1, 2, 20000);
        
        //Act
        project.Update("Novo Titulo", "Descrição nova", 5000);
        
        //Assert
        Assert.NotEqual("Projeto A", project.Title);
        Assert.NotEqual("Descrição do projeto", project.Description);
        Assert.NotEqual(20000, project.TotalCost);
    }
}