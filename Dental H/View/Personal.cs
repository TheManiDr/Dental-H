using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dental_H.Components;
using Dental_H.Controller;
using Dental_H.Model;

namespace Dental_H.View
{
    public partial class Personal : Form
    {
        public Personal()
        {
            InitializeComponent();
        }

        private void Personal_Load(object sender, EventArgs e)
        {
            CargarPersonal();
        }

        private void CargarPersonal()
        {
            flpPersonal.Controls.Clear();

            PersonalController controller = new PersonalController();
            List<PersonalInfo> personalLista = controller.ObtenerPersonal();

            foreach (PersonalInfo personal in personalLista)
            {
                PersonalCard card = new PersonalCard();

                card.NombrePersonal = personal.Nombre + " " + personal.ApellidoPaterno;
                card.EdadPersonal = CalcularEdad(personal.FechaNacimiento) + " años";
                card.RolPersonal = string.IsNullOrWhiteSpace(personal.Rol) ? "Sin rol" : personal.Rol;
                card.UsuarioPersonal = "Usuario: " + ValorOMensaje(personal.NombreUsuario);
                card.CurpPersonal = "CURP: " + ValorOMensaje(personal.Curp);
                card.RfcPersonal = "RFC: " + ValorOMensaje(personal.Rfc);
                card.NssPersonal = "NSS: " + ValorOMensaje(personal.Nss);

                if (personal.Genero == "Masculino")
                {
                    card.AvatarPersonal = Properties.Resources.avatar_hombre;
                }
                else
                {
                    card.AvatarPersonal = Properties.Resources.avatar_mujer;
                }

                flpPersonal.Controls.Add(card);
            }
        }

        private int CalcularEdad(DateTime fechaNacimiento)
        {
            if (fechaNacimiento == DateTime.MinValue)
            {
                return 0;
            }

            int edad = DateTime.Now.Year - fechaNacimiento.Year;
            if (DateTime.Now < fechaNacimiento.AddYears(edad))
            {
                edad--;
            }

            return edad;
        }

        private string ValorOMensaje(string valor)
        {
            return string.IsNullOrWhiteSpace(valor) ? "Sin dato" : valor;
        }
    }
}
