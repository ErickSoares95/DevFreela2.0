using DevFreela.API.Entities;

namespace DevFreela.API.Models
{
    public class CreateSkillInputModel
    {
        public CreateSkillInputModel(string description)
        {
            Description = description;
        }

        public string Description { get; set; }

        public CreateSkillInputModel ToEntity()
            => new(Description);
    }
}
