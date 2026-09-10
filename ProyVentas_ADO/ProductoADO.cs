using ProyVentas_BE;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyVentas_ADO
{
    public  class ProductoADO
    {
        public List<ProductoBE> ListarProductosProveedor(String strCodigo)
        {
            try
            {
                // Creamos una lista de productos que abastece un proveedor.
                List<ProductoBE> objListaProductos = new List<ProductoBE>();
                // Creamos la instancia del modelo 
                using (VentasLeonEntities MisVentas = new VentasLeonEntities())
                {
                    // Codifique
                    //Hacemos la consulta llamando al SP ups_ListarProductoProvedor
                    //(no olvidemos agregar el SP AL MODELO y luego copilar)
                    var query = MisVentas.usp_ListarProductosProveedor(strCodigo);
                    //
                    foreach (var resultado in query) { 
                    ProductoBE objProductoBE = new ProductoBE();
                        objProductoBE.Cod_pro = resultado.cod_pro; 
                        objProductoBE.Des_pro = resultado.des_pro;
                        //Agregamos la instancia de producto a la lista de productos... 
                        objListaProductos.Add(objProductoBE); 
                    }
                }
                // retornamos el listado de facturas               
                return objListaProductos;
            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
