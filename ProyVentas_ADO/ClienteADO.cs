using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Agregar...
using System.Data.Entity.Core.Objects; // Para la clase ObjectParameter
using ProyVentas_BE;

namespace ProyVentas_ADO
{
    public class ClienteADO
    {
        
        public Decimal CalcularDeudaCliente(String strCodigo)
        {        
           
            try
            {
                //declaramos la variable con la deuda a retornar
                Decimal Deuda;

                // Declaramos una instancia del modelo
                using (VentasLeonEntities MisVentas = new VentasLeonEntities())
                {
                    // Forma 1: (Method syntax empleando expesiones Lambda)
                    //Deuda = Convert.ToDecimal(
                    //         MisVentas.Tb_Factura
                    //        .Join(MisVentas.Tb_Detalle_Factura,
                    //                 fact => fact.Num_fac, // Clave para la tabla de Facturas
                    //                 detalles => detalles.Num_fac, // Clave para la tabla de Detalles de Factura
                    //                 (fact, detalles) => new { Factura = fact, Detalle = detalles }) // Resultado intermedio para unir
                    //         .Where(joined => joined.Factura.Cod_cli == strCodigo && joined.Factura.Est_fac == 1)
                    //         // Casteamos a (float?) para que Sum() pueda devolver null
                    //         .Select(joined => (float?)(joined.Detalle.Can_ven * joined.Detalle.Pre_ven))
                    //         .Sum() ?? 0.0f // Si Sum() devuelve null, usa 0.0f
                    //        );

                    // Forma 2: (Query syntax)
                    //Deuda = Convert.ToDecimal(
                    //(from fact in MisVentas.Tb_Factura
                    // join detalles in MisVentas.Tb_Detalle_Factura on fact.Num_fac equals detalles.Num_fac
                    // where fact.Cod_cli == strCodigo && fact.Est_fac == 1
                    // select (float?)(detalles.Can_ven * detalles.Pre_ven)) // Casteamos a (float?) para que Sum() pueda devolver null
                    // .Sum() ?? 0.0f  // Si Sum() devuelve null, usa 0.0f
                    //);

                    // Forma 3: Empleando el SP usp_DeudaCliente , que debe agregarlo al modelo EF
                    // Este SP maneja un parametro de entrada que es el codigo del cliente cuya deuda desea obtener
                    // y el de salida que es la deuda obtenida por el SP llamado vdeuda

                    ObjectParameter paramdeuda = new ObjectParameter("vdeuda", typeof(Single)); // Parámetro de salida
                    // Se invoca al SP como si fuera un método, con los parámetros definidos.
                    MisVentas.usp_DeudaCliente(strCodigo, paramdeuda);
                    // Obtenemos la deuda y la asignación a la variable
                    Deuda = Convert.ToDecimal(paramdeuda.Value);
                }
                // retornamos la deuda
                return Deuda;
            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public ClienteBE ConsultarCliente(String strCodigo)
        {
          
            try
            {
                // Creamos una instancia del cliente para retornar el resultado
                ClienteBE objClienteBE = new ClienteBE();
                // Instancia del modelo
                using (VentasLeonEntities MisVentas = new VentasLeonEntities())
                {
                    // Obtenemos con LINQ la instancia del cliente a consultar
                    // Usemos el metodo FirstOrDeefault de LINQ  si se sabe que solo sera UNA INSTANCIA a devolver como  resultado 

                    Tb_Cliente objCliente = MisVentas.Tb_Cliente
                        .FirstOrDefault(miCLiente => miCLiente.Cod_cli == strCodigo);
                    if (objCliente == null)
                    {
                        // Sino existe el cliente devolvemos una entidad de negocio Nula
                        objClienteBE = null;
                    }
                    else
                    {
                        // Si el cliente existe asignamos sus datos a la entidad de negocios.
                        objClienteBE.Cod_cli = objCliente.Cod_cli;
                        objClienteBE.Raz_soc_cli = objCliente.Raz_soc_cli;
                        objClienteBE.Ruc_cli = objCliente.Ruc_cli;
                        objClienteBE.Tel_cli = objCliente.Tel_cli;
                        objClienteBE.Dir_cli = objCliente.Dir_cli;
                        objClienteBE.Departamento = objCliente.Tb_Ubigeo.Departamento;
                        objClienteBE.Provincia = objCliente.Tb_Ubigeo.Provincia;
                        objClienteBE.Distrito = objCliente.Tb_Ubigeo.Distrito;
                        objClienteBE.Deuda = CalcularDeudaCliente(objCliente.Cod_cli);
                        objClienteBE.Fec_Registro = objCliente.Fec_reg;
                        objClienteBE.Estado = (objCliente.Est_cli == 1) ? "Activo" : "Inactivo";
                        objClienteBE.Tipo = (objCliente.Tip_cli == 1) ? "Con acceso a crédito" : "Sin acceso a crédito";
                    }

                }

                //retornamos la entidad de negocios
                return objClienteBE;

            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }



        // Consulta de clientes para la paginacion de facturas
        public List<ClienteBE> ListarClientesRS()
        {
            try
            {
                // Lista a retornar
                List<ClienteBE> objListaClientes = new List<ClienteBE>();
                // Instancia del modelo
                using (VentasLeonEntities MisVentas = new VentasLeonEntities())
                {                  
                    // Obtenemos una lista de clientes con LINQ, solo seleccionando sus codigos y razon social.
                    var query = MisVentas.Tb_Cliente
                                              .OrderBy(miCliente => miCliente.Raz_soc_cli)
                                              .Select(miCliente => new { miCliente.Cod_cli, miCliente.Raz_soc_cli })
                                              .ToList();

                    // Recorremos el resultado 
                    foreach (var resultado in query)
                    {
                        ClienteBE objClienteBE = new ClienteBE();
                        // Solo se necesita el codigo del cliente asi como su razon social para el llenado del combo
                        objClienteBE.Cod_cli = resultado.Cod_cli;
                        objClienteBE.Raz_soc_cli = resultado.Raz_soc_cli;
                        objListaClientes.Add(objClienteBE);
                    }
                }                 
                return objListaClientes; // retornamos la lista de clientes solo con codigo y razon social
            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }
        }


        
    }
}
