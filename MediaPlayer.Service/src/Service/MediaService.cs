using MediaPlayer.Core.src.Abstraction;
using MediaPlayer.Core.src.Entity;
using MediaPlayer.Core.src.RepositoryAbstraction;
using MediaPlayer.Service.src.DTO;
using MediaPlayer.Service.src.Utils;

namespace MediaPlayer.Service.src.Service
{
    public class MediaService : IMediaPlayerMonitor
    {
        private List<INotify> _observers = new List<INotify>();
        private IMediaRepository _mediaRepository;
        private Admin _admin;

        public MediaService(IMediaRepository mediaRepo, Admin admin)
        { // must provide an admin to instantianize the service since this is admin only feature
            _mediaRepository = mediaRepo;
            _admin = admin;
            Notify($"A new user service is created by {_admin}");
        }

        public Media? AddMedia(MediaCreateDto mediaCreate)
        {
            var mediaFactory = new MediaFactory();
            try
            {
                Media? newMedia = mediaFactory.Create(mediaCreate);
                if (newMedia is not null)
                {
                    _mediaRepository.CreateNewMedia(newMedia);
                    Notify($"A new media is created:{newMedia}");
                }
                else
                {
                    Notify("Failed to create new media");
                }
                return newMedia;
            }
            catch (Exception e)
            {
                Notify($"An error has occurred:{e.Message}");
                return null;
            }
        }

        public void Attach(INotify observer)
        {
            _observers.Add(observer);
        }

        public void Detach(INotify observer)
        {
            _observers.Remove(observer);
        }

        public void Notify(string message)
        {
            foreach (var observer in _observers)
            {
                observer.Update(message);
            }
        }

        //public bool UpdateMedia(MediaUpdateDto mediaUpdate);

        //public bool DeleteMediaById(Guid id);
        //public bool DeleteAllMedia();
    }
}
