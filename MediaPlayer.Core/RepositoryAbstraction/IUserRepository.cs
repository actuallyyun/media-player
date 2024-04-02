using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaPlayer.Core.Entity;

namespace MediaPlayer.Core.RepositoryAbstraction
{
    public interface IUserRepository
    {
        public IEnumerable<BaseUser> GetAllUsers();
        public void AddNewUser(BaseUser user);
        public BaseUser? GetUserByUsername(string username);
    }
}