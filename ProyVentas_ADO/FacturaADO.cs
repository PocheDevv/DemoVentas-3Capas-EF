using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Agregar...
using ProyVentas_BE;
using System.Data.Entity.Core; // para la clase EntityException
using System.Data.Entity.Core.Objects; // para la clase ObjectParameter


namespace ProyVentas_ADO
{
    public  class FacturaADO
    {
        // Metodo para Graficacion
        public List<FacturacionAnual> ListarFacturacionAnual()
        {
            try
            {
                // Creamos una lista de facturas..
                List<FacturacionAnual> objListaFacturacionAnual = new List<FacturacionAnual>();
                // Creamos la instancia del modelo 
                using (VentasLeonEntities MisVentas = new VentasLeonEntities())
                {
                    // Hacemos la consulta con SP usp_VentasPorAño (no olvide agregarlo al modelo EF)

                    var query = MisVentas.usp_VentasPorAño();

                    // Recorremos el resultado ...
                    foreach (var resultado in query)
                    {
                        FacturacionAnual objFacturacionAnual = new FacturacionAnual();
                        objFacturacionAnual.Año = Convert.ToInt32(resultado.Año);
                        objFacturacionAnual.CantFacturas = Convert.ToInt32(resultado.CantFacturas);
                        objFacturacionAnual.TotalAnual = Convert.ToInt32(resultado.TotalAnual);

                        // Agregamos la instancia a la lista...
                        objListaFacturacionAnual.Add(objFacturacionAnual);
                    }

                // retornamos el listado de facturacion anual             
                return objListaFacturacionAnual;
            }}
            catch (EntityException ex)
            {
                throw new Exception("Error en el listado:" + ex.Message);
            }
        }

        public List<FacturaBE> ListarFacturasClienteFechas(String strCodigo, DateTime fecini, DateTime fecfin)
        {
            try
            {
                // Creamos una lista de facturas..
                List<FacturaBE> objListaFacturas = new List<FacturaBE>();
                // Creamos la instancia del modelo 
                using (VentasLeonEntities MisVentas = new VentasLeonEntities())
                {
                    // Hacemos la consulta con LINQ
                    var query = (from miFactura in MisVentas.Tb_Factura
                                 where miFactura.Cod_cli == strCodigo && miFactura.Fec_fac >= fecini && miFactura.Fec_fac <= fecfin
                                 select miFactura
                                    );
                    // Recorremos el resultado ...
                    foreach (var resultado in query)
                    {
                        FacturaBE objFacturaBE = new FacturaBE();
                        objFacturaBE.Num_fac = resultado.Num_fac;
                        objFacturaBE.Fec_fac = resultado.Fec_fac;
                        objFacturaBE.Fec_can = resultado.Fec_can;
                        objFacturaBE.Est_fac = Convert.ToInt32(resultado.Est_fac); ;
                        if (objFacturaBE.Est_fac == 1)
                        {
                            objFacturaBE.Estado = "Pendiente";
                        }
                        else if (objFacturaBE.Est_fac == 2)
                        {
                            objFacturaBE.Estado = "Cancelada";
                        }
                        else
                        {
                            objFacturaBE.Estado = "Anulada";
                        }
                        objFacturaBE.Cod_ven = resultado.Cod_ven;
                        objFacturaBE.ApellNom_ven = resultado.Tb_Vendedor.Ape_ven + " , " + resultado.Tb_Vendedor.Nom_ven;
                        objFacturaBE.Total = Convert.ToDecimal(
                            (
                                from miDetalle in MisVentas.Tb_Detalle_Factura
                                where miDetalle.Num_fac == resultado.Num_fac
                                select miDetalle.Can_ven * miDetalle.Pre_ven).Sum()
                            );

                        // Agregamos la instancia a la lista
                        objListaFacturas.Add(objFacturaBE);
                    }
                }
                // retornamos el listado de facturas               
                 return objListaFacturas;
            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<FacturaBE> ListarFacturasVendedorFechas(String strCodigo, DateTime fecini, DateTime fecfin)
        {
            try
            {
                // Creamos una lista de facturas..
                List<FacturaBE> objListaFacturas = new List<FacturaBE>();
                // Creamos la instancia del modelo 
                using (VentasLeonEntities MisVentas = new VentasLeonEntities())
                {
                    // Hacemos la consulta con SP usp_ListarFacturasVendedorFechas el cual debe agregar al modelo EF
                    // Codifique
               }
                //retornamos la lista de facturas               

                return objListaFacturas;
            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
