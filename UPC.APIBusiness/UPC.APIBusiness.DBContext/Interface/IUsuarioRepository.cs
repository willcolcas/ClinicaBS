using DBEntity;
using System.Collections.Generic;

namespace DBContext
{
    public interface IUsuarioRepository
    {
        List<EntityUsuario> findAll();
        EntityUsuario save(EntityUsuario ussuario);
        EntityUsuario findById(int id);
        Pagination<UsuarioExtend> pagination(string searchText = "", int page = 1, int numItems = 10);
        void delete(int id);
        EntityUsuario login(Login login);
    }
}
