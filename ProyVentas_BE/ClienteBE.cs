using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyVentas_BE
{
    public class ClienteBE
    {
       
        public String Cod_cli { get; set; }
        public String Raz_soc_cli { get; set; }       
        public String Dir_cli { get; set; }      
        public String Tel_cli { get; set; }            
        public String Ruc_cli { get; set; }        
        public String Contacto { get; set; }       
        public String Id_Ubigeo { get; set; }               
        public String Tip_cli { get; set; }      
        public DateTime Fec_Registro { get; set; }       
        public String Usu_Registro { get; set; }       
        public DateTime Fec_Ult_Mod { get; set; }       
        public String Usu_Ult_Mod { get; set; }      
        public Int16 Est_cli { get; set; }

        //Calculados
        public String Estado { get; set; }
        public String Tipo { get; set; }
        public String Departamento { get; set; }
        public String Provincia { get; set; }
        public String Distrito { get; set; }
        public Decimal Deuda { get; set; }


    }

}

