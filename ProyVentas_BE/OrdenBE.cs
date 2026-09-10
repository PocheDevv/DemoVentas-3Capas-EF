using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ProyVentas_BE
{
    public class OrdenBE
    {
        public String Num_oco { get; set; }        
        public System.DateTime Fec_oco { get; set; }       
        public String Cod_prv { get; set; }       
        public System.DateTime Fec_ate { get; set; }       
        public String Est_oco { get; set; }
        // Propiedades de Auditoria
        public DateTime Fec_Registro { get; set; }
        public String Usu_Registro { get; set; }
        public DateTime Fec_Ult_Mod { get; set; }
        public String Usu_Ult_Mod { get; set; }
        // Aqui an los detalles de la OC
        public List<DetalleOCBE> DetallesOC { get; set; }
    }
     public class DetalleOCBE
    {
        public String Num_oco { get; set; }
        public String Cod_pro { get; set; }
        public int Can_ped { get; set; }

    }
}
