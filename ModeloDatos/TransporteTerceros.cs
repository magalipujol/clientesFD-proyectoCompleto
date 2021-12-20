using System;
using System.Collections.Generic;
using System.Text;

namespace ModeloDatos
{
    public class TransporteTerceros
    {
        public DateTime DiaOperativo { get; set; }
        public int IdCliente { get; set; }
        public int IdCargador { get; set; }
        public int Asignado { get; set; }
    }
}
