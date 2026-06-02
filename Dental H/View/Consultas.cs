using Dental_H.Controller;
using Dental_H.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Dental_H.View
{
    public partial class Consultas : Form
    {
        private CitaMedicaController citaMedicaController;
        private List<ConsultaInfo> consultas;

        public Consultas()
        {
            citaMedicaController = new CitaMedicaController();
            InitializeComponent();
            ConfigurarPantalla();
        }

        private void Consultas_Load(object sender, EventArgs e)
        {
            CargarConsultas();
        }

        private void ConfigurarPantalla()
        {
            Text = "Consultas";
            BackColor = Color.FromArgb(245, 247, 250);

            pnlContenido.BackColor = Color.FromArgb(245, 247, 250);
            pnlContenido.Padding = new Padding(28);

            lblTitulo.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(28, 65, 111);
            lblTitulo.Text = "Historial de consultas";

            lblSubtitulo.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            lblSubtitulo.ForeColor = Color.FromArgb(92, 105, 119);
            lblSubtitulo.Text = "Registro cronologico de consultas realizadas y descripcion del procedimiento.";

            lblTotal.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblTotal.ForeColor = Color.FromArgb(37, 99, 163);

            txtBuscar.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            txtBuscar.BackColor = Color.White;
            txtBuscar.BorderStyle = BorderStyle.FixedSingle;
            txtBuscar.TextChanged += (s, e) => FiltrarConsultas();

            dgvConsultas.BackgroundColor = Color.White;
            dgvConsultas.BorderStyle = BorderStyle.None;
            dgvConsultas.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvConsultas.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvConsultas.EnableHeadersVisualStyles = false;
            dgvConsultas.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(37, 99, 163);
            dgvConsultas.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvConsultas.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvConsultas.ColumnHeadersHeight = 42;
            dgvConsultas.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvConsultas.DefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55);
            dgvConsultas.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
            dgvConsultas.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvConsultas.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgvConsultas.GridColor = Color.FromArgb(226, 232, 240);
            dgvConsultas.RowHeadersVisible = false;
            dgvConsultas.AllowUserToAddRows = false;
            dgvConsultas.AllowUserToDeleteRows = false;
            dgvConsultas.AllowUserToResizeRows = false;
            dgvConsultas.ReadOnly = true;
            dgvConsultas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvConsultas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void CargarConsultas()
        {
            consultas = citaMedicaController.ObtenerConsultas();
            MostrarConsultas(consultas);
        }

        private void FiltrarConsultas()
        {
            if (consultas == null)
            {
                return;
            }

            string busqueda = txtBuscar.Text.Trim().ToLower();

            if (string.IsNullOrWhiteSpace(busqueda))
            {
                MostrarConsultas(consultas);
                return;
            }

            List<ConsultaInfo> filtradas = consultas
                .Where(c =>
                    c.Paciente.ToLower().Contains(busqueda) ||
                    c.Odontologo.ToLower().Contains(busqueda) ||
                    c.Descripcion.ToLower().Contains(busqueda) ||
                    c.Fecha.ToString("dd/MM/yyyy").Contains(busqueda))
                .ToList();

            MostrarConsultas(filtradas);
        }

        private void MostrarConsultas(List<ConsultaInfo> lista)
        {
            dgvConsultas.Columns.Clear();
            dgvConsultas.Rows.Clear();

            dgvConsultas.Columns.Add("fecha", "Fecha");
            dgvConsultas.Columns.Add("hora", "Hora");
            dgvConsultas.Columns.Add("paciente", "Paciente");
            dgvConsultas.Columns.Add("odontologo", "Odontologo");
            dgvConsultas.Columns.Add("descripcion", "Descripcion");

            dgvConsultas.Columns["fecha"].FillWeight = 14;
            dgvConsultas.Columns["hora"].FillWeight = 10;
            dgvConsultas.Columns["paciente"].FillWeight = 22;
            dgvConsultas.Columns["odontologo"].FillWeight = 22;
            dgvConsultas.Columns["descripcion"].FillWeight = 46;

            foreach (ConsultaInfo consulta in lista)
            {
                dgvConsultas.Rows.Add(
                    consulta.Fecha.ToString("dd/MM/yyyy"),
                    DateTime.Today.Add(consulta.Hora).ToString("hh:mm tt"),
                    consulta.Paciente,
                    consulta.Odontologo,
                    consulta.Descripcion
                );
            }

            lblTotal.Text = lista.Count == 1 ? "1 consulta" : lista.Count + " consultas";
        }
    }
}
