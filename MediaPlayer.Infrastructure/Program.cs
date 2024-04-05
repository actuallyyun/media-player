using MediaPlayer.Core.src.Entity;
using MediaPlayer.Core.src.Enums;
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
    public static User CreateTestUser(){
        var user=new User("testusr","u@mail.com","User");
        return user;
    }

    public static void TestUserService(UserService userService)
    {
        var admin2 = new UserCreateDto("admin0", "admin@gmail.com", "Admin ", UserType.Admin);
        var user1 = new UserCreateDto("user", "user1@gmail.com", "User 1", UserType.User);
        Console.WriteLine("Should create users sucessfully\n");
        var user = userService.AddUser(user1);
        var admin = userService.AddUser(admin2);
        Console.WriteLine(user);
        Console.WriteLine(admin);
        Console.WriteLine("Should not create existing user\n");
        var user1Duplicate = new UserCreateDto("user", "user1@gmail.com", "User 1", UserType.User);
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

    public static void TestPlayListService(PlayListService playListService, User user, Database db)
    {
        var playList1 = new PlayListCreateDTO(user.Id, "Yun playlist 1", false);
        var playlist = playListService.CreateNewPlaylist(playList1);

        var media = db._media.First();
        playListService.AddMediaToPlayList(playlist.Id, media);
        playListService.RemoveMediaFromList(playlist.Id, media.Id);
        playListService.DeletePlaylistById(playlist.Id);
        playListService.RemoveMediaFromList(playlist.Id, media.Id);
    }

    public static void TestPlaylistControService(Database db,UserNotification userNotification,User user){
        // PlayList control service
        var playlist=db._playlists.First();
        var audio=db._media.First(m=>m.Type==MediaType.Audio);
        var video=db._media.First(m=>m.Type==MediaType.Video);
        var playListController = new PlayListControlService(playlist, user);
        playListController.Attach(userNotification);

        // play,stop,pause playlist
        playListController.PlayPlaylist();
        playListController.StopPlaylist();
        playListController.PausePlaylist();

        // change sound effect or brightness
        playListController.ChangeSoundEffect(audio,SoundEffectType.Zing);
        playListController.ChangeSoundEffect(video, SoundEffectType.Whirr);
        playListController.ChangeBrightness(audio, 4);
        playListController.ChangeBrightness(video, 10);
        playListController.ChangeBrightness(video, 11);
        playListController.ChangeBrightness(video, -1);
    }

    private static void Main(string[] args)
    {
        // setup test
        var db = Database.GetDatabase();
        var adminNotification = new AdminNotification();
        var userNotification = new UserNotification();

        var admin=CreateAdmin();
        var user=CreateTestUser();

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

        // Test playlist service
        var playListRepo = new PlaylistRepository(db);
        var playListService = new PlayListService(playListRepo, user);
        playListService.Attach(userNotification);
        TestPlayListService(playListService, user, db);

        // test playlist control
        TestPlaylistControService(db,userNotification,user);
        
    }
}
