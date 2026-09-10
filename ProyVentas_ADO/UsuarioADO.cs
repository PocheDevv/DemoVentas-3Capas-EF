using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Agregar 
using ProyVentas_BE;

namespace ProyVentas_ADO
{
    public  class UsuarioADO
    {
        public UsuarioBE ConsultarUsuario(String strLogin, String strPassword)
        {
            try
            {
                // Creamos una instancia del usuario para retornar el resultado
                UsuarioBE objUsuarioBE = new UsuarioBE();
                // Instancia del modelo
                using (VentasLeonEntities MisVentas = new VentasLeonEntities())
                {
                    // Obtenemos con LINQ la instancia del usuario a consultar
                    // Usemos el metodo FirstOrDeefault de LINQ  si se sabe que solo sera UNA INSTANCIA a devolver como  resultado 
                    Tb_Usuario objUsuario = MisVentas.Tb_Usuario
                                                                .FirstOrDefault(miUsuario => miUsuario.Login_Usuario == strLogin && miUsuario.Pass_Usuario == strPassword
                                                                && miUsuario.Est_Usuario == 1);

                    if (objUsuario == null)
                    {
                        // Sino existe el usuario devolvemos una entidad de negocios Nula
                        objUsuarioBE = null;
                    }
                    else
                    {
                        // Si el usuario existe solo nos interesara su login y su nivel
                        objUsuarioBE.Login_Usuario = objUsuario.Login_Usuario;
                        objUsuarioBE.Niv_Usuario  = Convert.ToInt32(objUsuario.Niv_Usuario);
                    }

                }

                //retornamos la entidad de negocios
                return objUsuarioBE;
               

            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
