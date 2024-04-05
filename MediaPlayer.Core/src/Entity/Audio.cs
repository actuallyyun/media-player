using MediaPlayer.Core.src.EntityAbstraction;
using MediaPlayer.Core.src.Enums;

namespace MediaPlayer.Core.src.Entity
{
    public class Audio : Media, IAudioable
    {
        public SoundEffectType SoundEffect { get; set; }

        public Audio(string title, string artist, int year)
            : base(title, artist, year) { 
                SoundEffect=SoundEffectType.None; // set default sound effect
            }

        public override MediaType Type => MediaType.Audio;

        public void SetSoundEffect(SoundEffectType soundEffect)
        {
            SoundEffect = soundEffect;
        }
    }
}
