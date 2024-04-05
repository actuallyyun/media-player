
using MediaPlayer.Core.src.Enums;

namespace MediaPlayer.Service.src.DTO
{
    public class UserCreateDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public UserType Type;

        public UserCreateDto(string username, string email, string fullName,UserType type)
        {
            Username = username;
            Email = email;
            FullName = fullName;
            Type=type;
        }
    }
    public class UserUpdateDto{ // does not allow update username. Username must be unique
        public string Email { get; set; }
        public string FullName { get; set; }

        public UserUpdateDto(string newEmail,string newName){
            Email=newEmail;
            FullName=newName;
        }
    }
}
