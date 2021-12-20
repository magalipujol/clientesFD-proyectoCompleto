using System;
using System.Collections.Generic;
using System.Text;

namespace ModeloDatos
{
    public class VolumenServicio
    {
        public int IdCliente { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Firme { get; set; }
        public string Transporte { get; set; }
        public string Servicio { get; set; }
        public int CDC { get; set; }

    }
}
