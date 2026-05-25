using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dental_H.Model
{
    public class Paciente : Persona
    {
        public string TipoSangre { get; set; }

        public string Alergias { get; set; }

        public string ContactoEmergencia { get; set; }

        public string NumeroEmergencia { get; set; }

        public string Calle { get; set; }

        public string Ciudad { get; set; }

        public string Estado { get; set; }

        public string CodigoPostal { get; set; }

        public string Telefono { get; set; }

        public Paciente()
        {

        }

        public Paciente(

            string tipoSangre,
            string alergias,
            string contactoEmergencia,
            string numeroEmergencia)
        {
            TipoSangre = tipoSangre;
            Alergias = alergias;
            ContactoEmergencia = contactoEmergencia;
            NumeroEmergencia = numeroEmergencia;
        }
    }
}
