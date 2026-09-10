using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyVentas_ADO;
using ProyVentas_BE;

namespace ProyVentas_BL
{
    public class ClienteBL
    {
        ClienteADO objClienteADO = new ClienteADO();

        public ClienteBE ConsultarCliente(string strCodigo) 
        {
            return objClienteADO.ConsultarCliente(strCodigo);
        }
    }
}
