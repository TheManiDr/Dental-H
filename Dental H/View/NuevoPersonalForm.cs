using Dental_H.Controller;
using Dental_H.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Dental_H.View
{
    public class NuevoPersonalForm : Form
    {
        private TextBox txtNombre;
        private TextBox txtApellidoPaterno;
        private TextBox txtApellidoMaterno;
        private DateTimePicker dtpNacimiento;
        private ComboBox cmbGenero;
        private TextBox txtCurp;
        private TextBox txtRfc;
        private TextBox txtNss;
        private ComboBox cmbRol;
        private TextBox txtUsuario;
        private TextBox txtContrasena;

        public NuevoPersonalForm()
        {
            ConfigurarVentana();
            ConstruirFormulario();
            CargarRoles();
        }

        private void ConfigurarVentana()
        {
            Text = "Registrar nuevo personal";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(760, 520);
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
                Text = "Agregar nuevo personal",
                Dock = DockStyle.Top,
                Height = 34,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(28, 65, 111)
            };

            Label subtitulo = new Label
            {
                Text = "Registra los datos del colaborador y, si aplica, su acceso al sistema.",
                Dock = DockStyle.Top,
                Height = 30,
                Font = new Font("Segoe UI", 9.5f),
                ForeColor = Color.FromArgb(92, 105, 119)
            };

            TableLayoutPanel grid = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 5,
                Padding = new Padding(0, 12, 0, 8)
            };

            grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));
            grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));
            grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));

            for (int i = 0; i < 5; i++)
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
            txtCurp = CrearTextBox();
            txtRfc = CrearTextBox();
            txtNss = CrearTextBox();
            cmbRol = CrearCombo();
            txtUsuario = CrearTextBox();
            txtContrasena = CrearTextBox();
            txtContrasena.UseSystemPasswordChar = true;

            AgregarCampo(grid, "Nombre", txtNombre, 0, 0);
            AgregarCampo(grid, "Apellido paterno", txtApellidoPaterno, 1, 0);
            AgregarCampo(grid, "Apellido materno", txtApellidoMaterno, 2, 0);
            AgregarCampo(grid, "Fecha nacimiento", dtpNacimiento, 0, 1);
            AgregarCampo(grid, "Genero", cmbGenero, 1, 1);
            AgregarCampo(grid, "Rol", cmbRol, 2, 1);
            AgregarCampo(grid, "CURP", txtCurp, 0, 2);
            AgregarCampo(grid, "RFC", txtRfc, 1, 2);
            AgregarCampo(grid, "NSS", txtNss, 2, 2);
            AgregarCampo(grid, "Usuario", txtUsuario, 0, 3);
            AgregarCampo(grid, "Contrasena", txtContrasena, 1, 3);

            FlowLayoutPanel acciones = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                Height = 54,
                FlowDirection = FlowDirection.RightToLeft,
                Padding = new Padding(0, 10, 0, 0)
            };

            Button btnGuardar = CrearBoton("Guardar personal", Color.FromArgb(37, 99, 163), Color.White);
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

        private void CargarRoles()
        {
            PersonalController controller = new PersonalController();
            List<ComboItem> roles = controller.ObtenerRoles();
            cmbRol.Items.Clear();

            foreach (ComboItem rol in roles)
            {
                cmbRol.Items.Add(rol);
            }

            if (cmbRol.Items.Count > 0)
            {
                cmbRol.SelectedIndex = 0;
            }
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

            ComboItem rol = cmbRol.SelectedItem as ComboItem;

            PersonalInfo personal = new PersonalInfo
            {
                Nombre = txtNombre.Text.Trim(),
                ApellidoPaterno = txtApellidoPaterno.Text.Trim(),
                ApellidoMaterno = txtApellidoMaterno.Text.Trim(),
                FechaNacimiento = dtpNacimiento.Value.Date,
                Genero = cmbGenero.Text,
                Curp = txtCurp.Text.Trim(),
                Rfc = txtRfc.Text.Trim(),
                Nss = txtNss.Text.Trim(),
                IdRol = rol == null ? 0 : rol.Id,
                Rol = rol == null ? string.Empty : rol.Texto,
                NombreUsuario = txtUsuario.Text.Trim(),
                Contrasena = txtContrasena.Text.Trim()
            };

            PersonalController controller = new PersonalController();

            if (!controller.RegistrarPersonal(personal))
            {
                MessageBox.Show("No se pudo registrar el personal. Revisa la conexion o los datos capturados.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Personal registrado correctamente.", "Personal guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
            Close();
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellidoPaterno.Text) ||
                cmbGenero.SelectedItem == null ||
                cmbRol.SelectedItem == null)
            {
                MessageBox.Show("Nombre, apellido paterno, genero y rol son obligatorios.", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            bool usuarioCapturado = !string.IsNullOrWhiteSpace(txtUsuario.Text);
            bool contrasenaCapturada = !string.IsNullOrWhiteSpace(txtContrasena.Text);

            if (usuarioCapturado != contrasenaCapturada)
            {
                MessageBox.Show("Si capturas usuario tambien debes capturar contrasena, y viceversa.", "Credenciales incompletas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
    }
}
