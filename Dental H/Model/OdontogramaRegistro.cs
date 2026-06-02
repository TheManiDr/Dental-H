using System;
using System.Collections.Generic;

namespace Dental_H.Model
{
    public class OdontogramaRegistro
    {
        public int IdRegistro { get; set; }
        public int IdPaciente { get; set; }
        public string NumeroPieza { get; set; }
        public string Tipo { get; set; }
        public string Estado { get; set; }
        public string Diagnostico { get; set; }
        public string Tratamiento { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaPadecimiento { get; set; }
        public List<MarcaOdontograma> Marcas { get; set; } = new List<MarcaOdontograma>();
    }

    public class MarcaOdontograma
    {
        public string Superficie { get; set; }
        public string Padecimiento { get; set; }
        public float PosicionX { get; set; }
        public float PosicionY { get; set; }
    }
}
