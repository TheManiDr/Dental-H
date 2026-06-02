namespace Dental_H.View
{
    partial class Personal
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
            this.flpPersonal = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // flpPersonal
            // 
            this.flpPersonal.AutoScroll = true;
            this.flpPersonal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(250)))));
            this.flpPersonal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpPersonal.Location = new System.Drawing.Point(0, 110);
            this.flpPersonal.Name = "flpPersonal";
            this.flpPersonal.Padding = new System.Windows.Forms.Padding(25);
            this.flpPersonal.Size = new System.Drawing.Size(800, 340);
            this.flpPersonal.TabIndex = 1;
            // 
            // headerControl1
            // 
            this.headerControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerControl1.Location = new System.Drawing.Point(0, 0);
            this.headerControl1.Name = "headerControl1";
            this.headerControl1.Size = new System.Drawing.Size(800, 110);
            this.headerControl1.TabIndex = 0;
            // 
            // Personal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.flpPersonal);
            this.Controls.Add(this.headerControl1);
            this.Name = "Personal";
            this.Text = "Personal";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Personal_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Components.HeaderControl headerControl1;
        private System.Windows.Forms.FlowLayoutPanel flpPersonal;
    }
}
