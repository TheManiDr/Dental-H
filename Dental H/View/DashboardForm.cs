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
    public partial class DashboardForm : Form
    {
        public DashboardForm()
        {
            InitializeComponent();
            ConfigurarEstiloDashboard();

            // Medidas exactas del área útil de tu pantalla (1904 x 922)
            this.ClientSize = new Size(1904, 922);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Dental H - Panel de Control";
            this.BackColor = Color.FromArgb(245, 246, 250);
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {
            // Inicializa la configuración estructural y visual de tu tabla dgvCalendario
            CargarHorariosYEstilos();
        }

        private void CargarHorariosYEstilos()
        {
            // Validamos que tu componente exista en el diseño
            if (this.dgvCalendario == null) return;

            // Propiedades esenciales de visualización plana clínica (Sin barras molestas)
            this.dgvCalendario.ScrollBars = ScrollBars.None;
            this.dgvCalendario.AllowUserToResizeRows = false;
            this.dgvCalendario.AllowUserToResizeColumns = false;
            this.dgvCalendario.AllowUserToAddRows = false;
            this.dgvCalendario.AllowUserToDeleteRows = false;
            this.dgvCalendario.ReadOnly = true;
            this.dgvCalendario.RowHeadersVisible = false;
            this.dgvCalendario.BorderStyle = BorderStyle.None;
            this.dgvCalendario.BackgroundColor = Color.White;
            this.dgvCalendario.GridColor = Color.FromArgb(230, 233, 237);
            this.dgvCalendario.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvCalendario.SelectionMode = DataGridViewSelectionMode.CellSelect;

            // ============================================================
            //   CONSTRUCCIÓN ESTRUCTURAL DE DÍAS Y HORARIOS
            // ============================================================
            this.dgvCalendario.Columns.Clear();
            this.dgvCalendario.Columns.Add("colHora", "Horario");
            this.dgvCalendario.Columns.Add("colLunes", "Lunes");
            this.dgvCalendario.Columns.Add("colMartes", "Martes");
            this.dgvCalendario.Columns.Add("colMiercoles", "Miércoles");
            this.dgvCalendario.Columns.Add("colJueves", "Jueves");
            this.dgvCalendario.Columns.Add("colViernes", "Viernes");
            this.dgvCalendario.Columns.Add("colSabado", "Sábado");

            // Estilos estéticos del encabezado principal azul
            this.dgvCalendario.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            this.dgvCalendario.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(37, 99, 163);
            this.dgvCalendario.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.dgvCalendario.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvCalendario.EnableHeadersVisualStyles = false;
            this.dgvCalendario.ColumnHeadersHeight = 48;

            // Configuración formal para la columna indicadora del tiempo
            this.dgvCalendario.Columns["colHora"].Width = 120;
            this.dgvCalendario.Columns["colHora"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvCalendario.Columns["colHora"].DefaultCellStyle.BackColor = Color.FromArgb(248, 249, 249);
            this.dgvCalendario.Columns["colHora"].DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            // Distribución equitativa automática de las columnas de los días
            for (int i = 1; i < this.dgvCalendario.Columns.Count; i++)
            {
                this.dgvCalendario.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            // Altura de fila calculada para eliminar las barras de scroll verticales
            this.dgvCalendario.RowTemplate.Height = 80;

            // Rango de horas laborables solicitado (7 AM - 3 PM)
            string[] horas = {
                "07:00 AM", "08:00 AM", "09:00 AM", "10:00 AM",
                "11:00 AM", "12:00 PM", "01:00 PM", "02:00 PM", "03:00 PM"
            };

            this.dgvCalendario.Rows.Clear();
            foreach (string hora in horas)
            {
                this.dgvCalendario.Rows.Add(hora, "", "", "", "", "", "");
            }

            // Estilos generales para el texto clínico ordinario de las celdas
            this.dgvCalendario.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            this.dgvCalendario.DefaultCellStyle.BackColor = Color.White;
            this.dgvCalendario.DefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55);
            this.dgvCalendario.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
            this.dgvCalendario.DefaultCellStyle.SelectionForeColor = Color.Black;
            this.dgvCalendario.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);

            // Inyección de citas muestra en la cuadrícula para validar el renderizado visual
            InsertarCitaMuestra(1, 2, "C. Pérez - 9 AM");       // Lunes 9:00 AM
            InsertarCitaMuestra(2, 4, "J. Gómez - 11 AM");      // Martes 11:00 AM
            InsertarCitaMuestra(3, 6, "C. Pérez - 9 AM");       // Miércoles 1:00 PM
            InsertarCitaMuestra(5, 2, "L. Gómez - 10 AM");      // Viernes 9:00 AM
        }

        private void InsertarCitaMuestra(int columnaDia, int filaHora, string informacionPaciente)
        {
            if (this.dgvCalendario != null && filaHora < this.dgvCalendario.Rows.Count && columnaDia < this.dgvCalendario.Columns.Count)
            {
                var celda = this.dgvCalendario.Rows[filaHora].Cells[columnaDia];
                celda.Value = informacionPaciente;
                celda.Style.BackColor = Color.FromArgb(174, 214, 241); // Azul clínico pastel
                celda.Style.ForeColor = Color.FromArgb(21, 67, 96);
                celda.Style.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
                celda.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        private void ConfigurarEstiloDashboard()
        {
            this.BackColor = Color.FromArgb(245, 247, 250);
            pnlContenido.BackColor = Color.FromArgb(245, 247, 250);
            panel2.BackColor = Color.FromArgb(245, 247, 250);

            panel2.Padding = new Padding(28, 28, 28, 28);

            panel3.BackColor = Color.White;
            panel3.Width = 292;
            panel3.Padding = new Padding(18);

            Panel panelAcciones = new Panel();
            panelAcciones.Dock = DockStyle.Fill;
            panelAcciones.BackColor = Color.White;
            panel3.Controls.Clear();
            panel3.Controls.Add(panelAcciones);

            Label lblAcciones = new Label
            {
                Text = "Acciones rapidas",
                Dock = DockStyle.Top,
                Height = 40,
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                ForeColor = Color.FromArgb(28, 65, 111),
                TextAlign = ContentAlignment.MiddleLeft
            };

            FlowLayoutPanel acciones = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                AutoScroll = true,
                Padding = new Padding(0, 10, 0, 0)
            };

            panelAcciones.Controls.Add(acciones);
            panelAcciones.Controls.Add(lblAcciones);

            EstilizarBotonDashboard(button1, "Nueva consulta\nAgendar cita", new Size(238, 118));
            EstilizarBotonDashboard(btnPacientes, "Pacientes", new Size(113, 112));
            EstilizarBotonDashboard(button2, "Personal", new Size(113, 112));
            EstilizarBotonDashboard(button4, "Consultas", new Size(113, 112));
            EstilizarBotonDashboard(button3, "Tratamientos", new Size(113, 112));
            EstilizarBotonDashboard(button5, "Citas hoy", new Size(238, 92));

            acciones.Controls.Add(button1);
            acciones.Controls.Add(btnPacientes);
            acciones.Controls.Add(button2);
            acciones.Controls.Add(button4);
            acciones.Controls.Add(button3);
            acciones.Controls.Add(button5);

            Panel panelAgenda = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(22, 18, 22, 22)
            };

            Label lblTitulo = new Label
            {
                Text = "Agenda semanal",
                Dock = DockStyle.Top,
                Height = 34,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(28, 65, 111),
                TextAlign = ContentAlignment.MiddleLeft
            };

            Label lblSubtitulo = new Label
            {
                Text = "Vista general de horarios y citas programadas",
                Dock = DockStyle.Top,
                Height = 26,
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = Color.FromArgb(92, 105, 119),
                TextAlign = ContentAlignment.MiddleLeft
            };

            panel2.Controls.Clear();
            panel2.Controls.Add(panelAgenda);
            panel2.Controls.Add(panel3);

            dgvCalendario.Dock = DockStyle.Fill;
            dgvCalendario.Margin = new Padding(0);

            panelAgenda.Controls.Add(dgvCalendario);
            panelAgenda.Controls.Add(lblSubtitulo);
            panelAgenda.Controls.Add(lblTitulo);
        }

        private void EstilizarBotonDashboard(Button boton, string texto, Size size)
        {
            boton.Text = texto;
            boton.Size = size;
            boton.Margin = new Padding(5);
            boton.BackColor = Color.FromArgb(248, 250, 252);
            boton.ForeColor = Color.FromArgb(31, 41, 55);
            boton.FlatStyle = FlatStyle.Flat;
            boton.FlatAppearance.BorderColor = Color.FromArgb(226, 232, 240);
            boton.FlatAppearance.BorderSize = 1;
            boton.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            boton.TextAlign = ContentAlignment.BottomCenter;
            boton.ImageAlign = ContentAlignment.TopCenter;
            boton.TextImageRelation = TextImageRelation.ImageAboveText;
            boton.Padding = new Padding(8, 10, 8, 10);

            if (boton.Image != null)
            {
                int altoMaximo = size.Height >= 118 ? 62 : 48;
                boton.Image = RedimensionarImagen(boton.Image, 72, altoMaximo);
            }
        }

        private Image RedimensionarImagen(Image imagen, int anchoMaximo, int altoMaximo)
        {
            double escala = Math.Min((double)anchoMaximo / imagen.Width, (double)altoMaximo / imagen.Height);
            int ancho = Math.Max(1, (int)(imagen.Width * escala));
            int alto = Math.Max(1, (int)(imagen.Height * escala));

            Bitmap bitmap = new Bitmap(ancho, alto);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.DrawImage(imagen, 0, 0, ancho, alto);
            }

            return bitmap;
        }

        private void btnPacientes_Click(object sender, EventArgs e)
        {
            AppNavigator.IrA(this, new PacienteListaForm());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AppNavigator.IrA(this, new Personal());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AppNavigator.IrA(this, new Consultas());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AppNavigator.IrA(this, new Tratamientos());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AppNavigator.IrA(this, new Citashoy());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 1. Creamos la instancia de la ventana flotante que programamos antes
            using (Dental_H.View.AgendarCita ventanaAgendar = new Dental_H.View.AgendarCita())
            {
                // 2. Mostramos la ventana al frente de la pantalla como un cuadro de diálogo
                ventanaAgendar.ShowDialog();
            }
        }
    }
}
