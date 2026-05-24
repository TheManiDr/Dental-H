using Dental_H.DAO;
using Dental_H.Model;
using Dental_H.Util;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dental_H.Controller
{
    public class PacienteController
    {
        private PersonaDAO personaDAO;
        private PacienteDAO pacienteDAO;

        public PacienteController()
        {
            personaDAO = new PersonaDAO();
            pacienteDAO = new PacienteDAO();
        }

        public bool RegistrarPaciente(Paciente paciente)
        {
            MySqlConnection conexion =
                Conexion.obtenerConexion();

            conexion.Open();

            MySqlTransaction transaccion =
                conexion.BeginTransaction();

            try
            {
                int idPersona = personaDAO.RegistrarPersona(
                        paciente,
                        conexion,
                        transaccion
                    );

                if (idPersona <= 0)
                {
                    transaccion.Rollback();

                    return false;
                }

                bool registrado = pacienteDAO.RegistrarPaciente(
                        idPersona,
                        paciente,
                        conexion,
                        transaccion
                    );

                if (!registrado)
                {
                    transaccion.Rollback();

                    return false;
                }

                transaccion.Commit();

                return true;
            }
            catch
            {
                transaccion.Rollback();

                return false;
            }
            finally
            {
                conexion.Close();
            }
        }
    }
}