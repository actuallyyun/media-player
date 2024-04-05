using MediaPlayer.Core.src.Enums;

namespace MediaPlayer.Service.src.DTO
{
    public class MediaReadDto
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public int Year { get; set; }

        public int Id;
    }

    public class MediaCreateDto
    {
        public MediaType Type {get;set;}
        public string Title { get; set; }
        public string Artist { get; set; }
        public int Year { get; set; }

        public MediaCreateDto(MediaType  type,string title, string artist, int year)
        {
            Title = title;
            Artist = artist;
            Year = year;
            Type=type;
        }
    }

    public class MediaUpdateDto
    {
        public string? Title { get; set; }
        public string? Artist { get; set; }
        public int? Year { get; set; }

        public MediaUpdateDto(string? title, string? artist, int? year)
        {
            Title = title;
            Artist = artist;
            Year = year;
        }
    }
}
