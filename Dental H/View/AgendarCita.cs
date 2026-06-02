using Dental_H.Controller;
using Dental_H.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Dental_H.View
{
    public partial class AgendarCita : Form
    {
        private ComboBox cmbPaciente;
        private DateTimePicker dtpFechaCita;
        private ComboBox cmbHoraCita;
        private ComboBox cmbOdontologo;
        private ComboBox cmbTratamiento;
        private TextBox txtNotas;
        private Button btnGuardar;
        private Button btnCancelar;
        private CitaMedicaController citaMedicaController;

        public AgendarCita()
        {
            citaMedicaController = new CitaMedicaController();
            InitializeComponent();
            ConfigurarEstiloVentana();
            ConstruirInterfazGraficaDistribuda();
        }

        private void AgendarCita_Load(object sender, EventArgs e)
        {
            CargarPacientes();
            CargarOdontologos();
            CargarTratamientos();
            CargarHorariosDisponibles();
        }

        private void ConfigurarEstiloVentana()
        {
            Text = "Agendar Nueva Cita Medica";
            WindowState = FormWindowState.Maximized;
            BackColor = Color.FromArgb(240, 244, 248);
        }

        private void ConstruirInterfazGraficaDistribuda()
        {
            Font fuenteLabels = new Font("Segoe UI", 12, FontStyle.Bold);
            Font fuenteControles = new Font("Segoe UI", 13, FontStyle.Regular);

            TableLayoutPanel contenedorPrincipal = new TableLayoutPanel();
            contenedorPrincipal.ColumnCount = 1;
            contenedorPrincipal.RowCount = 7;
            contenedorPrincipal.Dock = DockStyle.Fill;
            contenedorPrincipal.Padding = new Padding(100, 40, 100, 40);
            contenedorPrincipal.BackColor = Color.White;

            contenedorPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 14f));
            contenedorPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 14f));
            contenedorPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 14f));
            contenedorPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 14f));
            contenedorPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 14f));
            contenedorPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 20f));
            contenedorPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 10f));

            Panel pnlPaciente = new Panel { Dock = DockStyle.Fill };
            Label lblPaciente = new Label { Text = "Seleccionar Paciente:", Location = new Point(0, 0), AutoSize = true, Font = fuenteLabels, ForeColor = Color.FromArgb(44, 62, 80) };
            cmbPaciente = new ComboBox { Location = new Point(0, 28), Dock = DockStyle.Bottom, Font = fuenteControles, DropDownStyle = ComboBoxStyle.DropDownList };
            pnlPaciente.Controls.AddRange(new Control[] { lblPaciente, cmbPaciente });

            Panel pnlOdontologo = new Panel { Dock = DockStyle.Fill };
            Label lblOdontologo = new Label { Text = "Odontologo / Especialista Asignado:", Location = new Point(0, 0), AutoSize = true, Font = fuenteLabels, ForeColor = Color.FromArgb(44, 62, 80) };
            cmbOdontologo = new ComboBox { Location = new Point(0, 28), Dock = DockStyle.Bottom, Font = fuenteControles, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbOdontologo.SelectedIndexChanged += (s, e) => CargarHorariosDisponibles();
            pnlOdontologo.Controls.AddRange(new Control[] { lblOdontologo, cmbOdontologo });

            Panel pnlFecha = new Panel { Dock = DockStyle.Fill };
            Label lblFecha = new Label { Text = "Fecha Programada de la Cita:", Location = new Point(0, 0), AutoSize = true, Font = fuenteLabels, ForeColor = Color.FromArgb(44, 62, 80) };
            dtpFechaCita = new DateTimePicker { Location = new Point(0, 28), Dock = DockStyle.Bottom, Font = fuenteControles, Format = DateTimePickerFormat.Long };
            dtpFechaCita.ValueChanged += (s, e) => CargarHorariosDisponibles();
            pnlFecha.Controls.AddRange(new Control[] { lblFecha, dtpFechaCita });

            Panel pnlHora = new Panel { Dock = DockStyle.Fill };
            Label lblHora = new Label { Text = "Horarios Disponibles Consultorio (7:00 AM - 3:00 PM):", Location = new Point(0, 0), AutoSize = true, Font = fuenteLabels, ForeColor = Color.FromArgb(44, 62, 80) };
            cmbHoraCita = new ComboBox { Location = new Point(0, 28), Dock = DockStyle.Bottom, Font = fuenteControles, DropDownStyle = ComboBoxStyle.DropDownList };
            pnlHora.Controls.AddRange(new Control[] { lblHora, cmbHoraCita });

            Panel pnlTratamiento = new Panel { Dock = DockStyle.Fill };
            Label lblTratamiento = new Label { Text = "Motivo Clinico / Tratamiento:", Location = new Point(0, 0), AutoSize = true, Font = fuenteLabels, ForeColor = Color.FromArgb(44, 62, 80) };
            cmbTratamiento = new ComboBox { Location = new Point(0, 28), Dock = DockStyle.Bottom, Font = fuenteControles, DropDownStyle = ComboBoxStyle.DropDownList };
            pnlTratamiento.Controls.AddRange(new Control[] { lblTratamiento, cmbTratamiento });

            Panel pnlNotas = new Panel { Dock = DockStyle.Fill };
            Label lblNotas = new Label { Text = "Notas Adicionales / Cuadro Sintomatico Inicial:", Location = new Point(0, 0), AutoSize = true, Font = fuenteLabels, ForeColor = Color.FromArgb(44, 62, 80) };
            txtNotas = new TextBox { Location = new Point(0, 28), Height = 65, Dock = DockStyle.Bottom, Font = fuenteControles, Multiline = true, ScrollBars = ScrollBars.Vertical };
            pnlNotas.Controls.AddRange(new Control[] { lblNotas, txtNotas });

            TableLayoutPanel pnlBotones = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1 };
            pnlBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            pnlBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));

            btnCancelar = new Button { Text = "Cancelar Registro", Dock = DockStyle.Fill, Margin = new Padding(10), Font = fuenteLabels, BackColor = Color.FromArgb(231, 76, 60), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.Click += (s, e) => Close();

            btnGuardar = new Button { Text = "Agendar Cita Medica", Dock = DockStyle.Fill, Margin = new Padding(10), Font = fuenteLabels, BackColor = Color.FromArgb(46, 204, 113), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.Click += BtnGuardar_Click;

            pnlBotones.Controls.Add(btnCancelar, 0, 0);
            pnlBotones.Controls.Add(btnGuardar, 1, 0);

            contenedorPrincipal.Controls.Add(pnlPaciente, 0, 0);
            contenedorPrincipal.Controls.Add(pnlOdontologo, 0, 1);
            contenedorPrincipal.Controls.Add(pnlFecha, 0, 2);
            contenedorPrincipal.Controls.Add(pnlHora, 0, 3);
            contenedorPrincipal.Controls.Add(pnlTratamiento, 0, 4);
            contenedorPrincipal.Controls.Add(pnlNotas, 0, 5);
            contenedorPrincipal.Controls.Add(pnlBotones, 0, 6);

            Controls.Add(contenedorPrincipal);
            headerControl1.BringToFront();
        }

        private void CargarPacientes()
        {
            List<ComboItem> pacientes = citaMedicaController.ObtenerPacientes();
            cmbPaciente.DataSource = null;
            cmbPaciente.DisplayMember = "Texto";
            cmbPaciente.ValueMember = "Id";
            cmbPaciente.DataSource = pacientes;

            if (cmbPaciente.Items.Count > 0)
            {
                cmbPaciente.SelectedIndex = 0;
            }
        }

        private void CargarOdontologos()
        {
            List<ComboItem> odontologos = citaMedicaController.ObtenerOdontologos();
            cmbOdontologo.DataSource = null;
            cmbOdontologo.DisplayMember = "Texto";
            cmbOdontologo.ValueMember = "Id";
            cmbOdontologo.DataSource = odontologos;

            if (cmbOdontologo.Items.Count > 0)
            {
                cmbOdontologo.SelectedIndex = 0;
            }
        }

        private void CargarTratamientos()
        {
            List<ComboItem> tratamientos = citaMedicaController.ObtenerTratamientos();
            cmbTratamiento.DataSource = null;
            cmbTratamiento.DisplayMember = "Texto";
            cmbTratamiento.ValueMember = "Id";
            cmbTratamiento.DataSource = tratamientos;

            if (cmbTratamiento.Items.Count > 0)
            {
                cmbTratamiento.SelectedIndex = 0;
            }
        }

        private void CargarHorariosDisponibles()
        {
            if (cmbHoraCita == null || cmbOdontologo == null || cmbOdontologo.SelectedItem == null)
            {
                return;
            }

            cmbHoraCita.Items.Clear();
            ComboItem odontologo = (ComboItem)cmbOdontologo.SelectedItem;

            for (int h = 7; h <= 15; h++)
            {
                string horaString = $"{h:D2}:00";
                TimeSpan hora = TimeSpan.Parse(horaString);

                if (!citaMedicaController.HoraEstaOcupada(dtpFechaCita.Value, hora, odontologo.Id))
                {
                    cmbHoraCita.Items.Add(horaString);
                }
            }

            if (cmbHoraCita.Items.Count > 0)
            {
                cmbHoraCita.SelectedIndex = 0;
            }
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (cmbPaciente.SelectedItem == null || cmbOdontologo.SelectedItem == null || cmbHoraCita.SelectedItem == null || cmbTratamiento.SelectedItem == null)
            {
                MessageBox.Show("Por favor, complete todos los campos obligatorios antes de agendar.", "Campos Vacios", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ComboItem paciente = (ComboItem)cmbPaciente.SelectedItem;
            ComboItem doctor = (ComboItem)cmbOdontologo.SelectedItem;
            ComboItem tratamiento = (ComboItem)cmbTratamiento.SelectedItem;
            string hora = cmbHoraCita.SelectedItem.ToString();
            string motivo = tratamiento.Texto;

            if (!string.IsNullOrWhiteSpace(txtNotas.Text))
            {
                motivo += " - " + txtNotas.Text.Trim();
            }

            bool guardada = citaMedicaController.RegistrarCita(
                dtpFechaCita.Value,
                TimeSpan.Parse(hora),
                motivo,
                paciente.Id,
                doctor.Id
            );

            if (!guardada)
            {
                MessageBox.Show("No se pudo agendar la cita. Revise la conexion o los datos seleccionados.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show($"Cita agendada con exito.\n\nPaciente: {paciente.Texto}\nDoctor: {doctor.Texto}\nFecha: {dtpFechaCita.Value.ToShortDateString()} a las {hora}\nMotivo: {motivo}",
                "Cita Confirmada", MessageBoxButtons.OK, MessageBoxIcon.Information);

            DialogResult = DialogResult.OK;
            Close();
        }

        private void headerControl1_Load(object sender, EventArgs e)
        {
        }
    }
}
