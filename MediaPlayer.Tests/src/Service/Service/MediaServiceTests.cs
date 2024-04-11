
using Moq;

using MediaPlayer.Service.src.Service;
using MediaPlayer.Core.src.Abstraction;
using MediaPlayer.Core.src.RepositoryAbstraction;
using MediaPlayer.Core.src.Entity;
using MediaPlayer.Service.src.DTO;
using MediaPlayer.Core.src.Enums;

namespace MediaPlayer.Tests.src.Service.Service
{
    public class MediaServiceTests
    {
        private readonly MediaService _service; // SUT -> s
        //private List<INotify> _mockObservers = new List<INotify>();
        private Mock<List<INotify>> _mockObservers = new Mock<List<INotify>>();
            
        private Mock<IMediaRepository> _mockMediaRepo = new Mock<IMediaRepository>();

        private Mock<Admin> _mockAdmin=new Mock<Admin>("admin","admin","Admin");
        static MediaCreateDto testMediaCreate=new MediaCreateDto(MediaType.Audio,"test media","unkown artist",2000);
   
        public MediaServiceTests(){
            _mockMediaRepo.Setup(service=>service.Add(It.IsAny<Media>()));

        }

        [Fact]        
        public void AddMedia_WithValidData_ShouldCreateAndReturnMedia()
        {   
         var mediaService=new MediaService(_mockMediaRepo.Object,_mockAdmin.Object);
            var result=mediaService.AddMedia(testMediaCreate);
            Assert.IsType<Audio>(result);
            Assert.Equal(testMediaCreate.Title, result.Title);
        }
    }
}