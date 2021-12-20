using System;
using ModeloDatos;
using System.Collections.Generic;
using System.Linq;

namespace CalculadoraService
{
    public class Filtro
    {
        /// <summary>
        /// devuelve una lista con los Id de los clientes que tienen al menos un consumo en el periodo
        /// de agosto 2017
        /// NOTA: usa una tabla con información del periodo 
        /// </summary>
        public int[] ClientesConUnConsumo(List<Cliente> clientes, List<Consumo> consumos)
        {
            return (from consumo in consumos
                    join cliente in clientes
                    on consumo.IdCliente equals cliente.Id
                    select cliente.Id).ToArray();
        }

        /// <summary>
        /// devuelvo los clientes que pertenecen al mercado FD
        /// (FD = firme y distribución)
        /// </summary>
        public int[] ClientesFD(List<Cliente> clientes)
        {
            return clientes.Where(c => c.Mercado == "FD").Select(c => c.Id).ToArray();
        }

        // servicios
        //ID_CLIENTE FECHA_INICIO    FECHA_FIN FIRME   TRANSPORTE SERVICIO    CDC
        //1061	     1/5/2011	     30/4/2012 S       S          TYDF	      10000

        // clientes
        //ID_CLIENTE CLIENTE             MERCADO
        //1198	     COMBUSTIBLES TAFI   GNC
        /// <summary>
        /// devuelve los clientes que tienen volumen contratado
        /// en el mes que inicia en 'inicioMes'
        /// </summary>
        public int[] ClientesConVolContratado(List<Cliente> clientes, List<VolumenServicio> servicios, DateTime inicioMes)
        {
            return (from cliente in clientes
                    join servicio in servicios
                    on cliente.Id equals servicio.IdCliente
                    where servicio.FechaInicio <= inicioMes &&
                          servicio.FechaFin >= inicioMes.AddMonths(1).AddDays(-1) &&
                          servicio.Firme == "S" &&
                          servicio.CDC != 0
                    select cliente.Id).ToArray();
        }
        /// <summary>
        /// Son los clientes que tienen una CDC > 0 (Firme = S) y vigente en el mes 
        /// que inicia el 'inicioMes'
        /// NOTA: No tiene en cuenta que puede haber servicios que no abarquen todo el mes
        public Dictionary<int, int> ClientesConCDC(List<VolumenServicio> servicios, DateTime inicioMes)
        {
            return servicios.Where(s => s.Firme == "S" && s.CDC > 0 && s.FechaInicio <= inicioMes && s.FechaFin >= inicioMes.AddMonths(1).AddDays(-1))
                            .GroupBy(s => s.IdCliente).ToDictionary(k => k.Key, v => v.Sum(x => x.CDC));
        }

        public int[] ClientesFiltrados(List<Cliente> clientes,
                                        List<Consumo> consumos,
                                        List<VolumenServicio> servicios,
                                        DateTime inicioMes)
        {
            var query = from cliente in clientes
                        join consumo in consumos
                        on cliente.Id equals consumo.IdCliente
                        select new
                        {
                            cliente.Id, // TODO agregar todos los datos de ambas tablas que necesite
                        };

            int[] AllId = ClientesConUnConsumo(clientes, consumos);
            return AllId.Distinct().ToArray();

        }
    }
}
