using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Agregar...
using ProyVentas_ADO;
using ProyVentas_BE;

namespace ProyVentas_BL
{
    public  class ProductoBL
    {
        ProductoADO objProductoADO = new ProductoADO();
        public List<ProductoBE> ListarProductosProveedor(String strCodigo)
        {
            return objProductoADO.ListarProductosProveedor(strCodigo);
        }
        
    }
}
