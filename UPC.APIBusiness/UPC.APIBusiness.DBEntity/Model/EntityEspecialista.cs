﻿namespace DBEntity
{
    public class EntityEspecialista : EntityBase
    {
        public int id { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string dni { get; set; }
        public int id_especialidad { get; set; }
        public int id_sucursal { get; set; }
    }
}


