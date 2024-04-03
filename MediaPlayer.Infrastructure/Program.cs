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
        var playListRepo=new PlaylistRepository(db);
        var adminService = new AdminService(mediaRepo, userRepo);

        var mediaCreate = new MediaCreateDto(MediaType.Audio, "media1", "yun", 1995);
        var user1 = new UserCreateDto("yunyun", "yun@gmail.com", "Yun Ji", true);
        var user2 = new UserCreateDto("yunyun", "yun@gmail.com", "Yun Ji", false);
 
        var mediaCreated1=adminService.AddMedia(mediaCreate);
        adminService.AddUser(user1);
        adminService.AddUser(user2);

        // create a new playlist via user service

        Guid user1Id=adminService.GetUserByName("yunyun").Id;
               
        var user1Service=new UserService(playListRepo,user1Id);
        var playList1=new PlayListCreateDTO(user1Id,"Yun playlist 1",false);
        var playListCreated1=user1Service.CreateNewPlaylist(playList1);

        // Add media to playlist
        user1Service.AddMediaToPlayList(playListCreated1.Id,mediaCreated1);

        

    }
}
