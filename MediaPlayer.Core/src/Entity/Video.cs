using MediaPlayer.Core.src.EntityAbstraction;

namespace MediaPlayer.Core.src.Entity
{
    public class Video : Media, IVideoable
    {
        public Video(string title, string artist, int year) : base(title, artist, year)
        {
        }

        public override MediaType Type=>MediaType.Video;
        public string Brightness
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public void SetBrightness(string brightness)
        {
            throw new NotImplementedException();
        }
    }
}
