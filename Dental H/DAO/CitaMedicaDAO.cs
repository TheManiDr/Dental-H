using Dental_H.Model;
using Dental_H.Util;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Dental_H.DAO
{
    public class CitaMedicaDAO
    {
        public List<ComboItem> ObtenerPacientes()
        {
            List<ComboItem> pacientes = new List<ComboItem>();
            MySqlConnection conexion = Conexion.obtenerConexion();

            try
            {
                conexion.Open();

                string consulta = @"
                    SELECT
                        p.id_persona,
                        CONCAT(p.nombre, ' ', p.apellido_paterno, ' ', IFNULL(p.apellido_materno, '')) AS nombre_completo
                    FROM Persona p
                    INNER JOIN Paciente pa
                        ON p.id_persona = pa.id_persona
                    ORDER BY p.nombre, p.apellido_paterno";

                MySqlCommand comando = new MySqlCommand(consulta, conexion);
                MySqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    pacientes.Add(new ComboItem(
                        Convert.ToInt32(reader["id_persona"]),
                        reader["nombre_completo"].ToString().Trim()
                    ));
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

            return pacientes;
        }

        public List<ComboItem> ObtenerOdontologos()
        {
            List<ComboItem> odontologos = new List<ComboItem>();
            MySqlConnection conexion = Conexion.obtenerConexion();

            try
            {
                conexion.Open();

                string consulta = @"
                    SELECT
                        p.id_persona,
                        CONCAT(p.nombre, ' ', p.apellido_paterno, ' ', IFNULL(p.apellido_materno, '')) AS nombre_completo,
                        r.nombre_rol
                    FROM Persona p
                    INNER JOIN Personal pe
                        ON p.id_persona = pe.id_persona
                    INNER JOIN Usuario u
                        ON p.id_persona = u.id_persona
                    INNER JOIN Rol r
                        ON u.id_rol = r.id_rol
                    WHERE r.nombre_rol = 'Dentista'
                    ORDER BY p.nombre, p.apellido_paterno";

                MySqlCommand comando = new MySqlCommand(consulta, conexion);
                MySqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    string texto = reader["nombre_completo"].ToString().Trim() + " (" + reader["nombre_rol"] + ")";
                    odontologos.Add(new ComboItem(Convert.ToInt32(reader["id_persona"]), texto));
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

            return odontologos;
        }

        public List<ComboItem> ObtenerTratamientos()
        {
            List<ComboItem> tratamientos = new List<ComboItem>();
            MySqlConnection conexion = Conexion.obtenerConexion();

            try
            {
                conexion.Open();

                string consulta = @"
                    SELECT id_tratamiento, nombre
                    FROM Tratamiento
                    ORDER BY nombre";

                MySqlCommand comando = new MySqlCommand(consulta, conexion);
                MySqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    tratamientos.Add(new ComboItem(
                        Convert.ToInt32(reader["id_tratamiento"]),
                        reader["nombre"].ToString()
                    ));
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

            return tratamientos;
        }

        public bool HoraEstaOcupada(DateTime fecha, TimeSpan hora, int idPersonal)
        {
            MySqlConnection conexion = Conexion.obtenerConexion();

            try
            {
                conexion.Open();

                string consulta = @"
                    SELECT COUNT(*)
                    FROM CitaMedica
                    WHERE fecha = @fecha
                        AND hora = @hora
                        AND id_personal = @idPersonal";

                MySqlCommand comando = new MySqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@fecha", fecha.Date);
                comando.Parameters.AddWithValue("@hora", hora);
                comando.Parameters.AddWithValue("@idPersonal", idPersonal);

                int total = Convert.ToInt32(comando.ExecuteScalar());
                return total > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return true;
            }
            finally
            {
                conexion.Close();
            }
        }

        public bool RegistrarCita(DateTime fecha, TimeSpan hora, string motivo, int idPaciente, int idPersonal)
        {
            MySqlConnection conexion = Conexion.obtenerConexion();

            try
            {
                conexion.Open();

                string consulta = @"
                    INSERT INTO CitaMedica
                    (
                        fecha,
                        hora,
                        motivo,
                        id_paciente,
                        id_personal
                    )
                    VALUES
                    (
                        @fecha,
                        @hora,
                        @motivo,
                        @idPaciente,
                        @idPersonal
                    )";

                MySqlCommand comando = new MySqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@fecha", fecha.Date);
                comando.Parameters.AddWithValue("@hora", hora);
                comando.Parameters.AddWithValue("@motivo", motivo);
                comando.Parameters.AddWithValue("@idPaciente", idPaciente);
                comando.Parameters.AddWithValue("@idPersonal", idPersonal);

                return comando.ExecuteNonQuery() > 0;
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
    }
}
