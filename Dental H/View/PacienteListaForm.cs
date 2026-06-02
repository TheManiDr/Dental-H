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

            PacienteController controller = new PacienteController();
            List<Paciente> pacientes = controller.ObtenerPacientes();

            foreach (Paciente paciente in pacientes)
            {
                PacienteCard card = new PacienteCard();
                card.NombrePaciente = paciente.Nombre + " " + paciente.ApellidoPaterno;
                card.TipoSangre = paciente.TipoSangre;
                card.IdPaciente = paciente.IdPersona;
                card.EdadPaciente = CalcularEdad(paciente.FechaNacimiento) + " años";
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
            pnlAltaPaciente.BackColor = Color.FromArgb(245, 247, 250);
            pnlAltaPaciente.Padding = new Padding(28, 18, 28, 8);
            flpPacientes.BackColor = Color.FromArgb(245, 247, 250);
            flpPacientes.Padding = new Padding(25, 16, 25, 25);

            pnlAltaPaciente.Controls.Clear();
            pnlAltaPaciente.Controls.Add(CrearPanelNuevoPaciente());
        }

        private Control CrearPanelNuevoPaciente()
        {
            Panel card = new Panel();
            card.BackColor = Color.White;
            card.Dock = DockStyle.Fill;
            card.Padding = new Padding(22, 16, 22, 16);

            Label titulo = new Label();
            titulo.Text = "Agregar nuevo paciente";
            titulo.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            titulo.ForeColor = Color.FromArgb(28, 65, 111);
            titulo.Location = new Point(22, 14);
            titulo.Size = new Size(280, 28);

            Label subtitulo = new Label();
            subtitulo.Text = "Registro rapido para que el paciente quede disponible al agendar citas.";
            subtitulo.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            subtitulo.ForeColor = Color.FromArgb(92, 105, 119);
            subtitulo.Location = new Point(22, 42);
            subtitulo.Size = new Size(520, 22);

            TextBox txtNombre = CrearTextBox(22, 88, 165);
            TextBox txtApellidoPaterno = CrearTextBox(202, 88, 165);
            TextBox txtApellidoMaterno = CrearTextBox(382, 88, 165);
            DateTimePicker dtpNacimiento = new DateTimePicker
            {
                Location = new Point(562, 88),
                Size = new Size(160, 26),
                Font = new Font("Segoe UI", 9),
                Format = DateTimePickerFormat.Short
            };
            ComboBox cbGenero = CrearCombo(738, 88, 135, "Masculino", "Femenino");
            ComboBox cbTipoSangre = CrearCombo(888, 88, 92, "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-");
            TextBox txtAlergias = CrearTextBox(995, 88, 150);
            TextBox txtContacto = CrearTextBox(1160, 88, 165);
            TextBox txtNumero = CrearTextBox(1340, 88, 150);

            Button btnGuardarRapido = new Button();
            btnGuardarRapido.Text = "Guardar paciente";
            btnGuardarRapido.BackColor = Color.FromArgb(37, 99, 163);
            btnGuardarRapido.ForeColor = Color.White;
            btnGuardarRapido.FlatStyle = FlatStyle.Flat;
            btnGuardarRapido.FlatAppearance.BorderSize = 0;
            btnGuardarRapido.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnGuardarRapido.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnGuardarRapido.Location = new Point(1510, 86);
            btnGuardarRapido.Size = new Size(180, 30);
            btnGuardarRapido.Click += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    string.IsNullOrWhiteSpace(txtApellidoPaterno.Text) ||
                    cbGenero.SelectedItem == null ||
                    cbTipoSangre.SelectedItem == null)
                {
                    MessageBox.Show("Nombre, apellido paterno, genero y tipo de sangre son obligatorios.", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Paciente paciente = new Paciente();
                paciente.Nombre = txtNombre.Text.Trim();
                paciente.ApellidoPaterno = txtApellidoPaterno.Text.Trim();
                paciente.ApellidoMaterno = txtApellidoMaterno.Text.Trim();
                paciente.FechaNacimiento = dtpNacimiento.Value;
                paciente.Genero = cbGenero.Text;
                paciente.TipoSangre = cbTipoSangre.Text;
                paciente.Alergias = txtAlergias.Text.Trim();
                paciente.ContactoEmergencia = txtContacto.Text.Trim();
                paciente.NumeroEmergencia = txtNumero.Text.Trim();

                PacienteController controller = new PacienteController();

                if (controller.RegistrarPaciente(paciente))
                {
                    MessageBox.Show("Paciente registrado correctamente. Ya podra seleccionarse al agendar una cita.", "Paciente guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarPacientes();
                }
                else
                {
                    MessageBox.Show("No se pudo registrar el paciente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            card.Controls.Add(titulo);
            card.Controls.Add(subtitulo);
            AgregarEtiqueta(card, "Nombre", 22, 68, 165);
            AgregarEtiqueta(card, "Apellido paterno", 202, 68, 165);
            AgregarEtiqueta(card, "Apellido materno", 382, 68, 165);
            AgregarEtiqueta(card, "Nacimiento", 562, 68, 160);
            AgregarEtiqueta(card, "Genero", 738, 68, 135);
            AgregarEtiqueta(card, "Sangre", 888, 68, 92);
            AgregarEtiqueta(card, "Alergias", 995, 68, 150);
            AgregarEtiqueta(card, "Contacto emergencia", 1160, 68, 165);
            AgregarEtiqueta(card, "Telefono emergencia", 1340, 68, 150);
            card.Controls.Add(txtNombre);
            card.Controls.Add(txtApellidoPaterno);
            card.Controls.Add(txtApellidoMaterno);
            card.Controls.Add(dtpNacimiento);
            card.Controls.Add(cbGenero);
            card.Controls.Add(cbTipoSangre);
            card.Controls.Add(txtAlergias);
            card.Controls.Add(txtContacto);
            card.Controls.Add(txtNumero);
            card.Controls.Add(btnGuardarRapido);

            return card;
        }

        private TextBox CrearTextBox(int x, int y, int width)
        {
            return new TextBox
            {
                Location = new Point(x, y),
                Size = new Size(width, 24),
                Font = new Font("Segoe UI", 9),
                BackColor = Color.FromArgb(248, 250, 252),
                BorderStyle = BorderStyle.FixedSingle
            };
        }

        private ComboBox CrearCombo(int x, int y, int width, params string[] valores)
        {
            ComboBox combo = new ComboBox
            {
                Location = new Point(x, y),
                Size = new Size(width, 24),
                Font = new Font("Segoe UI", 9),
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.FromArgb(248, 250, 252)
            };
            combo.Items.AddRange(valores);
            return combo;
        }

        private void AgregarEtiqueta(Control parent, string texto, int x, int y, int width)
        {
            Label label = new Label
            {
                Text = texto,
                Location = new Point(x, y),
                Size = new Size(width, 18),
                Font = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 65, 85)
            };

            parent.Controls.Add(label);
        }

        private void btnNuevoPaciente_Click(object sender, EventArgs e)
        {
            PacienteForm pacientefrom = new PacienteForm(this);
            AppNavigator.AbrirSecundaria(this, pacientefrom);
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
