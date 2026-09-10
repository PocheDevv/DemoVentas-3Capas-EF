using ProyVentas_BE;
using ProyVentas_BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SitioWEB_VentasGUI
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Protege todas las paginas que usen este master page contra el boton
            //Navegar hacia atras del naveador

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {

            try
            {
                String LoginUsuario = txtUsername.Text;
                string PasswordUsuario = txtPassword.Text;
                UsuarioBL objUsuarioBL = new UsuarioBL();  
                UsuarioBE objUsuarioBE = objUsuarioBL.ConsultarUsuario(LoginUsuario , PasswordUsuario);    


                // Validación con acceso a BD
                if (objUsuarioBE.Login_Usuario  != null)
                {
                    //Crea la cookie de autneticacion y lo redirige a la pagina que intentaba ver
                    FormsAuthentication.RedirectFromLoginPage(txtUsername.Text, false);

                    Session["Usuario"] = objUsuarioBE.Login_Usuario;
                    Session["Nivel"] = objUsuarioBE.Niv_Usuario;
                    Response.Redirect("Default.aspx");
                }
                else
                {
                    throw new Exception("Usuario o contraseña incorrectos");
                }

            }
            catch (Exception ex)
            {

                ErrorMessage .Text = "<div class='alert alert-danger' role='alert'>" + ex.Message + "</div>";
            }          
            
        }
    }
}