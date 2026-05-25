using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dental_H.Model;
using Dental_H.Util;
using MySql.Data.MySqlClient;

namespace Dental_H.DAO
{
    public class PersonaDAO
    {
        public int RegistrarPersona(
            Persona persona,
            MySqlConnection conexion,
            MySqlTransaction transaccion
        )
        {

            try
            {
                string consulta =
                    @"INSERT INTO Persona
                    (
                        nombre,
                        apellido_paterno,
                        apellido_materno,
                        fecha_nacimiento,
                        genero
                    )
                    VALUES
                    (
                        @nombre,
                        @apellidoPaterno,
                        @apellidoMaterno,
                        @fechaNacimiento,
                        @genero
                    );

                    SELECT LAST_INSERT_ID();";

                MySqlCommand comando =
                    new MySqlCommand(
                        consulta,
                        conexion,
                        transaccion
                    );

                comando.Parameters.AddWithValue(
                    "@nombre",
                    persona.Nombre
                );

                comando.Parameters.AddWithValue(
                    "@apellidoPaterno",
                    persona.ApellidoPaterno
                );

                comando.Parameters.AddWithValue(
                    "@apellidoMaterno",
                    persona.ApellidoMaterno
                );

                comando.Parameters.AddWithValue(
                    "@fechaNacimiento",
                    persona.FechaNacimiento
                );

                comando.Parameters.AddWithValue(
                    "@genero",
                    persona.Genero
                );

                int idGenerado =
                    Convert.ToInt32(
                        comando.ExecuteScalar()
                    );


                return idGenerado;
            }
            catch
            {

                return -1;
            }
        }
    }
}