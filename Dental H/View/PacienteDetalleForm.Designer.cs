namespace Dental_H.View
{
    partial class PacienteDetalleForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.pbFoto = new System.Windows.Forms.PictureBox();
            this.lblNombrePaciente = new System.Windows.Forms.Label();
            this.lblEdad = new System.Windows.Forms.Label();
            this.lblPaciente = new System.Windows.Forms.Label();
            this.pnlTabs = new System.Windows.Forms.Panel();
            this.btnDatosPersonales = new System.Windows.Forms.Button();
            this.btnExpediente = new System.Windows.Forms.Button();
            this.btnPlanesTratamientos = new System.Windows.Forms.Button();
            this.btnRadiografia = new System.Windows.Forms.Button();
            this.pnlContenido = new System.Windows.Forms.Panel();
            this.pnlInfoBasica = new System.Windows.Forms.Panel();
            this.lblNombre = new System.Windows.Forms.Label();
            this.txtboxNombre = new System.Windows.Forms.TextBox();
            this.lblApellidoPaterno = new System.Windows.Forms.Label();
            this.txtboxApellidoPaterno = new System.Windows.Forms.TextBox();
            this.lblApellidoMaterno = new System.Windows.Forms.Label();
            this.txtboxApellidoMaterno = new System.Windows.Forms.TextBox();
            this.lblFechaNacimiento = new System.Windows.Forms.Label();
            this.dtpFechaNacimiento = new System.Windows.Forms.DateTimePicker();
            this.cmbGenero = new System.Windows.Forms.ComboBox();
            this.lblGenero = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbFoto)).BeginInit();
            this.pnlTabs.SuspendLayout();
            this.pnlContenido.SuspendLayout();
            this.pnlInfoBasica.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(156)))), ((int)(((byte)(232)))));
            this.pnlHeader.Controls.Add(this.lblEdad);
            this.pnlHeader.Controls.Add(this.lblPaciente);
            this.pnlHeader.Controls.Add(this.lblNombrePaciente);
            this.pnlHeader.Controls.Add(this.pbFoto);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1078, 120);
            this.pnlHeader.TabIndex = 0;
            // 
            // pbFoto
            // 
            this.pbFoto.Location = new System.Drawing.Point(20, 20);
            this.pbFoto.Name = "pbFoto";
            this.pbFoto.Size = new System.Drawing.Size(80, 80);
            this.pbFoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbFoto.TabIndex = 0;
            this.pbFoto.TabStop = false;
            // 
            // lblNombrePaciente
            // 
            this.lblNombrePaciente.AutoSize = true;
            this.lblNombrePaciente.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblNombrePaciente.ForeColor = System.Drawing.Color.White;
            this.lblNombrePaciente.Location = new System.Drawing.Point(120, 50);
            this.lblNombrePaciente.Name = "lblNombrePaciente";
            this.lblNombrePaciente.Size = new System.Drawing.Size(89, 13);
            this.lblNombrePaciente.TabIndex = 1;
            this.lblNombrePaciente.Text = "Nombre Paciente";
            // 
            // lblEdad
            // 
            this.lblEdad.AutoSize = true;
            this.lblEdad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblEdad.ForeColor = System.Drawing.Color.White;
            this.lblEdad.Location = new System.Drawing.Point(120, 70);
            this.lblEdad.Name = "lblEdad";
            this.lblEdad.Size = new System.Drawing.Size(45, 13);
            this.lblEdad.TabIndex = 2;
            this.lblEdad.Text = "22 años";
            // 
            // lblPaciente
            // 
            this.lblPaciente.AutoSize = true;
            this.lblPaciente.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblPaciente.ForeColor = System.Drawing.Color.White;
            this.lblPaciente.Location = new System.Drawing.Point(120, 30);
            this.lblPaciente.Name = "lblPaciente";
            this.lblPaciente.Size = new System.Drawing.Size(49, 13);
            this.lblPaciente.TabIndex = 1;
            this.lblPaciente.Text = "Paciente";
            // 
            // pnlTabs
            // 
            this.pnlTabs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(225)))), ((int)(((byte)(248)))));
            this.pnlTabs.Controls.Add(this.btnRadiografia);
            this.pnlTabs.Controls.Add(this.btnPlanesTratamientos);
            this.pnlTabs.Controls.Add(this.btnExpediente);
            this.pnlTabs.Controls.Add(this.btnDatosPersonales);
            this.pnlTabs.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTabs.Location = new System.Drawing.Point(0, 120);
            this.pnlTabs.Name = "pnlTabs";
            this.pnlTabs.Size = new System.Drawing.Size(1078, 40);
            this.pnlTabs.TabIndex = 1;
            // 
            // btnDatosPersonales
            // 
            this.btnDatosPersonales.BackColor = System.Drawing.Color.Transparent;
            this.btnDatosPersonales.FlatAppearance.BorderSize = 0;
            this.btnDatosPersonales.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDatosPersonales.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnDatosPersonales.Location = new System.Drawing.Point(20, 7);
            this.btnDatosPersonales.Name = "btnDatosPersonales";
            this.btnDatosPersonales.Size = new System.Drawing.Size(103, 23);
            this.btnDatosPersonales.TabIndex = 0;
            this.btnDatosPersonales.Text = "Datos Personales";
            this.btnDatosPersonales.UseVisualStyleBackColor = false;
            // 
            // btnExpediente
            // 
            this.btnExpediente.BackColor = System.Drawing.Color.Transparent;
            this.btnExpediente.FlatAppearance.BorderSize = 0;
            this.btnExpediente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExpediente.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnExpediente.Location = new System.Drawing.Point(130, 7);
            this.btnExpediente.Name = "btnExpediente";
            this.btnExpediente.Size = new System.Drawing.Size(110, 23);
            this.btnExpediente.TabIndex = 1;
            this.btnExpediente.Text = "Expediente Medico";
            this.btnExpediente.UseVisualStyleBackColor = false;
            // 
            // btnPlanesTratamientos
            // 
            this.btnPlanesTratamientos.BackColor = System.Drawing.Color.Transparent;
            this.btnPlanesTratamientos.FlatAppearance.BorderSize = 0;
            this.btnPlanesTratamientos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlanesTratamientos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnPlanesTratamientos.Location = new System.Drawing.Point(247, 7);
            this.btnPlanesTratamientos.Name = "btnPlanesTratamientos";
            this.btnPlanesTratamientos.Size = new System.Drawing.Size(130, 23);
            this.btnPlanesTratamientos.TabIndex = 2;
            this.btnPlanesTratamientos.Text = "Planes de Tratamientos";
            this.btnPlanesTratamientos.UseVisualStyleBackColor = false;
            // 
            // btnRadiografia
            // 
            this.btnRadiografia.BackColor = System.Drawing.Color.Transparent;
            this.btnRadiografia.FlatAppearance.BorderSize = 0;
            this.btnRadiografia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRadiografia.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnRadiografia.Location = new System.Drawing.Point(384, 7);
            this.btnRadiografia.Name = "btnRadiografia";
            this.btnRadiografia.Size = new System.Drawing.Size(75, 23);
            this.btnRadiografia.TabIndex = 3;
            this.btnRadiografia.Text = "Radiografias";
            this.btnRadiografia.UseVisualStyleBackColor = false;
            // 
            // pnlContenido
            // 
            this.pnlContenido.BackColor = System.Drawing.Color.White;
            this.pnlContenido.Controls.Add(this.pnlInfoBasica);
            this.pnlContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContenido.Location = new System.Drawing.Point(0, 160);
            this.pnlContenido.Name = "pnlContenido";
            this.pnlContenido.Size = new System.Drawing.Size(1078, 593);
            this.pnlContenido.TabIndex = 2;
            // 
            // pnlInfoBasica
            // 
            this.pnlInfoBasica.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlInfoBasica.Controls.Add(this.cmbGenero);
            this.pnlInfoBasica.Controls.Add(this.dtpFechaNacimiento);
            this.pnlInfoBasica.Controls.Add(this.txtboxApellidoMaterno);
            this.pnlInfoBasica.Controls.Add(this.txtboxApellidoPaterno);
            this.pnlInfoBasica.Controls.Add(this.txtboxNombre);
            this.pnlInfoBasica.Controls.Add(this.lblGenero);
            this.pnlInfoBasica.Controls.Add(this.lblFechaNacimiento);
            this.pnlInfoBasica.Controls.Add(this.lblApellidoMaterno);
            this.pnlInfoBasica.Controls.Add(this.lblApellidoPaterno);
            this.pnlInfoBasica.Controls.Add(this.lblNombre);
            this.pnlInfoBasica.Location = new System.Drawing.Point(0, 0);
            this.pnlInfoBasica.Name = "pnlInfoBasica";
            this.pnlInfoBasica.Size = new System.Drawing.Size(400, 593);
            this.pnlInfoBasica.TabIndex = 0;
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(41, 47);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(44, 13);
            this.lblNombre.TabIndex = 0;
            this.lblNombre.Text = "Nombre";
            // 
            // txtboxNombre
            // 
            this.txtboxNombre.Location = new System.Drawing.Point(44, 64);
            this.txtboxNombre.Name = "txtboxNombre";
            this.txtboxNombre.Size = new System.Drawing.Size(100, 20);
            this.txtboxNombre.TabIndex = 1;
            // 
            // lblApellidoPaterno
            // 
            this.lblApellidoPaterno.AutoSize = true;
            this.lblApellidoPaterno.Location = new System.Drawing.Point(41, 97);
            this.lblApellidoPaterno.Name = "lblApellidoPaterno";
            this.lblApellidoPaterno.Size = new System.Drawing.Size(84, 13);
            this.lblApellidoPaterno.TabIndex = 0;
            this.lblApellidoPaterno.Text = "Apellido Paterno";
            // 
            // txtboxApellidoPaterno
            // 
            this.txtboxApellidoPaterno.Location = new System.Drawing.Point(44, 114);
            this.txtboxApellidoPaterno.Name = "txtboxApellidoPaterno";
            this.txtboxApellidoPaterno.Size = new System.Drawing.Size(100, 20);
            this.txtboxApellidoPaterno.TabIndex = 1;
            // 
            // lblApellidoMaterno
            // 
            this.lblApellidoMaterno.AutoSize = true;
            this.lblApellidoMaterno.Location = new System.Drawing.Point(41, 152);
            this.lblApellidoMaterno.Name = "lblApellidoMaterno";
            this.lblApellidoMaterno.Size = new System.Drawing.Size(86, 13);
            this.lblApellidoMaterno.TabIndex = 0;
            this.lblApellidoMaterno.Text = "Apellido Materno";
            // 
            // txtboxApellidoMaterno
            // 
            this.txtboxApellidoMaterno.Location = new System.Drawing.Point(44, 169);
            this.txtboxApellidoMaterno.Name = "txtboxApellidoMaterno";
            this.txtboxApellidoMaterno.Size = new System.Drawing.Size(100, 20);
            this.txtboxApellidoMaterno.TabIndex = 1;
            // 
            // lblFechaNacimiento
            // 
            this.lblFechaNacimiento.AutoSize = true;
            this.lblFechaNacimiento.Location = new System.Drawing.Point(41, 205);
            this.lblFechaNacimiento.Name = "lblFechaNacimiento";
            this.lblFechaNacimiento.Size = new System.Drawing.Size(93, 13);
            this.lblFechaNacimiento.TabIndex = 0;
            this.lblFechaNacimiento.Text = "Fecha Nacimiento";
            // 
            // dtpFechaNacimiento
            // 
            this.dtpFechaNacimiento.Location = new System.Drawing.Point(44, 221);
            this.dtpFechaNacimiento.Name = "dtpFechaNacimiento";
            this.dtpFechaNacimiento.Size = new System.Drawing.Size(200, 20);
            this.dtpFechaNacimiento.TabIndex = 2;
            // 
            // cmbGenero
            // 
            this.cmbGenero.FormattingEnabled = true;
            this.cmbGenero.Location = new System.Drawing.Point(44, 272);
            this.cmbGenero.Name = "cmbGenero";
            this.cmbGenero.Size = new System.Drawing.Size(121, 21);
            this.cmbGenero.TabIndex = 3;
            // 
            // lblGenero
            // 
            this.lblGenero.AutoSize = true;
            this.lblGenero.Location = new System.Drawing.Point(41, 256);
            this.lblGenero.Name = "lblGenero";
            this.lblGenero.Size = new System.Drawing.Size(42, 13);
            this.lblGenero.TabIndex = 0;
            this.lblGenero.Text = "Genero";
            // 
            // PacienteDetalleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1078, 753);
            this.Controls.Add(this.pnlContenido);
            this.Controls.Add(this.pnlTabs);
            this.Controls.Add(this.pnlHeader);
            this.Name = "PacienteDetalleForm";
            this.Text = "PacienteDetalleForm";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbFoto)).EndInit();
            this.pnlTabs.ResumeLayout(false);
            this.pnlContenido.ResumeLayout(false);
            this.pnlInfoBasica.ResumeLayout(false);
            this.pnlInfoBasica.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.PictureBox pbFoto;
        private System.Windows.Forms.Label lblEdad;
        private System.Windows.Forms.Label lblPaciente;
        private System.Windows.Forms.Label lblNombrePaciente;
        private System.Windows.Forms.Panel pnlTabs;
        private System.Windows.Forms.Button btnDatosPersonales;
        private System.Windows.Forms.Button btnRadiografia;
        private System.Windows.Forms.Button btnPlanesTratamientos;
        private System.Windows.Forms.Button btnExpediente;
        private System.Windows.Forms.Panel pnlContenido;
        private System.Windows.Forms.Panel pnlInfoBasica;
        private System.Windows.Forms.TextBox txtboxNombre;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtboxApellidoMaterno;
        private System.Windows.Forms.TextBox txtboxApellidoPaterno;
        private System.Windows.Forms.Label lblApellidoMaterno;
        private System.Windows.Forms.Label lblApellidoPaterno;
        private System.Windows.Forms.ComboBox cmbGenero;
        private System.Windows.Forms.DateTimePicker dtpFechaNacimiento;
        private System.Windows.Forms.Label lblGenero;
        private System.Windows.Forms.Label lblFechaNacimiento;
    }
}