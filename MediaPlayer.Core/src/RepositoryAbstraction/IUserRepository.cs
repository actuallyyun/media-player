using MediaPlayer.Core.src.Entity;

namespace MediaPlayer.Core.src.RepositoryAbstraction
{
    public interface IUserRepository
    {
        public void Add(User user);
        public User? GetUserById(Guid id);
        public User? GetUserByName(string name);
        public void Remove(User user);
        public void RemoveAll();
    }
}
