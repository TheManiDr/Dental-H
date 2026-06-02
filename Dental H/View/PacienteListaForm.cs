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
            ConfigurarPantallaPacientes();
        }

        private void PacienteListaForm_Load(object sender, EventArgs e)
        {
            CargarPacientes();
        }

        private void CargarPacientes()
        {
            flpPacientes.Controls.Clear();
            flpPacientes.BackColor = Color.FromArgb(245, 247, 250);
            flpPacientes.Controls.Add(CrearTarjetaAgregarPaciente());

            PacienteController controller = new PacienteController();
            List<Paciente> pacientes = controller.ObtenerPacientes();

            foreach (Paciente paciente in pacientes)
            {
                PacienteCard card = new PacienteCard();
                card.NombrePaciente = paciente.Nombre + " " + paciente.ApellidoPaterno + " " + paciente.ApellidoMaterno;
                card.TipoSangre = paciente.TipoSangre;
                card.AlergiasPaciente = paciente.Alergias;
                card.IdPaciente = paciente.IdPersona;
                card.EdadPaciente = "Edad: " + CalcularEdad(paciente.FechaNacimiento) + " años";
                card.Tag = this;

                if (paciente.Genero == "Masculino")
                {
                    card.AvatarPaciente = Properties.Resources.avatar_hombre;
                }
                else
                {
                    card.AvatarPaciente = Properties.Resources.avatar_mujer;
                }
                flpPacientes.Controls.Add(card);
            }
        }

        private void ConfigurarPantallaPacientes()
        {
            panel1.BackColor = Color.FromArgb(245, 247, 250);
            pnlAltaPaciente.Visible = false;
            pnlAltaPaciente.Height = 0;
            flpPacientes.BackColor = Color.FromArgb(245, 247, 250);
            flpPacientes.Padding = new Padding(28, 28, 28, 28);
            flpPacientes.AutoScroll = true;
        }

        private Control CrearTarjetaAgregarPaciente()
        {
            Panel card = new Panel();
            card.BackColor = Color.White;
            card.Size = new Size(270, 340);
            card.Margin = new Padding(10);
            card.Padding = new Padding(18);
            card.BorderStyle = BorderStyle.FixedSingle;
            card.Cursor = Cursors.Hand;

            Label icono = new Label();
            icono.Text = "+";
            icono.Font = new Font("Segoe UI", 44, FontStyle.Bold);
            icono.ForeColor = Color.FromArgb(37, 99, 163);
            icono.TextAlign = ContentAlignment.MiddleCenter;
            icono.Location = new Point(77, 52);
            icono.Size = new Size(116, 92);
            icono.Cursor = Cursors.Hand;

            Label titulo = new Label();
            titulo.Text = "Agregar paciente";
            titulo.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            titulo.ForeColor = Color.FromArgb(28, 65, 111);
            titulo.TextAlign = ContentAlignment.MiddleCenter;
            titulo.Location = new Point(18, 158);
            titulo.Size = new Size(234, 32);
            titulo.Cursor = Cursors.Hand;

            Label subtitulo = new Label();
            subtitulo.Text = "Registrar datos y dejarlo disponible para citas.";
            subtitulo.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            subtitulo.ForeColor = Color.FromArgb(92, 105, 119);
            subtitulo.TextAlign = ContentAlignment.MiddleCenter;
            subtitulo.Location = new Point(24, 196);
            subtitulo.Size = new Size(222, 48);
            subtitulo.Cursor = Cursors.Hand;

            Button btnAgregar = new Button();
            btnAgregar.Text = "Nuevo paciente";
            btnAgregar.BackColor = Color.FromArgb(37, 99, 163);
            btnAgregar.ForeColor = Color.White;
            btnAgregar.FlatStyle = FlatStyle.Flat;
            btnAgregar.FlatAppearance.BorderSize = 0;
            btnAgregar.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            btnAgregar.Location = new Point(40, 292);
            btnAgregar.Size = new Size(190, 34);

            card.Controls.Add(titulo);
            card.Controls.Add(subtitulo);
            card.Controls.Add(icono);
            card.Controls.Add(btnAgregar);

            EventHandler abrirRegistro = (sender, e) => AbrirRegistroPaciente();
            card.Click += abrirRegistro;
            icono.Click += abrirRegistro;
            titulo.Click += abrirRegistro;
            subtitulo.Click += abrirRegistro;
            btnAgregar.Click += abrirRegistro;

            return card;
        }

        private void btnNuevoPaciente_Click(object sender, EventArgs e)
        {
            AbrirRegistroPaciente();
        }

        private void AbrirRegistroPaciente()
        {
            using (NuevoPacienteForm registro = new NuevoPacienteForm())
            {
                if (registro.ShowDialog(this) == DialogResult.OK)
                {
                    CargarPacientes();
                }
            }
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

        private void flpPacientes_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}
