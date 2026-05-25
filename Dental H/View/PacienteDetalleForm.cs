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
            CargarDatosPaciente();
        }
        private void CargarDatosPaciente()
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
            MessageBox.Show(paciente.Nombre + " " + paciente.ApellidoPaterno);
        }
    }
}
