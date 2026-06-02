using Dental_H.DAO;
using Dental_H.Model;
using System.Collections.Generic;

namespace Dental_H.Controller
{
    public class OdontogramaController
    {
        private readonly OdontogramaDAO odontogramaDAO = new OdontogramaDAO();

        public List<OdontogramaRegistro> ObtenerHistorial(int idPaciente)
        {
            return odontogramaDAO.ObtenerHistorial(idPaciente);
        }

        public OdontogramaRegistro ObtenerUltimoRegistro(int idPaciente, string numeroPieza)
        {
            return odontogramaDAO.ObtenerUltimoRegistro(idPaciente, numeroPieza);
        }

        public bool GuardarRegistro(OdontogramaRegistro registro)
        {
            return odontogramaDAO.GuardarRegistro(registro);
        }

        public bool EliminarRegistro(int idRegistro)
        {
            return odontogramaDAO.EliminarRegistro(idRegistro);
        }
    }
}
