using MediaPlayer.Core.src.EntityAbstraction;

namespace MediaPlayer.Core.src.Entity
{
    public class Audio : Media, IAudioable
    {
        public Audio(string title, string artist, int year) : base(title, artist, year)
        {
        }

        public override MediaType Type=> MediaType.Audio;
        public string SoundAffect
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public void SetSoundAffect(string soundAffect)
        {
            throw new NotImplementedException();
        }
    }
}
