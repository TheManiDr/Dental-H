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
            Panel fondo = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(221, 235, 250),
                Padding = new Padding(34, 136, 34, 26)
            };

            Panel tarjeta = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(34, 26, 34, 24)
            };

            Label lblTitulo = new Label
            {
                Text = "Agendar nueva consulta",
                Dock = DockStyle.Top,
                Height = 36,
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Color.FromArgb(28, 65, 111)
            };

            Label lblDescripcion = new Label
            {
                Text = "Completa los datos para reservar un horario de atención para el paciente.",
                Dock = DockStyle.Top,
                Height = 34,
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(92, 105, 119)
            };

            Panel lineaTitulo = new Panel
            {
                Dock = DockStyle.Top,
                Height = 2,
                BackColor = Color.FromArgb(86, 141, 214)
            };

            TableLayoutPanel columnas = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Padding = new Padding(0, 22, 0, 10),
                BackColor = Color.White
            };
            columnas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            columnas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));

            Panel columnaPaciente = CrearColumnaFormulario("Paciente y especialista", "Selecciona a las personas relacionadas con la consulta.");
            Panel columnaConsulta = CrearColumnaFormulario("Fecha y tratamiento", "Define el horario disponible y el motivo de atención.");

            cmbPaciente = CrearCombo();
            cmbOdontologo = CrearCombo();
            cmbOdontologo.SelectedIndexChanged += (s, e) => CargarHorariosDisponibles();
            dtpFechaCita = CrearSelectorFecha();
            dtpFechaCita.ValueChanged += (s, e) => CargarHorariosDisponibles();
            cmbHoraCita = CrearCombo();
            cmbTratamiento = CrearCombo();
            txtNotas = new TextBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.FromArgb(248, 250, 252)
            };

            AgregarCampo(columnaPaciente, "Paciente", "Selecciona el paciente que recibirá la atención.", cmbPaciente, 112);
            AgregarCampo(columnaPaciente, "Odontólogo / especialista", "Asigna al responsable de la consulta.", cmbOdontologo, 112);
            AgregarAviso(columnaPaciente, "Los horarios se actualizan automáticamente según el especialista y la fecha seleccionados.");

            AgregarCampo(columnaConsulta, "Fecha programada", "Elige el día de la consulta.", dtpFechaCita, 112);
            AgregarCampo(columnaConsulta, "Horario disponible", "Horario del consultorio: 7:00 AM - 3:00 PM.", cmbHoraCita, 112);
            AgregarCampo(columnaConsulta, "Motivo clínico / tratamiento", "Selecciona el motivo principal de la visita.", cmbTratamiento, 112);
            AgregarCampo(columnaConsulta, "Notas adicionales", "Describe síntomas o indicaciones importantes.", txtNotas, 154);

            columnas.Controls.Add(columnaPaciente, 0, 0);
            columnas.Controls.Add(columnaConsulta, 1, 0);

            FlowLayoutPanel pnlBotones = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                Height = 58,
                FlowDirection = FlowDirection.RightToLeft,
                Padding = new Padding(0, 12, 0, 0),
                BackColor = Color.White
            };

            btnGuardar = CrearBoton("Agendar consulta", Color.FromArgb(46, 125, 84), Color.White, 174);
            btnGuardar.Click += BtnGuardar_Click;

            btnCancelar = CrearBoton("Cancelar", Color.White, Color.FromArgb(51, 65, 85), 118);
            btnCancelar.FlatAppearance.BorderSize = 1;
            btnCancelar.FlatAppearance.BorderColor = Color.FromArgb(148, 163, 184);
            btnCancelar.Click += (s, e) => Close();

            pnlBotones.Controls.Add(btnGuardar);
            pnlBotones.Controls.Add(btnCancelar);

            tarjeta.Controls.Add(columnas);
            tarjeta.Controls.Add(pnlBotones);
            tarjeta.Controls.Add(lineaTitulo);
            tarjeta.Controls.Add(lblDescripcion);
            tarjeta.Controls.Add(lblTitulo);
            fondo.Controls.Add(tarjeta);
            Controls.Add(fondo);
            headerControl1.BringToFront();
        }

        private Panel CrearColumnaFormulario(string titulo, string descripcion)
        {
            Panel columna = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(18, 82, 18, 14),
                Margin = new Padding(8)
            };

            Label lblTitulo = new Label
            {
                Text = titulo,
                Location = new Point(18, 14),
                Size = new Size(520, 26),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                ForeColor = Color.FromArgb(37, 99, 163)
            };

            Label lblDescripcion = new Label
            {
                Text = descripcion,
                Location = new Point(18, 42),
                Size = new Size(520, 24),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(92, 105, 119)
            };

            Panel linea = new Panel
            {
                Location = new Point(18, 70),
                Size = new Size(520, 2),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.FromArgb(86, 141, 214)
            };

            Panel campos = new Panel
            {
                Name = "campos",
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = Color.White,
                Tag = 0
            };

            columna.Controls.Add(campos);
            columna.Controls.Add(lblTitulo);
            columna.Controls.Add(lblDescripcion);
            columna.Controls.Add(linea);
            return columna;
        }

        private void AgregarCampo(Panel columna, string etiqueta, string ayuda, Control control, int alto)
        {
            Panel campos = columna.Controls["campos"] as Panel;
            int y = campos.Tag is int ? (int)campos.Tag : 0;
            Panel bloque = new Panel
            {
                Location = new Point(0, y),
                Width = Math.Max(240, campos.ClientSize.Width - 22),
                Height = alto,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            Label lblEtiqueta = new Label
            {
                Text = etiqueta,
                Location = new Point(0, 8),
                Size = new Size(540, 20),
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 65, 85)
            };

            Label lblAyuda = new Label
            {
                Text = ayuda,
                Location = new Point(0, 30),
                Size = new Size(540, 18),
                Font = new Font("Segoe UI", 8.5f),
                ForeColor = Color.FromArgb(100, 116, 139)
            };

            control.Location = new Point(0, 56);
            control.Size = new Size(540, alto - 64);
            control.Dock = DockStyle.None;
            control.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            bloque.Controls.Add(lblEtiqueta);
            bloque.Controls.Add(lblAyuda);
            bloque.Controls.Add(control);
            campos.Controls.Add(bloque);
            campos.Tag = y + alto + 6;

            campos.Resize += (sender, e) =>
            {
                bloque.Width = Math.Max(240, campos.ClientSize.Width - 22);
                control.Width = bloque.Width;
                lblEtiqueta.Width = bloque.Width;
                lblAyuda.Width = bloque.Width;
            };
        }

        private void AgregarAviso(Panel columna, string texto)
        {
            Panel campos = columna.Controls["campos"] as Panel;
            int y = campos.Tag is int ? (int)campos.Tag : 0;
            Label aviso = new Label
            {
                Text = texto,
                Location = new Point(0, y + 18),
                Width = Math.Max(240, campos.ClientSize.Width - 22),
                Height = 58,
                Padding = new Padding(12),
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(37, 99, 163),
                BackColor = Color.FromArgb(239, 246, 255)
            };
            campos.Controls.Add(aviso);
            campos.Tag = y + 94;
            campos.Resize += (sender, e) => aviso.Width = Math.Max(240, campos.ClientSize.Width - 22);
        }

        private ComboBox CrearCombo()
        {
            return new ComboBox
            {
                Font = new Font("Segoe UI", 10),
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.White
            };
        }

        private DateTimePicker CrearSelectorFecha()
        {
            return new DateTimePicker
            {
                Font = new Font("Segoe UI", 10),
                Format = DateTimePickerFormat.Long
            };
        }

        private Button CrearBoton(string texto, Color fondo, Color colorTexto, int ancho)
        {
            Button boton = new Button
            {
                Text = texto,
                Width = ancho,
                Height = 36,
                Margin = new Padding(10, 0, 0, 0),
                BackColor = fondo,
                ForeColor = colorTexto,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold)
            };
            boton.FlatAppearance.BorderSize = 0;
            return boton;
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
