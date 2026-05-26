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

            // ============================================================
            //   PROPIEDADES DE EXPANSIÓN (Ajuste gigante a la izquierda)
            // ============================================================
            this.dgvCalendario.Location = new Point(30, 110); // Margen correcto abajo del logo azul
            this.dgvCalendario.Width = 1580;                 // Se expande a lo ancho respetando tus botones a la derecha
            this.dgvCalendario.Height = 770;                 // Se expande a lo alto en tus 922px
            this.dgvCalendario.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

            // Propiedades esenciales de visualización plana clínica (Sin barras molestas)
            this.dgvCalendario.ScrollBars = ScrollBars.None;
            this.dgvCalendario.AllowUserToResizeRows = false;
            this.dgvCalendario.AllowUserToResizeColumns = false;
            this.dgvCalendario.RowHeadersVisible = false;
            this.dgvCalendario.BorderStyle = BorderStyle.None;
            this.dgvCalendario.BackgroundColor = Color.White;
            this.dgvCalendario.GridColor = Color.FromArgb(230, 233, 237);

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
            this.dgvCalendario.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 152, 219);
            this.dgvCalendario.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            this.dgvCalendario.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.dgvCalendario.EnableHeadersVisualStyles = false;
            this.dgvCalendario.ColumnHeadersHeight = 45;

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
            this.dgvCalendario.DefaultCellStyle.SelectionBackColor = Color.FromArgb(212, 239, 223);
            this.dgvCalendario.DefaultCellStyle.SelectionForeColor = Color.Black;

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

        // ==========================================
        //       MANEJO DE EVENTOS DE TUS BOTONES
        // ==========================================

        private void btnPacientes_Click(object sender, EventArgs e)
        {
            // Le pasamos 'this' para habilitar que PacienteListaForm pueda regresar aquí con su botón Volver
            PacienteListaForm pacientelista = new PacienteListaForm(this);
            pacientelista.Show();
            this.Hide();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void button5_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }
    }
}