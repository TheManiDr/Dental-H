using System;

namespace Dental_H.Model
{
    public class ConsultaInfo
    {
        public int IdCita { get; set; }

        public DateTime Fecha { get; set; }

        public TimeSpan Hora { get; set; }

        public string Paciente { get; set; }

        public string Odontologo { get; set; }

        public string Descripcion { get; set; }
    }
}
