using MediaPlayer.Core.src.Entity;
using MediaPlayer.Service.src.DTO;
using Microsoft.VisualBasic;

namespace MediaPlayer.Service.src.Utils
{
    public class MediaFactory
    {
        public Media? Create(MediaCreateDto mediaCreate)
        {
            // incapsulate all the media creating logic
            if (mediaCreate.Year < 1000 || mediaCreate.Year > DateAndTime.Now.Year)
            {
                throw new ArgumentException("Invalid year");
            }

            Media newMedia = null;
            
            if (mediaCreate.Type is MediaType.Audio)
            {
                newMedia = new Audio(mediaCreate.Title, mediaCreate.Artist, mediaCreate.Year);
            }
            else if (mediaCreate.Type is MediaType.Video)
            {
                newMedia = new Video(mediaCreate.Title, mediaCreate.Artist, mediaCreate.Year);
            }
            else
            {
                throw new ArgumentException("Invalid media type.");
            }
            return newMedia;
        }
    }
}
