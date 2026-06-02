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
using Dental_H.Util;
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
        private Button btnGuardarCambios;
        private Button btnEditarPaciente;
        private Button btnEliminarPaciente;
        private Button btnCancelarEdicion;
        private bool modoEdicion;
        private readonly Dictionary<Control, Label> valoresLectura = new Dictionary<Control, Label>();
        private readonly OdontogramaController odontogramaController = new OdontogramaController();
        private readonly Dictionary<string, Color> coloresPadecimientos = new Dictionary<string, Color>
        {
            { "Caries", Color.FromArgb(231, 76, 60) },
            { "Fractura", Color.FromArgb(243, 156, 18) },
            { "Desgaste", Color.FromArgb(126, 232, 51) },
            { "Sensibilidad", Color.FromArgb(116, 235, 238) },
            { "Inflamación", Color.FromArgb(224, 62, 221) },
            { "Infección", Color.FromArgb(126, 51, 232) },
            { "Placa", Color.FromArgb(46, 51, 232) },
            { "Sarro", Color.FromArgb(105, 105, 105) }
        };
        private bool odontogramaConfigurado;
        private Panel pnlEditorOdontograma;
        private Panel pnlVistaVertical;
        private Panel pnlVistaSuperior;
        private Panel pnlMapaOdontograma;
        private DataGridView dgvHistorialOdontograma;
        private OdontogramaRegistro registroSeleccionado;
        private bool mostrarDenticionTemporal;

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

            panelDatosPersonales.BackColor = Color.FromArgb(221, 235, 250);
            panelDatosPersonales.Padding = new Padding(20);
            panelDatosPersonales.AutoScroll = true;

            Panel hoja = new Panel();
            hoja.Dock = DockStyle.Fill;
            hoja.BackColor = Color.White;
            hoja.Padding = new Padding(12, 62, 12, 12);

            TableLayoutPanel layout = new TableLayoutPanel();
            layout.Dock = DockStyle.Fill;
            layout.ColumnCount = 3;
            layout.RowCount = 1;
            layout.BackColor = Color.White;
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34f));

            Panel cardBasica = CrearCardDetalle("Información básica", Color.FromArgb(86, 141, 214));
            Panel cardMedica = CrearCardDetalle("Información médica", Color.FromArgb(235, 87, 87));
            Panel cardContacto = CrearCardDetalle("Información de contacto", Color.FromArgb(86, 141, 214));

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
            AgregarSeparador(tablaMedica, "Dirección", Color.FromArgb(86, 141, 214));
            label9.Text = "Código postal";
            AgregarCampo(tablaMedica, label6, txtCalle);
            AgregarCampo(tablaMedica, label5, txtCiudad);
            AgregarCampo(tablaMedica, label8, txtEstado);
            AgregarCampo(tablaMedica, label9, txtCodigoPostal);

            AgregarCampo(tablaContacto, label11, txtTelefono);
            AgregarCampo(tablaContacto, label10, txtCorreo);
            AgregarSeparador(tablaContacto, "Contacto de emergencia", Color.FromArgb(235, 87, 87));
            AgregarCampo(tablaContacto, label15, txtContactoEmergencia);
            AgregarCampo(tablaContacto, label13, txtNumeroEmergencia);

            layout.Controls.Add(cardBasica, 0, 0);
            layout.Controls.Add(cardMedica, 1, 0);
            layout.Controls.Add(cardContacto, 2, 0);
            hoja.Controls.Add(layout);

            btnGuardarCambios = new Button();
            btnGuardarCambios.Text = "Guardar";
            btnGuardarCambios.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnGuardarCambios.Location = new Point(hoja.Width - 284, 16);
            btnGuardarCambios.Size = new Size(120, 34);
            btnGuardarCambios.BackColor = Color.FromArgb(46, 125, 84);
            btnGuardarCambios.ForeColor = Color.White;
            btnGuardarCambios.FlatStyle = FlatStyle.Flat;
            btnGuardarCambios.FlatAppearance.BorderSize = 0;
            btnGuardarCambios.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnGuardarCambios.Click += BtnGuardarCambios_Click;
            hoja.Controls.Add(btnGuardarCambios);

            btnCancelarEdicion = CrearBotonAccion("Cancelar", Color.White, Color.FromArgb(51, 65, 85), hoja.Width - 152);
            btnCancelarEdicion.FlatAppearance.BorderSize = 1;
            btnCancelarEdicion.FlatAppearance.BorderColor = Color.FromArgb(100, 116, 139);
            btnCancelarEdicion.Click += BtnCancelarEdicion_Click;

            btnEditarPaciente = CrearBotonAccion("Editar", Color.FromArgb(86, 141, 214), Color.White, hoja.Width - 284);
            btnEditarPaciente.Click += BtnEditarPaciente_Click;

            btnEliminarPaciente = CrearBotonAccion("Eliminar", Color.FromArgb(235, 87, 87), Color.White, hoja.Width - 152);
            btnEliminarPaciente.Click += BtnEliminarPaciente_Click;

            hoja.Controls.Add(btnCancelarEdicion);
            hoja.Controls.Add(btnEditarPaciente);
            hoja.Controls.Add(btnEliminarPaciente);
            btnGuardarCambios.BringToFront();
            btnCancelarEdicion.BringToFront();
            btnEditarPaciente.BringToFront();
            btnEliminarPaciente.BringToFront();

            panelDatosPersonales.Controls.Clear();
            panelDatosPersonales.Controls.Add(hoja);
            ConfigurarModoEdicion(false);
        }

        private Button CrearBotonAccion(string texto, Color fondo, Color colorTexto, int x)
        {
            Button boton = new Button();
            boton.Text = texto;
            boton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            boton.Location = new Point(x, 16);
            boton.Size = new Size(120, 34);
            boton.BackColor = fondo;
            boton.ForeColor = colorTexto;
            boton.FlatStyle = FlatStyle.Flat;
            boton.FlatAppearance.BorderSize = 0;
            boton.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            return boton;
        }

        private void ConfigurarModoEdicion(bool editar)
        {
            modoEdicion = editar;

            foreach (Control campo in ObtenerCamposEditables())
            {
                ConfigurarAparienciaCampo(campo, editar);
            }

            btnEditarPaciente.Visible = !editar;
            btnEliminarPaciente.Visible = !editar;
            btnGuardarCambios.Visible = editar;
            btnCancelarEdicion.Visible = editar;
        }

        private void ConfigurarAparienciaCampo(Control campo, bool editar)
        {
            Label valorLectura;

            if (!valoresLectura.TryGetValue(campo, out valorLectura))
            {
                valorLectura = new Label();
                valorLectura.Dock = DockStyle.Fill;
                valorLectura.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                valorLectura.ForeColor = Color.FromArgb(20, 33, 45);
                valorLectura.TextAlign = ContentAlignment.MiddleLeft;
                valorLectura.Margin = new Padding(0, 0, 0, 8);
                valoresLectura[campo] = valorLectura;

                TableLayoutPanel tabla = campo.Parent as TableLayoutPanel;
                if (tabla != null)
                {
                    tabla.Controls.Add(valorLectura, tabla.GetColumn(campo), tabla.GetRow(campo));
                }
            }

            valorLectura.Text = ObtenerTextoCampo(campo);
            valorLectura.Visible = !editar;
            campo.Visible = editar;
        }

        private string ObtenerTextoCampo(Control campo)
        {
            DateTimePicker fecha = campo as DateTimePicker;
            if (fecha != null)
            {
                return fecha.Value.ToString("dd/MM/yyyy");
            }

            return string.IsNullOrWhiteSpace(campo.Text) ? "Sin dato" : campo.Text;
        }

        private IEnumerable<Control> ObtenerCamposEditables()
        {
            yield return txtNombre;
            yield return txtApellidoPaterno;
            yield return txtApellidoMaterno;
            yield return dtpFechaNacimiento;
            yield return cmbGenero;
            yield return cmbTipoSangre;
            yield return txtAlergias;
            yield return txtCalle;
            yield return txtCiudad;
            yield return txtEstado;
            yield return txtCodigoPostal;
            yield return txtTelefono;
            yield return txtCorreo;
            yield return txtContactoEmergencia;
            yield return txtNumeroEmergencia;
        }

        private void BtnEditarPaciente_Click(object sender, EventArgs e)
        {
            ConfigurarModoEdicion(true);
        }

        private void BtnCancelarEdicion_Click(object sender, EventArgs e)
        {
            CargarPaciente();
            ConfigurarModoEdicion(false);
        }

        private void BtnEliminarPaciente_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show(
                "Seguro que deseas eliminar este paciente?",
                "Confirmar eliminacion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (respuesta != DialogResult.Yes)
            {
                return;
            }

            PacienteController controller = new PacienteController();

            if (!controller.EliminarPaciente(idPaciente))
            {
                return;
            }

            MessageBox.Show("Paciente eliminado correctamente.", "Paciente eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            AppNavigator.IrA(this, new PacienteListaForm());
        }

        private void BtnGuardarCambios_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellidoPaterno.Text))
            {
                MessageBox.Show("Nombre y apellido paterno son obligatorios.", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Paciente paciente = new Paciente();
            paciente.IdPersona = idPaciente;
            paciente.Nombre = txtNombre.Text.Trim();
            paciente.ApellidoPaterno = txtApellidoPaterno.Text.Trim();
            paciente.ApellidoMaterno = txtApellidoMaterno.Text.Trim();
            paciente.FechaNacimiento = dtpFechaNacimiento.Value;
            paciente.Genero = cmbGenero.Text;
            paciente.TipoSangre = cmbTipoSangre.Text;
            paciente.Alergias = txtAlergias.Text.Trim();
            paciente.Calle = txtCalle.Text.Trim();
            paciente.Ciudad = txtCiudad.Text.Trim();
            paciente.Estado = txtEstado.Text.Trim();
            paciente.CodigoPostal = txtCodigoPostal.Text.Trim();
            paciente.Telefono = txtTelefono.Text.Trim();
            paciente.Correo = txtCorreo.Text.Trim();
            paciente.ContactoEmergencia = txtContactoEmergencia.Text.Trim();
            paciente.NumeroEmergencia = txtNumeroEmergencia.Text.Trim();

            PacienteController controller = new PacienteController();

            if (controller.ActualizarPaciente(paciente))
            {
                lblNombrePaciente.Text = paciente.Nombre + " " + paciente.ApellidoPaterno + " " + paciente.ApellidoMaterno;
                lblEdad.Text = CalcularEdad(paciente.FechaNacimiento) + " años";
                MessageBox.Show("Cambios guardados correctamente.", "Paciente actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ConfigurarModoEdicion(false);
            }
            else
            {
                MessageBox.Show("No se pudieron guardar los cambios.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private Panel CrearCardDetalle(string titulo, Color colorAcento)
        {
            Panel card = new Panel();
            card.Dock = DockStyle.Fill;
            card.BackColor = Color.White;
            card.BorderStyle = BorderStyle.None;
            card.Margin = new Padding(8);
            card.Padding = new Padding(16, 66, 16, 16);

            Label labelTitulo = new Label();
            labelTitulo.Text = titulo;
            labelTitulo.Location = new Point(16, 10);
            labelTitulo.Size = new Size(360, 34);
            labelTitulo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            labelTitulo.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            labelTitulo.ForeColor = colorAcento;
            labelTitulo.TextAlign = ContentAlignment.MiddleLeft;

            Panel linea = new Panel();
            linea.Location = new Point(16, 46);
            linea.Size = new Size(card.Width - 32, 2);
            linea.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            linea.BackColor = colorAcento;

            card.Controls.Add(labelTitulo);
            card.Controls.Add(linea);

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
            label.ForeColor = Color.FromArgb(70, 70, 70);
            label.TextAlign = ContentAlignment.BottomLeft;
            label.Margin = new Padding(0, 8, 0, 2);

            control.Dock = DockStyle.Fill;
            control.Height = 34;
            control.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            control.Margin = new Padding(0, 0, 0, 8);
            control.BackColor = Color.White;

            tabla.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            tabla.Controls.Add(label, 0, tabla.RowCount - 1);
            tabla.RowCount++;

            tabla.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));
            tabla.Controls.Add(control, 0, tabla.RowCount - 1);
            tabla.RowCount++;
        }

        private void AgregarSeparador(TableLayoutPanel tabla, string texto, Color colorAcento)
        {
            Label separador = new Label();
            separador.Text = texto;
            separador.Dock = DockStyle.Fill;
            separador.Height = 42;
            separador.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            separador.ForeColor = colorAcento;
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

            txtCorreo.Text =
                paciente.Correo;

            if (paciente.Genero == "Masculino")
            {
                picAvatarPaciente.Image = Properties.Resources.avatar_hombre;
            }
            else
            {
                picAvatarPaciente.Image =
                    Properties.Resources.avatar_mujer;
            }

            if (btnEditarPaciente != null)
            {
                ConfigurarModoEdicion(false);
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
            if (!odontogramaConfigurado)
            {
                ConfigurarOdontogramaInteractivo();
            }

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
            Panel superficie = marca.Parent as Panel;
            int x = marca.Left + e.X - puntoInicial.X;
            int y = marca.Top + e.Y - puntoInicial.Y;
            marca.Location = superficie == null ? new Point(x, y) : AjustarMarcaASuperficie(superficie, x, y);
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

        private void ConfigurarOdontogramaInteractivo()
        {
            Image vistaVertical = picDienteVertical.Image;
            Image vistaSuperior = picDienteSuperior.Image;
            odontogramaConfigurado = true;
            panelOdontograma.Controls.Clear();
            panelOdontograma.BackColor = Color.FromArgb(221, 235, 250);
            panelOdontograma.Padding = new Padding(16);
            panelOdontograma.AutoScroll = false;

            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                BackColor = Color.White
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 330));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            pnlEditorOdontograma = CrearPanelEditorOdontograma(vistaVertical, vistaSuperior);
            Panel contenido = CrearPanelOdontogramaGeneral();
            layout.Controls.Add(pnlEditorOdontograma, 0, 0);
            layout.Controls.Add(contenido, 1, 0);
            panelOdontograma.Controls.Add(layout);
            CargarHistorialOdontograma();
        }

        private Panel CrearPanelEditorOdontograma(Image vistaVertical, Image vistaSuperior)
        {
            Panel editor = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(86, 141, 214),
                AutoScroll = true,
                Padding = new Padding(12)
            };

            Label titulo = CrearLabelEditor("Diente seleccionado", 16, 10, 285, 30, 15, true);
            titulo.TextAlign = ContentAlignment.MiddleCenter;
            editor.Controls.Add(titulo);

            Panel tarjetaDiente = new Panel
            {
                Location = new Point(14, 52),
                Size = new Size(286, 218),
                BackColor = Color.White
            };
            Label ayuda = new Label
            {
                Text = "Arrastra un círculo para indicar un padecimiento",
                Location = new Point(8, 6),
                Size = new Size(270, 20),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.FromArgb(51, 65, 85)
            };
            pnlVistaVertical = CrearPanelSuperficie("Vertical", vistaVertical, new Point(8, 30), new Size(126, 126));
            pnlVistaSuperior = CrearPanelSuperficie("Superior", vistaSuperior, new Point(146, 30), new Size(132, 126));
            tarjetaDiente.Controls.Add(ayuda);
            tarjetaDiente.Controls.Add(pnlVistaVertical);
            tarjetaDiente.Controls.Add(pnlVistaSuperior);

            int indice = 0;
            foreach (KeyValuePair<string, Color> padecimiento in coloresPadecimientos)
            {
                Panel circulo = CrearCirculoArrastrable(padecimiento.Key, padecimiento.Value);
                circulo.Location = new Point(26 + (indice % 8) * 31, 174);
                tarjetaDiente.Controls.Add(circulo);
                indice++;
            }
            editor.Controls.Add(tarjetaDiente);

            txtNumPieza = CrearCajaEditor(editor, "Número de pieza", 288);
            txtNumPieza.ReadOnly = true;
            txtTipo = CrearCajaEditor(editor, "Tipo", 350);
            txtTipo.ReadOnly = true;

            Label lblEstado = CrearLabelEditor("Estado", 16, 412, 280, 20, 10, true);
            editor.Controls.Add(lblEstado);
            checkBox1 = CrearCheckEditor(editor, "Sano", 438);
            checkBox2 = CrearCheckEditor(editor, "Tratado", 462);
            checkBox3 = CrearCheckEditor(editor, "Restaurado", 486);
            checkBox4 = CrearCheckEditor(editor, "Extraído", 510);

            textBox1 = CrearCajaEditor(editor, "Diagnóstico", 548);
            textBox1.Multiline = true;
            textBox1.Height = 76;

            cmbTratamiento = new ComboBox
            {
                Location = new Point(16, 680),
                Size = new Size(284, 26),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 9)
            };
            editor.Controls.Add(CrearLabelEditor("Tratamiento a realizar", 16, 654, 284, 20, 10, true));
            editor.Controls.Add(cmbTratamiento);
            CargarTratamientosOdontograma();

            dtpFechaRegistro = CrearFechaEditor(editor, "Fecha de registro", 724);
            dtpFechaPadecimiento = CrearFechaEditor(editor, "Fecha de padecimiento", 786);

            btnGuardar = CrearBotonOdontograma("Guardar", Color.FromArgb(46, 204, 113), 16, 858, 132);
            btnEliminar = CrearBotonOdontograma("Eliminar", Color.FromArgb(235, 87, 87), 160, 858, 140);
            btnGuardar.Click += BtnGuardarOdontograma_Click;
            btnEliminar.Click += BtnEliminarOdontograma_Click;
            editor.Controls.Add(btnGuardar);
            editor.Controls.Add(btnEliminar);
            return editor;
        }

        private Panel CrearPanelOdontogramaGeneral()
        {
            Panel contenido = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, Padding = new Padding(14) };
            Label titulo = new Label
            {
                Text = "Odontograma permanente",
                Dock = DockStyle.Top,
                Height = 36,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(28, 65, 111)
            };
            Label ayuda = new Label
            {
                Text = "Selecciona una pieza dental para consultar su último registro o capturar una nueva evolución.",
                Dock = DockStyle.Top,
                Height = 28,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.FromArgb(92, 105, 119)
            };
            Panel tabs = new Panel { Dock = DockStyle.Top, Height = 36, BackColor = Color.White };
            Button permanentes = CrearBotonDenticion("Permanentes", 0);
            Button temporales = CrearBotonDenticion("Temporales", 126);
            permanentes.Click += (sender, e) => CambiarDenticion(false, permanentes, temporales);
            temporales.Click += (sender, e) => CambiarDenticion(true, permanentes, temporales);
            tabs.Controls.Add(permanentes);
            tabs.Controls.Add(temporales);

            pnlMapaOdontograma = new Panel { Dock = DockStyle.Top, Height = 500, BackColor = Color.White };
            PictureBox imagen = new PictureBox
            {
                Dock = DockStyle.Fill,
                Image = Properties.Resources.Odontograma,
                SizeMode = PictureBoxSizeMode.Zoom
            };
            pnlMapaOdontograma.Controls.Add(imagen);
            CrearSelectoresPiezas(pnlMapaOdontograma);
            CambiarDenticion(false, permanentes, temporales);

            Label lblHistorial = new Label
            {
                Text = "Historial clínico de dientes",
                Dock = DockStyle.Top,
                Height = 34,
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                ForeColor = Color.FromArgb(28, 65, 111),
                TextAlign = ContentAlignment.BottomLeft
            };
            dgvHistorialOdontograma = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                RowHeadersVisible = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            dgvHistorialOdontograma.Columns.Add("fechaRegistro", "Fecha de registro");
            dgvHistorialOdontograma.Columns.Add("fechaPadecimiento", "Fecha del padecimiento");
            dgvHistorialOdontograma.Columns.Add("pieza", "Pieza");
            dgvHistorialOdontograma.Columns.Add("tipo", "Tipo");
            dgvHistorialOdontograma.Columns.Add("estado", "Estado");
            dgvHistorialOdontograma.Columns.Add("diagnostico", "Diagnóstico");
            dgvHistorialOdontograma.Columns.Add("tratamiento", "Tratamiento");
            dgvHistorialOdontograma.CellDoubleClick += DgvHistorialOdontograma_CellDoubleClick;

            contenido.Controls.Add(dgvHistorialOdontograma);
            contenido.Controls.Add(lblHistorial);
            contenido.Controls.Add(pnlMapaOdontograma);
            contenido.Controls.Add(ayuda);
            contenido.Controls.Add(tabs);
            contenido.Controls.Add(titulo);
            return contenido;
        }

        private Button CrearBotonDenticion(string texto, int x)
        {
            Button boton = new Button
            {
                Text = texto,
                Location = new Point(x, 2),
                Size = new Size(118, 30),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                ForeColor = Color.FromArgb(51, 65, 85),
                Font = new Font("Segoe UI", 9)
            };
            boton.FlatAppearance.BorderSize = 0;
            return boton;
        }

        private void CambiarDenticion(bool temporal, Button permanentes, Button temporales)
        {
            mostrarDenticionTemporal = temporal;
            permanentes.Font = new Font("Segoe UI", 9, temporal ? FontStyle.Regular : FontStyle.Bold);
            temporales.Font = new Font("Segoe UI", 9, temporal ? FontStyle.Bold : FontStyle.Regular);
            permanentes.ForeColor = temporal ? Color.FromArgb(100, 116, 139) : Color.FromArgb(37, 99, 163);
            temporales.ForeColor = temporal ? Color.FromArgb(37, 99, 163) : Color.FromArgb(100, 116, 139);
            CrearSelectoresPiezas(pnlMapaOdontograma);
        }

        private void DgvHistorialOdontograma_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            SeleccionarPieza(dgvHistorialOdontograma.Rows[e.RowIndex].Cells["pieza"].Value.ToString());
        }

        private void CrearSelectoresPiezas(Panel mapa)
        {
            List<Control> selectores = new List<Control>();
            foreach (Control control in mapa.Controls)
            {
                if (control is Label && control.Tag != null)
                {
                    selectores.Add(control);
                }
            }
            foreach (Control selector in selectores)
            {
                mapa.Controls.Remove(selector);
                selector.Dispose();
            }

            if (mostrarDenticionTemporal)
            {
                string[] superioresTemporales = { "55", "54", "53", "52", "51", "61", "62", "63", "64", "65" };
                string[] inferioresTemporales = { "85", "84", "83", "82", "81", "71", "72", "73", "74", "75" };
                AgregarFilaSelectores(mapa, superioresTemporales, 0.29f, 0.47f);
                AgregarFilaSelectores(mapa, inferioresTemporales, 0.29f, 0.61f);
                return;
            }

            string[] superiores = { "18", "17", "16", "15", "14", "13", "12", "11", "21", "22", "23", "24", "25", "26", "27", "28" };
            string[] inferiores = { "48", "47", "46", "45", "44", "43", "42", "41", "31", "32", "33", "34", "35", "36", "37", "38" };
            AgregarFilaSelectores(mapa, superiores, 0.17f, 0.47f);
            AgregarFilaSelectores(mapa, inferiores, 0.17f, 0.61f);
        }

        private void AgregarFilaSelectores(Panel mapa, string[] piezas, float inicioX, float y)
        {
            for (int i = 0; i < piezas.Length; i++)
            {
                Label selector = new Label
                {
                    Text = piezas[i],
                    Tag = piezas[i],
                    Size = new Size(30, 24),
                    BackColor = Color.FromArgb(220, 239, 246, 255),
                    ForeColor = Color.FromArgb(28, 65, 111),
                    BorderStyle = BorderStyle.FixedSingle,
                    Font = new Font("Segoe UI", 8, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Cursor = Cursors.Hand
                };
                float x = inicioX + i * 0.044f;
                selector.Location = new Point((int)(mapa.Width * x), (int)(mapa.Height * y));
                selector.Anchor = AnchorStyles.None;
                selector.Click += SelectorPieza_Click;
                mapa.Controls.Add(selector);
                selector.BringToFront();
                mapa.Resize += (sender, e) => selector.Location = new Point((int)(mapa.Width * x), (int)(mapa.Height * y));
            }
        }

        private void SelectorPieza_Click(object sender, EventArgs e)
        {
            Label selector = (Label)sender;
            SeleccionarPieza(selector.Tag.ToString());
        }

        private void SeleccionarPieza(string numeroPieza)
        {
            txtNumPieza.Text = numeroPieza;
            txtTipo.Text = ObtenerTipoPieza(numeroPieza);
            registroSeleccionado = odontogramaController.ObtenerUltimoRegistro(idPaciente, numeroPieza);
            LimpiarEditorOdontograma(false);

            if (registroSeleccionado == null)
            {
                return;
            }

            txtTipo.Text = registroSeleccionado.Tipo;
            textBox1.Text = registroSeleccionado.Diagnostico;
            dtpFechaRegistro.Value = registroSeleccionado.FechaRegistro;
            dtpFechaPadecimiento.Value = registroSeleccionado.FechaPadecimiento;
            SeleccionarEstado(registroSeleccionado.Estado);
            SeleccionarTratamiento(registroSeleccionado.Tratamiento);
            foreach (MarcaOdontograma marca in registroSeleccionado.Marcas)
            {
                Panel superficie = marca.Superficie == "Superior" ? pnlVistaSuperior : pnlVistaVertical;
                AgregarMarcaPersistida(superficie, marca);
            }
        }

        private void BtnGuardarOdontograma_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNumPieza.Text))
            {
                MessageBox.Show("Selecciona una pieza dental en el odontograma.", "Pieza requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            OdontogramaRegistro registro = new OdontogramaRegistro
            {
                IdPaciente = idPaciente,
                NumeroPieza = txtNumPieza.Text,
                Tipo = txtTipo.Text,
                Estado = ObtenerEstado(),
                Diagnostico = textBox1.Text.Trim(),
                Tratamiento = cmbTratamiento.Text,
                FechaRegistro = dtpFechaRegistro.Value.Date,
                FechaPadecimiento = dtpFechaPadecimiento.Value.Date,
                Marcas = ObtenerMarcasEditor()
            };

            if (!odontogramaController.GuardarRegistro(registro))
            {
                MessageBox.Show("No se pudo guardar el registro del diente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Registro dental guardado correctamente.", "Odontograma actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SeleccionarPieza(registro.NumeroPieza);
            CargarHistorialOdontograma();
        }

        private void BtnEliminarOdontograma_Click(object sender, EventArgs e)
        {
            if (registroSeleccionado == null)
            {
                return;
            }

            if (MessageBox.Show("¿Deseas eliminar el último registro de esta pieza?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }

            if (odontogramaController.EliminarRegistro(registroSeleccionado.IdRegistro))
            {
                SeleccionarPieza(txtNumPieza.Text);
                CargarHistorialOdontograma();
            }
        }

        private Panel CrearPanelSuperficie(string superficie, Image imagen, Point ubicacion, Size tamano)
        {
            Panel panel = new Panel
            {
                Name = superficie,
                Tag = superficie,
                Location = ubicacion,
                Size = tamano,
                AllowDrop = true,
                BackColor = Color.White
            };
            PictureBox dibujo = new PictureBox
            {
                Dock = DockStyle.Fill,
                Image = imagen,
                SizeMode = PictureBoxSizeMode.Zoom
            };
            panel.Controls.Add(dibujo);
            panel.DragEnter += Superficie_DragEnter;
            panel.DragDrop += Superficie_DragDrop;
            return panel;
        }

        private Panel CrearCirculoArrastrable(string padecimiento, Color color)
        {
            Panel circulo = CrearMarca(color);
            circulo.Tag = padecimiento;
            circulo.Cursor = Cursors.Hand;
            circulo.MouseDown -= Marca_MouseDown;
            circulo.MouseMove -= Marca_MouseMove;
            circulo.MouseUp -= Marca_MouseUp;
            circulo.MouseClick -= Marca_MouseClick;
            circulo.MouseDown += (sender, e) => circulo.DoDragDrop(padecimiento, DragDropEffects.Copy);
            return circulo;
        }

        private void Superficie_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void Superficie_DragDrop(object sender, DragEventArgs e)
        {
            string padecimiento = e.Data.GetData(DataFormats.StringFormat).ToString();
            if (!coloresPadecimientos.ContainsKey(padecimiento))
            {
                return;
            }

            Panel superficie = (Panel)sender;
            Panel marca = CrearMarca(coloresPadecimientos[padecimiento]);
            marca.Tag = padecimiento;
            Point punto = superficie.PointToClient(new Point(e.X, e.Y));
            marca.Location = AjustarMarcaASuperficie(superficie, punto.X - marca.Width / 2, punto.Y - marca.Height / 2);
            superficie.Controls.Add(marca);
            marca.BringToFront();
        }

        private Point AjustarMarcaASuperficie(Panel superficie, int x, int y)
        {
            return new Point(
                Math.Max(0, Math.Min(superficie.Width - 25, x)),
                Math.Max(0, Math.Min(superficie.Height - 25, y)));
        }

        private void AgregarMarcaPersistida(Panel superficie, MarcaOdontograma datos)
        {
            if (!coloresPadecimientos.ContainsKey(datos.Padecimiento))
            {
                return;
            }

            Panel marca = CrearMarca(coloresPadecimientos[datos.Padecimiento]);
            marca.Tag = datos.Padecimiento;
            marca.Location = AjustarMarcaASuperficie(
                superficie,
                (int)(datos.PosicionX * superficie.Width),
                (int)(datos.PosicionY * superficie.Height));
            superficie.Controls.Add(marca);
            marca.BringToFront();
        }

        private List<MarcaOdontograma> ObtenerMarcasEditor()
        {
            List<MarcaOdontograma> marcas = new List<MarcaOdontograma>();
            AgregarMarcasSuperficie(marcas, pnlVistaVertical);
            AgregarMarcasSuperficie(marcas, pnlVistaSuperior);
            return marcas;
        }

        private void AgregarMarcasSuperficie(List<MarcaOdontograma> marcas, Panel superficie)
        {
            foreach (Control control in superficie.Controls)
            {
                Panel marca = control as Panel;
                if (marca == null || marca.Tag == null)
                {
                    continue;
                }

                marcas.Add(new MarcaOdontograma
                {
                    Superficie = superficie.Tag.ToString(),
                    Padecimiento = marca.Tag.ToString(),
                    PosicionX = superficie.Width == 0 ? 0 : (float)marca.Left / superficie.Width,
                    PosicionY = superficie.Height == 0 ? 0 : (float)marca.Top / superficie.Height
                });
            }
        }

        private void LimpiarEditorOdontograma(bool limpiarPieza)
        {
            LimpiarMarcas(pnlVistaVertical);
            LimpiarMarcas(pnlVistaSuperior);
            if (limpiarPieza)
            {
                txtNumPieza.Clear();
                txtTipo.Clear();
            }
            textBox1.Clear();
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            dtpFechaRegistro.Value = DateTime.Today;
            dtpFechaPadecimiento.Value = DateTime.Today;
            if (cmbTratamiento.Items.Count > 0)
            {
                cmbTratamiento.SelectedIndex = 0;
            }
        }

        private void LimpiarMarcas(Panel superficie)
        {
            List<Control> marcas = new List<Control>();
            foreach (Control control in superficie.Controls)
            {
                if (control is Panel && control.Tag != null)
                {
                    marcas.Add(control);
                }
            }
            foreach (Control marca in marcas)
            {
                superficie.Controls.Remove(marca);
                marca.Dispose();
            }
        }

        private void CargarTratamientosOdontograma()
        {
            CitaMedicaController controller = new CitaMedicaController();
            cmbTratamiento.DataSource = controller.ObtenerTratamientos();
            cmbTratamiento.DisplayMember = "Texto";
            cmbTratamiento.ValueMember = "Id";
        }

        private void CargarHistorialOdontograma()
        {
            dgvHistorialOdontograma.Rows.Clear();
            foreach (OdontogramaRegistro registro in odontogramaController.ObtenerHistorial(idPaciente))
            {
                dgvHistorialOdontograma.Rows.Add(
                    registro.FechaRegistro.ToString("dd/MM/yyyy"),
                    registro.FechaPadecimiento.ToString("dd/MM/yyyy"),
                    registro.NumeroPieza,
                    registro.Tipo,
                    registro.Estado,
                    registro.Diagnostico,
                    registro.Tratamiento);
            }
        }

        private string ObtenerEstado()
        {
            if (checkBox4.Checked) return "Extraído";
            if (checkBox3.Checked) return "Restaurado";
            if (checkBox2.Checked) return "Tratado";
            if (checkBox1.Checked) return "Sano";
            return "Sin estado";
        }

        private void SeleccionarEstado(string estado)
        {
            checkBox1.Checked = estado == "Sano";
            checkBox2.Checked = estado == "Tratado";
            checkBox3.Checked = estado == "Restaurado";
            checkBox4.Checked = estado == "Extraído";
        }

        private void SeleccionarTratamiento(string tratamiento)
        {
            for (int i = 0; i < cmbTratamiento.Items.Count; i++)
            {
                if (cmbTratamiento.Items[i].ToString() == tratamiento)
                {
                    cmbTratamiento.SelectedIndex = i;
                    return;
                }
            }
        }

        private string ObtenerTipoPieza(string numeroPieza)
        {
            int unidad = Convert.ToInt32(numeroPieza.Substring(1, 1));
            if (unidad <= 2) return "Incisivo";
            if (unidad == 3) return "Canino";
            if (unidad <= 5) return "Premolar";
            if (unidad <= 7) return "Molar";
            return "Tercer molar (muela del juicio)";
        }

        private TextBox CrearCajaEditor(Panel editor, string etiqueta, int y)
        {
            editor.Controls.Add(CrearLabelEditor(etiqueta, 16, y, 284, 20, 10, true));
            TextBox caja = new TextBox
            {
                Location = new Point(16, y + 24),
                Size = new Size(284, 24),
                Font = new Font("Segoe UI", 9),
                BorderStyle = BorderStyle.FixedSingle
            };
            editor.Controls.Add(caja);
            return caja;
        }

        private DateTimePicker CrearFechaEditor(Panel editor, string etiqueta, int y)
        {
            editor.Controls.Add(CrearLabelEditor(etiqueta, 16, y, 284, 20, 10, true));
            DateTimePicker fecha = new DateTimePicker
            {
                Location = new Point(16, y + 24),
                Size = new Size(284, 24),
                Font = new Font("Segoe UI", 9),
                Format = DateTimePickerFormat.Short
            };
            editor.Controls.Add(fecha);
            return fecha;
        }

        private CheckBox CrearCheckEditor(Panel editor, string texto, int y)
        {
            CheckBox check = new CheckBox
            {
                Text = texto,
                Location = new Point(24, y),
                Size = new Size(150, 20),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.White
            };
            check.CheckedChanged += Estado_CheckedChanged;
            editor.Controls.Add(check);
            return check;
        }

        private void Estado_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox activo = (CheckBox)sender;
            if (!activo.Checked)
            {
                return;
            }
            foreach (CheckBox check in new[] { checkBox1, checkBox2, checkBox3, checkBox4 })
            {
                if (check != activo)
                {
                    check.Checked = false;
                }
            }
        }

        private Label CrearLabelEditor(string texto, int x, int y, int ancho, int alto, float tamano, bool negrita)
        {
            return new Label
            {
                Text = texto,
                Location = new Point(x, y),
                Size = new Size(ancho, alto),
                Font = new Font("Segoe UI", tamano, negrita ? FontStyle.Bold : FontStyle.Regular),
                ForeColor = Color.White
            };
        }

        private Button CrearBotonOdontograma(string texto, Color fondo, int x, int y, int ancho)
        {
            Button boton = new Button
            {
                Text = texto,
                Location = new Point(x, y),
                Size = new Size(ancho, 34),
                FlatStyle = FlatStyle.Flat,
                BackColor = fondo,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };
            boton.FlatAppearance.BorderSize = 0;
            return boton;
        }
    }
}
