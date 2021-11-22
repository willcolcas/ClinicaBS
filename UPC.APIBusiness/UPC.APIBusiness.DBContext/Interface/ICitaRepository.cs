using DBEntity;
using System.Collections.Generic;

namespace DBContext
{
    public interface ICitaRepository
    {
        List<EntityCita> findAll();
        EntityCita save(EntityCita entityCita);
        EntityCita findById(int id);
        Pagination<CitaExtend> paginationByIdUsuario(int id_ususario, string searchText = "", int page = 1, int numItems = 10);
        void delete(int id);

    }
}
