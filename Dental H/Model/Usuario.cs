using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dental_H.Model
{
    public class Usuario
    {
        // ATRIBUTOS
        public int IdUsuario { get; set; }

        public string NombreUsuario { get; set; }

        public string Contrasena { get; set; }

        public int IdPersonal { get; set; }

        public int IdRol { get; set; }

        // CONSTRUCTOR VACÍO
        public Usuario()
        {

        }

        // CONSTRUCTOR CON PARÁMETROS
        public Usuario(
            int idUsuario,
            string nombreUsuario,
            string contrasena,
            int idPersonal,
            int idRol)
        {
            IdUsuario = idUsuario;
            NombreUsuario = nombreUsuario;
            Contrasena = contrasena;
            IdPersonal = idPersonal;
            IdRol = idRol;
        }
    }
}
