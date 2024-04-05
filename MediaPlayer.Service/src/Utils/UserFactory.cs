
using MediaPlayer.Core.src.Entity;
using MediaPlayer.Core.src.Enums;
using MediaPlayer.Service.src.DTO;

namespace MediaPlayer.Service.src.Utils
{
    public class UserFactory
    {
        public User? Create(UserCreateDto userCreate){
            // create a user or an admin
            User newUser;
            if(userCreate.Type is UserType.Admin){
                newUser=new Admin(userCreate.Username,userCreate.Email,userCreate.FullName);
            }else{
                newUser=new User(userCreate.Username,userCreate.Email,userCreate.FullName);
            }
            return newUser;
        }
    }
}