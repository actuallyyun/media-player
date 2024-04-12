using MediaPlayer.Core.src.Abstraction;
using MediaPlayer.Core.src.Entity;
using MediaPlayer.Core.src.Enums;
using MediaPlayer.Core.src.RepositoryAbstraction;
using MediaPlayer.Service.src.DTO;
using MediaPlayer.Service.src.Utils;
using MediaPlayer.Service.src.ServiceAbstraction;

namespace MediaPlayer.Service.Service
{
    public class UserService 
    {
        private readonly IMediaPlayerMonitor _notificationService;
        private  readonly IUserRepository _userRepository;
        private readonly IAuthorizationService _authorizationService;

        public UserService(IUserRepository userRepo, IMediaPlayerMonitor notificationService,IAuthorizationService authorizationService)
        { 
            _userRepository = userRepo;
            _notificationService=notificationService;
            if(!authorizationService.IsAuthenticated ||!authorizationService.HasPermission(UserType.Admin)){
                throw new UnauthorizedAccessException("Unauthorized action.");
            }
            _authorizationService = authorizationService;
        }

        public User? AddUser(UserCreateDto userCreate)
        {
            var userFound = GetUserByName(userCreate.Username);
            if (userFound is not null)
            { // username must be unique
                _notificationService.Notify("Invalid username.");
                return null;
            }
            var useFactory = new UserFactory();
            User? newUser = useFactory.Create(userCreate);
            if (newUser is not null)
            {
                _userRepository.Add(newUser);
                _notificationService.Notify($"A new user is created:{newUser} by {_authorizationService.UserName}");
            }
            else
            {
                _notificationService.Notify("Failed to create user.");
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
                _notificationService.Notify("Cannot delete user: user not found");
                return false;
            }
            _notificationService.Notify($"User deleted by {_authorizationService.UserName}");
            _userRepository.Remove(userFound);
            return true;
        }

        public bool DeleteAllUsers()
        {
            _userRepository.RemoveAll();
            _notificationService.Notify($"All users are removed by {_authorizationService.UserName}");
            return true;
        }

        public bool UpdateUser(Guid id, UserUpdateDto userUpdate)
        {
            var userFound = _userRepository.GetUserById(id);
            if (userFound is null)
            {
                _notificationService.Notify("Cannot update user: user not found");
                return false;
            }
            _notificationService.Notify($"User updated by {_authorizationService.UserName}");
            _userRepository.Update(userFound,userUpdate.Email, userUpdate.FullName);
            return true;
        }
    }
}
