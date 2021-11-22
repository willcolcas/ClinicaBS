using System;

namespace DBEntity
{
    public class EntityHorario : EntityBase
    {

        public int id { get; set; }
        public int id_especialista { get; set; }
        public string inicio { get; set; }
        public string fin { get; set; }

    }
}


