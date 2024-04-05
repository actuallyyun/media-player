using MediaPlayer.Core.src.Entity;

namespace MediaPlayer.Core.src.RepositoryAbstraction
{
    public interface IMediaRepository
    {
        public Media? GetMediaById(Guid id);
        public void Add(Media media);// deals with the original shape of the object, no DTOs
        public void Remove(Media media);
        public void RemoveAll();
        public void Update(Media media,string? title,string? artist,int?year);
        
        
    }
}