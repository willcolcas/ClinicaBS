using DBEntity;
using System.Collections.Generic;

namespace DBContext
{
    public interface ISucursalRepository
    {
        List<EntitySucursal> findAll();
        EntitySucursal save(EntitySucursal sucursal);
        EntitySucursal findById(int id);
        Pagination<EntitySucursal> pagination(string searchText = "", int page = 1, int numItems = 10);
        void delete(int id);
    }
}
