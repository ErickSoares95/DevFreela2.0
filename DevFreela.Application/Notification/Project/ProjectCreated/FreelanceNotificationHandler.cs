using MediatR;

namespace DevFreela.Application.Notification.Project.ProjectCreated;

public class FreelanceNotificationHandler : INotificationHandler<ProjectCreatedNotification>
{
    public Task Handle(ProjectCreatedNotification notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Notificando os freelancer sobre o projeto {notification.Title}");
        
        return Task.CompletedTask;
    }
}