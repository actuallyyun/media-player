using MediaPlayer.Core.src.EntityAbstraction;
using MediaPlayer.Core.src.Enums;
using MediaPlayer.Core.src.Utils;

namespace MediaPlayer.Core.src.Entity
{
    public class Video : Media, IVideoable
    {
        public int Brightness { get; set; }

        public override MediaType Type => MediaType.Video;

        public Video(string title, string artist, int year)
            : base(title, artist, year)
        {
            Brightness = 5; // default brightness.
        }

        public void SetBrightness(int brightness)
        {
            if (!Validator.IsValidBrightness(brightness))
            {
                throw new ArgumentException("Invalid brightness");
            }
            Brightness = brightness;
        }
    }
}
