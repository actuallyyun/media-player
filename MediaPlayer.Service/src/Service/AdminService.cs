using MediaPlayer.Service.src.DTO;
using MediaPlayer.Core.src.RepositoryAbstraction;
using MediaPlayer.Service.src.Utils;
using MediaPlayer.Core.src.Entity;

namespace MediaPlayer.Service.src.Service
{
    public class AdminService
    {
         private IMediaRepository _mediaRepository;
         private IUserRepository _userRepository;
         public AdminService(IMediaRepository? mediaRepo=null,IUserRepository? userRepo=null)
        {
            _mediaRepository = mediaRepo;
            _userRepository=userRepo;
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


        public Media? AddMedia(MediaCreateDto mediaCreate){
            var mediaFactory = new MediaFactory();
            Media? newMedia = mediaFactory.Create(mediaCreate);
            if (newMedia is not null)
            {
                _mediaRepository.CreateNewMedia(newMedia);

                Console.WriteLine($"A new media is created:{newMedia}");
                
            }
            else
            {
                Console.WriteLine("Failed to create new media");
                
            }
            return newMedia;
        }
        //public bool UpdateMedia(MediaUpdateDto mediaUpdate);

        //public bool DeleteMediaById(Guid id);
        //public bool DeleteAllMedia();
    }
}