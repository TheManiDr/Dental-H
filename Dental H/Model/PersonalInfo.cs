using System;

namespace Dental_H.Model
{
    public class PersonalInfo : Persona
    {
        public string Curp { get; set; }

        public string Rfc { get; set; }

        public string Nss { get; set; }

        public string NombreUsuario { get; set; }

        public string Contrasena { get; set; }

        public int IdRol { get; set; }

        public string Rol { get; set; }

        public string Calle { get; set; }

        public string Ciudad { get; set; }

        public string Estado { get; set; }

        public string CodigoPostal { get; set; }

        public string Telefono { get; set; }

        public string Correo { get; set; }
    }
}
