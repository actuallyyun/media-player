using MediaPlayer.Core.src.Entity;
using MediaPlayer.Core.src.RepositoryAbstraction;
using MediaPlayer.Service.src.DTO;
using MediaPlayer.Service.src.Service;
using Moq;

namespace MediaPlayer.Tests.src.Service.Service
{
    public class PlayListServiceTests
    {
        private Mock<IPlaylistRepository> _mockPlayListRepository = new Mock<IPlaylistRepository>();
        private User _user;// Cannot mock user since it is not an interface. Mocking requires code base change. User a test user istead
        public static IEnumerable<object[]> InvalidPlaylistCreateData =>
            TestUtils.InvalidPlaylistCreateData;

        public static IEnumerable<object[]> InvalidPlaylistAddByIdData =>
            TestUtils.InvalidPlaylistAddByIdData;

        public PlayListServiceTests()
        {
            _user = TestUtils.User1;
        }

        [Fact]
        public void CreateNewPlaylist_WithValidData_ShouldCreateAndReturnPlayList()
        {
            PlayListCreateDTO validPlaylistCreate = new PlayListCreateDTO(
                _user.Id,
                "playlist create",
                false
            );
            _mockPlayListRepository.Setup(r => r.Add(It.IsAny<PlayList>()));
            _mockPlayListRepository.Setup(r =>
                r.AddToUserPlaylist(It.IsAny<User>(), It.IsAny<PlayList>())
            );

            var playlistService = new PlayListService(_mockPlayListRepository.Object, _user);
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
            _mockPlayListRepository.Setup(r => r.Add(It.IsAny<PlayList>()));
            _mockPlayListRepository.Setup(r =>
                r.AddToUserPlaylist(It.IsAny<User>(), It.IsAny<PlayList>())
            );

            var playlistService = new PlayListService(_mockPlayListRepository.Object, _user);
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
            var playlistService = new PlayListService(_mockPlayListRepository.Object, _user);
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
            _mockPlayListRepository.Setup(x => x.GetPlayListById(id)).Returns(playlist);
            var playlistService = new PlayListService(_mockPlayListRepository.Object, _user);
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
            
            TestUtils.PopulateUserPlaylist();
            var playlistId = TestUtils.User1Playlist1.Id;
            Media? media = null;
            _mockPlayListRepository
                .Setup(x => x.GetMediaInPlaylistById(playlistId, TestUtils.Media1.Id))
                .Returns(media);
            _mockPlayListRepository.Setup(x =>
                x.AddMediaToPlaylist(It.IsAny<PlayList>(), It.IsAny<Media>())
            );
            var playlistService = new PlayListService(_mockPlayListRepository.Object, _user);

            var result = playlistService.AddMediaToPlayList(playlistId, TestUtils.Media2);
            Assert.True(result);
            _mockPlayListRepository.Verify(
                x => x.AddMediaToPlaylist(It.IsAny<PlayList>(), It.IsAny<Media>()),
                Times.Once
            );
        }
    }
}
