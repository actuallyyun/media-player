namespace MediaPlayer.Core.src.Entity
{
    public class BaseEntity
    {
        public Guid Id{get;}
        public BaseEntity(){
            Id=Guid.NewGuid();
        }
    }
}