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
                    "WHERE usuario = @usuario " +
                    "AND contraseña = @contrasena";

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
                        reader.GetString("usuario");

                    usuarioEncontrado.Contrasena =
                        reader.GetString("contraseña");

                    usuarioEncontrado.IdPersonal =
                        reader.GetInt32("id_personal");

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
