using MediaPlayer.Core.src.Entity;
using MediaPlayer.Infrastructure.src.Data;
using MediaPlayer.Infrastructure.src.Repository;
using MediaPlayer.Service.Service;
using MediaPlayer.Service.src.DTO;
using MediaPlayer.Service.src.Service;

internal class Program
{
    public static Admin CreateAdmin()
    {
        var admin = new Admin("admin", "admin@mail.com", "Admin");
        return admin;
    }

    public static void TestUserService(UserService userService)
    {
        var admin2 = new UserCreateDto("admin2", "admin@gmail.com", "Admin 2", true);
        var user1 = new UserCreateDto("user", "user1@gmail.com", "User 1", false);
        Console.WriteLine("Should create users sucessfully\n");
        var user = userService.AddUser(user1);
        var admin = userService.AddUser(admin2);
        Console.WriteLine(user);
        Console.WriteLine(admin);
        Console.WriteLine("Should not create existing user\n");
        var user1Duplicate = new UserCreateDto("user", "user1@gmail.com", "User 1", false);
        userService.AddUser(user1Duplicate);
        Console.WriteLine("Should remove user successfully");
        userService.DeleteUserById(user.Id);
        Console.WriteLine("Should not remove user");
        userService.DeleteUserById(Guid.NewGuid());
        Console.WriteLine("Should update user");
        var userUpdate = new UserUpdateDto("newadmin@mail.com", "updated admin");
        userService.UpdateUser(admin.Id, userUpdate);
        Console.WriteLine("Should not update user");
        userService.UpdateUser(Guid.NewGuid(), userUpdate);
        Console.WriteLine("Should remove all user");
        userService.DeleteAllUsers();
    }

    public static void TestMediaService(MediaService mediaService)
    {
        var audio1 = new MediaCreateDto(MediaType.Audio, "audio", "yun", 1995);
        var video1 = new MediaCreateDto(MediaType.Video, "video", "dane", 2015);
        Console.WriteLine("Should create media sucessfully\n");
        var audio = mediaService.AddMedia(audio1);
        var video = mediaService.AddMedia(video1);
        Console.WriteLine(audio);
        Console.WriteLine(video);
        Console.WriteLine("Should not create media with invalid year \n");
        var invalidMedia = new MediaCreateDto(MediaType.Video, "video", "s", 500);
        var invalid = mediaService.AddMedia(invalidMedia);
        Console.WriteLine($"should return null:{invalid is null}");
        Console.WriteLine("Should delete media sucessfully \n");
        mediaService.DeleteMediaById(video.Id);
        Console.WriteLine("Should not delete");
        mediaService.DeleteMediaById(Guid.NewGuid());
    }

    public static void TestPlayListService(PlayListService playListService,User user)
    {
        var playList1 = new PlayListCreateDTO(user.Id, "Yun playlist 1", false);
        var playlist2 = new PlayListCreateDTO(user.Id, "My Playlist 1");

        var playlsit = playListService.CreateNewPlaylist(playList1);
        playListService.CreateNewPlaylist(playlist2);
    }

    private static void Main(string[] args)
    {
        Console.WriteLine("##############Set up test##############\n");
        var db = Database.GetDatabase();
        var admin = CreateAdmin();
        var adminNotification = new AdminNotification();
        var userNotification = new UserNotification();

        Console.WriteLine("##############Test user service##############\n");
        var userRepo = new UserRepository(db);
        var userService = new UserService(userRepo, admin);
        userService.Attach(adminNotification);

        TestUserService(userService);

        Console.WriteLine("\n##############Test media service##############\n");
        var mediaRepo = new MediaRepository(db);
        var mediaService = new MediaService(mediaRepo, admin);
        mediaService.Attach(adminNotification);

        TestMediaService(mediaService);

        //// Test playlist service
        var playListRepo = new PlaylistRepository(db);
        var user1 = new UserCreateDto("user", "user1@gmail.com", "User 1", false);
        Console.WriteLine("Should create users sucessfully\n");
        var user = userService.AddUser(user1);
        var playListService = new PlayListService(playListRepo, user);
        playListService.Attach(userNotification);
        TestPlayListService(playListService,user);

        //// PlayList control service
        //var playListController = new PlayListControlService(playlsit, user);
        //playListController.Attach(userNotification);

        //// play,stop,pause playlist
        //playListController.PlayPlaylist();
        //playListController.StopPlaylist();
        //playListController.PausePlaylist();

        //// change sound effect or brightness
        //playListController.ChangeSoundEffect(audio, "base");
        //playListController.ChangeSoundEffect(video, "base");
        //playListController.ChangeBrightness(audio, "bright");
        //playListController.ChangeBrightness(video, "bright");
    }
}
