using System;
using System.Collections.Generic;
using System.Text;
using ModeloDatos;
using Importador;
using System.Linq;


namespace CalculadoraService
{
    public class FactorCarga
    {
        public int IdCliente { get; set; }
        public string NombreCliente { get; set; }
        public double FD { get; set; }

        /// <summary>
        /// devuelve un int con el FD calculado con
        /// sumatoria de los consumos / CDC * periodo
        /// </summary>
        /// <param name="periodo"></param>
        /// <param name="consumos"></param>
        /// <param name="CDCDistco"></param>
        /// <returns></returns>
        public double CalcularFactorCarga(int idCliente,
                                          List<TransporteTerceros> transportes,
                                          List<Consumo> consumos,
                                          int CDC)
        {
            int tteFirme = 0;
            foreach (var c in consumos.Where(c => c.IdCliente == idCliente))
            {
                var tteTerceros = transportes.Where(t => t.IdCliente == idCliente && t.DiaOperativo == c.DiaOperativo)
                                             .Sum(t => t.Asignado);
                var tteDistco = c.ConsumoPlanta - tteTerceros;
                tteFirme += Math.Min(tteDistco, CDC);
            }

            return 1.0 * tteFirme / 31 * CDC;

        }

        public int getCDC(int idCliente,
                          List<VolumenServicio> volumenServicios,
                          DateTime fechaInicioPeriodo,
                          DateTime fechaFinPeriodo)
        {
            var cliente = volumenServicios.Where(s => s.FechaInicio <= fechaInicioPeriodo &&
                                   s.FechaFin >= fechaFinPeriodo &&
                                   s.IdCliente == idCliente && s.CDC != 0).Take(1).ToList();
            return cliente[0].CDC;
        }

        // terminar 
        public List<FactorCarga> ObtenerFactoresCarga()
        {
            var FactoresCarga = new List<FactorCarga>();
            var transportes = Parser.TxtToTteTerceros("C:/Users/magalip/Documents/Datos problema/Datos Problemas/tte_terceros-ago-2017.txt");
            var servicios = Parser.TxtToVolServicio("C:/Users/magalip/Documents/Datos problema/Datos Problemas/volumen_servicio_v2.txt");
            var consumos = Parser.TxtToConsumo("C:/Users/magalip/Documents/Datos problema/Datos Problemas/consumos-ago-2017.txt");
            var clientes = Parser.TxtToClients("C:/Users/magalip/Documents/Datos problema/Datos Problemas/clientes.txt");


            return FactoresCarga;
        }



    }
}

