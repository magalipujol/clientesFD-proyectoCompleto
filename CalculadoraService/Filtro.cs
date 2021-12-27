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
        
        // nota: no está funcionando el filtro, no elimina los clientes con consumo 0
        public List<int> ClientesConUnConsumo(List<Consumo> consumos)
        {
            return (from consumo in consumos where consumo.ConsumoPlanta != 0 select consumo.IdCliente).Distinct().ToList();
        }

        /// <summary>
        /// devuelvo los id de clientes que pertenecen al mercado FD
        /// (FD = firme y distribución)
        /// </summary>
        public List<int> ClientesFD(List<Cliente> clientes)
        {
            return clientes.Where(c => c.Mercado == "FD").Select(c => c.Id).Distinct().ToList();
        }

        /// <summary>
        /// devuelve los clientes que tienen volumen contratado
        /// en el mes que inicia en 'inicioMes'
        /// </summary>
        public List<int> ClientesConVolContratado(List<Cliente> clientes, List<VolumenServicio> servicios, DateTime inicioMes)
        {
            return (from servicio in servicios
                    where servicio.FechaInicio <= inicioMes &&
                          servicio.FechaFin >= inicioMes.AddMonths(1).AddDays(-1) &&
                          servicio.Firme == "S" &&
                          servicio.CDC != 0
                    select servicio.IdCliente).Distinct().ToList();
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

        /// <summary>
        /// devuelve una lista de los id de los clientes
        /// que tienen al menos un consumo en el periodo
        /// y una CDC contratada > 0
        /// </summary>
        public List<int> ClientesFiltrados(List<Cliente> clientes,
                                        List<Consumo> consumos,
                                        List<VolumenServicio> servicios,
                                        DateTime inicioMes)
        {
            var clientesFD = this.ClientesFD(clientes);
            var clientesConConsumo = this.ClientesConUnConsumo(consumos);
            var clientesConVolContratado = this.ClientesConVolContratado(clientes, servicios, inicioMes);
            var resultado = (clientesFD.Intersect(clientesConConsumo)).Intersect(clientesConVolContratado).ToList();
            return resultado;
        }
    }
}
