using MediaPlayer.Core.src.Entity;
using MediaPlayer.Core.src.Enums;
using MediaPlayer.Service.src.DTO;
using Microsoft.VisualBasic;

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

        public static IEnumerable<object[]> InvalidMediaData =>
            [new object[] { InvalidYear1 }, new object[] { InvalidYear2 }];

        public static Media Media1 => new Audio("audio1", "audio", 2000);
        public static User User1 => new User("user", "user@mail.com", "User");

        public static UserCreateDto UserCreate = new UserCreateDto(
            "user1",
            "user",
            "User",
            UserType.User
        );
        public static UserCreateDto AdminCreate = new UserCreateDto(
            "user2",
            "user",
            "User",
            UserType.Admin
        );
        public static UserUpdateDto validUserUpdate = new UserUpdateDto("user1", null);
        public static UserUpdateDto invalidUserUpdate = new UserUpdateDto("usernoneexisting", null);
    }
}
