using Dental_H.Model;
using Dental_H.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dental_H.Components
{
    public partial class PacienteCard : UserControl
    {
        public int IdPaciente { get; set; }
        public string NombrePaciente
        {
            get => lblNombre.Text;
            set => lblNombre.Text = value;
        }

        public string EdadPaciente
        {
            get => lblEdad.Text;
            set => lblEdad.Text = value;
        }

        public string TipoSangre
        {
            get => lblTipoSangre.Text;
            set => lblTipoSangre.Text = value;
        }
        public PacienteCard()
        {
            InitializeComponent();
            this.BorderStyle = BorderStyle.FixedSingle;
        }

        private void btnVerPerfil_Click(object sender, EventArgs e)
        {
            PacienteDetalleForm detalle = new PacienteDetalleForm(IdPaciente);
            detalle.Show();
        }
    }
}
