using MediaPlayer.Core.src.EntityAbstraction;

namespace MediaPlayer.Core.src.Entity
{
    public class Admin : User
    {

        public override bool IsAdmin => true;
        public Admin(string username, string email, string fullName) : base(username, email, fullName)
        {
        }
    }
}