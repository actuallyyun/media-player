
using MediaPlayer.Core.src.Entity;
using MediaPlayer.Core.src.Enums;

namespace MediaPlayer.Core.src.ValueObject
{
    public class CurrentPlayingMedia
    {
        public Media CurrentMedia{get;set;}
        public bool IsPlaying {get;set;}
        public bool IsPaused{get;set;}
        public int Volumn{get;set;}
        public int Brightness{get;set;}
        public SoundEffectType SoundEffect{get;set;}
        public void IncreaseVolumn(){
            Volumn++;
        }
        public void DecreaseVolumn(){
            Volumn--;
        }
        

    }
}