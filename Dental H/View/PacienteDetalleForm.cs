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

            CargarPaciente();
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

        private void btnAgregarTratamiento_Click(object sender, EventArgs e)
        {
            Panel fila = new Panel();
            fila.Width = 320;
            fila.Height = 35;

            ComboBox cmb = new ComboBox();
            cmb.Width = 250;
            cmb.DropDownStyle = ComboBoxStyle.DropDownList;

            // Copiar tratamientos del combo principal
            foreach (var item in cmbTratamiento.Items)
            {
                cmb.Items.Add(item);
            }

            Button btnEliminar = new Button();
            btnEliminar.Text = "X";
            btnEliminar.Width = 40;
            btnEliminar.Left = 260;

            btnEliminar.Click += (s, ev) =>
            {
                flpTratamientos.Controls.Remove(fila);
                fila.Dispose();
            };

            fila.Controls.Add(cmb);
            fila.Controls.Add(btnEliminar);

            flpTratamientos.Controls.Add(fila);
        }
    }
}