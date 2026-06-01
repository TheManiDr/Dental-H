using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Dental_H.View
{
    public partial class AgendarCita : Form
    {
        // Controles principales del formulario
        private ComboBox cmbPaciente;
        private DateTimePicker dtpFechaCita;
        private ComboBox cmbHoraCita;
        private ComboBox cmbOdontologo;
        private ComboBox cmbTratamiento;
        private TextBox txtNotas;
        private Button btnGuardar;
        private Button btnCancelar;

        public AgendarCita()
        {
            InitializeComponent();
            ConfigurarEstiloVentana();
            ConstruirInterfazGraficaDistribuda();
        }

        private void AgendarCita_Load(object sender, EventArgs e)
        {
            CargarPacientes();
            CargarOdontologos();
            CargarHorariosDisponibles();
            CargarTratamientos();
        }

        /// <summary>
        /// Configura el formulario para que se adapte perfectamente a pantalla completa si es necesario,
        /// manteniendo un fondo limpio.
        /// </summary>
        private void ConfigurarEstiloVentana()
        {
            this.Text = "📅 Agendar Nueva Cita Médica";
            this.WindowState = FormWindowState.Maximized; // Ocupa toda la pantalla de forma fluida
            this.BackColor = Color.FromArgb(240, 244, 248); // Fondo gris muy claro médico
        }

        /// <summary>
        /// Crea una estructura de contenedores dinámicos para rellenar la pantalla de forma simétrica.
        /// </summary>
        private void ConstruirInterfazGraficaDistribuda()
        {
            // Fuentes tipográficas de alta legibilidad y escala corporativa
            Font fuenteLabels = new Font("Segoe UI", 12, FontStyle.Bold);
            Font fuenteControles = new Font("Segoe UI", 13, FontStyle.Regular);

            // 1. CONTENEDOR CENTRAL (Centra todo el contenido para que no se vea vacío)
            TableLayoutPanel contenedorPrincipal = new TableLayoutPanel();
            contenedorPrincipal.ColumnCount = 1;
            contenedorPrincipal.RowCount = 7;
            contenedorPrincipal.Dock = DockStyle.Fill;
            contenedorPrincipal.Padding = new Padding(100, 40, 100, 40); // Márgenes generosos a los lados
            contenedorPrincipal.BackColor = Color.White;

            // Configurar alturas de fila proporcionales para distribuir el espacio verticalmente
            contenedorPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 14f)); // Paciente
            contenedorPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 14f)); // Odontólogo
            contenedorPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 14f)); // Fecha
            contenedorPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 14f)); // Hora
            contenedorPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 14f)); // Tratamiento
            contenedorPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 20f)); // Notas
            contenedorPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 10f)); // Botones de acción

            // ---- PANEL 1: PACIENTE ----
            Panel pnlPaciente = new Panel { Dock = DockStyle.Fill };
            Label lblPaciente = new Label { Text = "Seleccionar Paciente:", Location = new Point(0, 0), AutoSize = true, Font = fuenteLabels, ForeColor = Color.FromArgb(44, 62, 80) };
            cmbPaciente = new ComboBox { Location = new Point(0, 28), Dock = DockStyle.Bottom, Font = fuenteControles, DropDownStyle = ComboBoxStyle.DropDownList };
            pnlPaciente.Controls.AddRange(new Control[] { lblPaciente, cmbPaciente });

            // ---- PANEL 2: ODONTÓLOGO ----
            Panel pnlOdontologo = new Panel { Dock = DockStyle.Fill };
            Label lblOdontologo = new Label { Text = "Odontólogo / Especialista Asignado:", Location = new Point(0, 0), AutoSize = true, Font = fuenteLabels, ForeColor = Color.FromArgb(44, 62, 80) };
            cmbOdontologo = new ComboBox { Location = new Point(0, 28), Dock = DockStyle.Bottom, Font = fuenteControles, DropDownStyle = ComboBoxStyle.DropDownList };
            pnlOdontologo.Controls.AddRange(new Control[] { lblOdontologo, cmbOdontologo });

            // ---- PANEL 3: FECHA ----
            Panel pnlFecha = new Panel { Dock = DockStyle.Fill };
            Label lblFecha = new Label { Text = "Fecha Programada de la Cita:", Location = new Point(0, 0), AutoSize = true, Font = fuenteLabels, ForeColor = Color.FromArgb(44, 62, 80) };
            dtpFechaCita = new DateTimePicker { Location = new Point(0, 28), Dock = DockStyle.Bottom, Font = fuenteControles, Format = DateTimePickerFormat.Long };
            dtpFechaCita.ValueChanged += (s, e) => CargarHorariosDisponibles();
            pnlFecha.Controls.AddRange(new Control[] { lblFecha, dtpFechaCita });

            // ---- PANEL 4: HORA ----
            Panel pnlHora = new Panel { Dock = DockStyle.Fill };
            Label lblHora = new Label { Text = "Horarios Disponibles Consultorio (7:00 AM - 3:00 PM):", Location = new Point(0, 0), AutoSize = true, Font = fuenteLabels, ForeColor = Color.FromArgb(44, 62, 80) };
            cmbHoraCita = new ComboBox { Location = new Point(0, 28), Dock = DockStyle.Bottom, Font = fuenteControles, DropDownStyle = ComboBoxStyle.DropDownList };
            pnlHora.Controls.AddRange(new Control[] { lblHora, cmbHoraCita });

            // ---- PANEL 5: TRATAMIENTO ----
            Panel pnlTratamiento = new Panel { Dock = DockStyle.Fill };
            Label lblTratamiento = new Label { Text = "Motivo Clínico / Tratamiento:", Location = new Point(0, 0), AutoSize = true, Font = fuenteLabels, ForeColor = Color.FromArgb(44, 62, 80) };
            cmbTratamiento = new ComboBox { Location = new Point(0, 28), Dock = DockStyle.Bottom, Font = fuenteControles, DropDownStyle = ComboBoxStyle.DropDownList };
            pnlTratamiento.Controls.AddRange(new Control[] { lblTratamiento, cmbTratamiento });

            // ---- PANEL 6: NOTAS ----
            Panel pnlNotas = new Panel { Dock = DockStyle.Fill };
            Label lblNotas = new Label { Text = "Notas Adicionales / Cuadro Sintomático Inicial:", Location = new Point(0, 0), AutoSize = true, Font = fuenteLabels, ForeColor = Color.FromArgb(44, 62, 80) };
            txtNotas = new TextBox { Location = new Point(0, 28), Height = 65, Dock = DockStyle.Bottom, Font = fuenteControles, Multiline = true, ScrollBars = ScrollBars.Vertical };
            pnlNotas.Controls.AddRange(new Control[] { lblNotas, txtNotas });

            // ---- PANEL 7: BOTONES (Divididos al 50% del ancho inferior de la pantalla) ----
            TableLayoutPanel pnlBotones = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 1 };
            pnlBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            pnlBotones.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));

            btnCancelar = new Button { Text = "❌ Cancelar Registro", Dock = DockStyle.Fill, Margin = new Padding(10), Font = fuenteLabels, BackColor = Color.FromArgb(231, 76, 60), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.Click += (s, e) => this.Close();

            btnGuardar = new Button { Text = "💾 Agendar Cita Médica", Dock = DockStyle.Fill, Margin = new Padding(10), Font = fuenteLabels, BackColor = Color.FromArgb(46, 204, 113), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.Click += BtnGuardar_Click;

            pnlBotones.Controls.Add(btnCancelar, 0, 0);
            pnlBotones.Controls.Add(btnGuardar, 1, 0);

            // Agregar celdas ordenadas al contenedor principal
            contenedorPrincipal.Controls.Add(pnlPaciente, 0, 0);
            contenedorPrincipal.Controls.Add(pnlOdontologo, 0, 1);
            contenedorPrincipal.Controls.Add(pnlFecha, 0, 2);
            contenedorPrincipal.Controls.Add(pnlHora, 0, 3);
            contenedorPrincipal.Controls.Add(pnlTratamiento, 0, 4);
            contenedorPrincipal.Controls.Add(pnlNotas, 0, 5);
            contenedorPrincipal.Controls.Add(pnlBotones, 0, 6);

            // Inyectar el contenedor en la ventana activa respetando los menús base si existen
            this.Controls.Add(contenedorPrincipal);
            contenedorPrincipal.BringToFront();
        }

        #region Carga de Datos Clínicos

        private void CargarPacientes()
        {
            cmbPaciente.Items.Clear();
            cmbPaciente.Items.AddRange(new string[] { "Carlos Gómez", "Ana Martínez", "Juan Pérez", "Sofía Rodríguez" });
            if (cmbPaciente.Items.Count > 0) cmbPaciente.SelectedIndex = 0;
        }

        private void CargarOdontologos()
        {
            cmbOdontologo.Items.Clear();
            cmbOdontologo.Items.AddRange(new string[] { "Dr. Alejandro Soto (General)", "Dra. Elena Rostova (Ortodoncia)" });
            if (cmbOdontologo.Items.Count > 0) cmbOdontologo.SelectedIndex = 0;
        }

        private void CargarHorariosDisponibles()
        {
            cmbHoraCita.Items.Clear();
            int horaInicio = 7;
            int horaFin = 15;

            for (int h = horaInicio; h <= horaFin; h++)
            {
                string horaString = $"{h:D2}:00";
                bool estaOcupado = ValidarSiHoraEstaOcupada(dtpFechaCita.Value, horaString, cmbOdontologo.Text);

                if (!estaOcupado)
                {
                    cmbHoraCita.Items.Add(horaString);
                }
            }

            if (cmbHoraCita.Items.Count > 0) cmbHoraCita.SelectedIndex = 0;
        }

        private void CargarTratamientos()
        {
            cmbTratamiento.Items.Clear();
            cmbTratamiento.Items.AddRange(new string[] { "Limpieza Dental", "Resina / Calza", "Extracción Simple", "Revisión Ortodoncia", "Evaluación General" });
            if (cmbTratamiento.Items.Count > 0) cmbTratamiento.SelectedIndex = 0;
        }

        private bool ValidarSiHoraEstaOcupada(DateTime fecha, string hora, string doctor)
        {
            if (fecha.Date == DateTime.Today.Date && (hora == "09:00" || hora == "11:00"))
            {
                return true;
            }
            return false;
        }

        #endregion

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (cmbPaciente.SelectedItem == null || cmbOdontologo.SelectedItem == null || cmbHoraCita.SelectedItem == null)
            {
                MessageBox.Show("Por favor, complete todos los campos obligatorios antes de agendar.", "Campos Vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string paciente = cmbPaciente.SelectedItem.ToString();
            string doctor = cmbOdontologo.SelectedItem.ToString();
            string fecha = dtpFechaCita.Value.ToShortDateString();
            string hora = cmbHoraCita.SelectedItem.ToString();
            string tratamiento = cmbTratamiento.SelectedItem.ToString();

            MessageBox.Show($"¡Cita agendada con éxito!\n\nPaciente: {paciente}\nDoctor: {doctor}\nFecha: {fecha} a las {hora}\nMotivo: {tratamiento}",
                            "Cita Confirmada", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        #region Control de Errores por Diseñador

        private void headerControl1_Load(object sender, EventArgs e)
        {
            // Resuelve enlace roto del diseñador
        }

        #endregion
    }
}