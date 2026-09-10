using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DemoWEB_AJAX
{
    public partial class MasterModelo : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Protege todas las paginas que usen este master page contra el boton
            //Navegar hacia atras del naveador

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();


            lblUsuario.Text= "Usuario :" + Session["Usuario"].ToString ();    
        }
    }
}