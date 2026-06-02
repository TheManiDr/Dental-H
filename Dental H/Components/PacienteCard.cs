using Dental_H.Model;
using Dental_H.Util;
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
        private Label lblAlergias;

        public int IdPaciente { get; set; }
        public Image AvatarPaciente
        {
            get => picAvatar.Image;
            set => picAvatar.Image = value;
        }
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
            set => lblTipoSangre.Text = "Sangre: " + (string.IsNullOrWhiteSpace(value) ? "Sin dato" : value);
        }

        public string AlergiasPaciente
        {
            get => lblAlergias.Text;
            set => lblAlergias.Text = "Alergias: " + (string.IsNullOrWhiteSpace(value) ? "Ninguna" : value);
        }

        public PacienteCard()
        {
            InitializeComponent();
            ConfigurarEstilo();
        }

        private void ConfigurarEstilo()
        {
            this.Size = new Size(270, 340);
            this.Margin = new Padding(10);
            this.Padding = new Padding(16);
            this.BackColor = Color.White;
            this.BorderStyle = BorderStyle.FixedSingle;

            picAvatar.Location = new Point(85, 22);
            picAvatar.Size = new Size(100, 100);
            picAvatar.SizeMode = PictureBoxSizeMode.Zoom;

            lblNombre.AutoSize = false;
            lblNombre.Location = new Point(18, 136);
            lblNombre.Size = new Size(234, 46);
            lblNombre.Font = new Font("Segoe UI", 10.5f, FontStyle.Bold);
            lblNombre.ForeColor = Color.FromArgb(28, 65, 111);
            lblNombre.TextAlign = ContentAlignment.MiddleCenter;

            lblEdad.AutoSize = false;
            lblEdad.Location = new Point(18, 188);
            lblEdad.Size = new Size(234, 22);
            lblEdad.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            lblEdad.ForeColor = Color.FromArgb(37, 99, 163);
            lblEdad.TextAlign = ContentAlignment.MiddleCenter;

            lblTipoSangre.AutoSize = false;
            lblTipoSangre.Location = new Point(18, 214);
            lblTipoSangre.Size = new Size(234, 22);
            lblTipoSangre.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            lblTipoSangre.ForeColor = Color.FromArgb(51, 65, 85);
            lblTipoSangre.TextAlign = ContentAlignment.MiddleCenter;

            lblAlergias = new Label
            {
                AutoSize = false,
                Location = new Point(18, 240),
                Size = new Size(234, 36),
                Font = new Font("Segoe UI", 8.5f),
                ForeColor = Color.FromArgb(92, 105, 119),
                TextAlign = ContentAlignment.MiddleCenter
            };

            btnVerPerfil.Location = new Point(40, 292);
            btnVerPerfil.Size = new Size(190, 34);
            btnVerPerfil.Text = "Ver expediente";
            btnVerPerfil.BackColor = Color.FromArgb(37, 99, 163);
            btnVerPerfil.ForeColor = Color.White;
            btnVerPerfil.FlatStyle = FlatStyle.Flat;
            btnVerPerfil.FlatAppearance.BorderSize = 0;
            btnVerPerfil.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);

            this.Controls.Add(lblAlergias);
        }

        private void btnVerPerfil_Click(object sender, EventArgs e)
        {
            Form listaPadre = this.Tag as Form;
            PacienteDetalleForm detalle = new PacienteDetalleForm(IdPaciente, listaPadre);
            AppNavigator.AbrirSecundaria(listaPadre, detalle);
        }
    }
}
