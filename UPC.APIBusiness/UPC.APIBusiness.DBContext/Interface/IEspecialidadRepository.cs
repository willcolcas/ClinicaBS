using DBEntity;
using System.Collections.Generic;

namespace DBContext
{
    public interface IEspecialidadRepository
    {
        List<EntityEspecialidad> findAll();
        EntityEspecialidad save(EntityEspecialidad especialidad);
        EntityEspecialidad findById(int id);
        Pagination<EspecialidadExtend> pagination(string searchText = "", int page = 1, int numItems = 10);
        void delete(int id);
    }
}
