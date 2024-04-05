using MediaPlayer.Core.src.Abstraction;
using MediaPlayer.Core.src.Entity;
using MediaPlayer.Core.src.RepositoryAbstraction;
using MediaPlayer.Service.src.DTO;
using MediaPlayer.Service.src.Utils;

namespace MediaPlayer.Service.Service
{
    public class UserService : IMediaPlayerMonitor
    {
        private List<INotify> _observers = new List<INotify>();
        private IUserRepository _userRepository;
        private Admin _admin;

        public UserService(IUserRepository userRepo, Admin admin)
        { // must provide admin to create service instance
            _userRepository = userRepo;
            _admin = admin;
            Notify($"A new user service is created by {_admin}");
        }

        public User? AddUser(UserCreateDto userCreate)
        {
            var userFound = GetUserByName(userCreate.Username);
            if (userFound is not null)
            { // username must be unique
                Notify("Invalid username.");
                return null;
            }
            var useFactory = new UserFactory();
            User? newUser = useFactory.Create(userCreate);
            if (newUser is not null)
            {
                _userRepository.Add(newUser);
                Notify($"A new user is created:{newUser}");
            }
            else
            {
                Notify("Failed to create user.");
            }
            return newUser;
        }

        public User? GetUserByName(string name)
        {
            return _userRepository.GetUserByName(name);
        }


        public bool DeleteUserById(Guid id)
        {
            var userFound = _userRepository.GetUserById(id);
            if (userFound is null)
            {
                Notify("Cannot delete user: user not found");
                return false;
            }
            Notify("User deleted.");
            _userRepository.Remove(userFound);
            return true;
        }

        public bool DeleteAllUsers()
        {
            _userRepository.RemoveAll();
            Notify("All users are removed");
            return true;
        }

        public bool UpdateUser(Guid id, UserUpdateDto userUpdate)
        {
            var userFound = _userRepository.GetUserById(id);
            if (userFound is null)
            {
                Notify("Cannot update user: user not found");
                return false;
            }
            Notify("User updated.");
            _userRepository.Update(userFound,userUpdate.Email, userUpdate.FullName);
            return true;
        }

        public void Attach(INotify observer)
        {
            _observers.Add(observer);
        }

        public void Detach(INotify observer)
        {
            _observers.Remove(observer);
        }

        public void Notify(string message)
        {
            foreach (var observer in _observers)
            {
                observer.Update(message);
            }
        }
    }
}
