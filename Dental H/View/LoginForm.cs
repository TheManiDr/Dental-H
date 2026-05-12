
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
    }
}
