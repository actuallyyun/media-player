using Microsoft.VisualBasic;

namespace MediaPlayer.Core.src.Utils
{
    public  class Validator
    {
        public Validator(){

        }

        public static bool IsValidYear(int? year){
            if(year<1000 || year>DateAndTime.Now.Year){
                return false;
            }
            return true;
        }
    }
}