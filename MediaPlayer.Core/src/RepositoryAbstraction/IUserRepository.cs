using MediaPlayer.Core.src.Entity;

namespace MediaPlayer.Core.src.RepositoryAbstraction
{
    public interface IUserRepository
    {
        public IEnumerable<User> GetAllUsers();
        public bool CreateNewUser(User user);
        public User? GetUserByUsername(string username);
        public bool DeleteUserById(Guid id);
    }
}