namespace DBEntity
{
    public class EntityUsuario : EntityBase
    {

        public int id { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string email { get; set; }
        public int id_tipo_documento { get; set; }
        public string nro_documento { get; set; }
        public int id_tipo_usuario { get; set; }
        public string contrasena { get; set; }
    }
}