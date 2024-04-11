using MediaPlayer.Service.src.DTO;
using MediaPlayer.Core.src.Enums;
using Microsoft.VisualBasic;

namespace MediaPlayer.Tests.src.Service
{
    public static class TestUtils
    {
        
        public static MediaCreateDto invalidYear1 = new MediaCreateDto(
            MediaType.Audio,
            "test audio",
            "artist",
            20
        );

        public static MediaCreateDto invalidYear2 = new MediaCreateDto(
            MediaType.Audio,
            "test audio",
            "artist",
            DateAndTime.Now.AddYears(10).Year
        );

        public static IEnumerable<object[]> InvalidMediaData => [
                new object[] { invalidYear1 },
                new object[] { invalidYear2 }
            ];
    }
}