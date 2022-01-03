using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ModeloDatos;

namespace Importador
{
    public abstract class ParserBase
    {
        protected List<string[]> items = new List<string[]>();

        public ParserBase (string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException ("path is null");
            }
            string[] lines = File.ReadAllLines(path);
            foreach (string line in lines.Skip(1))
            {
                items.Add(line.Split('\t'));
            }
        }
    }

    public class ParserCliente : ParserBase
    {
        public ParserCliente(string path)
            : base(path)
        {
        }
        public List<Cliente> DoParse()
        {
            var clientes = new List<Cliente>();
            foreach (var linea in items)
            {
                clientes.Add(new Cliente()
                {
                    Id = int.Parse(linea[0]),
                    Nombre = linea[1],
                    Mercado = linea[2]
                }
                );
            }
            return clientes;
        }
    }

    public class ParserConsumo : ParserBase
    {
        public ParserConsumo(string path) : base(path) { }

        public List<Consumo> DoParse()
        {
            var Consumos = new List<Consumo>();
            foreach (var linea in items)
            {
                Consumos.Add(new Consumo()
                {
                    DiaOperativo = DateTime.Parse(linea[0]),
                    IdCliente = int.Parse(linea[1]),
                    ConsumoPlanta = int.Parse(linea[2])
                }
                );
            }
            return Consumos;
        }
    }

    public class ParserTteTerceros : ParserBase
    {
        public ParserTteTerceros(string path) : base(path) { }
        public List<TransporteTerceros> DoParse()
        {
            var TteTerceros = new List<TransporteTerceros>();
            foreach (var linea in items)
            {
                TteTerceros.Add(new TransporteTerceros()
                {
                    DiaOperativo = DateTime.Parse(linea[0]),
                    IdCliente = int.Parse(linea[1]),
                    IdCargador = int.Parse(linea[2]),
                    Asignado = int.Parse(linea[3])
                }
                );
            }
            return TteTerceros;
        }
    }

    public class ParserServicio : ParserBase
    {
        public ParserServicio(string path) : base(path) { }
        public List<VolumenServicio> DoParse()
        {
            var Servicios = new List<VolumenServicio>();
            foreach (var linea in items)
            {
                Servicios.Add(new VolumenServicio()
                {
                    IdCliente = int.Parse(linea[0]),
                    FechaInicio = DateTime.Parse(linea[1]),
                    FechaFin = DateTime.Parse(linea[2]),
                    Firme = linea[3],
                    Servicio = linea[4],
                    CDC = int.Parse(linea[5])
                });
            }
            return Servicios;
        }
    }
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
