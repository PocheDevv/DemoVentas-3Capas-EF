using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SitioWEB_VentasGUI
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Codifique
            //Protege todas las paginas que usen este master page contra el boton
            //Navegar hacia atras del naveador

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();


            //Elimina la coojie de atenticacion que maneja el archivo  web.config

            FormsAuthentication.SignOut();

            //2 lIMPIA TODAS LAS VARIABLES GUARDADAS EN LA MEMORIA TEMPORAL DEL SERVIDOR
            Session.Clear();

            //3 Destruye de forma definitiva la sesion actual en IIS
            Session.Abandon();

            //4 Limpia la cookies de sesion del navegador de forma explicita
            if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
            {
                HttpCookie cookiesAutenticacion = new HttpCookie(FormsAuthentication.FormsCookieName);
                cookiesAutenticacion.Expires=DateTime.UtcNow.AddDays(-1);
                Response.Cookies.Add(cookiesAutenticacion);
            }



        }
    }
}