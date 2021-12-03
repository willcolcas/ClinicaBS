
using System.Collections.Generic;
using DBEntity;

namespace DBContext
{
    public class SucursalLoad
    {
        public EntitySucursal sucursal { get; set; }
        public Pagination<SucursalExtend> pagination { get; set; }
        public List<EntityDepartamento> departamentos { get; set; }
        public List<EntityProvincia> provincias { get; set; }
        public List<EntityDistrito> distritos { get; set; }
    }
}


