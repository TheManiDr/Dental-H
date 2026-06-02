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

namespace Dental_H.View
{
    public partial class PacienteForm : Form
    {
        // Variable global que almacena el formulario base
        private Form _ventanaAnterior;

        public PacienteForm(Form anterior)
        {
            InitializeComponent();
            this._ventanaAnterior = anterior;
        }

        private void PacienteForm_Load(object sender, EventArgs e)
        {
            cbGenero.Items.Add("Masculino");
            cbGenero.Items.Add("Femenino");

            cbTipoSangre.Items.Add("A+");
            cbTipoSangre.Items.Add("A-");
            cbTipoSangre.Items.Add("B+");
            cbTipoSangre.Items.Add("B-");
            cbTipoSangre.Items.Add("AB+");
            cbTipoSangre.Items.Add("AB-");
            cbTipoSangre.Items.Add("O+");
            cbTipoSangre.Items.Add("O-");
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Paciente paciente = new Paciente();
            paciente.Nombre = txtNombre.Text;
            paciente.ApellidoPaterno = txtApellidoPaterno.Text;
            paciente.ApellidoMaterno = txtApellidoMaterno.Text;
            paciente.FechaNacimiento = dtpFechaNacimiento.Value;
            paciente.Genero = cbGenero.Text;
            paciente.TipoSangre = cbTipoSangre.Text;
            paciente.Alergias = txtAlergias.Text;
            paciente.ContactoEmergencia = txtContactoEmergencia.Text;
            paciente.NumeroEmergencia = txtNumeroEmergencia.Text;

            PacienteController controller = new PacienteController();
            bool registrado = controller.RegistrarPaciente(paciente);

            if (registrado)
            {
                MessageBox.Show("Paciente registrado correctamente");
                LimpiarFormulario();
            }
            else
            {
                MessageBox.Show("Error al registrar paciente");
            }
        }

        private void LimpiarFormulario()
        {
            txtNombre.Clear();
            txtApellidoPaterno.Clear();
            txtApellidoMaterno.Clear();
            txtAlergias.Clear();
            txtContactoEmergencia.Clear();
            txtNumeroEmergencia.Clear();
            cbGenero.SelectedIndex = -1;
            cbTipoSangre.SelectedIndex = -1;
            dtpFechaNacimiento.Value = DateTime.Now;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        // ============================================================
        // MODIFICADO: ACCIÓN REAL DE REGRESO EVALUANDO PACIENTELISTAFORM
        // ============================================================
        private void EjecutarRegreso()
        {
            if (this._ventanaAnterior != null)
            {
                AppNavigator.Regresar(this, this._ventanaAnterior);
            }
        }

        // Mantiene feliz al diseñador si busca este método
        private void btnVolver_Click(object sender, EventArgs e)
        {
            EjecutarRegreso();
        }

        // Por si tu botón físico se llama 'back' y tiene asignado su propio clic
        private void back_Click(object sender, EventArgs e)
        {
            EjecutarRegreso();
        }

        private void back_Click_1(object sender, EventArgs e)
        {
            EjecutarRegreso();
        }
    }
}
