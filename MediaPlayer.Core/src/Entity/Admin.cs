using MediaPlayer.Core.src.EntityAbstraction;
using MediaPlayer.Core.src.Enums;

namespace MediaPlayer.Core.src.Entity
{
    public class Admin : User
    {

        public override UserType Type => UserType.Admin;
        public Admin(string username, string email, string fullName)
            : base(username, email, fullName) { 
                
            }
    }
}
