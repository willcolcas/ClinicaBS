using System.Collections.Generic;
using DBEntity;

namespace DBContext
{
    public class EspecialidadLoad
    {
        public EntityEspecialidad especialidad { get; set; }
        public Pagination<EspecialidadExtend> pagination { get; set; }
        public List<EntitySucursal> sucursales { get; set; }
    }
}


