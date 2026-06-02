namespace Dental_H.View
{
    partial class Consultas
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.headerControl1 = new Dental_H.Components.HeaderControl();
            this.pnlContenido = new System.Windows.Forms.Panel();
            this.panelTabla = new System.Windows.Forms.Panel();
            this.dgvConsultas = new System.Windows.Forms.DataGridView();
            this.panelEncabezado = new System.Windows.Forms.Panel();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.pnlContenido.SuspendLayout();
            this.panelTabla.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConsultas)).BeginInit();
            this.panelEncabezado.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerControl1
            // 
            this.headerControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerControl1.Location = new System.Drawing.Point(0, 0);
            this.headerControl1.Name = "headerControl1";
            this.headerControl1.Size = new System.Drawing.Size(1200, 110);
            this.headerControl1.TabIndex = 0;
            // 
            // pnlContenido
            // 
            this.pnlContenido.Controls.Add(this.panelTabla);
            this.pnlContenido.Controls.Add(this.panelEncabezado);
            this.pnlContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContenido.Location = new System.Drawing.Point(0, 110);
            this.pnlContenido.Name = "pnlContenido";
            this.pnlContenido.Size = new System.Drawing.Size(1200, 590);
            this.pnlContenido.TabIndex = 1;
            // 
            // panelTabla
            // 
            this.panelTabla.BackColor = System.Drawing.Color.White;
            this.panelTabla.Controls.Add(this.dgvConsultas);
            this.panelTabla.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTabla.Location = new System.Drawing.Point(28, 132);
            this.panelTabla.Name = "panelTabla";
            this.panelTabla.Padding = new System.Windows.Forms.Padding(18);
            this.panelTabla.Size = new System.Drawing.Size(1144, 430);
            this.panelTabla.TabIndex = 1;
            // 
            // dgvConsultas
            // 
            this.dgvConsultas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvConsultas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvConsultas.Location = new System.Drawing.Point(18, 18);
            this.dgvConsultas.Name = "dgvConsultas";
            this.dgvConsultas.RowTemplate.Height = 30;
            this.dgvConsultas.Size = new System.Drawing.Size(1108, 394);
            this.dgvConsultas.TabIndex = 0;
            // 
            // panelEncabezado
            // 
            this.panelEncabezado.BackColor = System.Drawing.Color.White;
            this.panelEncabezado.Controls.Add(this.txtBuscar);
            this.panelEncabezado.Controls.Add(this.lblTotal);
            this.panelEncabezado.Controls.Add(this.lblSubtitulo);
            this.panelEncabezado.Controls.Add(this.lblTitulo);
            this.panelEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEncabezado.Location = new System.Drawing.Point(28, 28);
            this.panelEncabezado.Name = "panelEncabezado";
            this.panelEncabezado.Padding = new System.Windows.Forms.Padding(22, 14, 22, 14);
            this.panelEncabezado.Size = new System.Drawing.Size(1144, 104);
            this.panelEncabezado.TabIndex = 0;
            // 
            // txtBuscar
            // 
            this.txtBuscar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBuscar.Location = new System.Drawing.Point(781, 58);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(320, 20);
            this.txtBuscar.TabIndex = 3;
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotal.Location = new System.Drawing.Point(781, 25);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(320, 22);
            this.lblTotal.TabIndex = 2;
            this.lblTotal.Text = "0 consultas";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.Location = new System.Drawing.Point(25, 58);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(640, 26);
            this.lblSubtitulo.TabIndex = 1;
            this.lblSubtitulo.Text = "Subtitulo";
            // 
            // lblTitulo
            // 
            this.lblTitulo.Location = new System.Drawing.Point(25, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(520, 34);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Titulo";
            // 
            // Consultas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.pnlContenido);
            this.Controls.Add(this.headerControl1);
            this.Name = "Consultas";
            this.Text = "Consultas";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Consultas_Load);
            this.pnlContenido.ResumeLayout(false);
            this.panelTabla.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvConsultas)).EndInit();
            this.panelEncabezado.ResumeLayout(false);
            this.panelEncabezado.PerformLayout();
            this.ResumeLayout(false);
        }

        private Components.HeaderControl headerControl1;
        private System.Windows.Forms.Panel pnlContenido;
        private System.Windows.Forms.Panel panelTabla;
        private System.Windows.Forms.DataGridView dgvConsultas;
        private System.Windows.Forms.Panel panelEncabezado;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Label lblTitulo;
    }
}
