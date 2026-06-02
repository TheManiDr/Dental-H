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
            frm.Show(); // Muestra la nueva ventana primero

            if (actual != null) actual.Close(); // Cierra definitivamente la actual
        }

        private void btnInicio_Click(object sender, EventArgs e)
        {
            Form actual = this.FindForm();
            DashboardForm frm = new DashboardForm();
            frm.Show();

            if (actual != null) actual.Close();
        }

        private void btnPersonal_Click(object sender, EventArgs e)
        {
            Form actual = this.FindForm();
            PersonalListaForm frm = new PersonalListaForm();
            frm.Show();

            if (actual != null) actual.Close();
        }

        private void btnConsultas_Click(object sender, EventArgs e)
        {
            Form actual = this.FindForm();
            ConsultaListaForm frm = new ConsultaListaForm();
            frm.Show();

            if (actual != null) actual.Close();
        }

        private void btnTaratamientos_Click(object sender, EventArgs e)
        {
            Form actual = this.FindForm();
            TratamientoListaForm frm = new TratamientoListaForm();
            frm.Show();

            if (actual != null) actual.Close();
        }
    }
}