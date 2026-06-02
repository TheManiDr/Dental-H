using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dental_H.Components;
using Dental_H.Controller;
using Dental_H.Model;

namespace Dental_H.View
{
    public partial class Personal : Form
    {
        public Personal()
        {
            InitializeComponent();
        }

        private void Personal_Load(object sender, EventArgs e)
        {
            CargarPersonal();
        }

        private void CargarPersonal()
        {
            flpPersonal.Controls.Clear();
            flpPersonal.BackColor = Color.FromArgb(245, 247, 250);
            flpPersonal.Padding = new Padding(28, 28, 28, 28);
            flpPersonal.Controls.Add(CrearTarjetaAgregarPersonal());

            PersonalController controller = new PersonalController();
            List<PersonalInfo> personalLista = controller.ObtenerPersonal();

            foreach (PersonalInfo personal in personalLista)
            {
                PersonalCard card = new PersonalCard();

                card.IdPersonal = personal.IdPersona;
                card.NombrePersonal = personal.Nombre + " " + personal.ApellidoPaterno + " " + personal.ApellidoMaterno;
                card.EdadPersonal = "Edad: " + CalcularEdad(personal.FechaNacimiento) + " años";
                card.RolPersonal = string.IsNullOrWhiteSpace(personal.Rol) ? "Sin rol" : personal.Rol;
                card.UsuarioPersonal = "Usuario: " + ValorOMensaje(personal.NombreUsuario);
                card.Tag = this;

                if (personal.Genero == "Masculino")
                {
                    card.AvatarPersonal = Properties.Resources.avatar_hombre;
                }
                else
                {
                    card.AvatarPersonal = Properties.Resources.avatar_mujer;
                }

                flpPersonal.Controls.Add(card);
            }
        }

        private Control CrearTarjetaAgregarPersonal()
        {
            Panel card = new Panel();
            card.BackColor = Color.White;
            card.Size = new Size(250, 320);
            card.Margin = new Padding(6);
            card.Padding = new Padding(18);
            card.BorderStyle = BorderStyle.FixedSingle;
            card.Cursor = Cursors.Hand;

            Label icono = new Label();
            icono.Text = "+";
            icono.Font = new Font("Segoe UI", 44, FontStyle.Bold);
            icono.ForeColor = Color.FromArgb(37, 99, 163);
            icono.TextAlign = ContentAlignment.MiddleCenter;
            icono.Location = new Point(67, 48);
            icono.Size = new Size(116, 90);
            icono.Cursor = Cursors.Hand;

            Label titulo = new Label();
            titulo.Text = "Agregar personal";
            titulo.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            titulo.ForeColor = Color.FromArgb(28, 65, 111);
            titulo.TextAlign = ContentAlignment.MiddleCenter;
            titulo.Location = new Point(15, 154);
            titulo.Size = new Size(220, 32);
            titulo.Cursor = Cursors.Hand;

            Label subtitulo = new Label();
            subtitulo.Text = "Registrar colaborador y credenciales de acceso.";
            subtitulo.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            subtitulo.ForeColor = Color.FromArgb(92, 105, 119);
            subtitulo.TextAlign = ContentAlignment.MiddleCenter;
            subtitulo.Location = new Point(24, 194);
            subtitulo.Size = new Size(202, 48);
            subtitulo.Cursor = Cursors.Hand;

            Button btnAgregar = new Button();
            btnAgregar.Text = "Nuevo personal";
            btnAgregar.BackColor = Color.FromArgb(37, 99, 163);
            btnAgregar.ForeColor = Color.White;
            btnAgregar.FlatStyle = FlatStyle.Flat;
            btnAgregar.FlatAppearance.BorderSize = 0;
            btnAgregar.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            btnAgregar.Location = new Point(35, 270);
            btnAgregar.Size = new Size(180, 34);

            card.Controls.Add(titulo);
            card.Controls.Add(subtitulo);
            card.Controls.Add(icono);
            card.Controls.Add(btnAgregar);

            EventHandler abrirRegistro = (sender, e) => AbrirRegistroPersonal();
            card.Click += abrirRegistro;
            icono.Click += abrirRegistro;
            titulo.Click += abrirRegistro;
            subtitulo.Click += abrirRegistro;
            btnAgregar.Click += abrirRegistro;

            return card;
        }

        private void AbrirRegistroPersonal()
        {
            using (NuevoPersonalForm registro = new NuevoPersonalForm())
            {
                if (registro.ShowDialog(this) == DialogResult.OK)
                {
                    CargarPersonal();
                }
            }
        }

        private int CalcularEdad(DateTime fechaNacimiento)
        {
            if (fechaNacimiento == DateTime.MinValue)
            {
                return 0;
            }

            int edad = DateTime.Now.Year - fechaNacimiento.Year;
            if (DateTime.Now < fechaNacimiento.AddYears(edad))
            {
                edad--;
            }

            return edad;
        }

        private string ValorOMensaje(string valor)
        {
            return string.IsNullOrWhiteSpace(valor) ? "Sin dato" : valor;
        }
    }
}
