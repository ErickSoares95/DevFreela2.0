using DevFreela.API.Entities;

namespace DevFreela.API.Models
{
    public class UserItemViewModel
    {
        public UserItemViewModel(int id, string fullName, string email, DateTime birthDate)
        {
            Id = id;
            FullName = fullName;
            Email = email;
            BirthDate = birthDate;
        }

        public int Id { get; private set; }
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }

        public static UserItemViewModel FromEntity(User user)
                => new(
                    user.Id,
                    user.FullName,
                    user.Email,
                    user.BirthDate
                );
    }
}
