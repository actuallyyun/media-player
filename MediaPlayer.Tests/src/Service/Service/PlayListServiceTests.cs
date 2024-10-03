using MediaPlayer.Core.src.Abstraction;
using MediaPlayer.Core.src.Entity;
using MediaPlayer.Core.src.Enums;
using MediaPlayer.Core.src.RepositoryAbstraction;
using MediaPlayer.Service.src.DTO;
using MediaPlayer.Service.src.Service;
using MediaPlayer.Service.src.ServiceAbstraction;
using Moq;

namespace MediaPlayer.Tests.src.Service.Service
{
    public class PlayListServiceTests
    {
        private Mock<IPlaylistRepository> _mockPlayListRepository = new Mock<IPlaylistRepository>();
        private Mock<IAuthorizationService> _mockAuthorizationService =
            new Mock<IAuthorizationService>();
        private Mock<IMediaPlayerMonitor> _mockNotificationService =
            new Mock<IMediaPlayerMonitor>();

        public static IEnumerable<object[]> InvalidPlaylistCreateData =>
            TestUtils.InvalidPlaylistCreateData;

        public static IEnumerable<object[]> InvalidPlaylistAddByIdData =>
            TestUtils.InvalidPlaylistAddByIdData;

        public PlayListServiceTests()
        {
            _mockAuthorizationService.Setup(x => x.IsAuthenticated).Returns(true);
            _mockAuthorizationService
                .Setup(x => x.HasPermission(It.IsAny<UserType>()))
                .Returns(true);

            _mockPlayListRepository.Setup(r => r.Add(It.IsAny<PlayList>()));
            _mockPlayListRepository.Setup(r =>
                r.AddToUserPlaylist(It.IsAny<User>(), It.IsAny<PlayList>())
            );
        }

        [Fact]
        public void Instantiate_WithUnauthorizedCredentials_ShouldThrowExpection()
        {
            _mockAuthorizationService.Setup(x => x.IsAuthenticated).Returns(false);
            _mockAuthorizationService
                .Setup(x => x.HasPermission(It.IsAny<UserType>()))
                .Returns(false);
            var ex = Assert.Throws<UnauthorizedAccessException>(
                () =>
                    new PlayListService(
                        _mockPlayListRepository.Object,
                        _mockNotificationService.Object,
                        _mockAuthorizationService.Object
                    )
            );
            Assert.Contains("Unauthorized", ex.Message);
        }

        [Fact]
        public void CreateNewPlaylist_WithValidData_ShouldCreateAndReturnPlayList()
        {
            var user = new User("user", "user@mail.com", "User");
            _mockPlayListRepository
                .Setup(x => x.GetPlaylistsByOwnerId(It.IsAny<Guid>()))
                .Returns(new PlayList[] { new PlayList(user.Id, "playlist1") });
            _mockAuthorizationService.Setup(x => x.GetUser()).Returns(user);

            PlayListCreateDTO validPlaylistCreate = new PlayListCreateDTO(
                user.Id,
                "playlist2",
                false
            );
            var playlistService = new PlayListService(
                _mockPlayListRepository.Object,
                _mockNotificationService.Object,
                _mockAuthorizationService.Object
            );

            var result = playlistService.CreateNewPlaylist(validPlaylistCreate);

            Assert.Equal(validPlaylistCreate.Name, result?.Name);
            _mockPlayListRepository.Verify(r => r.Add(result), Times.Once());
            _mockPlayListRepository.Verify(
                r => r.AddToUserPlaylist(It.IsAny<User>(), It.IsAny<PlayList>()),
                Times.Once()
            );
        }

        [Theory]
        [MemberData(nameof(InvalidPlaylistCreateData))]
        public void CreateNewPlaylist_WithInvalidData_ShouldNotCreateAndReturnNull(
            PlayListCreateDTO invalidData
        )
        {
            var user = new User("user", "user@mail.com", "User");
            _mockPlayListRepository
                .Setup(x => x.GetPlaylistsByOwnerId(It.IsAny<Guid>()))
                .Returns(new PlayList[] { new PlayList(user.Id, "playlist1") });
            _mockAuthorizationService.Setup(x => x.GetUser()).Returns(user);

            var playlistService = new PlayListService(
                _mockPlayListRepository.Object,
                _mockNotificationService.Object,
                _mockAuthorizationService.Object
            );
            var result = playlistService.CreateNewPlaylist(invalidData);
            Assert.Null(result);
            _mockPlayListRepository.Verify(x => x.Add(It.IsAny<PlayList>()), Times.Never);
            _mockPlayListRepository.Verify(
                r => r.AddToUserPlaylist(It.IsAny<User>(), It.IsAny<PlayList>()),
                Times.Never()
            );
        }

        [Fact]
        public void AddPlaylistById_WithValidData_ShouldAddAndReturnTrue()
        {
            var id = TestUtils.Playlist2.Id;
            _mockPlayListRepository.Setup(x => x.GetPlayListById(id)).Returns(TestUtils.Playlist2);
            var playlistService = new PlayListService(
                _mockPlayListRepository.Object,
                _mockNotificationService.Object,
                _mockAuthorizationService.Object
            );
            var result = playlistService.AddPlaylistById(id);
            Assert.True(result);
            _mockPlayListRepository.Verify(
                x => x.AddToUserPlaylist(It.IsAny<User>(), It.IsAny<PlayList>()),
                Times.Once
            );
        }

        [Theory]
        [MemberData(nameof(InvalidPlaylistAddByIdData))]
        public void AddPlaylistById_WithInvalidData_ShouldNotAddAndReturnFalse(Guid id)
        {
            PlayList? playlist = null;
            Media? media = null;
            _mockPlayListRepository.Setup(x => x.GetPlayListById(id)).Returns(playlist);
            _mockPlayListRepository
                .Setup(x => x.GetMediaInPlaylistById(It.IsAny<Guid>(), It.IsAny<Guid>()))
                .Returns(media);
            var playlistService = new PlayListService(
                _mockPlayListRepository.Object,
                _mockNotificationService.Object,
                _mockAuthorizationService.Object
            );
            var result = playlistService.AddPlaylistById(id);
            Assert.False(result);
            _mockPlayListRepository.Verify(
                x => x.AddToUserPlaylist(It.IsAny<User>(), It.IsAny<PlayList>()),
                Times.Never
            );
        }

        [Fact]
        public void AddMediaToPlayList_WithValidData_ShouldAddAndReturnTrue()
        {
            var playlistId = TestUtils.User1Playlist1.Id;
            Media? media = null;
            _mockPlayListRepository
                .Setup(x => x.GetMediaInPlaylistById(playlistId, TestUtils.Media1.Id))
                .Returns(media);
            _mockPlayListRepository.Setup(x =>
                x.AddMediaToPlaylist(It.IsAny<PlayList>(), It.IsAny<Media>())
            );
            var playlistService = new PlayListService(
                _mockPlayListRepository.Object,
                _mockNotificationService.Object,
                _mockAuthorizationService.Object
            );

            var result = playlistService.AddMediaToPlayList(playlistId, TestUtils.Media2);
            Assert.True(result);
            _mockPlayListRepository.Verify(
                x => x.AddMediaToPlaylist(It.IsAny<PlayList>(), It.IsAny<Media>()),
                Times.Once
            );
        }
    }
}
