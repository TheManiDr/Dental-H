using System.Drawing;
using System.Windows.Forms;

namespace Dental_H.Components
{
    public class PersonalCard : UserControl
    {
        private PictureBox picAvatar;
        private Label lblNombre;
        private Label lblEdad;
        private Label lblRol;
        private Label lblUsuario;
        private Label lblCurp;
        private Label lblRfc;
        private Label lblNss;

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
            get => lblCurp.Text;
            set => lblCurp.Text = value;
        }

        public string RfcPersonal
        {
            get => lblRfc.Text;
            set => lblRfc.Text = value;
        }

        public string NssPersonal
        {
            get => lblNss.Text;
            set => lblNss.Text = value;
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
            lblCurp = new Label();
            lblRfc = new Label();
            lblNss = new Label();

            SuspendLayout();

            BackColor = Color.White;
            BorderStyle = BorderStyle.FixedSingle;
            Margin = new Padding(6);
            Size = new Size(250, 320);

            picAvatar.Location = new Point(65, 20);
            picAvatar.Size = new Size(120, 120);
            picAvatar.SizeMode = PictureBoxSizeMode.Zoom;

            ConfigurarLabel(lblNombre, new Point(15, 153), new Size(220, 20), Color.Black, true);
            ConfigurarLabel(lblEdad, new Point(15, 181), new Size(220, 18), Color.DodgerBlue, false);
            ConfigurarLabel(lblRol, new Point(15, 205), new Size(220, 18), Color.Black, true);
            ConfigurarLabel(lblUsuario, new Point(15, 231), new Size(220, 18), Color.FromArgb(70, 70, 70), false);
            ConfigurarLabel(lblCurp, new Point(15, 253), new Size(220, 18), Color.FromArgb(70, 70, 70), false);
            ConfigurarLabel(lblRfc, new Point(15, 273), new Size(220, 18), Color.FromArgb(70, 70, 70), false);
            ConfigurarLabel(lblNss, new Point(15, 293), new Size(220, 18), Color.FromArgb(70, 70, 70), false);

            Controls.Add(picAvatar);
            Controls.Add(lblNombre);
            Controls.Add(lblEdad);
            Controls.Add(lblRol);
            Controls.Add(lblUsuario);
            Controls.Add(lblCurp);
            Controls.Add(lblRfc);
            Controls.Add(lblNss);

            ResumeLayout(false);
        }

        private void ConfigurarLabel(Label label, Point location, Size size, Color color, bool bold)
        {
            label.Location = location;
            label.Size = size;
            label.Font = new Font("Microsoft Sans Serif", 8.25F, bold ? FontStyle.Bold : FontStyle.Regular);
            label.ForeColor = color;
            label.TextAlign = ContentAlignment.MiddleCenter;
        }
    }
}
