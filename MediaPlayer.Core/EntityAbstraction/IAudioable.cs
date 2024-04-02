using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaPlayer.Core.EntityAbstraction
{
    public interface IAudioable
    {
        public string SoundAffect{get;set;}
        public void SetSoundAffect(string soundAffect);

    }
}