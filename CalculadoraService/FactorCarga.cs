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

    }
    public class FactorCargaCalculadora
    {


        /// <summary>
        /// devuelve un int con el FD calculado con
        /// sumatoria de los consumos / CDC * periodo
        /// </summary>
        public double CalcularFactorCarga(int idCliente, List<TransporteTerceros> transportes, List<Consumo> consumos, int CDC)
        {
            int tteFirme = 0;
            foreach (var c in consumos.Where(c => c.IdCliente == idCliente))
            {
                var tteTerceros = transportes.Where(t => t.IdCliente == idCliente && t.DiaOperativo == c.DiaOperativo)
                                             .Sum(t => t.Asignado);
                var tteDistco = c.ConsumoPlanta - tteTerceros;
                tteFirme += Math.Min(tteDistco, CDC);
            }

            return Math.Round(100.0 * tteFirme / (31 * CDC), 5);
        }

        /// <summary>
        /// devuelve la CDC de un cliente en particular 
        /// dentro de un periodo
        /// </summary>
        public int getCDC(int idCliente, List<VolumenServicio> volumenServicios, DateTime fechaInicioPeriodo)
        {
            var cliente = volumenServicios.Where(s => s.FechaInicio <= fechaInicioPeriodo &&
                                   s.FechaFin >= fechaInicioPeriodo.AddMonths(1).AddDays(-1) &&
                                   s.IdCliente == idCliente && s.CDC != 0).Select(s => s.CDC).First();
            return cliente;
        }

        /// <summary>
        /// devuelve el nombre de un cliente a partir de su id
        /// </summary>
        public string NombreAPartirDeId(List<Cliente> clientes, int id)
        {
            return (from cliente in clientes where cliente.Id == id select cliente.Nombre).First();
        }

        /// <summary>
        /// devuelve una lista de FactorCarga, que incluye el id del cliente, su nombre y su factor de carga
        /// en un periodo determinado
        /// </summary>
        public List<FactorCarga> ObtenerFactoresCarga(List<TransporteTerceros> transportes, List<VolumenServicio> servicios, List<Consumo> consumos, List<Cliente> clientes, DateTime fechaInicio)
        {
            List<FactorCarga> FactoresCarga = new List<FactorCarga>();
            var filtro = new Filtro();
            var clientesFiltrados = filtro.ClientesFiltrados(clientes, consumos, servicios, fechaInicio);
            int CDC;
            foreach (int clienteId in clientesFiltrados)
            {
                CDC = getCDC(clienteId, servicios, fechaInicio);
                FactoresCarga.Add(new FactorCarga { FD = CalcularFactorCarga(clienteId, transportes, consumos, CDC), IdCliente = clienteId, NombreCliente = NombreAPartirDeId(clientes, clienteId) });
            };
            return FactoresCarga;
        }

        /// <summary>
        /// muestra por consola una tabla con el id del cliente,
        /// su nombre y el factor de carga
        /// </summary>
        public void MostrarFactoresCarga(List<FactorCarga> factoresCarga)
        {
            Console.WriteLine("ID_CLIENTE\tNOMBRE\t\t\t FACTOR_CARGA");
            foreach (FactorCarga factorCarga in factoresCarga)
            {
                Console.WriteLine($"{factorCarga.IdCliente, -16}{factorCarga.NombreCliente, -25}{factorCarga.FD, -10}");
            }
        }
    }
}


            /* var transporteAgrupadosPorDia = transportes.GroupBy(t => new { t.DiaOperativo, t.IdCliente }, t => t.Asignado, 
                (diaIdCliente, transporteGroup) => new
                {
                    DiaOperativo = diaIdCliente.DiaOperativo, 
                    AsignadoPorDia = transporteGroup.Sum(),
                    IdCliente = diaIdCliente.IdCliente
                });

            var query = (from clienteId in clientesFiltrados
                        join transporte in transporteAgrupadosPorDia on clienteId equals transporte.IdCliente
                        join servicio in servicios on clienteId equals servicio.IdCliente
                        join consumo in consumos on clienteId equals consumo.IdCliente
                        where servicio.CDC > 0 && servicio.FechaInicio <= fechaInicio && servicio.FechaFin >= fechaFin
                        && consumo.DiaOperativo >= fechaInicio && consumo.DiaOperativo <= fechaFin && consumo.DiaOperativo == transporte.DiaOperativo
                        select new { clienteId, transporte.AsignadoPorDia, transporte.DiaOperativo, servicio.CDC, consumo.ConsumoPlanta }).GroupBy(x => x.clienteId, x=> x, (idCliente, x) => new
                        {
                            idCliente,

                        })
                        ;
            */