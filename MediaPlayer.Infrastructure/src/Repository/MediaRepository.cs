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

        public void Add(Media media)
        {
            _meida.Add(media);
        }

        public Media? GetMediaById(Guid id)
        {
            return _meida.FirstOrDefault(m=>m.Id==id);
        }

        public void Remove(Media media)
        {
            _meida.Remove(media);
        }
        public void RemoveAll(){
            _meida.Clear();
        }

        public void Update(Media media, string? title, string? artist, int? year)
        {
            media.Update(title,artist,year);
        }
    }
}