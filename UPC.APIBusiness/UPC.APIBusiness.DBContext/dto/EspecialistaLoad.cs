using System.Collections.Generic;
using DBEntity;

namespace DBContext
{
    public class EspecialistaLoad
    {
        public EspecialistaExtend especialista { get; set; }
        public List<EntityEspecialidad> especialidades { get; set; }
        public List<EntitySucursal> sucursales { get; set; }

    }
}


