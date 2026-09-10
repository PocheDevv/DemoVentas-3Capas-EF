using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
//Agregar
using System.Data.Entity.Core;
using ProyVentas_BE;

namespace ProyVentas_ADO
{
   public  class OrdenADO
    {
        public String RegistrarOrden(OrdenBE objOrdenBE)
        {
            try // Protege a todo el metodo de excepciones
            {
                using (VentasLeonEntities MisVentas = new VentasLeonEntities())
                {
                    // 1. Iniciamos una transacción explícita gestionada por EF
                    using (var dbContextTransaction = MisVentas.Database.BeginTransaction())
                    {
                        try // protege especificamente a la transaccion
                        {
                            // 2. Generar el número correlativo de la Orden de Compra (equivalente a tu lógica de SQL)
                            string nuevoNumOco = "OC0001";

                            // Contamos los registros usando LINQ
                            int totalRegistros = MisVentas.Tb_Orden_Compra.Count();

                            if (totalRegistros > 0)
                            {
                                // Buscamos el valor máximo de num_oco, extraemos los últimos 4 dígitos, sumamos 1 y formateamos
                                var maxNumOco = MisVentas.Tb_Orden_Compra
                                                         .Select(o => o.Num_oco)
                                                         .Max(); // En nuestro caso: "OC4050"

                                if (!string.IsNullOrEmpty(maxNumOco) && maxNumOco.Length >= 6)
                                {
                                    int ultimoCorrelativo = Convert.ToInt32(maxNumOco.Substring(2)); // Toma "4050" 
                                    // El ToString("D4") hace que se rellene con 0 a la izquierda para completar los 4 digitos
                                    // Ejemplo : 5 --> 0005
                                    nuevoNumOco = "OC" + (ultimoCorrelativo + 1).ToString("D4"); //  Genera el  "OC4051"
                                }
                            }

                            // 3. Mapear y registrar la Cabecera (Tb_Orden_Compra)
                            var nuevaCabecera = new Tb_Orden_Compra
                            {
                                Num_oco = nuevoNumOco,
                                Fec_oco = objOrdenBE.Fec_oco,
                                Cod_prv = objOrdenBE.Cod_prv,
                                Fec_ate = objOrdenBE.Fec_ate,
                                Est_oco = Convert.ToInt32(objOrdenBE.Est_oco),
                                Fec_Registro = DateTime.Now, // Fecha de hoy
                                Usu_Registro = objOrdenBE.Usu_Registro
                            };
                            MisVentas.Tb_Orden_Compra.Add(nuevaCabecera);

                            // 4. Mapear y registrar los Detalles (Tb_Detalle_OCompra)
                            foreach (var det in objOrdenBE.DetallesOC)
                            {
                                var nuevoDetalle = new Tb_Detalle_Compra
                                {
                                    Num_oco = nuevoNumOco, // Aqui ya asignamos el código generado como Num OC
                                    Cod_pro = det.Cod_pro,
                                    Can_ped = det.Can_ped
                                };
                                MisVentas.Tb_Detalle_Compra.Add(nuevoDetalle);
                            }

                            // 5. Guardar todos los cambios en la Base de Datos de forma atómica
                            MisVentas.SaveChanges();

                            // 6. Si todo fue exitoso, confirmamos (Commit) la transacción
                            dbContextTransaction.Commit();

                            // Retornamos el número generado para que la capa de negocio/presentación lo use
                            return nuevoNumOco;
                        }
                        catch (EntityException)
                        {
                            // Si algo falla dentro del bloque, revertimos (Rollback) de forma automática
                            dbContextTransaction.Rollback();
                            throw; // Re-lanzamos la excepción para el catch externo
                        }
                    }
                }
            }
            catch (EntityException ex)
            {
                throw new Exception("Error en la capa de datos al registrar la OC con LINQ: " + ex.Message, ex);
            }
        }



       public List<OrdenBE > ListarOrdenes()
        {            
            try
            {
                List<OrdenBE > objListaOC = new List<OrdenBE>();
                // Codifique

                return objListaOC;
            }
            catch (EntityException ex)
            {
                throw new Exception(ex.Message);
            }

        }

    }
}
