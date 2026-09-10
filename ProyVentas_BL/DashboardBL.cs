using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Agregar 
using ProyVentas_ADO;
using ProyVentas_BE;

namespace ProyVentas_BL
{
    public class DashboardBL
    {
        DashboardADO objDashboardADO = new DashboardADO();
        public DashboardBE ObtenerTarjetasDashboard()
        {
            return objDashboardADO.ObtenerTarjetasDashboard();
        }
    }
}
