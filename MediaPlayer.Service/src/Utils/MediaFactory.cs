
using MediaPlayer.Service.src.DTO;
using MediaPlayer.Core.src.Entity;
using Microsoft.VisualBasic;

namespace MediaPlayer.Service.src.Utils
{
    public class MediaFactory
    {
        public Media? Create(MediaCreateDto mediaCreate)
        {
            // incapsulate all the media creating logic
            Media newMedia = null;
            if (mediaCreate.Type is MediaType.Audio)
            {
                if (mediaCreate.Year >= 1000 && mediaCreate.Year <= DateAndTime.Now.Year)
                {
                    newMedia = new Audio(mediaCreate.Title, mediaCreate.Artist, mediaCreate.Year);
                }
                else
                {
                    Console.WriteLine("Invalid year");
                }
            }
            if (mediaCreate.Type is MediaType.Video)
            {
                if (mediaCreate.Year >= 1800 && mediaCreate.Year <= DateAndTime.Now.Year)
                {
                    newMedia = new Video(mediaCreate.Title, mediaCreate.Artist, mediaCreate.Year);
                }
            }

            return newMedia;
        }
    }
}
