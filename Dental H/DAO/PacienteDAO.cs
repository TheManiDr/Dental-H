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
        public List<Paciente> ObtenerPacientes()
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

                    paciente.Genero =reader["genero"].ToString();

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

        public void RegistrarDatosComplementarios(
            int idPersona,
            Paciente paciente,
            MySqlConnection conexion,
            MySqlTransaction transaccion)
        {
            if (!string.IsNullOrWhiteSpace(paciente.Calle) ||
                !string.IsNullOrWhiteSpace(paciente.Ciudad) ||
                !string.IsNullOrWhiteSpace(paciente.Estado) ||
                !string.IsNullOrWhiteSpace(paciente.CodigoPostal))
            {
                string insertarDireccion = @"
                    INSERT INTO DireccionPersona
                    (id_persona, calle, ciudad, estado, codigo_postal)
                    VALUES
                    (@idPersona, @calle, @ciudad, @estado, @codigoPostal)";

                MySqlCommand comandoDireccion = new MySqlCommand(insertarDireccion, conexion, transaccion);
                comandoDireccion.Parameters.AddWithValue("@idPersona", idPersona);
                comandoDireccion.Parameters.AddWithValue("@calle", paciente.Calle);
                comandoDireccion.Parameters.AddWithValue("@ciudad", paciente.Ciudad);
                comandoDireccion.Parameters.AddWithValue("@estado", paciente.Estado);
                comandoDireccion.Parameters.AddWithValue("@codigoPostal", paciente.CodigoPostal);
                comandoDireccion.ExecuteNonQuery();
            }

            if (!string.IsNullOrWhiteSpace(paciente.Telefono))
            {
                string insertarTelefono = @"
                    INSERT INTO TelefonoPersona
                    (id_persona, telefono)
                    VALUES
                    (@idPersona, @telefono)";

                MySqlCommand comandoTelefono = new MySqlCommand(insertarTelefono, conexion, transaccion);
                comandoTelefono.Parameters.AddWithValue("@idPersona", idPersona);
                comandoTelefono.Parameters.AddWithValue("@telefono", paciente.Telefono);
                comandoTelefono.ExecuteNonQuery();
            }

            if (!string.IsNullOrWhiteSpace(paciente.Correo))
            {
                string insertarCorreo = @"
                    INSERT INTO CorreoPersona
                    (id_persona, correo)
                    VALUES
                    (@idPersona, @correo)";

                MySqlCommand comandoCorreo = new MySqlCommand(insertarCorreo, conexion, transaccion);
                comandoCorreo.Parameters.AddWithValue("@idPersona", idPersona);
                comandoCorreo.Parameters.AddWithValue("@correo", paciente.Correo);
                comandoCorreo.ExecuteNonQuery();
            }
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
            ,
            co.correo

        FROM Persona p

        INNER JOIN Paciente pa
            ON p.id_persona = pa.id_persona

        LEFT JOIN DireccionPersona d
            ON p.id_persona = d.id_persona

        LEFT JOIN TelefonoPersona t
            ON p.id_persona = t.id_persona

        LEFT JOIN CorreoPersona co
            ON p.id_persona = co.id_persona

        WHERE p.id_persona = @idPaciente";

                MySqlCommand comando =
                    new MySqlCommand(consulta, conexion);

                comando.Parameters.AddWithValue(
                    "@idPaciente",
                    idPaciente);

                MySqlDataReader reader =
                    comando.ExecuteReader();

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

                    paciente.ContactoEmergencia =
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

                    paciente.Correo =
                        reader["correo"].ToString();
                }

                reader.Close();

                return paciente;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                conexion.Close();
            }
        }

        public bool ActualizarPaciente(Paciente paciente)
        {
            MySqlConnection conexion = Conexion.obtenerConexion();

            try
            {
                conexion.Open();

                string actualizarPersona = @"
                    UPDATE Persona
                    SET
                        nombre = @nombre,
                        apellido_paterno = @apellidoPaterno,
                        apellido_materno = @apellidoMaterno,
                        fecha_nacimiento = @fechaNacimiento,
                        genero = @genero
                    WHERE id_persona = @idPersona";

                MySqlCommand comandoPersona = new MySqlCommand(actualizarPersona, conexion);
                comandoPersona.Parameters.AddWithValue("@nombre", paciente.Nombre);
                comandoPersona.Parameters.AddWithValue("@apellidoPaterno", paciente.ApellidoPaterno);
                comandoPersona.Parameters.AddWithValue("@apellidoMaterno", paciente.ApellidoMaterno);
                comandoPersona.Parameters.AddWithValue("@fechaNacimiento", paciente.FechaNacimiento);
                comandoPersona.Parameters.AddWithValue("@genero", paciente.Genero);
                comandoPersona.Parameters.AddWithValue("@idPersona", paciente.IdPersona);
                comandoPersona.ExecuteNonQuery();

                string actualizarPaciente = @"
                    UPDATE Paciente
                    SET
                        tipo_sangre = @tipoSangre,
                        alergias = @alergias,
                        contacto_emergencia = @contactoEmergencia,
                        numero_emergencia = @numeroEmergencia
                    WHERE id_persona = @idPersona";

                MySqlCommand comandoPaciente = new MySqlCommand(actualizarPaciente, conexion);
                comandoPaciente.Parameters.AddWithValue("@tipoSangre", paciente.TipoSangre);
                comandoPaciente.Parameters.AddWithValue("@alergias", paciente.Alergias);
                comandoPaciente.Parameters.AddWithValue("@contactoEmergencia", paciente.ContactoEmergencia);
                comandoPaciente.Parameters.AddWithValue("@numeroEmergencia", paciente.NumeroEmergencia);
                comandoPaciente.Parameters.AddWithValue("@idPersona", paciente.IdPersona);
                comandoPaciente.ExecuteNonQuery();

                EjecutarUpsertDatoRelacionado(
                    conexion,
                    "DireccionPersona",
                    "id_direccion",
                    paciente.IdPersona,
                    @"UPDATE DireccionPersona
                      SET calle = @calle, ciudad = @ciudad, estado = @estado, codigo_postal = @codigoPostal
                      WHERE id_persona = @idPersona",
                    @"INSERT INTO DireccionPersona (id_persona, calle, ciudad, estado, codigo_postal)
                      VALUES (@idPersona, @calle, @ciudad, @estado, @codigoPostal)",
                    comando =>
                    {
                        comando.Parameters.AddWithValue("@calle", paciente.Calle);
                        comando.Parameters.AddWithValue("@ciudad", paciente.Ciudad);
                        comando.Parameters.AddWithValue("@estado", paciente.Estado);
                        comando.Parameters.AddWithValue("@codigoPostal", paciente.CodigoPostal);
                    });

                EjecutarUpsertDatoRelacionado(
                    conexion,
                    "TelefonoPersona",
                    "id_telefono",
                    paciente.IdPersona,
                    "UPDATE TelefonoPersona SET telefono = @telefono WHERE id_persona = @idPersona",
                    "INSERT INTO TelefonoPersona (id_persona, telefono) VALUES (@idPersona, @telefono)",
                    comando => comando.Parameters.AddWithValue("@telefono", paciente.Telefono));

                EjecutarUpsertDatoRelacionado(
                    conexion,
                    "CorreoPersona",
                    "id_correo",
                    paciente.IdPersona,
                    "UPDATE CorreoPersona SET correo = @correo WHERE id_persona = @idPersona",
                    "INSERT INTO CorreoPersona (id_persona, correo) VALUES (@idPersona, @correo)",
                    comando => comando.Parameters.AddWithValue("@correo", paciente.Correo));

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                conexion.Close();
            }
        }

        private void EjecutarUpsertDatoRelacionado(
            MySqlConnection conexion,
            string tabla,
            string idColumna,
            int idPersona,
            string updateSql,
            string insertSql,
            Action<MySqlCommand> agregarParametros)
        {
            string existeSql = $"SELECT COUNT({idColumna}) FROM {tabla} WHERE id_persona = @idPersona";
            MySqlCommand existeComando = new MySqlCommand(existeSql, conexion);
            existeComando.Parameters.AddWithValue("@idPersona", idPersona);
            int total = Convert.ToInt32(existeComando.ExecuteScalar());

            MySqlCommand comando = new MySqlCommand(total > 0 ? updateSql : insertSql, conexion);
            comando.Parameters.AddWithValue("@idPersona", idPersona);
            agregarParametros(comando);
            comando.ExecuteNonQuery();
        }

    }
}
