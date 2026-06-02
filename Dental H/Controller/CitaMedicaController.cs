using Dental_H.DAO;
using Dental_H.Model;
using System;
using System.Collections.Generic;

namespace Dental_H.Controller
{
    public class CitaMedicaController
    {
        private CitaMedicaDAO citaMedicaDAO;

        public CitaMedicaController()
        {
            citaMedicaDAO = new CitaMedicaDAO();
        }

        public List<ComboItem> ObtenerPacientes()
        {
            return citaMedicaDAO.ObtenerPacientes();
        }

        public List<ComboItem> ObtenerOdontologos()
        {
            return citaMedicaDAO.ObtenerOdontologos();
        }

        public List<ComboItem> ObtenerTratamientos()
        {
            return citaMedicaDAO.ObtenerTratamientos();
        }

        public bool HoraEstaOcupada(DateTime fecha, TimeSpan hora, int idPersonal)
        {
            return citaMedicaDAO.HoraEstaOcupada(fecha, hora, idPersonal);
        }

        public bool RegistrarCita(DateTime fecha, TimeSpan hora, string motivo, int idPaciente, int idPersonal)
        {
            return citaMedicaDAO.RegistrarCita(fecha, hora, motivo, idPaciente, idPersonal);
        }
    }
}
