using DevFreela.Core.Entities;

namespace DevFreela.Application.Models
{
    public class SkillItemViewModel
    {
        public SkillItemViewModel(string description)
        {
            Description = description;
        }

        public string Description { get; set; }
        public static SkillItemViewModel FromEntity(Skill skill)
            => new(
                skill.Description
            );
    }
}
