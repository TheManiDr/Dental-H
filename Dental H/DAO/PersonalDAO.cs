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
    }
}
