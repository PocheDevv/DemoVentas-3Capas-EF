using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ProyVentas_ADO;
using ProyVentas_BE;

namespace ProyVentas_BL
{
    public class OrdenBL
    {
        OrdenADO objOrdenADO = new OrdenADO();       
        public string RegistrarOrden(OrdenBE objOrdenBE)
        {
            return objOrdenADO.RegistrarOrden(objOrdenBE);
        }

       public List<OrdenBE> ListarOrdenes()
        {
            return objOrdenADO.ListarOrdenes();
        }


    }
}
