using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Agregamos
using ProyVentas_ADO;
using ProyVentas_BE;

namespace ProyVentas_BL
{
    public class UsuarioBL
    {
        UsuarioADO objUsuarioADO = new UsuarioADO();

        public UsuarioBE ConsultarUsuario(string strLogin, string strPassword)
        {
            return objUsuarioADO.ConsultarUsuario(strLogin, strPassword);
        }
    }
}
