namespace Dental_H.View
{
    partial class Citashoy
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
            this.dgvHorario2 = new System.Windows.Forms.DataGridView();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.headerControl1 = new Dental_H.Components.HeaderControl();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHorario2)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvHorario2
            // 
            this.dgvHorario2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHorario2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHorario2.Location = new System.Drawing.Point(0, 110);
            this.dgvHorario2.Name = "dgvHorario2";
            this.dgvHorario2.Size = new System.Drawing.Size(800, 340);
            this.dgvHorario2.TabIndex = 1;
            this.dgvHorario2.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHorario2_CellContentClick_1);
            // 
            // dtpFecha
            // 
            this.dtpFecha.Dock = System.Windows.Forms.DockStyle.Top;
            this.dtpFecha.Location = new System.Drawing.Point(0, 0);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(800, 20);
            this.dtpFecha.TabIndex = 2;
            this.dtpFecha.ValueChanged += new System.EventHandler(this.dtpFecha_ValueChanged_1);
            // 
            // headerControl1
            // 
            this.headerControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerControl1.Location = new System.Drawing.Point(0, 0);
            this.headerControl1.Name = "headerControl1";
            this.headerControl1.Size = new System.Drawing.Size(800, 110);
            this.headerControl1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dtpFecha);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 110);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 22);
            this.panel1.TabIndex = 3;
            // 
            // Citashoy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dgvHorario2);
            this.Controls.Add(this.headerControl1);
            this.Name = "Citashoy";
            this.Text = "Citashoy";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Citashoy_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHorario2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Components.HeaderControl headerControl1;
        private System.Windows.Forms.DataGridView dgvHorario2;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Panel panel1;
    }
}