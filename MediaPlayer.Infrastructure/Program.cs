
using MediaPlayer.Core.src.Entity;
using MediaPlayer.Infrastructure.src.Data;
using MediaPlayer.Infrastructure.src.Repository;
using MediaPlayer.Service.src.DTO;
using MediaPlayer.Service.src.Service;

internal class Program
{
    private static void Main(string[] args)
    {
        var db=new Database();
        var mediaRepo=new MediaRepository(db);
        var adminService=new AdminService(mediaRepo);
        
        var mediaCreate=new MediaCreateDto(MediaType.Audio,"media1","yun",1995);
        Console.WriteLine(mediaCreate);
        adminService.AddMedia(mediaCreate);
    }
}