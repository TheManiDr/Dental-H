using Dental_H.Controller;
using Dental_H.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dental_H.Components;
using System.Drawing.Drawing2D;

namespace Dental_H.View
{
    public partial class PacienteDetalleForm : Form
    {
        private int idPaciente;
        private bool arrastrando = false;
        private Point puntoInicial;

        // 1. VARIABLE GLOBAL PARA RECORDAR LA VENTANA ANTERIOR
        private Form _ventanaAnterior;

        // 2. CONSTRUCTOR MODIFICADO (Ahora recibe el ID del paciente Y la ventana de origen)
        public PacienteDetalleForm(int idPaciente, Form anterior)
        {
            InitializeComponent();
            this.idPaciente = idPaciente;
            this._ventanaAnterior = anterior; // Guardamos la lista de pacientes en memoria

            ConfigurarEstiloDetalle();
            CargarPaciente();
        }

        private void ConfigurarEstiloDetalle()
        {
            Text = "Detalle del paciente";
            WindowState = FormWindowState.Maximized;
            BackColor = Color.FromArgb(245, 247, 250);

            panel1.BackColor = Color.FromArgb(245, 247, 250);

            pnlHeader.Height = 138;
            pnlHeader.BackColor = Color.FromArgb(86, 141, 214);
            pnlHeader.Padding = new Padding(28, 18, 28, 18);

            picAvatarPaciente.Location = new Point(28, 24);
            picAvatarPaciente.Size = new Size(92, 92);
            picAvatarPaciente.SizeMode = PictureBoxSizeMode.Zoom;

            lblPaciente.Location = new Point(142, 35);
            lblPaciente.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            lblPaciente.ForeColor = Color.White;

            lblNombrePaciente.Location = new Point(142, 58);
            lblNombrePaciente.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblNombrePaciente.ForeColor = Color.White;
            lblNombrePaciente.AutoSize = true;

            lblEdad.Location = new Point(142, 90);
            lblEdad.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblEdad.ForeColor = Color.White;

            pnlTabs.Height = 48;
            pnlTabs.BackColor = Color.FromArgb(221, 235, 250);
            EstilizarTab(btnDatosPersonales, 28, 10, 130);
            EstilizarTab(btnExpediente, 168, 10, 140);
            EstilizarTab(btnPlanesTratamientos, 318, 10, 165);
            EstilizarTab(btnRadiografia, 493, 10, 120);

            panelDatosPersonales.BackColor = Color.FromArgb(245, 247, 250);
            panelDatosPersonales.Padding = new Padding(26);
            panelDatosPersonales.AutoScroll = true;

            TableLayoutPanel layout = new TableLayoutPanel();
            layout.Dock = DockStyle.Fill;
            layout.ColumnCount = 3;
            layout.RowCount = 1;
            layout.BackColor = Color.FromArgb(245, 247, 250);
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34f));

            Panel cardBasica = CrearCardDetalle("Informacion basica");
            Panel cardMedica = CrearCardDetalle("Informacion medica y direccion");
            Panel cardContacto = CrearCardDetalle("Contacto y emergencia");

            TableLayoutPanel tablaBasica = CrearTablaCampos();
            TableLayoutPanel tablaMedica = CrearTablaCampos();
            TableLayoutPanel tablaContacto = CrearTablaCampos();

            cardBasica.Controls.Add(tablaBasica);
            cardMedica.Controls.Add(tablaMedica);
            cardContacto.Controls.Add(tablaContacto);

            AgregarCampo(tablaBasica, lblNombre, txtNombre);
            AgregarCampo(tablaBasica, lblApellidoPaterno, txtApellidoPaterno);
            AgregarCampo(tablaBasica, lblApellidoMaterno, txtApellidoMaterno);
            AgregarCampo(tablaBasica, lblFechaNacimiento, dtpFechaNacimiento);
            AgregarCampo(tablaBasica, lblGenero, cmbGenero);

            AgregarCampo(tablaMedica, label3, cmbTipoSangre);
            AgregarCampo(tablaMedica, label4, txtAlergias);
            AgregarSeparador(tablaMedica, "Direccion");
            label9.Text = "Codigo postal";
            AgregarCampo(tablaMedica, label6, txtCalle);
            AgregarCampo(tablaMedica, label5, txtCiudad);
            AgregarCampo(tablaMedica, label8, txtEstado);
            AgregarCampo(tablaMedica, label9, txtCodigoPostal);

            AgregarCampo(tablaContacto, label11, txtTelefono);
            AgregarCampo(tablaContacto, label10, txtCorreo);
            AgregarSeparador(tablaContacto, "Contacto de emergencia");
            AgregarCampo(tablaContacto, label15, txtContactoEmergencia);
            AgregarCampo(tablaContacto, label13, txtNumeroEmergencia);

            layout.Controls.Add(cardBasica, 0, 0);
            layout.Controls.Add(cardMedica, 1, 0);
            layout.Controls.Add(cardContacto, 2, 0);

