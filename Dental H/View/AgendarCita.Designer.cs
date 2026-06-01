namespace Dental_H.View
{
    partial class AgendarCita
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
            this.headerControl1 = new Dental_H.Components.HeaderControl();
            this.SuspendLayout();
            // 
            // headerControl1
            // 
            this.headerControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerControl1.Location = new System.Drawing.Point(0, 0);
            this.headerControl1.Name = "headerControl1";
            this.headerControl1.Size = new System.Drawing.Size(800, 110);
            this.headerControl1.TabIndex = 0;
            this.headerControl1.Load += new System.EventHandler(this.headerControl1_Load);
            // 
            // AgendarCita
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.headerControl1);
            this.Name = "AgendarCita";
            this.Text = "AgendarCita";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private Components.HeaderControl headerControl1;
    }
}