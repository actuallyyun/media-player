using MediaPlayer.Core.src.Entity;

namespace MediaPlayer.Core.src.RepositoryAbstraction
{
    public interface IMediaRepository
    {
        public IEnumerable<Media> GetAllMedia();
        public IEnumerable<Media>? GetMediaByTitle(string title);
        public IEnumerable<Media>? GetMediaByArtist(string artist);
        public void CreateNewMedia(Media media);// deals with the original shape of the object, no DTOs
        public void RemoveMediaById(Guid id);
        public void UpdateMedia(Media media);

        
    }
}