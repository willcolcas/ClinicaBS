using System.Collections.Generic;
using DBEntity;

namespace DBContext
{
    public class CitaExtend : EntityCita
    {
        public string sucursal { get; set; }
        public string especialidad { get; set; }
        public string especialista { get; set; }
        public string horario { get; set; }
    }
}


