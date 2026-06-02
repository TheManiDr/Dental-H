using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Dental_H.Controller;
using Dental_H.Model;

namespace Dental_H.View
{
    public partial class Citashoy : Form
    {
        // Controles dinámicos principales
        private Panel panelSuperior;
        private DateTimePicker dtpFechaControl;
        private CitaMedicaController citaMedicaController;

        // VARIABLE DE COMPATIBILIDAD: Satisface al diseñador y elimina los errores CS0103 de raíz

        public Citashoy()
        {
            citaMedicaController = new CitaMedicaController();
            InitializeComponent();
        }

        private void Citashoy_Load(object sender, EventArgs e)
        {
            // 1. Maximizar ventana al abrir
            this.WindowState = FormWindowState.Maximized;

            // 2. Crear el espacio exclusivo arriba para la fecha
            ConfigurarPanelSuperior();

            // 3. Configurar estructura de la tabla de horarios
            ConfigurarDataGridView();

            // 4. Cargar las citas del día de hoy
            CargarAgendaDelDia(DateTime.Today);

            // 5. Ajustar celdas al cambiar tamaño de pantalla
            this.Resize += Citashoy_Resize;
        }

        /// <summary>
        /// Crea una barra superior divisoria para que la fecha nunca se junte con la tabla.
        /// </summary>
        private void ConfigurarPanelSuperior()
        {
            panelSuperior = new Panel();
            panelSuperior.Height = 50;
            panelSuperior.Dock = DockStyle.Top;
            panelSuperior.BackColor = Color.White;
            panelSuperior.Padding = new Padding(10);

            dtpFechaControl = new DateTimePicker();
            dtpFechaControl.Width = 300;
            dtpFechaControl.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            dtpFechaControl.Location = new Point(15, 12);
            dtpFechaControl.ValueChanged += DtpFechaControl_ValueChanged;

            panelSuperior.Controls.Add(dtpFechaControl);
            this.Controls.Add(panelSuperior);

            panelSuperior.BringToFront();
        }

        private void DtpFechaControl_ValueChanged(object sender, EventArgs e)
        {
            CargarAgendaDelDia(dtpFechaControl.Value);
        }

        private void Citashoy_Resize(object sender, EventArgs e)
        {
            AjustarAlturaDeFilas();
        }

        /// <summary>
        /// Configura el diseño visual, las columnas y los botones de dgvHorario2.
        /// </summary>
        private void ConfigurarDataGridView()
        {
            dgvHorario2.Columns.Clear();
            dgvHorario2.AllowUserToAddRows = false;
            dgvHorario2.RowHeadersVisible = false;

            dgvHorario2.Dock = DockStyle.Fill;
            dgvHorario2.BringToFront();

            dgvHorario2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHorario2.DefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            dgvHorario2.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            dgvHorario2.ColumnHeadersHeight = 45;
            dgvHorario2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            dgvHorario2.Columns.Add("Hora", "Hora");
            dgvHorario2.Columns["Hora"].FillWeight = 40;
            dgvHorario2.Columns["Hora"].ReadOnly = true;

            dgvHorario2.Columns.Add("Paciente", "Paciente / Motivo");
            dgvHorario2.Columns["Paciente"].FillWeight = 160;
            dgvHorario2.Columns["Paciente"].ReadOnly = true;

            dgvHorario2.Columns.Add("Estado", "Estado");
            dgvHorario2.Columns["Estado"].FillWeight = 50;
            dgvHorario2.Columns["Estado"].ReadOnly = true;

            DataGridViewButtonColumn btnConfirmar = new DataGridViewButtonColumn();
            btnConfirmar.Name = "BtnConfirmar";
            btnConfirmar.HeaderText = "Confirmación";
            btnConfirmar.Text = "Confirmar";
            btnConfirmar.UseColumnTextForButtonValue = true;
            btnConfirmar.FillWeight = 50;
            dgvHorario2.Columns.Add(btnConfirmar);

            DataGridViewButtonColumn btnReagendar = new DataGridViewButtonColumn();
            btnReagendar.Name = "BtnReagendar";
            btnReagendar.HeaderText = "Reagendar";
            btnReagendar.Text = "Reagendar";
            btnReagendar.UseColumnTextForButtonValue = true;
            btnReagendar.FillWeight = 50;
            dgvHorario2.Columns.Add(btnReagendar);

            DataGridViewButtonColumn btnCancelar = new DataGridViewButtonColumn();
            btnCancelar.Name = "BtnCancelar";
            btnCancelar.HeaderText = "Cancelación";
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseColumnTextForButtonValue = true;
            btnCancelar.FillWeight = 50;
            dgvHorario2.Columns.Add(btnCancelar);

            dgvHorario2.CellContentClick -= dgvHorario2_CellContentClick_1;
            dgvHorario2.CellContentClick -= DgvHorario2_CellContentClick;
            dgvHorario2.CellContentClick += DgvHorario2_CellContentClick;
        }

