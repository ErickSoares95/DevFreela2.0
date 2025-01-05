using DevFreela.API.Entities;
using System.Net.Sockets;

namespace DevFreela.API.Models
{
    public class CreateUserInputModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }

        public User ToEntity()
            => new(
                FullName,
                Email,
                BirthDate
            );
    }
}
    
