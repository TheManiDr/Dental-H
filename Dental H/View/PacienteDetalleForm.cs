using Dental_H.Controller;
using Dental_H.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dental_H.View
{
    public partial class PacienteDetalleForm : Form
    {
        private int idPaciente;

        // 1. VARIABLE GLOBAL PARA RECORDAR LA VENTANA ANTERIOR
        private Form _ventanaAnterior;

        // 2. CONSTRUCTOR MODIFICADO (Ahora recibe el ID del paciente Y la ventana de origen)
        public PacienteDetalleForm(int idPaciente, Form anterior)
        {
            InitializeComponent();
            this.idPaciente = idPaciente;
            this._ventanaAnterior = anterior; // Guardamos la lista de pacientes en memoria
            CargarPaciente();
        }

        private void CargarPaciente()
        {
            PacienteController controller =
                new PacienteController();

            Paciente paciente =
                controller.ObtenerPacientePorId(idPaciente);

            if (paciente == null)
            {
                MessageBox.Show("Paciente no encontrado");
                return;
            }
            lblNombrePaciente.Text = paciente.Nombre + " " + paciente.ApellidoPaterno + " " + paciente.ApellidoMaterno;

            lblEdad.Text = CalcularEdad(paciente.FechaNacimiento) + " años";

            txtNombre.Text = paciente.Nombre;
            txtApellidoPaterno.Text = paciente.ApellidoPaterno;
            txtApellidoMaterno.Text = paciente.ApellidoMaterno;

            dtpFechaNacimiento.Value =
                paciente.FechaNacimiento;

            cmbGenero.Text =
                paciente.Genero;

            cmbTipoSangre.Text =
                paciente.TipoSangre;

            txtAlergias.Text =
                paciente.Alergias;

            txtContactoEmergencia.Text =
                paciente.ContactoEmergencia;

            txtNumeroEmergencia.Text =
                paciente.NumeroEmergencia;

            txtCalle.Text =
                paciente.Calle;

            txtCiudad.Text =
                paciente.Ciudad;

            txtEstado.Text =
                paciente.Estado;

            txtCodigoPostal.Text =
                paciente.CodigoPostal;

            txtTelefono.Text =
                paciente.Telefono;

            if (paciente.Genero == "Masculino")
            {
                picAvatarPaciente.Image =
                    Properties.Resources.avatar_hombre;
            }
            else
            {
                picAvatarPaciente.Image =
                    Properties.Resources.avatar_mujer;
            }
        }

        private int CalcularEdad(DateTime fechaNacimiento)
        {
            int edad = DateTime.Now.Year - fechaNacimiento.Year;

            if (DateTime.Now < fechaNacimiento.AddYears(edad))
            {
                edad--;
            }

            return edad;
        }

        // ============================================================
        // 3. EVENTO DEL BOTÓN DE REGRESO (Usando tu método existente)
        // ============================================================
        private void back2_Click(object sender, EventArgs e)
        {
            if (this._ventanaAnterior != null)
            {
                this._ventanaAnterior.Show(); // Volvemos a mostrar la lista de pacientes
                this.Close();                 // Cerramos esta vista detallada
            }
        }


        private void btnPlanesTratamientos_Click(object sender, EventArgs e)
        {

        }
    }
}