using MySql.Data.MySqlClient;

namespace Dental_H.Util
{
    public class Conexion
    {
        // DATOS DE TU BASE
        private static string servidor = "13.71.191.183";
        private static string bd = "dental_H_DB";
        private static string usuario = "root";
        private static string password = "SuperSecretPassword123!#";
        private static string puerto = "3306";

        // CADENA DE CONEXIÓN
        private static string cadenaConexion =
            "server=" + servidor +
            ";port=" + puerto +
            ";user id=" + usuario +
            ";password=" + password +
            ";database=" + bd;

        // MÉTODO PARA OBTENER CONEXIÓN
        public static MySqlConnection obtenerConexion()
        {
            MySqlConnection conexion = new MySqlConnection(cadenaConexion);

            return conexion;
        }
    }
}