using System.Collections.Generic;
using DBEntity;

namespace DBContext
{
    public class UsuarioExtend : EntityUsuario
    {
        public string tipo_documento { get; set; }
        public string tipo_usuario { get; set; }
    }
}


