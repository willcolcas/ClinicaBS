namespace DBEntity
{
    public class EntitySucursal : EntityBase
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string direccion { get; set; }
        public int id_departamento { get; set; }
        public int id_provincia { get; set; }
        public int id_distrito { get; set; }

    }
}


