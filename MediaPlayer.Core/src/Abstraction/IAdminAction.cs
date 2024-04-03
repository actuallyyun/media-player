namespace MediaPlayer.Core.src.EntityAbstraction
{
    public interface IAdminAction
    {
        public void AddUser() ;

        public void RemoveUser();

        public void UpdateUser();

        public void DeleteAllUsers() ;

        public void AddMedia() ;

        public void RemoveMedia() ;

        public void UpdateMedia() ;

        public void DeleteAllMedia() ;
    }
}
