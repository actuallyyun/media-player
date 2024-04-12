using MediaPlayer.Core.src.Entity;
using MediaPlayer.Core.src.Enums;
using MediaPlayer.Service.src.ServiceAbstraction;

namespace MediaPlayer.Service.src.Service
{
    public class AuthorizationService : IAuthorizationService
    {

        private User _currentUser{get;}
        public AuthorizationService(User currentUser){
            _currentUser=currentUser;
        }
        
        public bool IsAuthenticated => _currentUser is null?false:true;

        public string UserName => _currentUser.FullName;

        public bool HasPermission(UserType typeRequired)
        {
            if(_currentUser.Type==UserType.Admin){ // Admin has full access to all
                return true;
            }

            if(_currentUser.Type ==typeRequired){
                return true;
            }
                return false;
          
        }
    }
}