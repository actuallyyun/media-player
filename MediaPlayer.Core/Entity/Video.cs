using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaPlayer.Core.EntityAbstraction;

namespace MediaPlayer.Core.Entity
{
    public class Video : Media, IVideoable
    {
        public Video(string title, string artist, int year) : base(title, artist, year)
        {
        }

        public string Brightness { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void SetBrightness(string brightness)
        {
            throw new NotImplementedException();
        }
    }
}
