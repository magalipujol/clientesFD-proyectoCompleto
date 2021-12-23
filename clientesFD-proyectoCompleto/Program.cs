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
        static void Main(string[] args)
        {
            var transportes = Parser.TxtToTteTerceros("C:/Users/magalip/Documents/Datos problema/Datos Problemas/tte_terceros-ago-2017.txt");
            var servicios = Parser.TxtToVolServicio("C:/Users/magalip/Documents/Datos problema/Datos Problemas/volumen_servicio_v2.txt");
            var consumos = Parser.TxtToConsumo("C:/Users/magalip/Documents/Datos problema/Datos Problemas/consumos-ago-2017.txt");
            var clientes = Parser.TxtToClients("C:/Users/magalip/Documents/Datos problema/Datos Problemas/clientes.txt");
            DateTime fechaInicio = new DateTime(2017, 8, 1);


            FactorCargaCalculadora calculadoraService = new FactorCargaCalculadora();
            var FC = calculadoraService.ObtenerFactoresCarga(transportes, servicios, consumos, clientes, fechaInicio);

            calculadoraService.MostrarFactoresCarga(FC);
        }
    }
}
