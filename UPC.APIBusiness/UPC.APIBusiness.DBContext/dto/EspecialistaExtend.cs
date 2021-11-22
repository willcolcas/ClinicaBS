using System.Collections.Generic;
using DBEntity;

namespace DBContext
{
    public class EspecialistaExtend : EntityEspecialista
    {
        public string especialidad { get; set; }
        public string sucursal { get; set; }
        public List<EntityHorario> horarios { get; set; }
    }
}


