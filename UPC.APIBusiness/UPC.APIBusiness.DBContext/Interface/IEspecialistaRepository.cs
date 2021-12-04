using DBEntity;
using System.Collections.Generic;

namespace DBContext
{
    public interface IEspecialistaRepository
    {
        List<EspecialistaExtend> findAll();
        EntityEspecialista save(EntityEspecialista especialidad);
        EntityEspecialista findById(int id);
        Pagination<EntityEspecialista> pagination(string searchText = "_", int page = 1, int numItems = 10);
        void delete(int id);
        Pagination<EspecialistaExtend> filter(Filter filter);
        void saveHorarios(List<EntityHorario> horarios);

        List<EntityHorario> loadHorarios(int id,int id_sucursal,int id_especialidad,int dia);

    }
}
