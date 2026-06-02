using Dental_H.Model;
using Dental_H.Util;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Dental_H.DAO
{
    public class PersonalDAO
    {
        public List<PersonalInfo> ObtenerPersonal()
        {
            List<PersonalInfo> lista = new List<PersonalInfo>();
            MySqlConnection conexion = Conexion.obtenerConexion();

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
                        pe.curp,
                        pe.rfc,
                        pe.nss,
                        u.nombre_usuario,
                        r.nombre_rol
                    FROM Persona p
                    INNER JOIN Personal pe
                        ON p.id_persona = pe.id_persona
                    LEFT JOIN Usuario u
                        ON p.id_persona = u.id_persona
                    LEFT JOIN Rol r
                        ON u.id_rol = r.id_rol";

                MySqlCommand comando = new MySqlCommand(consulta, conexion);
                MySqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    PersonalInfo personal = new PersonalInfo();

                    personal.IdPersona = Convert.ToInt32(reader["id_persona"]);
                    personal.Nombre = reader["nombre"].ToString();
                    personal.ApellidoPaterno = reader["apellido_paterno"].ToString();
                    personal.ApellidoMaterno = reader["apellido_materno"].ToString();
                    personal.Genero = reader["genero"].ToString();
                    personal.Curp = reader["curp"].ToString();
                    personal.Rfc = reader["rfc"].ToString();
                    personal.Nss = reader["nss"].ToString();
                    personal.NombreUsuario = reader["nombre_usuario"].ToString();
                    personal.Rol = reader["nombre_rol"].ToString();

                    if (!reader.IsDBNull(reader.GetOrdinal("fecha_nacimiento")))
                    {
                        personal.FechaNacimiento = Convert.ToDateTime(reader["fecha_nacimiento"]);
                    }

                    lista.Add(personal);
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

        public List<ComboItem> ObtenerRoles()
        {
            List<ComboItem> roles = new List<ComboItem>();
            MySqlConnection conexion = Conexion.obtenerConexion();

            try
            {
                conexion.Open();

                string consulta = "SELECT id_rol, nombre_rol FROM Rol ORDER BY nombre_rol";
                MySqlCommand comando = new MySqlCommand(consulta, conexion);
                MySqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    roles.Add(new ComboItem(
                        Convert.ToInt32(reader["id_rol"]),
                        reader["nombre_rol"].ToString()));
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

            return roles;
        }

        public PersonalInfo ObtenerPersonalPorId(int idPersonal)
        {
            PersonalInfo personal = null;
            MySqlConnection conexion = Conexion.obtenerConexion();

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
                        pe.curp,
                        pe.rfc,
                        pe.nss,
                        u.nombre_usuario,
                        u.contrasena,
                        u.id_rol,
                        r.nombre_rol,
                        d.calle,
                        d.ciudad,
                        d.estado,
                        d.codigo_postal,
                        t.telefono,
                        co.correo
                    FROM Persona p
                    INNER JOIN Personal pe
                        ON p.id_persona = pe.id_persona
                    LEFT JOIN Usuario u
                        ON p.id_persona = u.id_persona
                    LEFT JOIN Rol r
                        ON u.id_rol = r.id_rol
                    LEFT JOIN DireccionPersona d
                        ON p.id_persona = d.id_persona
                    LEFT JOIN TelefonoPersona t
                        ON p.id_persona = t.id_persona
                    LEFT JOIN CorreoPersona co
                        ON p.id_persona = co.id_persona
                    WHERE p.id_persona = @idPersonal";

                MySqlCommand comando = new MySqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@idPersonal", idPersonal);
                MySqlDataReader reader = comando.ExecuteReader();

                if (reader.Read())
                {
                    personal = new PersonalInfo();
                    personal.IdPersona = Convert.ToInt32(reader["id_persona"]);
                    personal.Nombre = reader["nombre"].ToString();
                    personal.ApellidoPaterno = reader["apellido_paterno"].ToString();
                    personal.ApellidoMaterno = reader["apellido_materno"].ToString();
                    personal.Genero = reader["genero"].ToString();
                    personal.Curp = reader["curp"].ToString();
                    personal.Rfc = reader["rfc"].ToString();
                    personal.Nss = reader["nss"].ToString();
                    personal.NombreUsuario = reader["nombre_usuario"].ToString();
                    personal.Contrasena = reader["contrasena"].ToString();
                    personal.Rol = reader["nombre_rol"].ToString();
                    personal.Calle = reader["calle"].ToString();
                    personal.Ciudad = reader["ciudad"].ToString();
                    personal.Estado = reader["estado"].ToString();
                    personal.CodigoPostal = reader["codigo_postal"].ToString();
                    personal.Telefono = reader["telefono"].ToString();
                    personal.Correo = reader["correo"].ToString();

                    if (!reader.IsDBNull(reader.GetOrdinal("id_rol")))
                    {
                        personal.IdRol = Convert.ToInt32(reader["id_rol"]);
                    }

                    if (!reader.IsDBNull(reader.GetOrdinal("fecha_nacimiento")))
                    {
                        personal.FechaNacimiento = Convert.ToDateTime(reader["fecha_nacimiento"]);
                    }
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

            return personal;
        }

        public List<ConsultaInfo> ObtenerTratamientosAsignados(int idPersonal)
        {
            List<ConsultaInfo> lista = new List<ConsultaInfo>();
            MySqlConnection conexion = Conexion.obtenerConexion();

            try
            {
                conexion.Open();

                string consulta = @"
                    SELECT
                        c.id_cita,
                        c.fecha,
                        c.hora,
                        c.motivo,
                        CONCAT(pp.nombre, ' ', pp.apellido_paterno, ' ', IFNULL(pp.apellido_materno, '')) AS paciente
                    FROM CitaMedica c
                    INNER JOIN Persona pp
                        ON c.id_paciente = pp.id_persona
                    WHERE c.id_personal = @idPersonal
                    ORDER BY c.fecha DESC, c.hora DESC";

                MySqlCommand comando = new MySqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@idPersonal", idPersonal);
                MySqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new ConsultaInfo
                    {
                        IdCita = Convert.ToInt32(reader["id_cita"]),
                        Fecha = Convert.ToDateTime(reader["fecha"]),
                        Hora = (TimeSpan)reader["hora"],
                        Descripcion = reader["motivo"].ToString(),
                        Paciente = reader["paciente"].ToString()
                    });
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

        public bool RegistrarPersonal(PersonalInfo personal)
        {
            MySqlConnection conexion = Conexion.obtenerConexion();
            conexion.Open();
            MySqlTransaction transaccion = conexion.BeginTransaction();

            try
            {
                PersonaDAO personaDAO = new PersonaDAO();
                int idPersona = personaDAO.RegistrarPersona(personal, conexion, transaccion);

                if (idPersona <= 0)
                {
                    transaccion.Rollback();
                    return false;
                }

                string insertarPersonal = @"
                    INSERT INTO Personal
                    (id_persona, curp, rfc, nss)
                    VALUES
                    (@idPersona, @curp, @rfc, @nss)";

                MySqlCommand comandoPersonal = new MySqlCommand(insertarPersonal, conexion, transaccion);
                comandoPersonal.Parameters.AddWithValue("@idPersona", idPersona);
                comandoPersonal.Parameters.AddWithValue("@curp", personal.Curp);
                comandoPersonal.Parameters.AddWithValue("@rfc", personal.Rfc);
                comandoPersonal.Parameters.AddWithValue("@nss", personal.Nss);
                comandoPersonal.ExecuteNonQuery();

                if (!string.IsNullOrWhiteSpace(personal.NombreUsuario) &&
                    !string.IsNullOrWhiteSpace(personal.Contrasena))
                {
                    string insertarUsuario = @"
                        INSERT INTO Usuario
                        (id_persona, id_rol, nombre_usuario, contrasena)
                        VALUES
                        (@idPersona, @idRol, @nombreUsuario, @contrasena)";

                    MySqlCommand comandoUsuario = new MySqlCommand(insertarUsuario, conexion, transaccion);
                    comandoUsuario.Parameters.AddWithValue("@idPersona", idPersona);
                    comandoUsuario.Parameters.AddWithValue("@idRol", personal.IdRol);
                    comandoUsuario.Parameters.AddWithValue("@nombreUsuario", personal.NombreUsuario);
                    comandoUsuario.Parameters.AddWithValue("@contrasena", personal.Contrasena);
                    comandoUsuario.ExecuteNonQuery();
                }

                transaccion.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaccion.Rollback();
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                conexion.Close();
            }
        }

        public bool ActualizarPersonal(PersonalInfo personal)
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
                comandoPersona.Parameters.AddWithValue("@nombre", personal.Nombre);
                comandoPersona.Parameters.AddWithValue("@apellidoPaterno", personal.ApellidoPaterno);
                comandoPersona.Parameters.AddWithValue("@apellidoMaterno", personal.ApellidoMaterno);
                comandoPersona.Parameters.AddWithValue("@fechaNacimiento", personal.FechaNacimiento);
                comandoPersona.Parameters.AddWithValue("@genero", personal.Genero);
                comandoPersona.Parameters.AddWithValue("@idPersona", personal.IdPersona);
                comandoPersona.ExecuteNonQuery();

                string actualizarPersonal = @"
                    UPDATE Personal
                    SET curp = @curp, rfc = @rfc, nss = @nss
                    WHERE id_persona = @idPersona";

                MySqlCommand comandoPersonal = new MySqlCommand(actualizarPersonal, conexion);
                comandoPersonal.Parameters.AddWithValue("@curp", personal.Curp);
                comandoPersonal.Parameters.AddWithValue("@rfc", personal.Rfc);
                comandoPersonal.Parameters.AddWithValue("@nss", personal.Nss);
                comandoPersonal.Parameters.AddWithValue("@idPersona", personal.IdPersona);
                comandoPersonal.ExecuteNonQuery();

                EjecutarUpsertDatoRelacionado(
                    conexion,
                    "DireccionPersona",
                    "id_direccion",
                    personal.IdPersona,
                    @"UPDATE DireccionPersona
                      SET calle = @calle, ciudad = @ciudad, estado = @estado, codigo_postal = @codigoPostal
                      WHERE id_persona = @idPersona",
                    @"INSERT INTO DireccionPersona (id_persona, calle, ciudad, estado, codigo_postal)
                      VALUES (@idPersona, @calle, @ciudad, @estado, @codigoPostal)",
                    comando =>
                    {
                        comando.Parameters.AddWithValue("@calle", personal.Calle);
                        comando.Parameters.AddWithValue("@ciudad", personal.Ciudad);
                        comando.Parameters.AddWithValue("@estado", personal.Estado);
                        comando.Parameters.AddWithValue("@codigoPostal", personal.CodigoPostal);
                    });

                EjecutarUpsertDatoRelacionado(
                    conexion,
                    "TelefonoPersona",
                    "id_telefono",
                    personal.IdPersona,
                    "UPDATE TelefonoPersona SET telefono = @telefono WHERE id_persona = @idPersona",
                    "INSERT INTO TelefonoPersona (id_persona, telefono) VALUES (@idPersona, @telefono)",
                    comando => comando.Parameters.AddWithValue("@telefono", personal.Telefono));

                EjecutarUpsertDatoRelacionado(
                    conexion,
                    "CorreoPersona",
                    "id_correo",
                    personal.IdPersona,
                    "UPDATE CorreoPersona SET correo = @correo WHERE id_persona = @idPersona",
                    "INSERT INTO CorreoPersona (id_persona, correo) VALUES (@idPersona, @correo)",
                    comando => comando.Parameters.AddWithValue("@correo", personal.Correo));

                ActualizarUsuario(conexion, personal);

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

        public bool EliminarPersonal(int idPersonal)
        {
            MySqlConnection conexion = Conexion.obtenerConexion();
            conexion.Open();
            MySqlTransaction transaccion = conexion.BeginTransaction();

            try
            {
                string validarCitas = "SELECT COUNT(*) FROM CitaMedica WHERE id_personal = @idPersonal";
                MySqlCommand validarComando = new MySqlCommand(validarCitas, conexion, transaccion);
                validarComando.Parameters.AddWithValue("@idPersonal", idPersonal);
                int citasAsignadas = Convert.ToInt32(validarComando.ExecuteScalar());

                if (citasAsignadas > 0)
                {
                    MessageBox.Show("No se puede eliminar este personal porque tiene citas asignadas.", "Personal con historial", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    transaccion.Rollback();
                    return false;
                }

                EjecutarDelete(conexion, transaccion, "DELETE FROM Usuario WHERE id_persona = @idPersonal", idPersonal);
                EjecutarDelete(conexion, transaccion, "DELETE FROM DireccionPersona WHERE id_persona = @idPersonal", idPersonal);
                EjecutarDelete(conexion, transaccion, "DELETE FROM TelefonoPersona WHERE id_persona = @idPersonal", idPersonal);
                EjecutarDelete(conexion, transaccion, "DELETE FROM CorreoPersona WHERE id_persona = @idPersonal", idPersonal);
                EjecutarDelete(conexion, transaccion, "DELETE FROM Personal WHERE id_persona = @idPersonal", idPersonal);
                EjecutarDelete(conexion, transaccion, "DELETE FROM Persona WHERE id_persona = @idPersonal", idPersonal);

                transaccion.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaccion.Rollback();
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {
                conexion.Close();
            }
        }

        private void ActualizarUsuario(MySqlConnection conexion, PersonalInfo personal)
        {
            bool tieneCredenciales = !string.IsNullOrWhiteSpace(personal.NombreUsuario) &&
                                     !string.IsNullOrWhiteSpace(personal.Contrasena);

            string existeSql = "SELECT COUNT(id_usuario) FROM Usuario WHERE id_persona = @idPersona";
            MySqlCommand existeComando = new MySqlCommand(existeSql, conexion);
            existeComando.Parameters.AddWithValue("@idPersona", personal.IdPersona);
            int total = Convert.ToInt32(existeComando.ExecuteScalar());

            if (!tieneCredenciales && total > 0)
            {
                MySqlCommand borrar = new MySqlCommand("DELETE FROM Usuario WHERE id_persona = @idPersona", conexion);
                borrar.Parameters.AddWithValue("@idPersona", personal.IdPersona);
                borrar.ExecuteNonQuery();
                return;
            }

            if (!tieneCredenciales)
            {
                return;
            }

            string sql = total > 0
                ? @"UPDATE Usuario
                    SET id_rol = @idRol, nombre_usuario = @nombreUsuario, contrasena = @contrasena
                    WHERE id_persona = @idPersona"
                : @"INSERT INTO Usuario (id_persona, id_rol, nombre_usuario, contrasena)
                    VALUES (@idPersona, @idRol, @nombreUsuario, @contrasena)";

            MySqlCommand comando = new MySqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@idPersona", personal.IdPersona);
            comando.Parameters.AddWithValue("@idRol", personal.IdRol);
            comando.Parameters.AddWithValue("@nombreUsuario", personal.NombreUsuario);
            comando.Parameters.AddWithValue("@contrasena", personal.Contrasena);
            comando.ExecuteNonQuery();
        }

        private void EjecutarDelete(MySqlConnection conexion, MySqlTransaction transaccion, string sql, int idPersonal)
        {
            MySqlCommand comando = new MySqlCommand(sql, conexion, transaccion);
            comando.Parameters.AddWithValue("@idPersonal", idPersonal);
            comando.ExecuteNonQuery();
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
