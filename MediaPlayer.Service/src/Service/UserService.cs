using MediaPlayer.Core.src.Entity;
using MediaPlayer.Core.src.EntityAbstraction;
using MediaPlayer.Core.src.RepositoryAbstraction;
using MediaPlayer.Service.src.DTO;
using MediaPlayer.Service.src.Utils;

namespace MediaPlayer.Service.Service
{
    public class UserService : IUserAction
    {
         private IUserRepository _userRepository;
        private Admin _admin;

        public UserService(IUserRepository userRepo,Admin admin)
        {// must provide admin to create service instance
          _userRepository=userRepo;
          _admin=admin;
        }

        public User? AddUser(UserCreateDto userCreate){
            var useFactory=new UserFactory();
            User? newUser=useFactory.Create(userCreate);
            if(newUser is not null){
                _userRepository.CreateNewUser(newUser);
                Console.WriteLine($"A new user is created:{newUser}");
      
            }else{
                Console.WriteLine("Fail to create user.");
            }
            return newUser;
            
        }

        public User GetUserByName(string name){
            return _userRepository.GetUserByName(name);
        }
        //public bool UpdateUser(UserUpdateDto userUpdate);

        //public bool DeleteUserById(Guid id);
        //public bool DeleteAllUsers();

        

        
    }
}
