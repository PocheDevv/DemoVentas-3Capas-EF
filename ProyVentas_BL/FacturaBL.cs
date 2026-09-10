using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyVentas_ADO;
using ProyVentas_BE;

namespace ProyVentas_BL
{
    public class FacturaBL
    {
        FacturaADO objFacturaADO = new FacturaADO();

        public List<FacturaBE> ListarFacturasClienteFechas(string strCodigo, DateTime fecini, DateTime fecfin)
        {
            return objFacturaADO.ListarFacturasClienteFechas(strCodigo, fecini, fecfin);
        }

        public List<FacturacionAnual> ListarFacturacionAnual()
        {
            return objFacturaADO.ListarFacturacionAnual();
        }
    }
}
