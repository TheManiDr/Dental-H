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
            this.panel1 = new System.Windows.Forms.Panel();
            this.flpPacientes = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlAltaPaciente = new System.Windows.Forms.Panel();
            this.headerControl1 = new Dental_H.Components.HeaderControl();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.flpPacientes);
            this.panel1.Controls.Add(this.pnlAltaPaciente);
            this.panel1.Controls.Add(this.headerControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1904, 1041);
            this.panel1.TabIndex = 2;
            // 
            // flpPacientes
            // 
            this.flpPacientes.AutoScroll = true;
            this.flpPacientes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpPacientes.Location = new System.Drawing.Point(0, 315);
            this.flpPacientes.Name = "flpPacientes";
            this.flpPacientes.Padding = new System.Windows.Forms.Padding(25);
            this.flpPacientes.Size = new System.Drawing.Size(1904, 726);
            this.flpPacientes.TabIndex = 2;
            // 
            // pnlAltaPaciente
            // 
            this.pnlAltaPaciente.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAltaPaciente.Location = new System.Drawing.Point(0, 110);
            this.pnlAltaPaciente.Name = "pnlAltaPaciente";
            this.pnlAltaPaciente.Size = new System.Drawing.Size(1904, 205);
            this.pnlAltaPaciente.TabIndex = 3;
            // 
            // headerControl1
            // 
            this.headerControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerControl1.Location = new System.Drawing.Point(0, 0);
            this.headerControl1.Name = "headerControl1";
            this.headerControl1.Size = new System.Drawing.Size(1904, 110);
            this.headerControl1.TabIndex = 0;
            // 
            // PacienteListaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.panel1);
            this.Name = "PacienteListaForm";
            this.Text = "Pacientes";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.PacienteListaForm_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel flpPacientes;
        private System.Windows.Forms.Panel pnlAltaPaciente;
        private Components.HeaderControl headerControl1;
    }
}
