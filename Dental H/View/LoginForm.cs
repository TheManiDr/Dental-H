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
        } 


        private void LoginForm_Load(object sender, EventArgs e)
        {

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
