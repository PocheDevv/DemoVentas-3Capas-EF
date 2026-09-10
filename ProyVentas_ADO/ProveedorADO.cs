using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Agregar
using ProyVentas_BE;
using System.Data.Entity.Core;

namespace ProyVentas_ADO
{
    public class ProveedorADO
    {
       
        // Lista  de proveedores para el registro de OC
        public List<ProveedorBE> ListarProveedorRS()
        {
            try
            {
                // Lista a retornar
                List<ProveedorBE> objListaProveedores = new List<ProveedorBE>();
                // Instancia del modelo
                using (VentasLeonEntities MisVentas = new VentasLeonEntities())
                {
                    // Obtenemos una lista de clientes con LINQ, solo seleccionando sus codigos y razon social.
                   //Codifique
                   var query = MisVentas.Tb_Proveedor
                        .OrderBy(miProv => miProv.Raz_soc_prv)
                        .Where(miProv => miProv.Est_prv ==1)
                        .Select(miProv => new {miProv.Cod_prv, miProv.Raz_soc_prv})
                        .ToList ();

                    //Recorremos el resultado 
                    foreach (var resultado in query)
                    {
                        ProveedorBE objProveedorBE = new ProveedorBE(); 
                        //Solo se necesita el codigo del proveedor asi como su razon social
                        //para el llenado del combo en la web form de registro OC
                        objProveedorBE.Cod_prv = resultado.Cod_prv; 
                        objProveedorBE.Raz_soc_prv = resultado.Raz_soc_prv; 
                        objListaProveedores.Add (objProveedorBE); 
                    }
                }
                // Retornamos la lista de proveedores solo con codigo y razon social
                return objListaProveedores; 
            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
