using MediaPlayer.Core.src.Entity;

namespace MediaPlayer.Core.src.RepositoryAbstraction
{
    public interface IMediaRepository
    {
        public IEnumerable<Media> GetAllMedia();
        public Media? GetMediaById(Guid id);
        public IEnumerable<Media>? GetMediaByTitle(string title);
        public IEnumerable<Media>? GetMediaByArtist(string artist);
        public void CreateNewMedia(Media media);// deals with the original shape of the object, no DTOs
        public void Remove(Media media);
        public void RemoveAll();
        public void UpdateMedia(Media media);

        
    }
}