using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyVentas_BE
{
    public class DashboardBE
    {
        public Decimal  TotalVentas { get; set; }
        public Int32 CantClientesActivos { get; set; }
        public Decimal TotalDeuda { get; set; }
    }
}
