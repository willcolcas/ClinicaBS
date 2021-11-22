using DBEntity;
using System.Collections.Generic;

namespace DBContext
{
    public interface IEspecialistaRepository
    {
        List<EspecialistaExtend> findAll();
        EntityEspecialista save(EspecialistaExtend especialidad);
        EspecialistaExtend findById(int id);
        Pagination<EspecialistaExtend> pagination(string searchText = "_", int page = 1, int numItems = 10);
        Pagination<EspecialistaExtend> filter(int id_especialidad,int is_sucursal);
        void delete(int id);
        Pagination<EspecialistaExtend> filter(Filter filter);
    }
}
