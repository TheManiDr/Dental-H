using Dental_H.Components;
using Dental_H.Controller;
using Dental_H.Model;
using Dental_H.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Dental_H.View
{
    public class PersonalDetalleForm : Form
    {
        private readonly int idPersonal;
        private readonly Form ventanaAnterior;
        private PersonalInfo personalActual;

        private Panel panelContenido;
        private Panel panelTabs;
        private Panel panelPerfil;
        private PictureBox picAvatar;
        private Label lblNombre;
        private Label lblEdad;
        private Label lblRol;
        private Label lblEspecialidad;
        private Label lblContacto;
        private Button btnDatosPersonales;
        private Button btnInfoLaboral;
        private Button btnTratamientos;
        private Button btnActualizar;
        private Button btnGuardar;
        private Button btnEliminar;
        private Button btnCancelar;
        private bool modoEdicion;
        private Dictionary<string, TextBox> camposEditables = new Dictionary<string, TextBox>();
        private DateTimePicker dtpFechaNacimiento;

        public PersonalDetalleForm(int idPersonal, Form anterior)
        {
            this.idPersonal = idPersonal;
            ventanaAnterior = anterior;
            ConfigurarVentana();
            ConstruirBase();
            CargarPersonal();
        }

        private void ConfigurarVentana()
        {
            Text = "Detalle del personal";
            WindowState = FormWindowState.Maximized;
            BackColor = Color.FromArgb(245, 247, 250);
        }

        private void ConstruirBase()
        {
            Panel panelPrincipal = new Panel { Dock = DockStyle.Fill, BackColor = Color.FromArgb(245, 247, 250) };
            HeaderControl header = new HeaderControl { Dock = DockStyle.Top, Height = 110 };

            panelPerfil = new Panel
            {
                Dock = DockStyle.Top,
                Height = 96,
                BackColor = Color.FromArgb(86, 141, 214),
                Padding = new Padding(28, 10, 28, 10)
            };

            picAvatar = new PictureBox
            {
                Location = new Point(28, 8),
                Size = new Size(80, 80),
                SizeMode = PictureBoxSizeMode.Zoom
            };

            Label lblTipo = CrearTextoPerfil("Personal:", new Point(140, 18), new Size(240, 20), 9.5f, false);
            lblNombre = CrearTextoPerfil("", new Point(140, 36), new Size(430, 28), 14.5f, true);
            lblEdad = CrearTextoPerfil("", new Point(140, 64), new Size(160, 22), 10, true);
            lblRol = CrearTextoPerfil("", new Point(610, 20), new Size(260, 54), 10, false);
            lblEspecialidad = CrearTextoPerfil("", new Point(890, 20), new Size(230, 54), 10, false);
            lblContacto = CrearTextoPerfil("", new Point(1140, 18), new Size(430, 60), 9.5f, false);

            Panel separadorRol = CrearSeparadorPerfil(590);
            Panel separadorEspecialidad = CrearSeparadorPerfil(875);
            Panel separadorContacto = CrearSeparadorPerfil(1125);

            panelPerfil.Controls.Add(picAvatar);
            panelPerfil.Controls.Add(lblTipo);
            panelPerfil.Controls.Add(lblNombre);
            panelPerfil.Controls.Add(lblEdad);
            panelPerfil.Controls.Add(separadorRol);
            panelPerfil.Controls.Add(separadorEspecialidad);
            panelPerfil.Controls.Add(separadorContacto);
            panelPerfil.Controls.Add(lblRol);
            panelPerfil.Controls.Add(lblEspecialidad);
            panelPerfil.Controls.Add(lblContacto);

            panelTabs = new Panel
            {
                Dock = DockStyle.Top,
                Height = 48,
                BackColor = Color.FromArgb(221, 235, 250)
            };

            btnDatosPersonales = CrearTab("Datos Personales", 28, 10, 140);
            btnInfoLaboral = CrearTab("Informacion Laboral", 178, 10, 160);
            btnTratamientos = CrearTab("Tratamientos Asignados", 348, 10, 190);

            btnDatosPersonales.Click += (sender, e) => MostrarDatosPersonales();
            btnInfoLaboral.Click += (sender, e) => MostrarInformacionLaboral();
            btnTratamientos.Click += (sender, e) => MostrarTratamientosAsignados();

            panelTabs.Controls.Add(btnDatosPersonales);
            panelTabs.Controls.Add(btnInfoLaboral);
            panelTabs.Controls.Add(btnTratamientos);

            panelContenido = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(221, 235, 250),
                Padding = new Padding(16),
                AutoScroll = true
            };

            panelPrincipal.Controls.Add(panelContenido);
            panelPrincipal.Controls.Add(panelTabs);
            panelPrincipal.Controls.Add(panelPerfil);
            panelPrincipal.Controls.Add(header);
            Controls.Add(panelPrincipal);
        }

        private Label CrearTextoPerfil(string texto, Point location, Size size, float tamano, bool bold)
        {
            return new Label
            {
                Text = texto,
                Location = location,
                Size = size,
                Font = new Font("Segoe UI", tamano, bold ? FontStyle.Bold : FontStyle.Regular),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleLeft
            };
        }

        private Panel CrearSeparadorPerfil(int x)
        {
            return new Panel
            {
                Location = new Point(x, 22),
                Size = new Size(1, 52),
                BackColor = Color.FromArgb(221, 235, 250)
            };
        }

        private Button CrearTab(string texto, int x, int y, int ancho)
        {
            Button boton = new Button
            {
                Text = texto,
                Location = new Point(x, y),
                Size = new Size(ancho, 30),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = Color.FromArgb(28, 65, 111),
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold)
            };

            boton.FlatAppearance.BorderSize = 0;
            return boton;
        }

        private void CargarPersonal()
        {
            PersonalController controller = new PersonalController();
            personalActual = controller.ObtenerPersonalPorId(idPersonal);

            if (personalActual == null)
            {
                MessageBox.Show("Personal no encontrado.", "Sin datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            lblNombre.Text = NombreCompleto(personalActual);
            lblEdad.Text = CalcularEdad(personalActual.FechaNacimiento) + " anos";
            lblRol.Text = "Cargo / Rol:" + Environment.NewLine + Valor(personalActual.Rol);
            lblEspecialidad.Text = "Especialidad:" + Environment.NewLine + Valor(personalActual.Rol);
            lblContacto.Text = "Contacto" + Environment.NewLine + "Numero: " + Valor(personalActual.Telefono) + Environment.NewLine + "Correo: " + Valor(personalActual.Correo);
            picAvatar.Image = personalActual.Genero == "Masculino" ? Properties.Resources.avatar_hombre : Properties.Resources.avatar_mujer;

            MostrarDatosPersonales();
        }

        private void MostrarDatosPersonales()
        {
            MarcarTab(btnDatosPersonales);
            panelContenido.Controls.Clear();
            camposEditables.Clear();
            dtpFechaNacimiento = null;

            Panel hoja = CrearHojaContenido();
            TableLayoutPanel layout = CrearLayoutColumnas(3);
            Panel infoBasica = CrearSeccion("Informacion Basica");
            Panel documentos = CrearSeccion("Documentos de Identificacion");
            Panel direccion = CrearSeccion("Direccion");
            Panel contacto = CrearSeccion("Informacion de Contacto");
            TableLayoutPanel columnaContacto = CrearLayoutVertical(2);

            AgregarCampoEditable(infoBasica, "Nombre(s)", "nombre", personalActual.Nombre);
            AgregarCampoEditable(infoBasica, "Apellido Paterno", "apellidoPaterno", personalActual.ApellidoPaterno);
            AgregarCampoEditable(infoBasica, "Apellido Materno", "apellidoMaterno", personalActual.ApellidoMaterno);
            AgregarDatoDoble(infoBasica, "Edad", CalcularEdad(personalActual.FechaNacimiento).ToString(), "Genero", personalActual.Genero);
            if (modoEdicion)
            {
                AgregarFecha(infoBasica, "Fecha de nacimiento", personalActual.FechaNacimiento);
            }
            else
            {
                AgregarDato(infoBasica, "Fecha de nacimiento", personalActual.FechaNacimiento.ToString("dd/MM/yyyy"));
            }

            AgregarCampoEditable(documentos, "CURP", "curp", personalActual.Curp);
            AgregarCampoEditable(documentos, "RFC", "rfc", personalActual.Rfc);
            AgregarCampoEditable(documentos, "NSS", "nss", personalActual.Nss);
            AgregarDato(documentos, "ID de personal", "PER-" + personalActual.IdPersona.ToString("000"));

            AgregarCampoEditable(direccion, "Direccion", "calle", personalActual.Calle);
            AgregarCamposEditablesDobles(direccion, "Ciudad", "ciudad", personalActual.Ciudad, "Estado", "estado", personalActual.Estado);
            AgregarCamposEditablesDobles(direccion, "Pais", "pais", "Mexico", "C. P.", "codigoPostal", personalActual.CodigoPostal);
            AgregarCampoEditable(contacto, "Telefono", "telefono", personalActual.Telefono);
            AgregarCampoEditable(contacto, "Correo electronico", "correo", personalActual.Correo);

            columnaContacto.Controls.Add(direccion, 0, 0);
            columnaContacto.Controls.Add(contacto, 0, 1);
            layout.Controls.Add(infoBasica, 0, 0);
            layout.Controls.Add(documentos, 1, 0);
            layout.Controls.Add(columnaContacto, 2, 0);
            hoja.Controls.Add(layout);
            TraerBotonesAccionAlFrente(hoja);
            panelContenido.Controls.Add(hoja);
        }

        private void MostrarInformacionLaboral()
        {
            MarcarTab(btnInfoLaboral);
            panelContenido.Controls.Clear();
            camposEditables.Clear();
            dtpFechaNacimiento = null;

            Panel hoja = CrearHojaContenido();
            TableLayoutPanel layout = CrearLayoutColumnas(2);
            Panel laboral = CrearSeccion("Informacion Laboral");
            Panel acceso = CrearSeccion("Acceso al Sistema");

            AgregarCampoEditable(laboral, "Rol", "rolTexto", personalActual.Rol);
            AgregarCampoEditable(laboral, "CURP", "curp", personalActual.Curp);
            AgregarCampoEditable(laboral, "RFC", "rfc", personalActual.Rfc);
            AgregarCampoEditable(laboral, "NSS", "nss", personalActual.Nss);

            AgregarCampoEditable(acceso, "Usuario", "usuario", personalActual.NombreUsuario);
            AgregarCampoEditable(acceso, "Contrasena", "contrasena", personalActual.Contrasena);

            layout.Controls.Add(laboral, 0, 0);
            layout.Controls.Add(acceso, 1, 0);
            hoja.Controls.Add(layout);
            TraerBotonesAccionAlFrente(hoja);
            panelContenido.Controls.Add(hoja);
        }

        private void MostrarTratamientosAsignados()
        {
            MarcarTab(btnTratamientos);
            panelContenido.Controls.Clear();

            Panel hoja = CrearHojaContenido();
            Panel card = CrearSeccion("Tratamientos Asignados");
            card.Dock = DockStyle.Fill;
            card.Padding = new Padding(24, 58, 24, 24);

            DataGridView tabla = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                RowHeadersVisible = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            tabla.Columns.Add("fecha", "Fecha");
            tabla.Columns.Add("hora", "Hora");
            tabla.Columns.Add("paciente", "Paciente");
            tabla.Columns.Add("tratamiento", "Tratamiento / Motivo");

            PersonalController controller = new PersonalController();
            List<ConsultaInfo> consultas = controller.ObtenerTratamientosAsignados(idPersonal);

            foreach (ConsultaInfo consulta in consultas)
            {
                tabla.Rows.Add(
                    consulta.Fecha.ToString("dd/MM/yyyy"),
                    DateTime.Today.Add(consulta.Hora).ToString("hh:mm tt"),
                    consulta.Paciente,
                    consulta.Descripcion);
            }

            if (consultas.Count == 0)
            {
                tabla.Rows.Add("Sin registros", "", "", "No hay tratamientos o consultas asignadas.");
            }

            card.Controls.Add(tabla);
            hoja.Controls.Add(card);
            TraerBotonesAccionAlFrente(hoja);
            panelContenido.Controls.Add(hoja);
        }

        private TableLayoutPanel CrearLayoutColumnas(int columnas)
        {
            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = columnas,
                RowCount = 1,
                BackColor = Color.White,
                Padding = new Padding(8)
            };

            for (int i = 0; i < columnas; i++)
            {
                layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / columnas));
            }

            return layout;
        }

        private TableLayoutPanel CrearLayoutVertical(int filas)
        {
            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = filas,
                BackColor = Color.White
            };

            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));

            for (int i = 0; i < filas; i++)
            {
                layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / filas));
            }

            return layout;
        }

        private Panel CrearHojaContenido()
        {
            Panel hoja = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(20),
                Margin = new Padding(0)
            };

            hoja.Paint += (sender, e) =>
            {
                ControlPaint.DrawBorder(e.Graphics, hoja.ClientRectangle, Color.FromArgb(203, 213, 225), ButtonBorderStyle.Solid);
            };

            btnActualizar = CrearBotonAccion("Editar", Color.FromArgb(86, 141, 214), Color.White);
            btnGuardar = CrearBotonAccion("Guardar", Color.FromArgb(46, 125, 84), Color.White);
            btnEliminar = CrearBotonAccion("Eliminar", Color.FromArgb(235, 87, 87), Color.White);
            btnCancelar = CrearBotonAccion("Cancelar", Color.White, Color.FromArgb(51, 65, 85));

            btnActualizar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnGuardar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnEliminar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCancelar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnActualizar.Click += BtnActualizar_Click;
            btnGuardar.Click += BtnGuardar_Click;
            btnEliminar.Click += BtnEliminar_Click;
            btnCancelar.Click += BtnCancelar_Click;
            btnCancelar.FlatAppearance.BorderSize = 1;
            btnCancelar.FlatAppearance.BorderColor = Color.FromArgb(100, 116, 139);

            btnActualizar.Visible = !modoEdicion;
            btnEliminar.Visible = !modoEdicion;
            btnGuardar.Visible = modoEdicion;
            btnCancelar.Visible = modoEdicion;

            hoja.Resize += (sender, e) =>
            {
                btnEliminar.Location = new Point(hoja.Width - 152, 18);
                btnActualizar.Location = new Point(hoja.Width - 284, 18);
                btnCancelar.Location = new Point(hoja.Width - 152, 18);
                btnGuardar.Location = new Point(hoja.Width - 284, 18);
            };

            hoja.Controls.Add(btnActualizar);
            hoja.Controls.Add(btnGuardar);
            hoja.Controls.Add(btnEliminar);
            hoja.Controls.Add(btnCancelar);

            return hoja;
        }

        private Button CrearBotonAccion(string texto, Color fondo, Color colorTexto)
        {
            Button boton = new Button
            {
                Text = texto,
                Size = new Size(120, 34),
                BackColor = fondo,
                ForeColor = colorTexto,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8.5f, FontStyle.Bold)
            };

            boton.FlatAppearance.BorderSize = 0;
            return boton;
        }

        private void TraerBotonesAccionAlFrente(Panel hoja)
        {
            if (btnActualizar == null || btnGuardar == null || btnEliminar == null || btnCancelar == null)
            {
                return;
            }

            btnActualizar.BringToFront();
            btnGuardar.BringToFront();
            btnEliminar.BringToFront();
            btnCancelar.BringToFront();
        }

        private Panel CrearSeccion(string titulo)
        {
            Panel card = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                BorderStyle = BorderStyle.None,
                Margin = new Padding(12),
                Padding = new Padding(16, 54, 16, 16),
                Tag = 58
            };

            Label labelTitulo = new Label
            {
                Text = titulo,
                Location = new Point(16, 14),
                Size = new Size(360, 32),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(37, 99, 163),
                TextAlign = ContentAlignment.MiddleLeft
            };

            Panel linea = new Panel
            {
                Location = new Point(16, 48),
                Size = new Size(card.Width - 32, 2),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.FromArgb(86, 141, 214)
            };

            card.Controls.Add(labelTitulo);
            card.Controls.Add(linea);
            return card;
        }

        private void AgregarDato(Panel card, string etiqueta, string valor)
        {
            int y = card.Tag is int ? (int)card.Tag : 58;

            Label lblEtiqueta = new Label
            {
                Text = etiqueta,
                Location = new Point(16, y),
                Size = new Size(card.Width - 32, 18),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Font = new Font("Segoe UI", 8.5f),
                ForeColor = Color.FromArgb(70, 70, 70)
            };

            Label lblValor = new Label
            {
                Text = Valor(valor),
                Location = new Point(16, y + 18),
                Size = new Size(card.Width - 32, 24),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = Color.FromArgb(20, 33, 45)
            };

            card.Controls.Add(lblEtiqueta);
            card.Controls.Add(lblValor);
            card.Tag = y + 54;
        }

        private void AgregarCampoEditable(Panel card, string etiqueta, string clave, string valor)
        {
            int y = card.Tag is int ? (int)card.Tag : 58;

            Label lblEtiqueta = new Label
            {
                Text = etiqueta,
                Location = new Point(16, y),
                Size = new Size(card.Width - 32, 18),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Font = new Font("Segoe UI", 8.5f),
                ForeColor = Color.FromArgb(70, 70, 70)
            };

            TextBox txtValor = CrearTextBoxEditable(valor);
            txtValor.Location = new Point(16, y + 18);
            txtValor.Size = new Size(card.Width - 32, 24);
            txtValor.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            camposEditables[clave] = txtValor;
            card.Controls.Add(lblEtiqueta);
            card.Controls.Add(txtValor);
            card.Tag = y + 54;
        }

        private void AgregarCamposEditablesDobles(Panel card, string etiquetaUno, string claveUno, string valorUno, string etiquetaDos, string claveDos, string valorDos)
        {
            int y = card.Tag is int ? (int)card.Tag : 58;
            int ancho = Math.Max(120, (card.Width - 48) / 2);

            Label lblEtiquetaUno = CrearLabelDato(etiquetaUno, new Point(16, y), new Size(ancho, 18), false);
            TextBox txtUno = CrearTextBoxEditable(valorUno);
            txtUno.Location = new Point(16, y + 18);
            txtUno.Size = new Size(ancho, 24);

            Label lblEtiquetaDos = CrearLabelDato(etiquetaDos, new Point(32 + ancho, y), new Size(ancho, 18), false);
            TextBox txtDos = CrearTextBoxEditable(valorDos);
            txtDos.Location = new Point(32 + ancho, y + 18);
            txtDos.Size = new Size(ancho, 24);

            camposEditables[claveUno] = txtUno;
            camposEditables[claveDos] = txtDos;
            card.Controls.Add(lblEtiquetaUno);
            card.Controls.Add(txtUno);
            card.Controls.Add(lblEtiquetaDos);
            card.Controls.Add(txtDos);
            card.Tag = y + 54;
        }

        private TextBox CrearTextBoxEditable(string valor)
        {
            TextBox textBox = new TextBox
            {
                Text = Valor(valor),
                Font = new Font("Segoe UI", 10, FontStyle.Regular),
                ForeColor = Color.FromArgb(20, 33, 45),
                ReadOnly = !modoEdicion,
                BorderStyle = modoEdicion ? BorderStyle.FixedSingle : BorderStyle.None,
                BackColor = modoEdicion ? Color.FromArgb(248, 250, 252) : Color.White
            };

            return textBox;
        }

        private void AgregarFecha(Panel card, string etiqueta, DateTime fecha)
        {
            int y = card.Tag is int ? (int)card.Tag : 58;

            Label lblEtiqueta = new Label
            {
                Text = etiqueta,
                Location = new Point(16, y),
                Size = new Size(card.Width - 32, 18),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Font = new Font("Segoe UI", 8.5f),
                ForeColor = Color.FromArgb(70, 70, 70)
            };

            dtpFechaNacimiento = new DateTimePicker
            {
                Location = new Point(16, y + 18),
                Size = new Size(180, 24),
                Font = new Font("Segoe UI", 10),
                Format = DateTimePickerFormat.Short,
                Enabled = modoEdicion,
                Value = fecha == DateTime.MinValue ? DateTime.Today : fecha
            };

            card.Controls.Add(lblEtiqueta);
            card.Controls.Add(dtpFechaNacimiento);
            card.Tag = y + 54;
        }

        private void AgregarDatoDoble(Panel card, string etiquetaUno, string valorUno, string etiquetaDos, string valorDos)
        {
            int y = card.Tag is int ? (int)card.Tag : 58;
            int ancho = Math.Max(120, (card.Width - 48) / 2);

            Label lblEtiquetaUno = CrearLabelDato(etiquetaUno, new Point(16, y), new Size(ancho, 18), false);
            Label lblValorUno = CrearLabelDato(Valor(valorUno), new Point(16, y + 18), new Size(ancho, 24), true);
            Label lblEtiquetaDos = CrearLabelDato(etiquetaDos, new Point(32 + ancho, y), new Size(ancho, 18), false);
            Label lblValorDos = CrearLabelDato(Valor(valorDos), new Point(32 + ancho, y + 18), new Size(ancho, 24), true);

            lblEtiquetaUno.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            lblValorUno.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            lblEtiquetaDos.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            lblValorDos.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            card.Controls.Add(lblEtiquetaUno);
            card.Controls.Add(lblValorUno);
            card.Controls.Add(lblEtiquetaDos);
            card.Controls.Add(lblValorDos);
            card.Tag = y + 54;
        }

        private Label CrearLabelDato(string texto, Point ubicacion, Size tamano, bool valor)
        {
            return new Label
            {
                Text = texto,
                Location = ubicacion,
                Size = tamano,
                Font = new Font("Segoe UI", valor ? 10 : 8.5f, FontStyle.Regular),
                ForeColor = valor ? Color.FromArgb(20, 33, 45) : Color.FromArgb(70, 70, 70),
                TextAlign = ContentAlignment.MiddleLeft
            };
        }

        private void MarcarTab(Button activo)
        {
            foreach (Control control in panelTabs.Controls)
            {
                Button boton = control as Button;
                if (boton == null) continue;

                boton.BackColor = boton == activo ? Color.White : Color.Transparent;
                boton.ForeColor = Color.FromArgb(28, 65, 111);
            }
        }

        private void BtnActualizar_Click(object sender, EventArgs e)
        {
            modoEdicion = true;
            MostrarPestanaActual();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            modoEdicion = false;
            MostrarPestanaActual();
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (!modoEdicion)
            {
                return;
            }

            PersonalInfo personal = new PersonalInfo
            {
                IdPersona = personalActual.IdPersona,
                Nombre = ObtenerValorCampo("nombre", personalActual.Nombre),
                ApellidoPaterno = ObtenerValorCampo("apellidoPaterno", personalActual.ApellidoPaterno),
                ApellidoMaterno = ObtenerValorCampo("apellidoMaterno", personalActual.ApellidoMaterno),
                FechaNacimiento = dtpFechaNacimiento == null ? personalActual.FechaNacimiento : dtpFechaNacimiento.Value.Date,
                Genero = personalActual.Genero,
                Curp = ObtenerValorCampo("curp", personalActual.Curp),
                Rfc = ObtenerValorCampo("rfc", personalActual.Rfc),
                Nss = ObtenerValorCampo("nss", personalActual.Nss),
                IdRol = personalActual.IdRol,
                Rol = personalActual.Rol,
                NombreUsuario = ObtenerValorCampo("usuario", personalActual.NombreUsuario),
                Contrasena = ObtenerValorCampo("contrasena", personalActual.Contrasena),
                Calle = ObtenerValorCampo("calle", personalActual.Calle),
                Ciudad = ObtenerValorCampo("ciudad", personalActual.Ciudad),
                Estado = ObtenerValorCampo("estado", personalActual.Estado),
                CodigoPostal = ObtenerValorCampo("codigoPostal", personalActual.CodigoPostal),
                Telefono = ObtenerValorCampo("telefono", personalActual.Telefono),
                Correo = ObtenerValorCampo("correo", personalActual.Correo)
            };

            if (string.IsNullOrWhiteSpace(personal.Nombre) || string.IsNullOrWhiteSpace(personal.ApellidoPaterno))
            {
                MessageBox.Show("Nombre y apellido paterno son obligatorios.", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            PersonalController controller = new PersonalController();

            if (!controller.ActualizarPersonal(personal))
            {
                MessageBox.Show("No se pudieron guardar los cambios.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Cambios guardados correctamente.", "Personal actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            modoEdicion = false;
            CargarPersonal();
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show(
                "Seguro que deseas eliminar este personal?",
                "Confirmar eliminacion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (respuesta != DialogResult.Yes)
            {
                return;
            }

            PersonalController controller = new PersonalController();

            if (!controller.EliminarPersonal(idPersonal))
            {
                return;
            }

            MessageBox.Show("Personal eliminado correctamente.", "Personal eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            AppNavigator.IrA(this, new Personal());
        }

        private void MostrarPestanaActual()
        {
            if (btnInfoLaboral.BackColor == Color.White)
            {
                MostrarInformacionLaboral();
                return;
            }

            if (btnTratamientos.BackColor == Color.White)
            {
                MostrarTratamientosAsignados();
                return;
            }

            MostrarDatosPersonales();
        }

        private string ObtenerValorCampo(string clave, string valorActual)
        {
            if (!camposEditables.ContainsKey(clave))
            {
                return valorActual ?? string.Empty;
            }

            string valor = camposEditables[clave].Text.Trim();
            return valor == "Sin dato" ? string.Empty : valor;
        }

        private string NombreCompleto(PersonalInfo personal)
        {
            return string.Join(" ", new[] { personal.Nombre, personal.ApellidoPaterno, personal.ApellidoMaterno });
        }

        private string Valor(string valor)
        {
            return string.IsNullOrWhiteSpace(valor) ? "Sin dato" : valor;
        }

        private int CalcularEdad(DateTime fechaNacimiento)
        {
            if (fechaNacimiento == DateTime.MinValue)
            {
                return 0;
            }

            int edad = DateTime.Now.Year - fechaNacimiento.Year;
            if (DateTime.Now < fechaNacimiento.AddYears(edad))
            {
                edad--;
            }
            return edad;
        }
    }
}
