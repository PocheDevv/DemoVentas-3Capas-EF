
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
// Agregar
using ProyVentas_BE;
using ProyVentas_BL;
namespace SitioWEB_VentasGUI
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Como todo el codigo se hara en el evento Load del formulario
            // declaramos aqui mismo las instancias...
            try
            {
                if (Page.IsPostBack == false)
                {
                    // Tarjetas del dashboard
                    DashboardBL objDashboardBL = new DashboardBL();
                    DashboardBE objDashboardBE = objDashboardBL.ObtenerTarjetasDashboard();
                    litClientesActivos.Text = objDashboardBE .CantClientesActivos .ToString ();
                    litTotalVentas.Text = objDashboardBE.TotalVentas.ToString("C2");
                    litDeudaActual.Text = objDashboardBE.TotalDeuda.ToString("C2");

                    //Graficos del dashboard
                    //Codifique
                    //Graficos del dashboard
                    FacturaBL objFacturaBL = new FacturaBL();
                    List<FacturacionAnual> objListaFacturacionAnual = objFacturaBL.ListarFacturacionAnual();
                    //grafTotal: Total facturado por año
                    grafTotales.Series.Add("Totales");
                    grafTotales.Series["Totales"].Points.DataBindXY(objListaFacturacionAnual, "Año", objListaFacturacionAnual, "TotalAnual");
                    grafTotales.Series["Totales"].IsValueShownAsLabel = true;
                    grafTotales.Series["Totales"].LabelFormat = "c";// c formato monetario

                    //grafCantidad Cantidad
                    grafCantidad.Series.Add("Cantidades");
                    grafCantidad.Series["Cantidades"].Points.DataBindXY(objListaFacturacionAnual, "Año", objListaFacturacionAnual, "CantFacturas");
                    grafCantidad.Series["Cantidades"].IsValueShownAsLabel = true;
                    grafCantidad.Series["Cantidades"].LabelFormat = "n";

                }
            }
            catch (Exception ex)
            {
                litTotalVentas .Text = ex.Message;
            }
        }
    }
}