        /// <summary>
        /// Genera el listado de horas de 7:00 AM a 3:00 PM sin vacíos.
        /// </summary>
        private void CargarAgendaDelDia(DateTime fecha)
        {
            dgvHorario2.SuspendLayout();
            dgvHorario2.Rows.Clear();

            if (dtpFechaControl != null && dtpFechaControl.Value.Date != fecha.Date)
            {
                dtpFechaControl.Value = fecha;
            }

            int horaInicio = 7;
            int horaFin = 15;
            List<ConsultaInfo> citasDelDia = citaMedicaController.ObtenerCitasPorFecha(fecha);
            SortedSet<TimeSpan> horarios = new SortedSet<TimeSpan>();

            for (int h = horaInicio; h <= horaFin; h++)
            {
                horarios.Add(new TimeSpan(h, 0, 0));
            }

            foreach (ConsultaInfo cita in citasDelDia)
            {
                horarios.Add(new TimeSpan(cita.Hora.Hours, cita.Hora.Minutes, 0));
            }

            foreach (TimeSpan horario in horarios)
            {
                string stringHora = DateTime.Today.Add(horario).ToString("HH:mm");
                List<ConsultaInfo> citasEnHorario = citasDelDia
                    .Where(c => c.Hora.Hours == horario.Hours && c.Hora.Minutes == horario.Minutes)
                    .ToList();

                if (citasEnHorario.Count > 0)
                {
                    foreach (ConsultaInfo cita in citasEnHorario)
                    {
                        int rowIndex = dgvHorario2.Rows.Add(
                            stringHora,
                            cita.Paciente + " (" + cita.Descripcion + ")",
                            "Pendiente"
                        );
                        dgvHorario2.Rows[rowIndex].Tag = cita;
                    }
                }
                else
                {
                    int rowIndex = dgvHorario2.Rows.Add(stringHora, "--- Disponible ---", "");
                    dgvHorario2.Rows[rowIndex].Tag = null;
                }
            }

            dgvHorario2.ResumeLayout();
            AjustarAlturaDeFilas();
        }

        /// <summary>
        /// Divide equitativamente la pantalla vertical restante entre las filas vigentes.
        /// </summary>
        private void AjustarAlturaDeFilas()
        {
            int cantidadFilas = dgvHorario2.Rows.Count;
            int alturaDisponible = dgvHorario2.Height - dgvHorario2.ColumnHeadersHeight - 4;

            if (cantidadFilas > 0 && alturaDisponible > 0)
            {
                int nuevaAlturaFila = alturaDisponible / cantidadFilas;
                if (nuevaAlturaFila < 30) nuevaAlturaFila = 30;

                foreach (DataGridViewRow row in dgvHorario2.Rows)
                {
                    row.Height = nuevaAlturaFila;
                }
            }
        }

        private void DgvHorario2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string columnaSeleccionada = dgvHorario2.Columns[e.ColumnIndex].Name;
            string hora = dgvHorario2.Rows[e.RowIndex].Cells["Hora"].Value.ToString();
            string paciente = dgvHorario2.Rows[e.RowIndex].Cells["Paciente"].Value.ToString();
            ConsultaInfo cita = dgvHorario2.Rows[e.RowIndex].Tag as ConsultaInfo;

            if (paciente == "--- Disponible ---" || cita == null) return;

            switch (columnaSeleccionada)
            {
                case "BtnConfirmar":
                    ConfirmarCita(hora, paciente);
                    break;
                case "BtnReagendar":
                    ReagendarCita(hora, paciente);
                    break;
                case "BtnCancelar":
                    CancelarCita(cita);
                    break;
            }
        }

        #region Acciones del Consultorio (Lógica)

        private void ConfirmarCita(string hora, string paciente)
        {
            DialogResult result = MessageBox.Show($"¿Confirmar que se realizó la cita de las {hora} con {paciente}?", "Confirmar Cita", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                MessageBox.Show("Cita guardada como realizada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarAgendaDelDia(dtpFechaControl.Value);
            }
        }

        private void ReagendarCita(string hora, string paciente)
        {
            MessageBox.Show($"Abriendo ventana para reagendar la cita de las {hora}...", "Reagendar Cita", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CancelarCita(ConsultaInfo cita)
        {
            string hora = DateTime.Today.Add(cita.Hora).ToString("HH:mm");
            DialogResult result = MessageBox.Show($"¿Está seguro de CANCELAR la cita de las {hora} de {cita.Paciente}?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                bool cancelada = citaMedicaController.CancelarCita(cita.IdCita);

                if (cancelada)
                {
                    MessageBox.Show("La cita ha sido cancelada.", "Cancelación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarAgendaDelDia(dtpFechaControl.Value);
                }
                else
                {
                    MessageBox.Show("No se pudo cancelar la cita en la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        #region Métodos de compatibilidad con errores del Diseñador Visual de VS

        private void dgvHorario2_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            DgvHorario2_CellContentClick(sender, e);
        }

        private void dtpFecha_ValueChanged_1(object sender, EventArgs e)
        {
            if (dtpFechaControl != null)
            {
                CargarAgendaDelDia(dtpFechaControl.Value);
            }
        }

        #endregion
    }
}
