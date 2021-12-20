using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ModeloDatos;

namespace Importador
{
    public class Parser
    {
        /// <summary>
        /// estos métodos van a devolver colecciones de objetos 
        /// con la info de los archivos txt
        /// </summary>
        /// <returns></returns>
        public static List<Cliente> TxtToClients(string pathTxtCliente)
        {
            string[] lines = File.ReadAllLines(pathTxtCliente);
            List<Cliente> clientes = new List<Cliente>();
            foreach (string line in lines.Skip(1))
            {
                string[] csvLine = line.Split('\t');
                clientes.Add(new Cliente
                {
                    Id = int.Parse(csvLine[0]),
                    Nombre = csvLine[1],
                    Mercado = csvLine[2]
                });
            }
            return clientes;
        }

        public static List<Consumo> TxtToConsumo(string pathTxtConsumo)
        {
            string[] lines = File.ReadAllLines(pathTxtConsumo);
            List<Consumo> consumos = new List<Consumo>();
            foreach (string line in lines.Skip(1))
            {
                string[] csvLine = line.Split('\t');
                consumos.Add(new Consumo
                {
                    DiaOperativo = DateTime.Parse(csvLine[0]),
                    IdCliente = int.Parse(csvLine[1]),
                    ConsumoPlanta = int.Parse(csvLine[2])
                });
            }
            return consumos;
        }

        public static List<TransporteTerceros> TxtToTteTerceros(string pathTxtTte)
        {
            string[] lines = File.ReadAllLines(pathTxtTte);
            List<TransporteTerceros> transportes = new List<TransporteTerceros>();
            foreach (string line in lines.Skip(1))
            {
                string[] csvLine = line.Split('\t');
                transportes.Add(new TransporteTerceros
                {
                    DiaOperativo = DateTime.Parse(csvLine[0]),
                    IdCliente = int.Parse(csvLine[1]),
                    IdCargador = int.Parse(csvLine[2]),
                    Asignado = int.Parse(csvLine[3])

                });
            }
            return transportes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pathTxtVolumen"></param>
        /// <returns></returns>
        public static List<VolumenServicio> TxtToVolServicio(string pathTxtVolumen)
        {
            // ID_CLIENTE  FECHA_INICIO    FECHA_FIN     FIRME   TRANSPORTE SERVICIO    CDC
            // 1061        1/5/2011        30/4/2012     S       S          TYDF        10000

            string[] lines = File.ReadAllLines(pathTxtVolumen);
            List<VolumenServicio> transportes = new List<VolumenServicio>();
            foreach (string line in lines.Skip(1).Where(l => l.Length > 1))
            {
                string[] csvLine = line.Split('\t');
                transportes.Add(new VolumenServicio
                {
                    IdCliente = int.Parse(csvLine[0]),
                    FechaInicio = DateTime.Parse(csvLine[1]),
                    FechaFin = DateTime.Parse(csvLine[2]),
                    Firme = csvLine[3],
                    Servicio = csvLine[4],
                    CDC = int.Parse(csvLine[5]),
                });
            }
            return transportes;
        }

        // TODO podría hacer un método que pase de List<FactorCarga> a txt
    }
}
