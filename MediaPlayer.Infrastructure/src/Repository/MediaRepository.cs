using MediaPlayer.Core.src.Entity;
using MediaPlayer.Core.src.RepositoryAbstraction;
using MediaPlayer.Infrastructure.src.Data;

namespace MediaPlayer.Infrastructure.src.Repository
{
    public class MediaRepository:IMediaRepository
    {
        private HashSet<Media> _meida;
        public MediaRepository(Database database){
            _meida=database._media;
        }

        public void CreateNewMedia(Media media)
        {
            _meida.Add(media);
        }

        public IEnumerable<Media> GetAllMedia()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Media>? GetMediaByArtist(string artist)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Media>? GetMediaByTitle(string title)
        {
            throw new NotImplementedException();
        }

        public void RemoveMediaById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void UpdateMedia(Media media)
        {
            throw new NotImplementedException();
        }
    }
}