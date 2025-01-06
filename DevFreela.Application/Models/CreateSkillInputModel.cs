using DevFreela.Core.Entities;

namespace DevFreela.Application.Models
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
