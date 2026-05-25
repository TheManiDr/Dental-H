using Dental_H.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dental_H.Util;
using Dental_H.Enums;
using Dental_H.Controller;
using Dental_H.Model;
using Dental_H.View;

namespace Dental_H.View
{
    public partial class PacienteListaForm : Form
    {
        public PacienteListaForm()
        {
            InitializeComponent();
        }
        private void PacienteListaForm_Load(object sender, EventArgs e)
        {
            CargarPacientes();
        }



        private void CargarPacientes()
        {
            flpPacientes.Controls.Clear();

            PacienteController controller =
                new PacienteController();

            List<Paciente> pacientes =
                controller.ObtenerPacientes();

            foreach (Paciente paciente in pacientes)
            {
                PacienteCard card =
                    new PacienteCard();

                card.NombrePaciente =
                    paciente.Nombre + " " +
                    paciente.ApellidoPaterno;

                card.TipoSangre =
                    paciente.TipoSangre;

                card.IdPaciente = paciente.IdPersona;

                card.EdadPaciente = CalcularEdad(paciente.FechaNacimiento) + " años";

                if (paciente.Genero == "Masculino")
                {
                    card.AvatarPaciente =
                        Properties.Resources.avatar_hombre;
                }
                else
                {
                    card.AvatarPaciente =
                        Properties.Resources.avatar_mujer;
                }
                flpPacientes.Controls.Add(card);
            }
        }

        private void btnNuevoPaciente_Click(object sender, EventArgs e)
        {
            PacienteForm pacientefrom = new PacienteForm();
            pacientefrom.Show();
            this.Hide();
        }
        private int CalcularEdad(DateTime fechaNacimiento)
        {
            int edad = DateTime.Now.Year - fechaNacimiento.Year;

            if (DateTime.Now < fechaNacimiento.AddYears(edad))
            {
                edad--;
            }

            return edad;
        }
    }
}
