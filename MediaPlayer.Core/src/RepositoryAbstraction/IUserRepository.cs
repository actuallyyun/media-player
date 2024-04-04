using MediaPlayer.Core.src.Entity;

namespace MediaPlayer.Core.src.RepositoryAbstraction
{
    public interface IUserRepository
    {
        public IEnumerable<User> GetAllUsers();
        public void CreateNewUser(User user);
        public User? GetUserByName(string name);
        public void RemoveUser(User user);
        public bool UpdateUser(User user);
    }
}