using System.Drawing;
using System.Windows.Forms;
using Dental_H.Util;
using Dental_H.View;

namespace Dental_H.Components
{
    public class PersonalCard : UserControl
    {
        private int idPersonal;
        private PictureBox picAvatar;
        private Label lblNombre;
        private Label lblEdad;
        private Label lblRol;
        private Label lblUsuario;
        private Button btnVerExpediente;

        public int IdPersonal
        {
            get => idPersonal;
            set => idPersonal = value;
        }

        public Image AvatarPersonal
        {
            get => picAvatar.Image;
            set => picAvatar.Image = value;
        }

        public string NombrePersonal
        {
            get => lblNombre.Text;
            set => lblNombre.Text = value;
        }

        public string EdadPersonal
        {
            get => lblEdad.Text;
            set => lblEdad.Text = value;
        }

        public string RolPersonal
        {
            get => lblRol.Text;
            set => lblRol.Text = value;
        }

        public string UsuarioPersonal
        {
            get => lblUsuario.Text;
            set => lblUsuario.Text = value;
        }

        public string CurpPersonal
        {
            get => string.Empty;
            set { }
        }

        public string RfcPersonal
        {
            get => string.Empty;
            set { }
        }

        public string NssPersonal
        {
            get => string.Empty;
            set { }
        }

        public PersonalCard()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            picAvatar = new PictureBox();
            lblNombre = new Label();
            lblEdad = new Label();
            lblRol = new Label();
            lblUsuario = new Label();
            btnVerExpediente = new Button();

            SuspendLayout();

            BackColor = Color.White;
            BorderStyle = BorderStyle.FixedSingle;
            Margin = new Padding(10);
            Size = new Size(270, 340);

            picAvatar.Location = new Point(85, 24);
            picAvatar.Size = new Size(100, 100);
            picAvatar.SizeMode = PictureBoxSizeMode.Zoom;

            ConfigurarLabel(lblNombre, new Point(18, 140), new Size(234, 44), Color.FromArgb(28, 65, 111), true);
            ConfigurarLabel(lblEdad, new Point(18, 190), new Size(234, 22), Color.FromArgb(37, 99, 163), false);
            ConfigurarLabel(lblRol, new Point(18, 218), new Size(234, 24), Color.FromArgb(31, 41, 55), true);
            ConfigurarLabel(lblUsuario, new Point(18, 246), new Size(234, 28), Color.FromArgb(92, 105, 119), false);

            btnVerExpediente.Text = "Ver expediente";
            btnVerExpediente.Location = new Point(40, 292);
            btnVerExpediente.Size = new Size(190, 34);
            btnVerExpediente.BackColor = Color.FromArgb(37, 99, 163);
            btnVerExpediente.ForeColor = Color.White;
            btnVerExpediente.FlatStyle = FlatStyle.Flat;
            btnVerExpediente.FlatAppearance.BorderSize = 0;
            btnVerExpediente.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            btnVerExpediente.Click += BtnVerExpediente_Click;

            Controls.Add(picAvatar);
            Controls.Add(lblNombre);
            Controls.Add(lblEdad);
            Controls.Add(lblRol);
            Controls.Add(lblUsuario);
            Controls.Add(btnVerExpediente);

            ResumeLayout(false);
        }

        private void ConfigurarLabel(Label label, Point location, Size size, Color color, bool bold)
        {
            label.Location = location;
            label.Size = size;
            label.Font = new Font("Segoe UI", bold ? 10.5f : 9f, bold ? FontStyle.Bold : FontStyle.Regular);
            label.ForeColor = color;
            label.TextAlign = ContentAlignment.MiddleCenter;
        }

        private void BtnVerExpediente_Click(object sender, System.EventArgs e)
        {
            Form listaPadre = this.Tag as Form;
            PersonalDetalleForm detalle = new PersonalDetalleForm(IdPersonal, listaPadre);
            AppNavigator.AbrirSecundaria(listaPadre, detalle);
        }
    }
}
