using DevFreela.API.Enums;
using System.Net.Sockets;

namespace DevFreela.API.Entities
{
    public class Project : BaseEntity
    {

        public Project(string title, string description, int idClient, int idFreelancer) : base()
        {
            Title = title;
            Description = description;
            IdClient = idClient;
            IdFreelancer = idFreelancer;

            Status = ProjectStateEnum.Created;
            Comments = [];
        }

        protected Project(){}

        public string Title { get; private set; }
        public string Description { get; private set; }
        public int IdClient { get; private set; }
        public int IdFreelancer { get; private set; }
        public User Client { get; private set; }
        public User Freelancer { get; private set; }
        public decimal TotalCoast { get; private set; }
        public DateTime? StartedAt { get; private set; }
        public DateTime? CompletedAt { get; private set; }
        public ProjectStateEnum Status { get; private set; }
        public List<ProjectComment> Comments { get; private set; }

        public void Cancel()
        {
            if (Status == ProjectStateEnum.InProgress || Status == ProjectStateEnum.Suspended)
            {
                Status = ProjectStateEnum.Cancelled;
            }
        }

        public void Start()
        {
            if (Status == ProjectStateEnum.Created)

            {
                Status = ProjectStateEnum.InProgress;
                StartedAt = DateTime.Now;
            }
        }

        public void Complete()
        {
            if (Status == ProjectStateEnum.InProgress || Status == ProjectStateEnum.PaymentPending)

            {
                Status = ProjectStateEnum.Completed;
                CompletedAt = DateTime.Now;
            }
        }

        public void SetPaymentPending()
        {
            if (Status == ProjectStateEnum.InProgress)

            {
                Status = ProjectStateEnum.PaymentPending;
            }
        }
        public void Update(string title, string description, decimal totalCost)
        {
            Title = title;
            Description = description;
            TotalCoast = totalCost;
        }

    }
}
