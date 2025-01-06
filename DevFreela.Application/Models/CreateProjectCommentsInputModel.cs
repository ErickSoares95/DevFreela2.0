namespace DevFreela.Application.Models
{
    public class CreateProjectCommentsInputModel
    {
        public string Content { get; set; }

        public int IdProject { get; set; }
        public int IdUser { get; set; }
    }
}
