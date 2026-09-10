using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
// Agregar
using System.Data;
using ProyVentas_BE;
using ProyVentas_BL;
using System.Threading; // Parala clase Thread
using System.Globalization; // Para la clase CultureInfo

using System.IO;
namespace SitioVentasWEB_GUI.Transacciones
{
    public partial class RegistrarOrden : System.Web.UI.Page
    {
        // Los insumos:
        DataTable mitb;
        ProveedorBL objProveedorBL = new ProveedorBL();
        ProductoBL objProductoBL = new ProductoBL();
        OrdenBL objOrdenBL= new OrdenBL();
        OrdenBE objOrdenBE = new OrdenBE();

        private void CrearTabla()
        {
            // En este DataTable se guardaran en memoria los detalles de la OC , 
            // hasta que se confirme su registro.
             mitb = new DataTable("TBCompras");
            // Agregamos las columnas directamente definiendo su nombre y tipo de dato
            mitb.Columns.Add("Cod_Pro", typeof(string));
            mitb.Columns.Add("Des_Pro", typeof(string));
            mitb.Columns.Add("Can_Ped", typeof(short)); // System.Int16 equivale a short en C#

            // Definimos la Clave Primaria directamente con la columna creada
            mitb.PrimaryKey = new[] { mitb.Columns["Cod_Pro"] };

            // Enlazamos al control GridView y guardamos en Sesión
            grDetalles.DataSource = mitb;
            grDetalles.DataBind();
            Session["Detalles"] = mitb;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsPostBack == false)
                {
                    // Para manejar la fecha en formato dd-mm-yyyy
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-ES");
                    // Pintamos la fecha de hoy por defecto
                    txtFecOco.Text = DateTime.Now.Date.ToShortDateString();
                    txtFecAte.Text = DateTime.Now.Date.ToShortDateString();
                    // Enlazamos el combo de proveedores (solo los proveedores  activos)
                    //Codifique
                    List<ProveedorBE> objListarProveedores = objProveedorBL.ListarProveedorRS(); 
                    ProveedorBE objProveedorBE = new ProveedorBE ();
                    objProveedorBE.Cod_prv = "V000";
                    objProveedorBE.Raz_soc_prv = "--Selecciones--";
                    objListarProveedores.Insert(0, objProveedorBE); 
                    cboProveedor.DataSource = objListarProveedores;
                    cboProveedor.DataValueField = "Cod_prv";
                    cboProveedor.DataTextField = "Raz_soc_prv";
                    cboProveedor.DataBind();
                    //Creamos el datatable en memoria para almacenar los detalles 
                    CrearTabla(); 
                   
                }
            }
            catch (Exception ex)
            {
                lblMensajePopup.Text = ex.Message;
                PopMensaje.Show();
            }

        }

        protected void cboProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                 //Validamos la seleccion correcta del proveedor
                string codProveedor = cboProveedor.SelectedValue.ToString();
                if (codProveedor == "V000")
                {
                    lblMensajePopup.Text = "Seleccione un proveedor correcto.";
                    PopMensaje.Show();
                    return;
                }

                // Si todo es OK, cargamos el combo de productos con
                // los  productos del proveedor seleccionado
                List<ProductoBE > objListaProductos =  objProductoBL.ListarProductosProveedor(codProveedor);
                ProductoBE objProductoBE = new ProductoBE();
                objProductoBE.Cod_pro = "P000";
                objProductoBE .Des_pro = "--Seleccione--";
                objListaProductos.Insert(0, objProductoBE);
                cboProducto.DataSource = objListaProductos;
                cboProducto.DataValueField = "Cod_pro";
                cboProducto.DataTextField = "Des_pro";
                cboProducto.DataBind();
               
            }
            catch (Exception ex)
            {
                lblMensajePopup.Text = ex.Message;
                PopMensaje.Show();
            }

        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                //Validamos que el proveedor sea correcto
                if (cboProveedor.SelectedValue == "V000")
                {
                    lblMensajePopup.Text = "Por favor, debe seleccionar un proveedor.";
                    PopMensaje.Show();
                    return;
                }
                // Si todo OK, mostramos el popup  de detalle para seleccionar el producto 
                // a solicitar en la OC
                cboProducto.SelectedValue ="P000"; //Arranca el cboProductos con --Seleccione--
                txtCanPed.Text = String.Empty;
                lblMensajeDetalle.Text = String.Empty;
                PopDetalle.Show();

            }
            catch (Exception ex)
            {
                lblMensajePopup.Text = ex.Message;
                PopMensaje.Show();
            }
          
        }

        protected void btnGrabarDetalle_Click(object sender, EventArgs e)
        {
            try
            {
                // Validamos el detalle antes de registrarlo en memoria
                if (cboProducto.SelectedValue == "P000")
                {
                    lblMensajeDetalle.Text = "Por favor debe seleccionar un producto";
                    PopDetalle.Show();
                    return;                                     
                }
                
                if (txtCanPed.Text.Trim() == String.Empty || txtCanPed.Text.Trim()=="0" ) 
                {
                    lblMensajeDetalle.Text = "Por favor debe ingresar una cantidad a pedir.";
                    PopDetalle.Show();
                    return;
                }
                // Si todo esta OK casteamos la variable de sesion "Detalles" a DataTable
                mitb = (DataTable)Session["Detalles"];
                //Codifique..
                DataRow dr; 
                dr= mitb.NewRow(); 
                //Llenamos los campos con lo ingresado en el Popup de detalle
                dr["Cod_Pro"] = cboProducto.SelectedValue.ToString();
                dr["Des_Pro"] = cboProducto.SelectedItem.ToString();
                dr["Can_Ped"] = Convert.ToInt16(txtCanPed.Text);
                //Agregamos la fila a la coleccion de filas de detalle 
                mitb.Rows.Add(dr); 
                //Lo mostramos en la grilla
                grDetalles.DataSource = mitb; 
                grDetalles.DataBind(); 

                //Se inactico el cboProveedores para no cambiar de provedor a mitad de la transaccion 
                cboProveedor.Enabled = false; 

            }
            catch (Exception ex)
            {
                lblMensajeDetalle.Text = "Error: " + ex.Message;
                PopDetalle.Show();
            }

        }
        protected void grDetalles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                // SE ELIMINA DE MEMORIA (mitb) EL DETALLE SELECCIONADO
                //==================================================
                // Obtenemos el indice de la fila seleccionada
                int indicefila = Convert.ToInt16(e.CommandArgument);

                // Si se pulso en el boton eliminar , eliminamos el detalle de memoria
                if (e.CommandName == "Eliminar")
                {
                    mitb = (DataTable)Session["Detalles"];
                    mitb.Rows.RemoveAt(indicefila);
                    grDetalles.DataSource = mitb;
                    grDetalles.DataBind();
                    Session["Detalles"] = mitb;
                }
            }
            catch (Exception ex)
            {
                lblMensajePopup.Text = ex.Message;
                PopMensaje.Show();
            }
        }

        protected void btnVerGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                //Obtenemos los detalles registrados
                mitb = (DataTable)Session["Detalles"];

                // Validamos las fechas...
                if (txtFecOco.Text.Trim() == String.Empty)
                {
                    lblMensajePopup.Text = "Debe ingresar fecha de OC.";
                    PopMensaje.Show();
                    return;
                }

                if (txtFecAte.Text.Trim() == String.Empty)
                {
                    lblMensajePopup.Text = "Debe ingresar fecha de Atencion.";
                    PopMensaje.Show();
                    return;

                }

                if (Convert.ToDateTime(txtFecOco.Text) > Convert.ToDateTime(txtFecAte.Text))
                {
                    lblMensajePopup.Text = "La fecha de orden no puede ser mayor que la de atencion.";
                    PopMensaje.Show();
                    return;
                }
                //Si no hay detalles, no se puede registrar la  orden
                if (mitb.Rows.Count == 0)
                {
                    lblMensajePopup.Text = "No puede registrar una orden sin detalles.";
                    PopMensaje.Show();
                    return;
                }

                // Si todo esta OK....iniciamos el registro de la OC                           
                // Empezamos asignando los valores para la cabecera de la OC
                //Codifique
                //Asignamos os valores para la cabecera OC
                objOrdenBE.Fec_oco = Convert.ToDateTime(txtFecOco.Text.Trim());
                objOrdenBE.Fec_ate = Convert.ToDateTime(txtFecAte.Text.Trim());
                objOrdenBE.Est_oco = "1"; //Pendiente
                objOrdenBE.Cod_prv = cboProveedor.SelectedValue.ToString();
                objOrdenBE.Usu_Registro = Session["Usuario"].ToString();

                //Asiganmos los detalles .. 
                List<DetalleOCBE> objListaDetallesOCBE = new List<DetalleOCBE>();

                //Recorremos todas las filas del DataTable y definimos la lista de detalles 
                DataRow drDetalle;
                mitb = (DataTable)Session["Detalles"];
                foreach (DataRow dr in mitb.Rows)
                {
                    DetalleOCBE objDetalleBE = new DetalleOCBE();
                    objDetalleBE.Num_oco = String.Empty; //Aun no se sabe elnumero de OC
                    objDetalleBE.Cod_pro = dr["Cod_pro"].ToString();
                    objDetalleBE.Can_ped = Convert.ToInt32(dr["Can_ped"]);
                    objListaDetallesOCBE.Add(objDetalleBE); 
                }

                //Asignamos la lista a la entidad de negocios 
                objOrdenBE.DetallesOC = objListaDetallesOCBE;

                //Con la entidad de negocios completa ehecutamos 
                String strNumOC = objOrdenBL.RegistrarOrden(objOrdenBE);
                if (strNumOC == String.Empty)
                {
                    //Si el metodo retorna un numero de OC vacio quiere decir que 
                    //no se registro OC
                    lblMensajePopup.Text = "No se registro la orden de compra contante con IT";
                    PopMensaje.Show();
                    return;
                }
                else
                {
                    //Si llego a registrar la OC mostramos el mensaje con el numero de la nueva OC
                    //devuelto por el metodo RegistrarOrden
                    lblMensajePopup.Text = "Se registro la orden Nro:" + strNumOC + "exitosamente";
                    PopMensaje.Show();
                    Reiniciar(); 
                }
            }
            catch (Exception ex)
            {
                lblMensajePopup.Text = ex.Message;
                PopMensaje.Show();
            }
            
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Reiniciar();
        }

        protected void Reiniciar()
        {
            // Reiniciamos todos los controles  por si se desea registrar una nueva OC...
            txtFecOco.Text = DateTime.Now.Date.ToShortDateString();
            txtFecAte.Text = DateTime.Now.Date.ToShortDateString();
            cboProveedor.Enabled = true;
            cboProveedor.SelectedIndex = 0;
            CrearTabla();
        }

        protected void grDetalles_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
