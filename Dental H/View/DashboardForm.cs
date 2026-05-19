using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dental_H.Util;
namespace Dental_H.View
{
    public partial class DashboardForm : Form
    {
        public DashboardForm()
        {
            InitializeComponent();
            MessageBox.Show("Bienvenido " + Sesion.UsuarioActual.NombreUsuario);
            MessageBox.Show("Rol: " + Sesion.UsuarioActual.IdRol);
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {

        }
    }
}
