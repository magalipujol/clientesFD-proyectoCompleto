using System;
using ModeloDatos;
using System.Linq;
using System.Collections.Generic;
using Importador;
using CalculadoraService;

namespace clientesFD_proyectoCompleto
{
    class Program
    {
        public static object ConfigurationManager { get; private set; }

        [Obsolete]
        static void Main(string[] args)
        {
            
            var basePath = System.Configuration.ConfigurationSettings.AppSettings["Base"];
            var pathClientes = basePath + System.Configuration.ConfigurationSettings.AppSettings["Clientes"];
            var pathConsumos= basePath + System.Configuration.ConfigurationSettings.AppSettings["Consumos"];
            var pathTteTerceros = basePath + System.Configuration.ConfigurationSettings.AppSettings["TteTerceros"];
            var pathServicios = basePath + System.Configuration.ConfigurationSettings.AppSettings["Servicios"];

            var transportes = Parser.TxtToTteTerceros(pathTteTerceros);
            var servicios = Parser.TxtToVolServicio(pathServicios);
            var consumos = Parser.TxtToConsumo(pathConsumos);
            var clientes = Parser.TxtToClients(pathClientes);
            DateTime fechaInicio = new DateTime(2017, 8, 1);


            FactorCargaCalculadora calculadoraService = new FactorCargaCalculadora();
            var FC = calculadoraService.ObtenerFactoresCarga(transportes, servicios, consumos, clientes, fechaInicio);

            calculadoraService.MostrarFactoresCarga(FC);

        }
    }
}
