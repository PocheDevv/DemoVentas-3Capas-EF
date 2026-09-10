using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyVentas_BE
{
    public class ProductoBE
    {
        // Definimos la entidad de negocio Producto, con todas las propiedades de
        // acuerdo a la estructura de la tabla Tb_Producto
        public String Cod_pro { get; set; }
        public String Des_pro { get; set; }
        public Decimal  Pre_pro { get; set; }
        public Int32 Stk_act { get; set; }
        public Int32 Stk_min { get; set; }
        public Int32 Id_UM { get; set; }
        public Int32 Id_Cat { get; set; }
        public Int32 Importado { get; set; }
        public DateTime Fec_Registro { get; set; }
        public String  Usu_Registro { get; set; }
        public DateTime Fec_Ult_Mod { get; set; }
        public String Usu_Ult_Mod { get; set; }
        public Int32 Est_pro { get; set; }
    }
}
