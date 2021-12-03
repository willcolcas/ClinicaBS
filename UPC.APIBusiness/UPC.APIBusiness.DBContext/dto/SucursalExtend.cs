
using DBEntity;

namespace DBContext
{
    public class SucursalExtend : EntitySucursal
    {
        public string departamento { get; set; }
        public string provincia { get; set; }
        public string distrito { get; set; }
    }
}


