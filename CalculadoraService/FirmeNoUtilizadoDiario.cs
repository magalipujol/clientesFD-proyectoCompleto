using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModeloDatos;


namespace CalculadoraService
{
    public class FirmeNoUtilizadoDiario
    {
        public DateTime DiaOperativo { get; set; }
        public int FirmeNoUtilizado { get; set; }
    }
    public class FirmeUtilizadoDiarioCalculadora
    {
        /// <summary>
        /// FirmeNoUtilizadoDiario = Firme - Consumo
        /// </summary>
        public int CalcularFirmeNoUtilizadoDiario (DateTime diaOperativo, List<VolumenServicio> servicios, List<Consumo> consumos)
        {
            // para un dia determinado, obtener la suma de todas las CDC de todos los clientes
            var Firme = servicios.Where(s => s.FechaInicio <= diaOperativo).Select(s => s.CDC).Sum();
            // tambien obtener todos los consumos para ese dia
            var consumoDiario = consumos.Where(c => c.DiaOperativo == diaOperativo).Select(c => c.ConsumoPlanta).Sum();
            // restar esos dos numeros
            if (consumoDiario >= Firme)
            {
                return 0;
            }
            else
            {
                return Firme - consumoDiario;
            }
        }

        public List<FirmeNoUtilizadoDiario> ObtenerFirmesNoUtilizadosDiarios (DateTime inicioPeriodo, List<VolumenServicio> servicios, List<Consumo> consumos)
        {
            var firmesNoUtilizadosDiarios = new List<FirmeNoUtilizadoDiario>();
            // por cada dia del periodo, calcular el firme no utilizado
            // para eso tengo que tener manera de iterar sobre cada dia
            for (DateTime Day = inicioPeriodo; Day < inicioPeriodo.AddMonths(1); Day += TimeSpan.FromDays(1))
            {
                firmesNoUtilizadosDiarios.Add(new FirmeNoUtilizadoDiario
                {
                    DiaOperativo = Day,
                    FirmeNoUtilizado = CalcularFirmeNoUtilizadoDiario(Day, servicios, consumos)
                });
            }
            return firmesNoUtilizadosDiarios;
        }

        public void MostrarFirmesNoUtilizadosDiarios(List<FirmeNoUtilizadoDiario> firmesNoUtilizados)
        {
            Console.WriteLine("DIA_OPERATIVO\tFIRME_NO_UTILIZADO");
            foreach (FirmeNoUtilizadoDiario firmeNoUtilizado in firmesNoUtilizados)
            {
                Console.WriteLine($"{firmeNoUtilizado.DiaOperativo.ToString("dd/MM/yyyy"),-16}{firmeNoUtilizado.FirmeNoUtilizado,-25}");
            }
        }
    }
}