            panelDatosPersonales.Controls.Clear();
            panelDatosPersonales.Controls.Add(layout);
        }

        private void EstilizarTab(Button boton, int x, int y, int width)
        {
            boton.Location = new Point(x, y);
            boton.Size = new Size(width, 30);
            boton.FlatStyle = FlatStyle.Flat;
            boton.FlatAppearance.BorderSize = 0;
            boton.BackColor = Color.Transparent;
            boton.ForeColor = Color.FromArgb(28, 65, 111);
            boton.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
        }

        private Panel CrearCardDetalle(string titulo)
        {
            Panel card = new Panel();
            card.Dock = DockStyle.Fill;
            card.BackColor = Color.White;
            card.BorderStyle = BorderStyle.FixedSingle;
            card.Margin = new Padding(10);
            card.Padding = new Padding(24, 72, 24, 24);

            Label labelTitulo = new Label();
            labelTitulo.Text = titulo;
            labelTitulo.Location = new Point(24, 18);
            labelTitulo.Size = new Size(360, 40);
            labelTitulo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            labelTitulo.Font = new Font("Segoe UI", 15, FontStyle.Bold);
            labelTitulo.ForeColor = Color.FromArgb(28, 65, 111);
            labelTitulo.TextAlign = ContentAlignment.MiddleLeft;

            card.Controls.Add(labelTitulo);

            return card;
        }

        private TableLayoutPanel CrearTablaCampos()
        {
            TableLayoutPanel tabla = new TableLayoutPanel();
            tabla.Dock = DockStyle.Fill;
            tabla.ColumnCount = 1;
            tabla.RowCount = 1;
            tabla.AutoScroll = true;
            tabla.BackColor = Color.White;
            tabla.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            return tabla;
        }

        private void AgregarCampo(TableLayoutPanel tabla, Label label, Control control)
        {
            label.AutoSize = false;
            label.Dock = DockStyle.Fill;
            label.Height = 22;
            label.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            label.ForeColor = Color.FromArgb(51, 65, 85);
            label.TextAlign = ContentAlignment.BottomLeft;
            label.Margin = new Padding(0, 8, 0, 2);

            control.Dock = DockStyle.Fill;
            control.Height = 34;
            control.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            control.Margin = new Padding(0, 0, 0, 8);
            control.BackColor = Color.FromArgb(248, 250, 252);

            tabla.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            tabla.Controls.Add(label, 0, tabla.RowCount - 1);
            tabla.RowCount++;

            tabla.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));
            tabla.Controls.Add(control, 0, tabla.RowCount - 1);
            tabla.RowCount++;
        }

        private void AgregarSeparador(TableLayoutPanel tabla, string texto)
        {
            Label separador = new Label();
            separador.Text = texto;
            separador.Dock = DockStyle.Fill;
            separador.Height = 42;
            separador.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            separador.ForeColor = Color.FromArgb(28, 65, 111);
            separador.TextAlign = ContentAlignment.BottomLeft;
            separador.Margin = new Padding(0, 18, 0, 4);

            tabla.RowStyles.Add(new RowStyle(SizeType.Absolute, 54));
            tabla.Controls.Add(separador, 0, tabla.RowCount - 1);
            tabla.RowCount++;
        }

        private void CargarPaciente()
        {
            PacienteController controller =
                new PacienteController();

            Paciente paciente =
                controller.ObtenerPacientePorId(idPaciente);

            if (paciente == null)
            {
                MessageBox.Show("Paciente no encontrado");
                return;
            }
            lblNombrePaciente.Text = paciente.Nombre + " " + paciente.ApellidoPaterno + " " + paciente.ApellidoMaterno;

            lblEdad.Text = CalcularEdad(paciente.FechaNacimiento) + " años";

            txtNombre.Text = paciente.Nombre;
            txtApellidoPaterno.Text = paciente.ApellidoPaterno;
            txtApellidoMaterno.Text = paciente.ApellidoMaterno;

            dtpFechaNacimiento.Value =
                paciente.FechaNacimiento;

            cmbGenero.Text =
                paciente.Genero;

            cmbTipoSangre.Text =
                paciente.TipoSangre;

            txtAlergias.Text =
                paciente.Alergias;

            txtContactoEmergencia.Text =
                paciente.ContactoEmergencia;

            txtNumeroEmergencia.Text =
                paciente.NumeroEmergencia;

            txtCalle.Text =
                paciente.Calle;

            txtCiudad.Text =
                paciente.Ciudad;

            txtEstado.Text =
                paciente.Estado;

            txtCodigoPostal.Text =
                paciente.CodigoPostal;

            txtTelefono.Text =
                paciente.Telefono;

            if (paciente.Genero == "Masculino")
            {
                picAvatarPaciente.Image = Properties.Resources.avatar_hombre;
            }
            else
            {
                picAvatarPaciente.Image =
                    Properties.Resources.avatar_mujer;
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


        private void btnPlanesTratamientos_Click(object sender, EventArgs e)
        {
            panelOdontograma.Visible = true;

            panelDatosPersonales.Visible = false;
            //panelMedicos.Visible = false;
            //panelContacto.Visible = false;
        }

        private void btnDatosPersonales_Click(object sender, EventArgs e)
        {
            panelDatosPersonales.Visible = true;

            panelOdontograma.Visible = false;
            //panelMedicos.Visible = false;
            //panelContacto.Visible = false;
        }

        private void picFractura_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox icono = (PictureBox)sender;

            icono.DoDragDrop(icono.Name, DragDropEffects.Copy);
        }

        private void picDesgaste_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox icono = (PictureBox)sender;

            icono.DoDragDrop(icono.Name, DragDropEffects.Copy);
        }

        private void picSensibilidad_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox icono = (PictureBox)sender;

            icono.DoDragDrop(icono.Name, DragDropEffects.Copy);
        }

        private void picPlaca_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox icono = (PictureBox)sender;

            icono.DoDragDrop(icono.Name, DragDropEffects.Copy);
        }

        private void picSarro_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox icono = (PictureBox)sender;

            icono.DoDragDrop(icono.Name, DragDropEffects.Copy);
        }

        private void picInfeccion_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox icono = (PictureBox)sender;

            icono.DoDragDrop(icono.Name, DragDropEffects.Copy);
        }

        private void picInflamacion_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox icono = (PictureBox)sender;

            icono.DoDragDrop(icono.Name, DragDropEffects.Copy);
        }

        private void picCaries_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox icono = (PictureBox)sender;

            icono.DoDragDrop(icono.Name, DragDropEffects.Copy);
        }

        private void panelDienteVertical_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void panelDienteVertical_DragDrop(object sender, DragEventArgs e)
        {
            string padecimiento =
        e.Data.GetData(DataFormats.StringFormat).ToString();

            Panel panelDiente = (Panel)sender;

            Color color = Color.Red;

            switch (padecimiento)
            {
                case "picCaries":
                    color = Color.Red;
                    break;

                case "picFractura":
                    color = Color.Orange;
                    break;

                case "picDesgaste":
                    color = Color.GreenYellow;
                    break;

                case "picSensibilidad":
                    color = Color.LightCyan;
                    break;

                case "picPlaca":
                    color = Color.Blue;
                    break;

                case "picSarro":
                    color = Color.Gray;
                    break;

                case "picInfeccion":
                    color = Color.BlueViolet;
                    break;

                case "picInflamacion":
                    color = Color.Magenta;
                    break;
            }

            Panel marca = CrearMarca(color);

            Point p =
                panelDiente.PointToClient(
                    new Point(e.X, e.Y));

            marca.Location =
                new Point(
                    p.X - marca.Width / 2,
                    p.Y - marca.Height / 2);

            panelDiente.Controls.Add(marca);

            marca.BringToFront();
        }

        private void panelDienteSuperior_DragDrop(object sender, DragEventArgs e)
        {
            string padecimiento =
        e.Data.GetData(DataFormats.StringFormat).ToString();

            Panel panelDiente = (Panel)sender;

            Color color = Color.Red;

            switch (padecimiento)
            {
                case "picCaries":
                    color = Color.Red;
                    break;

                case "picFractura":
                    color = Color.Orange;
                    break;

                case "picDesgaste":
                    color = Color.GreenYellow;
                    break;

                case "picSensibilidad":
                    color = Color.LightCyan;
                    break;

                case "picPlaca":
                    color = Color.Blue;
                    break;

                case "picSarro":
                    color = Color.Gray;
                    break;

                case "picInfeccion":
                    color = Color.BlueViolet;
                    break;

                case "picInflamacion":
                    color = Color.Magenta;
                    break;
            }

            Panel marca = CrearMarca(color);

            Point p =
                panelDiente.PointToClient(
                    new Point(e.X, e.Y));

            marca.Location =
                new Point(
                    p.X - marca.Width / 2,
                    p.Y - marca.Height / 2);

            panelDiente.Controls.Add(marca);

            marca.BringToFront();
        }

        private void panelDienteSuperior_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void Marca_MouseDown(object sender,MouseEventArgs e)
        {
            arrastrando = true;

            puntoInicial = e.Location;
        }
        private void Marca_MouseMove(object sender,MouseEventArgs e)
        {
            if (!arrastrando)
                return;

            Panel marca = (Panel)sender;

            marca.Left += e.X - puntoInicial.X;
            marca.Top += e.Y - puntoInicial.Y;
        }
        private void Marca_MouseUp(object sender,MouseEventArgs e)
        {
            arrastrando = false;
        }
        private void Marca_MouseClick(object sender,MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Panel marca = (Panel)sender;

                marca.Parent.Controls.Remove(marca);

                marca.Dispose();
            }
        }
        private Panel CrearMarca(Color color)
        {
            Panel marca = new Panel();

            marca.Size = new Size(25, 25);
            marca.BackColor = color;

            GraphicsPath gp = new GraphicsPath();
            gp.AddEllipse(0, 0, 25, 25);

            marca.Region = new Region(gp);

            marca.Cursor = Cursors.Hand;

            marca.MouseDown += Marca_MouseDown;
            marca.MouseMove += Marca_MouseMove;
            marca.MouseUp += Marca_MouseUp;
            marca.MouseClick += Marca_MouseClick;

            return marca;
        }
    }
}
