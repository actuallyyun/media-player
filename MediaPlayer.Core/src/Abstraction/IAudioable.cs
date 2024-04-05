using MediaPlayer.Core.src.Enums;

namespace MediaPlayer.Core.src.EntityAbstraction
{
    public interface IAudioable
    {
        public SoundEffectType SoundEffect{get;set;}
        public void SetSoundEffect(SoundEffectType soundEffect);

    }
}