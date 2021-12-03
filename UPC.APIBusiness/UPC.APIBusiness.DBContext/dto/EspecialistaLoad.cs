using System.Collections.Generic;
using DBEntity;

namespace DBContext
{
    public class EspecialistaLoad
    {
        public EntityEspecialista especialista { get; set; }
        public Pagination<EntityEspecialista> pagination { get; set; }
    }
}


