using MediaPlayer.Core.src.Entity;

namespace MediaPlayer.Core.src.RepositoryAbstraction
{
    public interface IUserRepository
    {
        public IEnumerable<User> GetAllUsers();
        public void CreateNewUser(User user);
        public User GetUserByName(string name);
        public bool DeleteUserById(Guid id);
        public bool UpdateUser(User user);
    }
}