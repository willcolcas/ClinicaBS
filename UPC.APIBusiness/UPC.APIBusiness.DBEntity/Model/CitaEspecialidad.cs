namespace DBEntity
{
    public class EntityCita : EntityBase
    {
        public int id { get; set; }
        public int id_usuario { get; set; }
        public int id_sucursal { get; set; }
        public int id_especialidad { get; set; }
        public int id_especialista { get; set; }
        public int id_horario { get; set; }
        public string fecha { get; set; }
        public string condicion { get; set; }

    }
}


