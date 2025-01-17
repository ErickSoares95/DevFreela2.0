using MediatR;

namespace DevFreela.Application.Notification.Project.ProjectCreated;

public class ProjectCreatedNotification : INotification
{
    public ProjectCreatedNotification(int id, string title, decimal totalCost)
    {
        Id = id;
        Title = title;
        TotalCost = totalCost;
    }

    public int Id { get; set; }
    public string Title { get; set; }
    public decimal TotalCost { get; private set; }
}