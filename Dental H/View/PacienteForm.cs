using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Dental_H.Controller;
using Dental_H.Model;

namespace Dental_H.View
{
    public partial class PacienteForm : Form
    {
        public PacienteForm()
        {
            InitializeComponent();
        }

        private void PacienteForm_Load(object sender, EventArgs e)
        {
            cbGenero.Items.Add("Masculino");
            cbGenero.Items.Add("Femenino");

            cbTipoSangre.Items.Add("A+");
            cbTipoSangre.Items.Add("A-");

            cbTipoSangre.Items.Add("B+");
            cbTipoSangre.Items.Add("B-");

            cbTipoSangre.Items.Add("AB+");
            cbTipoSangre.Items.Add("AB-");

            cbTipoSangre.Items.Add("O+");
            cbTipoSangre.Items.Add("O-");
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Paciente paciente = new Paciente();

            paciente.Nombre =
                txtNombre.Text;

            paciente.ApellidoPaterno =
                txtApellidoPaterno.Text;

            paciente.ApellidoMaterno =
                txtApellidoMaterno.Text;

            paciente.FechaNacimiento =
                dtpFechaNacimiento.Value;

            paciente.Genero =
                cbGenero.Text;

            paciente.TipoSangre =
                cbTipoSangre.Text;

            paciente.Alergias =
                txtAlergias.Text;

            paciente.ContactoEmergencia =
                txtContactoEmergencia.Text;

            paciente.NumeroEmergencia =
                txtNumeroEmergencia.Text;

            PacienteController controller =
                new PacienteController();

            bool registrado =
                controller.RegistrarPaciente(
                    paciente
                );

            if (registrado)
            {
                MessageBox.Show(
                    "Paciente registrado correctamente"
                );

                LimpiarFormulario();
            }
            else
            {
                MessageBox.Show(
                    "Error al registrar paciente"
                );
            }
        }
        private void LimpiarFormulario()
        {
            txtNombre.Clear();

            txtApellidoPaterno.Clear();

            txtApellidoMaterno.Clear();

            txtAlergias.Clear();

            txtContactoEmergencia.Clear();

            txtNumeroEmergencia.Clear();

            cbGenero.SelectedIndex = -1;

            cbTipoSangre.SelectedIndex = -1;

            dtpFechaNacimiento.Value =
                DateTime.Now;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }
    }
}
