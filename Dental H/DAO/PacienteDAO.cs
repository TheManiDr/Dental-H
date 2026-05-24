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

                conexion.Close();

                return filasAfectadas > 0;
            }
            catch
            {
                conexion.Close();

                return false;
            }
        }
    }
}
