using System.Collections.Generic;
using DBEntity;

namespace DBContext
{
    public class EspecialistaExtend : EntityEspecialista
    {
        public List<EntityHorario> horarios { get; set; }
    }
}


