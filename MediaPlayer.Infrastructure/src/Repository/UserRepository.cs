using MediaPlayer.Core.src.Entity;
using MediaPlayer.Core.src.RepositoryAbstraction;
using MediaPlayer.Infrastructure.src.Data;

namespace MediaPlayer.Infrastructure.src.Repository
{
    public class UserRepository : IUserRepository
    {
        private HashSet<User> _users;

        public UserRepository(Database database)
        {
            _users = database._users;
        }

        public void CreateNewUser(User user)
        {
            _users.Add(user);
        }

        public User? GetUserByName(string name)
        {
            return _users.FirstOrDefault(u => u.Username == name);
        }

        public void RemoveUser(User user)
        {
            _users.Remove(user);
        }

        public IEnumerable<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public bool UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
