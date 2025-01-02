namespace DevFreela.API.Entities
{
    public class ProjectComment : BaseEntity
    {
        public ProjectComment(string content, int idUser, int idProject)
        {
            Content = content;
            IdUser = idUser;
            IdProject = idProject;
        }

        public string Content { get; private set; }
        public int IdUser { get; private set; }
        public int IdProject { get; private set; }
        public Project Project { get; private set; }
        public User User { get; private set; }
    }
}
