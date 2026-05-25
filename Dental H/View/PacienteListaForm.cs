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
            PacienteCard card = new PacienteCard();
            card.NombrePaciente = "Manuel Viveros";
            card.EdadPaciente = "24 años";
            card.TipoSangre = "O+";
            flpPacientes.Controls.Add(card);
        }

        private void btnNuevoPaciente_Click(object sender, EventArgs e)
        {
            PacienteForm pacientefrom = new PacienteForm();
            pacientefrom.Show();
            this.Hide();
        }
    }
}
