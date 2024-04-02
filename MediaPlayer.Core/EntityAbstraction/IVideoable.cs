using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaPlayer.Core.EntityAbstraction
{
    public interface IVideoable
    {
        public string Brightness{get;set;}
        public void SetBrightness(string brightness);
    }
}