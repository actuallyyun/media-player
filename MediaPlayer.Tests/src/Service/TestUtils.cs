using MediaPlayer.Service.src.DTO;
using MediaPlayer.Core.src.Enums;
using Microsoft.VisualBasic;
using MediaPlayer.Core.src.Entity;

namespace MediaPlayer.Tests.src.Service
{
    public static class TestUtils
    {
        
        public static MediaCreateDto InvalidYear1 = new MediaCreateDto(
            MediaType.Audio,
            "test audio",
            "artist",
            20
        );

        public static MediaCreateDto InvalidYear2 = new MediaCreateDto(
            MediaType.Audio,
            "test audio",
            "artist",
            DateAndTime.Now.AddYears(10).Year
        );

        public static IEnumerable<object[]> InvalidMediaData => [
                new object[] { InvalidYear1 },
                new object[] { InvalidYear2 }
            ];

        public static Media Media1=>new Audio("audio1","audio",2000);
    }
}