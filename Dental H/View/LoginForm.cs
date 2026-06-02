using Dental_H.Controller;
using Dental_H.Model;
using Dental_H.Util;
using Dental_H.View;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dental_H
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            ConfigurarEstiloLogin();
        } 


        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void ConfigurarEstiloLogin()
        {
            this.BackColor = Color.FromArgb(238, 244, 249);
            this.Text = "Dental H - Inicio de sesion";

            pictureBox1.BackColor = Color.FromArgb(37, 99, 163);
            pictureBox1.Width = 84;

            pictureBox2.BackColor = Color.FromArgb(232, 241, 250);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.Padding = new Padding(48);

            cerrarLogin.FlatStyle = FlatStyle.Flat;
            cerrarLogin.FlatAppearance.BorderSize = 0;
            cerrarLogin.BackColor = Color.FromArgb(37, 99, 163);
            cerrarLogin.ForeColor = Color.White;
            cerrarLogin.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            cerrarLogin.Size = new Size(42, 36);
            cerrarLogin.Text = "X";
            cerrarLogin.BringToFront();

            panelLogin.BackColor = Color.White;
            panelLogin.Width = 430;
            panelLogin.Padding = new Padding(44, 34, 44, 34);

            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.ColumnStyles.Clear();
            tableLayoutPanel1.RowStyles.Clear();
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.RowCount = 8;
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Padding = new Padding(0);
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 175));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 46));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 34));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 52));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 34));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 52));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            picLogo.Dock = DockStyle.Fill;
            picLogo.Margin = new Padding(0, 0, 0, 12);
            picLogo.SizeMode = PictureBoxSizeMode.Zoom;

            Label lblTitulo = CrearEtiquetaLogin("Bienvenido", 22, FontStyle.Bold, Color.FromArgb(28, 65, 111));
            Label lblSubtitulo = CrearEtiquetaLogin("Ingresa tus credenciales para continuar", 10, FontStyle.Regular, Color.FromArgb(92, 105, 119));
            Label lblUsuario = CrearEtiquetaLogin("Usuario", 10, FontStyle.Bold, Color.FromArgb(45, 55, 72));
            Label lblPassword = CrearEtiquetaLogin("Contrasena", 10, FontStyle.Bold, Color.FromArgb(45, 55, 72));

            EstilizarTextBox(txtUsuario);
            EstilizarTextBox(txtContraseña);
            txtContraseña.UseSystemPasswordChar = true;

            btnLogin.Dock = DockStyle.Top;
            btnLogin.Height = 44;
            btnLogin.Margin = new Padding(0, 16, 0, 0);
            btnLogin.BackColor = Color.FromArgb(37, 99, 163);
            btnLogin.ForeColor = Color.White;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnLogin.Text = "Entrar";

            tableLayoutPanel1.Controls.Add(picLogo, 0, 0);
            tableLayoutPanel1.Controls.Add(lblTitulo, 0, 1);
            tableLayoutPanel1.Controls.Add(lblSubtitulo, 0, 2);
            tableLayoutPanel1.Controls.Add(lblUsuario, 0, 3);
            tableLayoutPanel1.Controls.Add(txtUsuario, 0, 4);
            tableLayoutPanel1.Controls.Add(lblPassword, 0, 5);
            tableLayoutPanel1.Controls.Add(txtContraseña, 0, 6);
            tableLayoutPanel1.Controls.Add(btnLogin, 0, 7);

            this.AcceptButton = btnLogin;
        }

        private Label CrearEtiquetaLogin(string texto, float size, FontStyle style, Color color)
        {
            return new Label
            {
                Text = texto,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", size, style),
                ForeColor = color,
                TextAlign = ContentAlignment.MiddleLeft,
                AutoSize = false
            };
        }

        private void EstilizarTextBox(TextBox textBox)
        {
            textBox.Dock = DockStyle.Top;
            textBox.Height = 36;
            textBox.Margin = new Padding(0, 0, 0, 8);
            textBox.Font = new Font("Segoe UI", 12, FontStyle.Regular);
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.BackColor = Color.FromArgb(248, 250, 252);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // CAPTURAR DATOS
            string usuario = txtUsuario.Text;
            string contraseña = txtContraseña.Text;

            // CREAR CONTROLLER
            LoginController controller = new LoginController();

            // INTENTAR LOGIN
            Usuario usuarioEncontrado = controller.IniciarSesion(usuario,contraseña);

            // VALIDAR RESULTADO
            if (usuarioEncontrado != null)
            {
                Sesion.UsuarioActual = usuarioEncontrado;
                DashboardForm dashboard = new DashboardForm();
                AppNavigator.AbrirDesdeLogin(this, dashboard);

            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos");
            }
        }

        private void panelLogin_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {

        }

        private void cerrarLogin_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
