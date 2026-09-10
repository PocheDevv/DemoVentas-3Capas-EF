using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Agregar
using ProyVentas_BE;
namespace ProyVentas_ADO
{
    public class DashboardADO
    {
     
        public DashboardBE ObtenerTarjetasDashboard ()
        {
            try
            {
                // declaramos la instancia a retornar
                DashboardBE objDashboardBE = new DashboardBE();
                // Declaramos una instancia del modelo
                using (VentasLeonEntities MisVentas = new VentasLeonEntities())
                {
                    // Con query sintax LINQ
                    objDashboardBE.TotalVentas = Convert.ToDecimal(
                       (from fact in MisVentas.Tb_Factura
                        join detalles in MisVentas.Tb_Detalle_Factura on fact.Num_fac equals detalles.Num_fac
                        where fact.Est_fac == 2
                        select detalles.Can_ven * detalles.Pre_ven).Sum()
                      );
                    //Codifique...
                    objDashboardBE.TotalDeuda = Convert.ToDecimal(
                       (from fact in MisVentas.Tb_Factura
                        join detalles in MisVentas.Tb_Detalle_Factura on fact.Num_fac equals detalles.Num_fac
                        where fact.Est_fac == 1
                        select detalles.Can_ven * detalles.Pre_ven).Sum()
                      );

                    objDashboardBE.CantClientesActivos = Convert.ToInt32
                        (MisVentas.Tb_Cliente.Where(miCliente => miCliente.Est_cli == 1).Count());
                }                   
                // Retornamos los datos del Dashboard
                return objDashboardBE; 
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
    }
}
