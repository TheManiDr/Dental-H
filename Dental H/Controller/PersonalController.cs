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
    }
}
