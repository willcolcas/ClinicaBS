using System;

namespace DBEntity
{
    public class EntityHorario : EntityBase
    {

        public int id { get; set; }
        public int id_especialista { get; set; }
        public int id_sucursal { get; set; }
        public int id_especialidad { get; set; }
        public string tipo { get; set; }
        public int dia { get; set; }

    }
}


