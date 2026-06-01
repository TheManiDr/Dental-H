using Dental_H.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dental_H.Controller;
using Dental_H.Model;
using Dental_H.Util;
using Dental_H.DAO;
using Dental_H.Enums;

namespace Dental_H.Components
{
    public partial class HeaderControl : UserControl
    {
        public HeaderControl()
        {
            InitializeComponent();
        }

        private void btnPacientes_Click(object sender, EventArgs e)
        {
            Form actual = this.FindForm();
            PacienteListaForm frm = new PacienteListaForm();
            frm.Show();
            actual.Hide();
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            Form actual = this.FindForm();

            DashboardForm frm = new DashboardForm();
            frm.Show();

            actual.Hide();
        }
    }
}
