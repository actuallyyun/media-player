using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaPlayer.Core.Entity;

namespace MediaPlayer.Core.RepositoryAbstraction
{
    public interface IMediaRepository
    {
        public IEnumerable<BaseMedia> GetAllMedia();
        public void AddNewMedia(BaseMedia media);
        public void RemoveMedia(BaseMedia media);
        public IEnumerable<BaseMedia>? GetMediaByTitle(string title);
        public IEnumerable<BaseMedia>? GetMediaByArtist(string artist);
    }
}