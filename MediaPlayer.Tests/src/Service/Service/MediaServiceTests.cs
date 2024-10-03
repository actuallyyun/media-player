//using MediaPlayer.Core.src.Abstraction;
//using MediaPlayer.Core.src.Entity;
//using MediaPlayer.Core.src.Enums;
//using MediaPlayer.Core.src.RepositoryAbstraction;
//using MediaPlayer.Service.src.DTO;
//using MediaPlayer.Service.src.Service;
//using MediaPlayer.Service.src.ServiceAbstraction;
//using Moq;

//namespace MediaPlayer.Tests.src.Service.Service
//{
//    public class MediaServiceTests
//    {
//        public static IEnumerable<object[]> InvalidMediaData => TestUtils.InvalidMediaData;
//        private Mock<IMediaRepository> _mockMediaRepo = new Mock<IMediaRepository>();
//        private Mock<IAuthorizationService> _mockAuthorizationService =
//            new Mock<IAuthorizationService>();
//        private Mock<IMediaPlayerMonitor> _mockNotificationService =
//            new Mock<IMediaPlayerMonitor>();
//        static MediaCreateDto testMediaCreate = new MediaCreateDto(
//            MediaType.Audio,
//            "test media",
//            "unkown artist",
//            2000
//        );

//        public MediaServiceTests()
//        {
//            _mockMediaRepo.Setup(service => service.Add(It.IsAny<Media>()));
//            _mockAuthorizationService.Setup(x => x.IsAuthenticated).Returns(true);
//            _mockAuthorizationService
//                .Setup(x => x.HasPermission(It.IsAny<UserType>()))
//                .Returns(true);
//        }

//        [Fact]
//        public void Instantiate_WithUnauthorizedCredentials_ShouldThrowExpection()
//        {
//            _mockAuthorizationService.Setup(x => x.IsAuthenticated).Returns(false);
//            _mockAuthorizationService
//                .Setup(x => x.HasPermission(It.IsAny<UserType>()))
//                .Returns(false);
//            var ex = Assert.Throws<UnauthorizedAccessException>(
//                () =>
//                    new MediaService(
//                        _mockMediaRepo.Object,
//                        _mockNotificationService.Object,
//                        _mockAuthorizationService.Object
//                    )
//            );
//            Assert.Contains("Unauthorized", ex.Message);
//        }

//        [Fact]
//        public void AddMedia_WithValidData_ShouldCreateAndReturnMedia()
//        {
//            var mediaService = new MediaService(
//                _mockMediaRepo.Object,
//                _mockNotificationService.Object,
//                _mockAuthorizationService.Object
//            );
//            var result = mediaService.AddMedia(testMediaCreate);
//            Assert.IsType<Audio>(result);
//            Assert.Equal(testMediaCreate.Title, result.Title);
//        }

//        [Theory]
//        [MemberData(nameof(InvalidMediaData))]
//        public void AddMedia_WithInvalidData_ShouldReturnNull(MediaCreateDto invalidMediaData)
//        {
//            var mediaService = new MediaService(
//                _mockMediaRepo.Object,
//                _mockNotificationService.Object,
//                _mockAuthorizationService.Object
//            );

//            var result = mediaService.AddMedia(invalidMediaData);
//            Assert.Null(result);
//        }

//        [Theory]
//        [InlineData(true)]
//        [InlineData(false)]
//        public void DeleteMediaById_WhenMediaFound_ShouldReturnTrue(bool mediaFound)
//        {
//            _mockMediaRepo
//                .Setup(repo => repo.GetMediaById(It.IsAny<Guid>()))
//                .Returns(mediaFound ? TestUtils.Media1 : null);
//            var mediaService = new MediaService(
//                _mockMediaRepo.Object,
//                _mockNotificationService.Object,
//                _mockAuthorizationService.Object
//            );
//            var result = mediaService.DeleteMediaById(Guid.NewGuid());
//            Assert.Equal(mediaFound, result);
//        }

//        [Fact]
//        public void UpdateMedia_WithValidData_ShouldReturnTrue()
//        {
//            Guid id = Guid.NewGuid();
//            MediaUpdateDto mediaUpdate = new MediaUpdateDto("update", null, 2006);
//            _mockMediaRepo
//                .Setup(repo => repo.GetMediaById(It.IsAny<Guid>()))
//                .Returns(TestUtils.Media1);
//            var mediaService = new MediaService(
//                _mockMediaRepo.Object,
//                _mockNotificationService.Object,
//                _mockAuthorizationService.Object
//            );
//            var result = mediaService.UpdateMedia(id, mediaUpdate);
//            Assert.True(result);
//        }

//        [Fact]
//        public void UpdateMedia_WithInValidData_ShouldReturnFalse()
//        {
//            Guid id = Guid.NewGuid();
//            MediaUpdateDto mediaUpdate = new MediaUpdateDto("update", null, 2);
//            _mockMediaRepo
//                .Setup(repo => repo.GetMediaById(It.IsAny<Guid>()))
//                .Returns(TestUtils.Media1);
//            var mediaService = new MediaService(
//                _mockMediaRepo.Object,
//                _mockNotificationService.Object,
//                _mockAuthorizationService.Object
//            );
//            var result = mediaService.UpdateMedia(id, mediaUpdate);
//            Assert.False(result);
//        }
//    }
//}
