using System.Security.Principal;
using MediaPlayer.Core.src.Abstraction;
using MediaPlayer.Core.src.Entity;
using MediaPlayer.Core.src.Enums;
using MediaPlayer.Core.src.RepositoryAbstraction;
using MediaPlayer.Service.src.DTO;
using MediaPlayer.Service.src.Service;
using Moq;

namespace MediaPlayer.Tests.src.Service.Service
{
    public class MediaServiceTests
    {
        public static IEnumerable<object[]> InvalidMediaData => TestUtils.InvalidMediaData;
        private Mock<IMediaRepository> _mockMediaRepo = new Mock<IMediaRepository>();

        private Mock<Admin> _mockAdmin = new Mock<Admin>("admin", "admin", "Admin");
        static MediaCreateDto testMediaCreate = new MediaCreateDto(
            MediaType.Audio,
            "test media",
            "unkown artist",
            2000
        );

        public MediaServiceTests()
        {
            _mockMediaRepo.Setup(service => service.Add(It.IsAny<Media>()));
        }

        [Fact]
        public void AddMedia_WithValidData_ShouldCreateAndReturnMedia()
        {
            var mediaService = new MediaService(_mockMediaRepo.Object, _mockAdmin.Object);
            var result = mediaService.AddMedia(testMediaCreate);
            Assert.IsType<Audio>(result);
            Assert.Equal(testMediaCreate.Title, result.Title);
        }

        [Theory]
        [MemberData(nameof(InvalidMediaData))]
        public void AddMedia_WithInvalidData_ShouldReturnNull(MediaCreateDto invalidMediaData)
        {
            var mediaService = new MediaService(_mockMediaRepo.Object, _mockAdmin.Object);

            var result = mediaService.AddMedia(invalidMediaData);
            Assert.Null(result);
        }

        [Fact]
        public void DeleteMediaById_WhenMediaFound_ShouldReturnTrue()
        {
            _mockMediaRepo
                .Setup(repo => repo.GetMediaById(It.IsAny<Guid>()))
                .Returns(TestUtils.Media1);
            var mediaService = new MediaService(_mockMediaRepo.Object, _mockAdmin.Object);
            var result = mediaService.DeleteMediaById(Guid.NewGuid());
            Assert.True(result);
        }

        [Fact]
        public void DeleteMediaById_WhenMediaNotFound_ShouldReturnFalse()
        {
            Media? media = null;
            _mockMediaRepo.Setup(repo => repo.GetMediaById(It.IsAny<Guid>())).Returns(media);
            var mediaService = new MediaService(_mockMediaRepo.Object, _mockAdmin.Object);
            var result = mediaService.DeleteMediaById(Guid.NewGuid());
            Assert.False(result);
        }

        [Fact]
        public void UpdateMedia_WithValidData_ShouldReturnTrue()
        {
            Guid id = Guid.NewGuid();
            MediaUpdateDto mediaUpdate = new MediaUpdateDto("update", null, 2006);
            _mockMediaRepo
                .Setup(repo => repo.GetMediaById(It.IsAny<Guid>()))
                .Returns(TestUtils.Media1);
            var mediaService = new MediaService(_mockMediaRepo.Object, _mockAdmin.Object);
            var result = mediaService.UpdateMedia(id, mediaUpdate);
            Assert.True(result);
        }

        [Fact]
        public void UpdateMedia_WithInValidData_ShouldReturnFalse()
        {
            Guid id = Guid.NewGuid();
            MediaUpdateDto mediaUpdate = new MediaUpdateDto("update", null, 2);
            _mockMediaRepo
                .Setup(repo => repo.GetMediaById(It.IsAny<Guid>()))
                .Returns(TestUtils.Media1);
            var mediaService = new MediaService(_mockMediaRepo.Object, _mockAdmin.Object);
            var result = mediaService.UpdateMedia(id, mediaUpdate);
            Assert.False(result);
        }
    }
}
