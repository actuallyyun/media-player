using MediaPlayer.Core.src.Entity;
using MediaPlayer.Core.src.Enums;
using MediaPlayer.Service.src.DTO;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.TestPlatform.TestHost;

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
        public static Media Media2 => new Video("video", "audio", 2000);
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

         public static IEnumerable<object[]> ValidUserCreateData =
        [
            new object[] { UserCreate },
            new object[] { AdminCreate }
        ];
        public static UserUpdateDto validUserUpdate = new UserUpdateDto("user1", null);
        public static UserUpdateDto invalidUserUpdate = new UserUpdateDto("usernoneexisting", null);

        public static PlayList User1Playlist1 = new PlayList(User1.Id, "playlist1", false);

        public static PlayList Playlist2 = new PlayList(Guid.NewGuid(), "playlist2", false);

        public static IEnumerable<PlayList> User1Playlists = [User1Playlist1,Playlist2];
        public static PlayListCreateDTO InvalidUserIdPlaylistCreate = new PlayListCreateDTO(
            Guid.NewGuid(),
            "playlist create",
            false
        );
        public static PlayListCreateDTO InvalidExisitingTitlePlaylistCreate = new PlayListCreateDTO(
            User1.Id,
            "playlist1",
            false
        );
        public static IEnumerable<object[]> InvalidPlaylistCreateData =
        [
            new object[] { InvalidUserIdPlaylistCreate },
            new object[] { InvalidExisitingTitlePlaylistCreate }
        ];
        public static IEnumerable<object[]> InvalidPlaylistAddByIdData =
        [
            new object[] { Guid.NewGuid() },
            new object[] { User1Playlist1.Id }
        ];

    

    }
}
