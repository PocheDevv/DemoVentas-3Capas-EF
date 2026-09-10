using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;
// Agregar...
using ProyVentas_BL;
using ProyVentas_BE;
using System.Data;
// Para la descarga Excel (no olvide instalar EPPlus)
using System.IO;
using OfficeOpenXml;
using System.Runtime.ConstrainedExecution;
namespace SitioWEB_VentasGUI.Consultas
{
    
    public partial class WebFacturacionCliente : System.Web.UI.Page
    {
        // Instancias ...
        ClienteBL objClienteBL = new ClienteBL();
        ClienteBE objClienteBE = new ClienteBE();
        FacturaBL objFacturaBL = new FacturaBL();
       

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsPostBack)
                {
                    // Definimos el contexto de la licencia del ExcelPackage como no comercial.... 
                    ExcelPackage.License.SetNonCommercialOrganization("ISIL");
                }                
               
            }
            catch (Exception ex)
            {
                lblMensajePopup.Text = ex.Message;
                PopMensaje.Show();
            }

        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                // Simulamos demora en la consulta de 2 segundos
                System.Threading.Thread.Sleep(2000);
                // Obtenemos los datos del cliente consultado
                objClienteBE = objClienteBL.ConsultarCliente(txtCod.Text.Trim());

                // Validamos la existencia del cliente
                if (objClienteBE ==  null )
                {
                    LimpiarDatos(this);
                    // Mostramos el mensaje el el lblMensaje del panel y lanzamos el modal de mensajes
                    lblMensajePopup.Text = "Cliente no existe.";
                    PopMensaje.Show();
                    return;
                }
                else
                {
                    txtRazSoc.Text = objClienteBE.Raz_soc_cli;
                    txtDir.Text = objClienteBE.Dir_cli;
                    txtTel.Text = objClienteBE.Tel_cli;
                    txtUbicacion.Text = objClienteBE.Departamento + "-" + objClienteBE.Provincia + "-" + objClienteBE.Distrito;
                    txtRUC.Text = objClienteBE.Ruc_cli;
                    txtEstado.Text = objClienteBE.Estado;
                    txtTipo.Text = objClienteBE.Tipo;

                    // Mostramos la deuda
                    txtDeuda.Text = Convert.ToSingle(objClienteBE.Deuda).ToString("C2");
                    // Cargamos el grvFacturas
                    CargarDatos();
                }
            }
            catch (Exception ex)
            {
                lblMensajePopup.Text = "Error: " + ex.Message;
                PopMensaje.Show();
            }
        }

        protected void CargarDatos()
        {
            DateTime fecInicio = Convert.ToDateTime(txtFecIni.Text);
            DateTime fecFin = Convert.ToDateTime(txtFecFin.Text);

            if (fecInicio > fecFin)
            {
                lblMensajePopup.Text = "La fecha de inicio no puede ser mayor que la de fin";
                PopMensaje.Show();
                return;
            }

            // Si todo está OK... Mostramos las facturas
            List<FacturaBE> objListaFacturaBE = objFacturaBL.ListarFacturasClienteFechas(txtCod.Text.Trim(), fecInicio, fecFin);
            grvFacturas.DataSource = objListaFacturaBE;
            grvFacturas.DataBind();
            lblRegistros.Text = "Facturas: " + objListaFacturaBE.Count.ToString();
        }

        protected void LimpiarDatos(Control _control)
         {
            foreach (Control miControl in _control.Controls)
            {
               // Limpiamos los controles sin son textBox
                if (miControl is TextBox)
                {
                    TextBox miCaja = new TextBox();
                    miCaja = (TextBox)miControl;
                    miCaja.Text = String.Empty;
                }
                // Si el control tiene controles hijos, recórrelos también
                if (miControl.Controls.Count > 0)
                {
                    LimpiarDatos(miControl);
                }                    
            }

            // Limpiamos el GridView y lblRegistros
            grvFacturas.DataSource = null;
            grvFacturas.DataBind();
            lblRegistros.Text = "Facturas : ";
           

        }
        protected void btnDescargar_Click(object sender, EventArgs e)
        {
            try
            {              
                //Validamos que haya facturas en el grid
                if (grvFacturas.Rows.Count == 0)
                {
                    lblMensajePopup.Text = "No hay facturas registradas";
                    PopMensaje.Show();
                    return;
                }
                // Ruta del archivo plantilla del reporte en desarrollo
                String rutaarchivo = Server.MapPath("/") + @"Documentos\ListadofacturasCliente.xlsx";
                // Obtenemos las facturas para su descarga
                List<FacturaBE> objListaFacturas = new List<FacturaBE>();
                objListaFacturas = objFacturaBL.ListarFacturasClienteFechas(txtCod.Text.Trim(),
                    Convert.ToDateTime(txtFecIni.Text.Trim()), Convert.ToDateTime(txtFecFin.Text.Trim()));               
                Int16 intRegistros = Convert.ToInt16 (objListaFacturas.Count );              
               
                // Fila de inicio del reporte
                Int16 fila1 = 5;
                using (var pck = new OfficeOpenXml.ExcelPackage(new FileInfo(rutaarchivo)))
                {
                    //Nombre de archivo para descargar
                    String filename = "ListadoFacturasCliente_" + DateTime.Today.ToShortDateString();
                    ExcelWorksheet ws = pck.Workbook.Worksheets["Hoja1"];
                    // Se imprime la cabecera
                    ws.Cells[2, 4].Value = txtRazSoc.Text;
                    ws.Cells[2, 6].Value = txtFecIni.Text + " y " + txtFecFin.Text;
                    //Llenamos el Excel con las facturas
                    foreach (FacturaBE  miFactura in objListaFacturas )
                    {
                        ws.Cells[fila1, 1].Value = miFactura.Num_fac ;

                        DateTime fecfac = Convert.ToDateTime(miFactura.Fec_fac);
                        ws.Cells[fila1, 2].Value = fecfac.ToShortDateString();                       
                        if (miFactura.Fec_can == null )
                        {                           
                            ws.Cells[fila1, 3].Value = "--";
                        }
                        else
                        {                            
                            ws.Cells[fila1, 3].Value =Convert.ToDateTime (miFactura .Fec_can ).ToShortDateString();
                        }
                        Single total = Convert.ToSingle(miFactura.Total );
                        ws.Cells[fila1, 4].Value =total.ToString("#,###,##0.00");
                       ws.Cells[fila1, 5].Value = miFactura .ApellNom_ven;
                        ws.Cells[fila1, 6].Value = miFactura .Estado;
                        fila1 += 1;
                    }
                    ws.Cells[fila1, 1].Value = "Total registros : " + intRegistros.ToString();
                    //Modificando el ancho de las columnas
                    ws.Column(1).Width = 20;
                    ws.Column(2).Width = 30;
                    ws.Column(3).Width = 30;
                    ws.Column(4).Width = 30;
                    ws.Column(5).Width = 50;
                    ws.Column(6).Width = 25;                    
                    //Escribir de nuevo al cliente y descargar el archivo desde el navegador
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;  filename=" + filename + ".xlsx");
                    using (var memoryStream = new MemoryStream())
                    {
                        pck.SaveAs(memoryStream);
                        memoryStream.WriteTo(Response.OutputStream);
                    }
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                lblMensajePopup.Text = ex.Message;
                PopMensaje.Show();
            }
        }

        protected void grvFacturas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            System.Threading.Thread.Sleep(2000);
            grvFacturas.PageIndex = e.NewPageIndex;
            CargarDatos();
        }
    }
}