using Dental_H.Controller;
using Dental_H.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dental_H.View
{
    public partial class PacienteDetalleForm : Form
    {
        private int idPaciente;

        public PacienteDetalleForm(int idPaciente)
        {
            InitializeComponent();
            this.idPaciente = idPaciente;
            CargarPaciente();
        }
        private void CargarPaciente()
        {
            PacienteController controller =
                new PacienteController();

            Paciente paciente =
                controller.ObtenerPacientePorId(idPaciente);

            if (paciente == null)
            {
                MessageBox.Show("Paciente no encontrado");
                return;
            }

            txtNombre.Text = paciente.Nombre;
            txtApellidoPaterno.Text = paciente.ApellidoPaterno;
            txtApellidoMaterno.Text = paciente.ApellidoMaterno;

            dtpFechaNacimiento.Value =
                paciente.FechaNacimiento;

            cmbGenero.Text =
                paciente.Genero;

            cmbTipoSangre.Text =
                paciente.TipoSangre;

            txtAlergias.Text =
                paciente.Alergias;

            txtContactoEmergencia.Text =
                paciente.ContactoEmergencia;

            txtNumeroEmergencia.Text =
                paciente.NumeroEmergencia;

            txtCalle.Text =
                paciente.Calle;

            txtCiudad.Text =
                paciente.Ciudad;

            txtEstado.Text =
                paciente.Estado;

            txtCodigoPostal.Text =
                paciente.CodigoPostal;

            txtTelefono.Text =
                paciente.Telefono;
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
