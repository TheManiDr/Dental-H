using Dental_H.Controller;
using Dental_H.Model;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Dental_H.View
{
    public class NuevoPacienteForm : Form
    {
        private TextBox txtNombre;
        private TextBox txtApellidoPaterno;
        private TextBox txtApellidoMaterno;
        private DateTimePicker dtpNacimiento;
        private ComboBox cmbGenero;
        private ComboBox cmbSangre;
        private TextBox txtAlergias;
        private TextBox txtTelefono;
        private TextBox txtCorreo;
        private TextBox txtCalle;
        private TextBox txtCiudad;
        private TextBox txtEstado;
        private TextBox txtCodigoPostal;
        private TextBox txtContactoEmergencia;
        private TextBox txtTelefonoEmergencia;

        public NuevoPacienteForm()
        {
            ConfigurarVentana();
            ConstruirFormulario();
        }

        private void ConfigurarVentana()
        {
            Text = "Registrar nuevo paciente";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(860, 620);
            BackColor = Color.FromArgb(245, 247, 250);
        }

        private void ConstruirFormulario()
        {
            Panel contenedor = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(28),
                BackColor = Color.FromArgb(245, 247, 250)
            };

            Panel tarjeta = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(26)
            };

            Label titulo = new Label
            {
                Text = "Agregar nuevo paciente",
                Dock = DockStyle.Top,
                Height = 34,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(28, 65, 111)
            };

            Label subtitulo = new Label
            {
                Text = "Captura la informacion necesaria para dejar al paciente disponible en citas y expediente.",
                Dock = DockStyle.Top,
                Height = 30,
                Font = new Font("Segoe UI", 9.5f),
                ForeColor = Color.FromArgb(92, 105, 119)
            };

            TableLayoutPanel grid = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 8,
                Padding = new Padding(0, 12, 0, 8)
            };

            grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));
            grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));
            grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));

            for (int i = 0; i < 8; i++)
            {
                grid.RowStyles.Add(new RowStyle(SizeType.Absolute, 66));
            }

            txtNombre = CrearTextBox();
            txtApellidoPaterno = CrearTextBox();
            txtApellidoMaterno = CrearTextBox();
            dtpNacimiento = new DateTimePicker
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10),
                Format = DateTimePickerFormat.Short
            };
            cmbGenero = CrearCombo("Masculino", "Femenino");
            cmbSangre = CrearCombo("A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-");
            txtAlergias = CrearTextBox();
            txtTelefono = CrearTextBox();
            txtCorreo = CrearTextBox();
            txtCalle = CrearTextBox();
            txtCiudad = CrearTextBox();
            txtEstado = CrearTextBox();
            txtCodigoPostal = CrearTextBox();
            txtContactoEmergencia = CrearTextBox();
            txtTelefonoEmergencia = CrearTextBox();

            AgregarCampo(grid, "Nombre", txtNombre, 0, 0);
            AgregarCampo(grid, "Apellido paterno", txtApellidoPaterno, 1, 0);
            AgregarCampo(grid, "Apellido materno", txtApellidoMaterno, 2, 0);
            AgregarCampo(grid, "Fecha nacimiento", dtpNacimiento, 0, 1);
            AgregarCampo(grid, "Genero", cmbGenero, 1, 1);
            AgregarCampo(grid, "Tipo de sangre", cmbSangre, 2, 1);
            AgregarCampo(grid, "Alergias", txtAlergias, 0, 2);
            AgregarCampo(grid, "Telefono", txtTelefono, 1, 2);
            AgregarCampo(grid, "Correo electronico", txtCorreo, 2, 2);
            AgregarCampo(grid, "Calle", txtCalle, 0, 3);
            AgregarCampo(grid, "Ciudad", txtCiudad, 1, 3);
            AgregarCampo(grid, "Estado", txtEstado, 2, 3);
            AgregarCampo(grid, "Codigo postal", txtCodigoPostal, 0, 4);
            AgregarCampo(grid, "Contacto emergencia", txtContactoEmergencia, 1, 4);
            AgregarCampo(grid, "Telefono emergencia", txtTelefonoEmergencia, 2, 4);

            FlowLayoutPanel acciones = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                Height = 54,
                FlowDirection = FlowDirection.RightToLeft,
                Padding = new Padding(0, 10, 0, 0)
            };

            Button btnGuardar = CrearBoton("Guardar paciente", Color.FromArgb(37, 99, 163), Color.White);
            Button btnCancelar = CrearBoton("Cancelar", Color.FromArgb(226, 232, 240), Color.FromArgb(31, 41, 55));

            btnGuardar.Click += BtnGuardar_Click;
            btnCancelar.Click += (sender, e) => Close();

            acciones.Controls.Add(btnGuardar);
            acciones.Controls.Add(btnCancelar);

            tarjeta.Controls.Add(grid);
            tarjeta.Controls.Add(acciones);
            tarjeta.Controls.Add(subtitulo);
            tarjeta.Controls.Add(titulo);
            contenedor.Controls.Add(tarjeta);
            Controls.Add(contenedor);
        }

        private TextBox CrearTextBox()
        {
            return new TextBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10),
                BackColor = Color.FromArgb(248, 250, 252),
                BorderStyle = BorderStyle.FixedSingle
            };
        }

        private ComboBox CrearCombo(params string[] opciones)
        {
            ComboBox combo = new ComboBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10),
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.FromArgb(248, 250, 252)
            };

            combo.Items.AddRange(opciones);
            return combo;
        }

        private Button CrearBoton(string texto, Color fondo, Color textoColor)
        {
            Button boton = new Button
            {
                Text = texto,
                Width = 160,
                Height = 36,
                Margin = new Padding(8, 0, 0, 0),
                BackColor = fondo,
                ForeColor = textoColor,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold)
            };

            boton.FlatAppearance.BorderSize = 0;
            return boton;
        }

        private void AgregarCampo(TableLayoutPanel grid, string etiqueta, Control campo, int columna, int fila)
        {
            Panel panelCampo = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(8, 0, 8, 8)
            };

            Label label = new Label
            {
                Text = etiqueta,
                Dock = DockStyle.Top,
                Height = 22,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 65, 85)
            };

            campo.Height = 28;
            panelCampo.Controls.Add(campo);
            panelCampo.Controls.Add(label);
            grid.Controls.Add(panelCampo, columna, fila);
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
            {
                return;
            }

            Paciente paciente = new Paciente
            {
                Nombre = txtNombre.Text.Trim(),
                ApellidoPaterno = txtApellidoPaterno.Text.Trim(),
                ApellidoMaterno = txtApellidoMaterno.Text.Trim(),
                FechaNacimiento = dtpNacimiento.Value.Date,
                Genero = cmbGenero.Text,
                TipoSangre = cmbSangre.Text,
                Alergias = txtAlergias.Text.Trim(),
                Telefono = txtTelefono.Text.Trim(),
                Correo = txtCorreo.Text.Trim(),
                Calle = txtCalle.Text.Trim(),
                Ciudad = txtCiudad.Text.Trim(),
                Estado = txtEstado.Text.Trim(),
                CodigoPostal = txtCodigoPostal.Text.Trim(),
                ContactoEmergencia = txtContactoEmergencia.Text.Trim(),
                NumeroEmergencia = txtTelefonoEmergencia.Text.Trim()
            };

            PacienteController controller = new PacienteController();

            if (!controller.RegistrarPaciente(paciente))
            {
                MessageBox.Show("No se pudo registrar el paciente. Revisa la conexion o los datos capturados.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Paciente registrado correctamente.", "Paciente guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
            Close();
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellidoPaterno.Text) ||
                cmbGenero.SelectedItem == null ||
                cmbSangre.SelectedItem == null)
            {
                MessageBox.Show("Nombre, apellido paterno, genero y tipo de sangre son obligatorios.", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
    }
}
