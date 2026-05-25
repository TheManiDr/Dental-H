using Dental_H.Model;
using Dental_H.Util;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dental_H.DAO
{
    public class PacienteDAO
    {
        public bool RegistrarPaciente(
            int idPersona,
            Paciente paciente,
            MySqlConnection conexion,
            MySqlTransaction transaccion)
        {
            try
            {
                string consulta =
                   @"INSERT INTO Paciente
                    (
                     id_persona,
                     tipo_sangre,
                     alergias,
                     contacto_emergencia,
                     numero_emergencia
                    )
                    VALUES
                    (
                        @idPaciente,
                        @tipoSangre,
                        @alergias,
                        @contactoEmergencia,
                        @numeroEmergencia
                    )";

                MySqlCommand comando =
                    new MySqlCommand(
                        consulta,
                        conexion,
                        transaccion
                    );

                comando.Parameters.AddWithValue(
                    "@idPaciente",
                    idPersona
                );

                comando.Parameters.AddWithValue(
                    "@tipoSangre",
                    paciente.TipoSangre
                );

                comando.Parameters.AddWithValue(
                    "@alergias",
                    paciente.Alergias
                );

                comando.Parameters.AddWithValue(
                    "@contactoEmergencia",
                    paciente.ContactoEmergencia
                );

                comando.Parameters.AddWithValue(
                    "@numeroEmergencia",
                    paciente.NumeroEmergencia
                );

                int filasAfectadas =
                    comando.ExecuteNonQuery();


                return filasAfectadas > 0;
            }
            catch
            {

                return false;
            }
        }
        public List<Paciente> ObtenerPacientes(int idPaciente)
        {
            List<Paciente> lista =
                new List<Paciente>();

            MySqlConnection conexion =
                Conexion.obtenerConexion();

            try
            {
                conexion.Open();

                string consulta =
                @"SELECT
                p.id_persona,
                p.nombre,
                p.apellido_paterno,
                p.apellido_materno,
                p.fecha_nacimiento,
                p.genero,
                pa.tipo_sangre,
                pa.alergias,
                pa.contacto_emergencia,
                pa.numero_emergencia
                FROM Persona p
                INNER JOIN Paciente pa
                ON p.id_persona = pa.id_persona";

                MySqlCommand comando =
                    new MySqlCommand(
                        consulta,
                        conexion);

                MySqlDataReader reader =
                    comando.ExecuteReader();

                while (reader.Read())
                {
                    Paciente paciente = new Paciente();

                    paciente.IdPersona = Convert.ToInt32(reader["id_persona"]);

                    paciente.Nombre = reader["nombre"].ToString();

                    paciente.ApellidoPaterno = reader["apellido_paterno"].ToString();

                    paciente.ApellidoMaterno = reader["apellido_materno"].ToString();

                    paciente.TipoSangre = reader["tipo_sangre"].ToString();

                    paciente.Alergias = reader["alergias"].ToString();

                    paciente.FechaNacimiento = Convert.ToDateTime(reader["fecha_nacimiento"]);

                    lista.Add(paciente);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conexion.Close();
            }

            return lista;
        }
        public Paciente ObtenerPacientePorId(int idPaciente)
        {
            Paciente paciente = null;

            MySqlConnection conexion =
                Conexion.obtenerConexion();

            try
            {
                conexion.Open();

                string consulta = @"
                SELECT
                    p.id_persona,
                    p.nombre,
                    p.apellido_paterno,
                    p.apellido_materno,
                    p.fecha_nacimiento,
                    p.genero,

                    pa.tipo_sangre,
                    pa.alergias,
                    pa.contacto_emergencia,
                    pa.numero_emergencia,

                    d.calle,
                    d.ciudad,
                    d.estado,
                    d.codigo_postal,

                    t.telefono

                FROM Persona p

                INNER JOIN Paciente pa
                ON p.id_persona = pa.id_persona

                LEFT JOIN DireccionPersona d
                ON p.id_persona = d.id_persona

                LEFT JOIN TelefonoPersona t
                ON p.id_persona = t.id_persona

                WHERE p.id_persona = @idPaciente";
            }
            MySqlCommand comando = new MySqlCommand(consulta, conexion);

            comando.Parameters.AddWithValue("@idPaciente", idPaciente);

            MySqlDataReader reader = comando.ExecuteReader();

            if (reader.Read())
            {
                paciente = new Paciente();

                paciente.IdPersona =
                    Convert.ToInt32(reader["id_persona"]);

                paciente.Nombre =
                    reader["nombre"].ToString();

                paciente.ApellidoPaterno =
                    reader["apellido_paterno"].ToString();

                paciente.ApellidoMaterno =
                    reader["apellido_materno"].ToString();

                paciente.FechaNacimiento =
                    Convert.ToDateTime(reader["fecha_nacimiento"]);

                paciente.Genero =
                    reader["genero"].ToString();

                paciente.TipoSangre =
                    reader["tipo_sangre"].ToString();

                paciente.Alergias =
                    reader["alergias"].ToString();

                paciente.NombreEmergencia =
                    reader["contacto_emergencia"].ToString();

                paciente.NumeroEmergencia =
                    reader["numero_emergencia"].ToString();

                paciente.Calle =
                    reader["calle"].ToString();

                paciente.Ciudad =
                    reader["ciudad"].ToString();

                paciente.Estado =
                    reader["estado"].ToString();

                paciente.CodigoPostal =
                    reader["codigo_postal"].ToString();

                paciente.Telefono =
                    reader["telefono"].ToString();
            }
            return paciente;


        }

    }
}