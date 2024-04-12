using MediaPlayer.Core.src.Abstraction;
using MediaPlayer.Core.src.Entity;
using MediaPlayer.Core.src.RepositoryAbstraction;
using MediaPlayer.Core.src.Utils;
using MediaPlayer.Service.src.DTO;
using MediaPlayer.Service.src.ServiceAbstraction;
using MediaPlayer.Service.src.Utils;
using MediaPlayer.Core.src.Enums;

namespace MediaPlayer.Service.src.Service
{
    public class MediaService
    {
        private IMediaRepository _mediaRepository;
        private readonly IMediaPlayerMonitor _notificationService;
        private readonly IAuthorizationService _authorizationService;

        public MediaService(IMediaRepository mediaRepo, IMediaPlayerMonitor notificationService, IAuthorizationService authorizationService)
        { 
            _mediaRepository = mediaRepo;
            _notificationService=notificationService;
            if(!authorizationService.IsAuthenticated ||!authorizationService.HasPermission(UserType.Admin)){
                throw new UnauthorizedAccessException("Unauthorized action.");
            }
            _authorizationService = authorizationService;
        }

        public Media? AddMedia(MediaCreateDto mediaCreate)
        {
            var mediaFactory = new MediaFactory();
            try
            {
                Media? newMedia = mediaFactory.Create(mediaCreate);
                if (newMedia is not null)
                {
                    _mediaRepository.Add(newMedia);
                    _notificationService.Notify($"A new media is created:{newMedia}");
                }
                else
                {
                    _notificationService.Notify("Failed to create new media");
                }
                return newMedia;
            }
            catch (Exception e)
            {
                _notificationService.Notify($"An error has occurred:{e.Message}");
                return null;
            }
        }

        public bool DeleteMediaById(Guid id)
        {
            var mediaFound = _mediaRepository.GetMediaById(id);
            if (mediaFound is null)
            {
                _notificationService.Notify("Cannot delete.Media not found");
                return false;
            }
            _mediaRepository.Remove(mediaFound);
            _notificationService.Notify($"Media removed: {mediaFound}");
            return true;
        }

        public bool DeleteAllMedia()
        {
            _mediaRepository.RemoveAll();
            _notificationService.Notify("All media is removed");
            return true;
        }

        public bool UpdateMedia(Guid id, MediaUpdateDto mediaUpdate)
        {
            var mediaFound = _mediaRepository.GetMediaById(id);
            if (mediaFound is null)
            {
                _notificationService.Notify("Cannot update: media not found");
                return false;
            }
            if (!Validator.IsValidYear(mediaUpdate.Year))
            {
                _notificationService.Notify("Cannot update: invalid year");
                return false;
            }
            _mediaRepository.Update(
                mediaFound,
                mediaUpdate.Title,
                mediaUpdate.Artist,
                mediaUpdate.Year
            );
            _notificationService.Notify("Update successful!");
            return true;
        }
    }
}
