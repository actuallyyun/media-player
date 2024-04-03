using MediaPlayer.Core.src.Entity;
using MediaPlayer.Infrastructure.src.Data;
using MediaPlayer.Infrastructure.src.Repository;
using MediaPlayer.Service.Service;
using MediaPlayer.Service.src.DTO;
using MediaPlayer.Service.src.Service;

internal class Program
{
    private static void Main(string[] args)
    {
        // create user and media via admin service
        var db = new Database();
        var mediaRepo = new MediaRepository(db);
        var userRepo = new UserRepository(db);
        var playListRepo = new PlaylistRepository(db);
        var adminService = new AdminService(mediaRepo, userRepo);

        var mediaCreate1 = new MediaCreateDto(MediaType.Audio, "audio", "yun", 1995);
        var mediaCreate2 = new MediaCreateDto(MediaType.Video, "video", "dane", 2015);

        var user1 = new UserCreateDto("yunyun", "yun@gmail.com", "Yun Ji", true);
        var user2 = new UserCreateDto("yunyun", "yun@gmail.com", "Yun Ji", false);

        var audio1 = adminService.AddMedia(mediaCreate1);
        var video1 = adminService.AddMedia(mediaCreate2);

        var user1Created = adminService.AddUser(user1);
        var user2Created = adminService.AddUser(user2);

        // create a new playlist via user service

        var user1Service = new UserService(playListRepo, user1Created);
        var user2Service = new UserService(playListRepo, user2Created);

        var playList1 = new PlayListCreateDTO(user1Created.Id, "Yun playlist 1", false);
        var playlist2 = new PlayListCreateDTO(user2Created.Id, "My Playlist 1");

        var playListCreated1 = user1Service.CreateNewPlaylist(playList1);
        var playListCreated2 = user1Service.CreateNewPlaylist(playlist2);
        // Add media to playlist
        user1Service.AddMediaToPlayList(playListCreated1.Id, audio1);
        
        // add playlist
        user2Service.AddPlaylistById(playListCreated1.Id);

        // play,stop,pause playlist
        user1Service.PlayPlaylist(playListCreated1);
        user1Service.StopPlaylist(playListCreated1);
        user1Service.PausePlaylist(playListCreated1);

        // change sound effect or brightness
        user1Service.ChangeSoundEffect(audio1, "base");
        user1Service.ChangeSoundEffect(video1, "base");
        user1Service.ChangeBrightness(audio1, "bright");
        user1Service.ChangeBrightness(video1, "bright");
    }
}
