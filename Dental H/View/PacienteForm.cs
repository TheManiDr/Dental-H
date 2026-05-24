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

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Paciente paciente = new Paciente();

            paciente.TipoSangre = txtTipoSangre.Text;

            paciente.Alergias = txtAlergias.Text;

            paciente.NombreEmergencia = txtNombreEmergencia.Text;

            paciente.NumeroEmergencia = txtNumeroEmergencia.Text;

            PacienteController controller = new PacienteController();

            bool registrado = controller.RegistrarPaciente(paciente);

            if (registrado)
            {
                MessageBox.Show("Paciente registrado");
            }
            else
            {
                MessageBox.Show("Error al registrar");
            }
        }
    }
}
