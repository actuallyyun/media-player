using MediaPlayer.Core.src.Entity;
using MediaPlayer.Infrastructure.src.Data;
using MediaPlayer.Infrastructure.src.Repository;
using MediaPlayer.Service.Service;
using MediaPlayer.Service.src.DTO;
using MediaPlayer.Service.src.Service;

internal class Program
{

    public static Admin CreateAdmin(){
        var admin=new Admin("admin","admin@mail.com","Admin");
        return admin;
    }
    private static void Main(string[] args)
    {
        // Test user service
        var db = Database.GetDatabase();
        var userRepo = new UserRepository(db);
        var admin=CreateAdmin();

        // create user service
        var userService=new UserService(userRepo,admin);
        var admin2 = new UserCreateDto("admin2", "admin@gmail.com", "Admin 2", true);
        var user1 = new UserCreateDto("user1", "user1@gmail.com", "User 1", false);

        var user=userService.AddUser(user1);
        userService.AddUser(admin2);

        // Test media service
        var mediaRepo = new MediaRepository(db);
        var mediaService=new MediaService(mediaRepo,admin);

        var audio1 = new MediaCreateDto(MediaType.Audio, "audio", "yun", 1995);
        var video1 = new MediaCreateDto(MediaType.Video, "video", "dane", 2015);

        var audio=mediaService.AddMedia(audio1);
        var video=mediaService.AddMedia(video1);

        // Test playlist service

        var playListRepo = new PlaylistRepository(db);
        var playListService=new PlayListService(playListRepo,user);


        var playList1 = new PlayListCreateDTO(user.Id, "Yun playlist 1", false);
        var playlist2 = new PlayListCreateDTO(user.Id, "My Playlist 1");

        var playlsit=playListService.CreateNewPlaylist(playList1);
        playListService.CreateNewPlaylist(playlist2);


        // PlayList control service
        var playListController=new PlayListControlService(playlsit,user);

        // play,stop,pause playlist
        playListController.PlayPlaylist();
        playListController.StopPlaylist();
        playListController.PausePlaylist();

        // change sound effect or brightness
        playListController.ChangeSoundEffect(audio, "base");
        playListController.ChangeSoundEffect(video, "base");
        playListController.ChangeBrightness(audio, "bright");
        playListController.ChangeBrightness(video, "bright");
    }
}
