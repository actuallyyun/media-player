using MediaPlayer.Core.src.Enums;


namespace MediaPlayer.Service.src.ServiceAbstraction
{
    public interface IAuthorizationService
    {
                bool IsAuthenticated{get;}

        bool HasPermission(UserType typeRequired);
        string UserName{get;}
        
    }
}