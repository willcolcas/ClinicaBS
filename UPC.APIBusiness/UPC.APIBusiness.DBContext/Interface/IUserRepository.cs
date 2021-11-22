using DBEntity;
using System.Collections.Generic;

namespace DBContext
{
    public interface IUserRepository
    {
        List<EntityUser> GetUsers();
    }
}
