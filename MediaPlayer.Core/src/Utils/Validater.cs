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

        public static bool IsValidBrightness(int? brightness){
            if(brightness is null || brightness is not int){
                return false;
            }
            if(brightness<1 || brightness>10){
                return false;
            }
            return true;
        }
    }
}