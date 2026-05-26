namespace Dental_H.View
{
    partial class PacienteListaForm
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
            this.panelSuperior = new System.Windows.Forms.Panel();
            this.btnNuevoPaciente = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.flpPacientes = new System.Windows.Forms.FlowLayoutPanel();
            this.regresar = new System.Windows.Forms.Button();
            this.panelSuperior.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSuperior
            // 
            this.panelSuperior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(143)))), ((int)(((byte)(216)))));
            this.panelSuperior.Controls.Add(this.regresar);
            this.panelSuperior.Controls.Add(this.btnNuevoPaciente);
            this.panelSuperior.Controls.Add(this.txtBuscar);
            this.panelSuperior.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSuperior.Location = new System.Drawing.Point(0, 0);
            this.panelSuperior.Margin = new System.Windows.Forms.Padding(4);
            this.panelSuperior.Name = "panelSuperior";
            this.panelSuperior.Size = new System.Drawing.Size(1904, 98);
            this.panelSuperior.TabIndex = 0;
            // 
            // btnNuevoPaciente
            // 
            this.btnNuevoPaciente.Location = new System.Drawing.Point(892, 33);
            this.btnNuevoPaciente.Margin = new System.Windows.Forms.Padding(4);
            this.btnNuevoPaciente.Name = "btnNuevoPaciente";
            this.btnNuevoPaciente.Size = new System.Drawing.Size(143, 28);
            this.btnNuevoPaciente.TabIndex = 1;
            this.btnNuevoPaciente.Text = "+ Nuevo Paciente";
            this.btnNuevoPaciente.UseVisualStyleBackColor = true;
            this.btnNuevoPaciente.Click += new System.EventHandler(this.btnNuevoPaciente_Click);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtBuscar.Location = new System.Drawing.Point(28, 33);
            this.txtBuscar.Margin = new System.Windows.Forms.Padding(4);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(465, 29);
            this.txtBuscar.TabIndex = 0;
            this.txtBuscar.Text = "Buscar paciente...";
            // 
            // flpPacientes
            // 
            this.flpPacientes.AutoScroll = true;
            this.flpPacientes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpPacientes.Location = new System.Drawing.Point(0, 98);
            this.flpPacientes.Margin = new System.Windows.Forms.Padding(4);
            this.flpPacientes.Name = "flpPacientes";
            this.flpPacientes.Padding = new System.Windows.Forms.Padding(33, 31, 33, 31);
            this.flpPacientes.Size = new System.Drawing.Size(1904, 943);
            this.flpPacientes.TabIndex = 1;
            this.flpPacientes.Paint += new System.Windows.Forms.PaintEventHandler(this.flpPacientes_Paint);
            // 
            // regresar
            // 
            this.regresar.Location = new System.Drawing.Point(1765, 25);
            this.regresar.Name = "regresar";
            this.regresar.Size = new System.Drawing.Size(108, 44);
            this.regresar.TabIndex = 2;
            this.regresar.Text = "<";
            this.regresar.UseVisualStyleBackColor = true;
            this.regresar.Click += new System.EventHandler(this.button1_Click);
            // 
            // PacienteListaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.flpPacientes);
            this.Controls.Add(this.panelSuperior);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "PacienteListaForm";
            this.Text = "Pacientes";
            this.Load += new System.EventHandler(this.PacienteListaForm_Load);
            this.panelSuperior.ResumeLayout(false);
            this.panelSuperior.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelSuperior;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Button btnNuevoPaciente;
        private System.Windows.Forms.FlowLayoutPanel flpPacientes;
        private System.Windows.Forms.Button regresar;
    }
}