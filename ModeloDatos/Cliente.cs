using System;

namespace ModeloDatos
{
    /// <summary>
    /// Cliente de la distribuidora
    /// </summary>
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        /// <summary>
        /// Tipos de mercado: GU, G, P, GNC, etc
        /// </summary>
        public string Mercado { get; set; }
    }
}
