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
using Dental_H.Enums;
using Dental_H.Controller;
using Dental_H.Model;
using Dental_H.View;

namespace Dental_H.View
{
    public partial class DashboardForm : Form
    {
        public DashboardForm()
        {
            InitializeComponent();
            MessageBox.Show("Bienvenido " + Sesion.UsuarioActual.NombreUsuario);
            MessageBox.Show("Rol: " + Sesion.UsuarioActual.IdRol);
            if (Sesion.UsuarioActual.IdRol != (int)Rol.ADMINISTRADOR)
            {
                btnUsuarios.Enabled = false;
            }
        }



        private void DashboardForm_Load(object sender, EventArgs e)
        {

        }

        private void btnPacientes_Click(object sender, EventArgs e)
        {
            PacienteForm pacientes = new PacienteForm();
            pacientes.Show();
            this.Hide();
        }
    }
}
