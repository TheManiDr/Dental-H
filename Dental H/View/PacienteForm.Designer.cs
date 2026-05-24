namespace Dental_H.View
{
    partial class PacienteForm
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
            this.txtTipoSangre = new System.Windows.Forms.TextBox();
            this.txtAlergias = new System.Windows.Forms.TextBox();
            this.txtNombreEmergencia = new System.Windows.Forms.TextBox();
            this.txtNumeroEmergencia = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtTipoSangre
            // 
            this.txtTipoSangre.Location = new System.Drawing.Point(72, 146);
            this.txtTipoSangre.Name = "txtTipoSangre";
            this.txtTipoSangre.Size = new System.Drawing.Size(100, 20);
            this.txtTipoSangre.TabIndex = 0;
            // 
            // txtAlergias
            // 
            this.txtAlergias.Location = new System.Drawing.Point(72, 197);
            this.txtAlergias.Name = "txtAlergias";
            this.txtAlergias.Size = new System.Drawing.Size(100, 20);
            this.txtAlergias.TabIndex = 1;
            // 
            // txtNombreEmergencia
            // 
            this.txtNombreEmergencia.Location = new System.Drawing.Point(72, 244);
            this.txtNombreEmergencia.Name = "txtNombreEmergencia";
            this.txtNombreEmergencia.Size = new System.Drawing.Size(100, 20);
            this.txtNombreEmergencia.TabIndex = 2;
            // 
            // txtNumeroEmergencia
            // 
            this.txtNumeroEmergencia.Location = new System.Drawing.Point(72, 291);
            this.txtNumeroEmergencia.Name = "txtNumeroEmergencia";
            this.txtNumeroEmergencia.Size = new System.Drawing.Size(100, 20);
            this.txtNumeroEmergencia.TabIndex = 3;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(419, 214);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 23);
            this.btnGuardar.TabIndex = 4;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // PacienteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.txtNumeroEmergencia);
            this.Controls.Add(this.txtNombreEmergencia);
            this.Controls.Add(this.txtAlergias);
            this.Controls.Add(this.txtTipoSangre);
            this.Name = "PacienteForm";
            this.Text = "PacienteForm";
            this.Load += new System.EventHandler(this.PacienteForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtTipoSangre;
        private System.Windows.Forms.TextBox txtAlergias;
        private System.Windows.Forms.TextBox txtNombreEmergencia;
        private System.Windows.Forms.TextBox txtNumeroEmergencia;
        private System.Windows.Forms.Button btnGuardar;
    }
}