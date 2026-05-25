namespace Dental_H.Components
{
    partial class PacienteCard
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.picAvatar = new System.Windows.Forms.PictureBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.lblEdad = new System.Windows.Forms.Label();
            this.lblTipoSangre = new System.Windows.Forms.Label();
            this.btnVerPerfil = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picAvatar)).BeginInit();
            this.SuspendLayout();
            // 
            // picAvatar
            // 
            this.picAvatar.Location = new System.Drawing.Point(57, 24);
            this.picAvatar.Name = "picAvatar";
            this.picAvatar.Size = new System.Drawing.Size(120, 120);
            this.picAvatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picAvatar.TabIndex = 0;
            this.picAvatar.TabStop = false;
            // 
            // lblNombre
            // 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblNombre.Location = new System.Drawing.Point(71, 157);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(89, 13);
            this.lblNombre.TabIndex = 1;
            this.lblNombre.Text = "Nombre Paciente";
            this.lblNombre.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblEdad
            // 
            this.lblEdad.AutoSize = true;
            this.lblEdad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblEdad.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblEdad.Location = new System.Drawing.Point(85, 192);
            this.lblEdad.Name = "lblEdad";
            this.lblEdad.Size = new System.Drawing.Size(45, 13);
            this.lblEdad.TabIndex = 2;
            this.lblEdad.Text = "22 años";
            // 
            // lblTipoSangre
            // 
            this.lblTipoSangre.AutoSize = true;
            this.lblTipoSangre.Location = new System.Drawing.Point(95, 215);
            this.lblTipoSangre.Name = "lblTipoSangre";
            this.lblTipoSangre.Size = new System.Drawing.Size(21, 13);
            this.lblTipoSangre.TabIndex = 3;
            this.lblTipoSangre.Text = "O+";
            // 
            // btnVerPerfil
            // 
            this.btnVerPerfil.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(143)))), ((int)(((byte)(216)))));
            this.btnVerPerfil.Location = new System.Drawing.Point(57, 231);
            this.btnVerPerfil.Name = "btnVerPerfil";
            this.btnVerPerfil.Size = new System.Drawing.Size(103, 23);
            this.btnVerPerfil.TabIndex = 4;
            this.btnVerPerfil.Text = "Ver Expediente";
            this.btnVerPerfil.UseVisualStyleBackColor = false;
            this.btnVerPerfil.Click += new System.EventHandler(this.btnVerPerfil_Click);
            // 
            // PacienteCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.btnVerPerfil);
            this.Controls.Add(this.lblTipoSangre);
            this.Controls.Add(this.lblEdad);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.picAvatar);
            this.Name = "PacienteCard";
            this.Size = new System.Drawing.Size(250, 320);
            ((System.ComponentModel.ISupportInitialize)(this.picAvatar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picAvatar;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label lblEdad;
        private System.Windows.Forms.Label lblTipoSangre;
        private System.Windows.Forms.Button btnVerPerfil;
    }
}
