using MediaPlayer.Core.src.EntityAbstraction;

namespace MediaPlayer.Core.src.Entity
{
    public class Admin : User,IAdminAction
    {
        public Admin(string username, string email, string fullName) : base(username, email, fullName)
        {
        }

        public void AddMedia()
        {
            throw new NotImplementedException();
        }

        public void AddUser()
        {
            throw new NotImplementedException();
        }

        public void DeleteAllMedia()
        {
            throw new NotImplementedException();
        }

        public void DeleteAllUsers()
        {
            throw new NotImplementedException();
        }

        public void RemoveMedia()
        {
            throw new NotImplementedException();
        }

        public void RemoveUser()
        {
            throw new NotImplementedException();
        }

        public void UpdateMedia()
        {
            throw new NotImplementedException();
        }

        public void UpdateUser()
        {
            throw new NotImplementedException();
        }
    }
}