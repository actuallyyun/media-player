using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaPlayer.Core.src.Abstraction;

namespace MediaPlayer.Core.src.Entity
{
    public class AdminNotification : INotify
    {
        public void Update(string message)
        {
            Console.WriteLine($"Admin notification:{message}");
        }
    }
}