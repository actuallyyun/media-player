using MediaPlayer.Core.src.Entity;
using MediaPlayer.Core.src.Enums;
using MediaPlayer.Service.src.DTO;
using MediaPlayer.Service.src.Utils;
using Microsoft.VisualBasic;

namespace MediaPlayer.Tests.src.Service.Utils
{
    public class MediaFactoryTests
    {
        public static IEnumerable<object[]> InvalidYearData { get; set; }
        static MediaCreateDto testVideo = new MediaCreateDto(
            MediaType.Video,
            "test video",
            "artist1",
            2000
        ); // non-literal value
        static MediaCreateDto testAudio = new MediaCreateDto(
            MediaType.Audio,
            "test audio",
            "artist",
            2000
        );

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

        //public static IEnumerable<object[]> InvalidYearMediaData => [[invalidYear1, invalidYear2]];

        static MediaFactoryTests()
        {
            InvalidYearData = [new object[] { invalidYear1 }, new object[] { invalidYear2 }];
        }

        [Fact]
        public void Create_WithVideoType_ShouldReturnVideoObject()
        {
            var mediaFactory = new MediaFactory();
            var result = mediaFactory.Create(testVideo);
            Assert.IsType<Video>(result);
        }

        [Fact]
        public void Create_WithAudioType_ShouldReturnAudioObject()
        {
            var mediaFactory = new MediaFactory();
            var result = mediaFactory.Create(testAudio);
            Assert.IsType<Audio>(result);
        }

        [Theory]
        [MemberData(nameof(InvalidYearData))]
        public void Create_WithInvalidYear_ShouldThrowError(MediaCreateDto invalidYear)
        {
            var expectionType = typeof(ArgumentException);
            var mediaFactory = new MediaFactory();

            var ex = Assert.Throws<ArgumentException>(() => mediaFactory.Create(invalidYear));

            Assert.Contains("year", ex.Message);
        }

    }
}
