using System.Collections.Generic;
using DBEntity;

namespace DBContext
{
    public class HorarioLoad
    {
        public List<EntityHorario> horarios { get; set; }
        public List<EntitySucursal> sucursales { get; set; }
        public List<EntityEspecialidad> especialidades { get; set; }
    }
}


