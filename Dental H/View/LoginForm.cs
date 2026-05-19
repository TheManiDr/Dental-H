using Dental_H.Controller;
using Dental_H.Model;
using Dental_H.Util;
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

            try
            {
                MySqlConnection conexion = Conexion.obtenerConexion();

                conexion.Open();

                MessageBox.Show("Conexión exitosa con MySQL");

                conexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
                MessageBox.Show("Bienvenido " +usuarioEncontrado.NombreUsuario);
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos");
            }
        }
    }
}
