using Dental_H.DAO;
using Dental_H.Model;
using System.Collections.Generic;

namespace Dental_H.Controller
{
    public class PersonalController
    {
        private PersonalDAO personalDAO;

        public PersonalController()
        {
            personalDAO = new PersonalDAO();
        }

        public List<PersonalInfo> ObtenerPersonal()
        {
            return personalDAO.ObtenerPersonal();
        }

        public List<ComboItem> ObtenerRoles()
        {
            return personalDAO.ObtenerRoles();
        }

        public bool RegistrarPersonal(PersonalInfo personal)
        {
            return personalDAO.RegistrarPersonal(personal);
        }

        public PersonalInfo ObtenerPersonalPorId(int idPersonal)
        {
            return personalDAO.ObtenerPersonalPorId(idPersonal);
        }

        public List<ConsultaInfo> ObtenerTratamientosAsignados(int idPersonal)
        {
            return personalDAO.ObtenerTratamientosAsignados(idPersonal);
        }

        public bool ActualizarPersonal(PersonalInfo personal)
        {
            return personalDAO.ActualizarPersonal(personal);
        }

        public bool EliminarPersonal(int idPersonal)
        {
            return personalDAO.EliminarPersonal(idPersonal);
        }
    }
}
