using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Agregar ...
using ProyVentas_ADO;
using ProyVentas_BE;
namespace ProyVentas_BL
{
    public class ProveedorBL
    {
        // Creamos una instancia de la clase ProveedorADO para invocar a sus metodos.
        ProveedorADO objProveedorADO = new ProveedorADO();        

        public List<ProveedorBE> ListarProveedorRS()
        {
            return objProveedorADO.ListarProveedorRS();
        }
    }
}
