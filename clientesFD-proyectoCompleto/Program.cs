using System;
using ModeloDatos;
using System.Linq;
using System.Collections.Generic;
using Importador;
using CalculadoraService;
using System.Configuration;

namespace clientesFD_proyectoCompleto
{
    class Program
    {

        static void Main(string[] args)
        {
            var basePath = ConfigurationManager.AppSettings["Base"];
            var pathClientes = basePath + ConfigurationManager.AppSettings["Clientes"];
            var pathConsumos= basePath + ConfigurationManager.AppSettings["Consumos"];
            var pathTteTerceros = basePath + ConfigurationManager.AppSettings["TteTerceros"];
            var pathServicios = basePath + ConfigurationManager.AppSettings["Servicios"];

            var transportes = new ParserTteTerceros(pathTteTerceros).DoParse();
            var servicios = new ParserServicio(pathServicios).DoParse();
            var consumos = new ParserConsumo(pathConsumos).DoParse();
            var clientes = new ParserCliente(pathClientes).DoParse();
            DateTime fechaInicio = new DateTime(2017, 8, 1);

            FirmeUtilizadoDiarioCalculadora FNUCalculadora = new FirmeUtilizadoDiarioCalculadora();
            var FNU = FNUCalculadora.ObtenerFirmesNoUtilizadosDiarios(fechaInicio, servicios, consumos);

            //FNUCalculadora.MostrarFirmesNoUtilizadosDiarios(FNU);
            
            FactorCargaCalculadora FCCalculadora = new FactorCargaCalculadora();
            var FC = FCCalculadora.ObtenerFactoresCarga(transportes, servicios, consumos, clientes, fechaInicio);

            //FCCalculadora.MostrarFactoresCarga(FC);
        }
    }
}
