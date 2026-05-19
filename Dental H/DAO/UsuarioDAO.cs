using Dental_H.Model;
using Dental_H.Util;
using MySql.Data.MySqlClient;

namespace Dental_H.DAO
{
    public class UsuarioDAO
    {
        public Usuario Login(string usuario, string contrasena)
        {
            Usuario usuarioEncontrado = null;

            MySqlConnection conexion = Conexion.obtenerConexion();

            try
            {
                conexion.Open();

                string consulta =
                    "SELECT * FROM Usuario " +
                    "WHERE nombre_usuario = @usuario " +
                    "AND contrasena = @contrasena";

                MySqlCommand comando =
                    new MySqlCommand(consulta, conexion);

                comando.Parameters.AddWithValue(
                    "@usuario",
                    usuario
                );

                comando.Parameters.AddWithValue(
                    "@contrasena",
                    contrasena
                );

                MySqlDataReader reader =
                    comando.ExecuteReader();

                if (reader.Read())
                {
                    usuarioEncontrado = new Usuario();

                    usuarioEncontrado.IdUsuario =
                        reader.GetInt32("id_usuario");

                    usuarioEncontrado.NombreUsuario =
                        reader.GetString("nombre_usuario");

                    usuarioEncontrado.Contrasena =
                        reader.GetString("contrasena");

                    usuarioEncontrado.IdPersonal =
                        reader.GetInt32("id_persona");

                    usuarioEncontrado.IdRol =
                        reader.GetInt32("id_rol");
                }

                conexion.Close();
            }
            catch
            {
                conexion.Close();
            }

            return usuarioEncontrado;
        }
    }
}
