using MediaPlayer.Core.src.Entity;
using MediaPlayer.Core.src.RepositoryAbstraction;
using MediaPlayer.Service.src.DTO;
using MediaPlayer.Service.src.Utils;

namespace MediaPlayer.Service.src.Service
{
    public class MediaService
    {
        private IMediaRepository _mediaRepository;
        private Admin _admin;

        public MediaService(IMediaRepository mediaRepo, Admin admin)
        { // must provide an admin to instantianize the service since this is admin only feature
            _mediaRepository = mediaRepo;
            _admin = admin;
        }

        public Media? AddMedia(MediaCreateDto mediaCreate){
            var mediaFactory = new MediaFactory();
            Media? newMedia = mediaFactory.Create(mediaCreate);
            if (newMedia is not null)
            {
                _mediaRepository.CreateNewMedia(newMedia);

                Console.WriteLine($"A new media is created:{newMedia}");
                
            }
            else
            {
                Console.WriteLine("Failed to create new media");
                
            }
            return newMedia;
        }

        //public bool UpdateMedia(MediaUpdateDto mediaUpdate);

        //public bool DeleteMediaById(Guid id);
        //public bool DeleteAllMedia();
    }
}
