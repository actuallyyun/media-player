
namespace MediaPlayer.Service.src.DTO
{
    public class UserCreateDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public bool IsAdmin;

        public UserCreateDto(string username, string email, string fullName,bool isAdmin)
        {
            Username = username;
            Email = email;
            FullName = fullName;
            IsAdmin=isAdmin;
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
