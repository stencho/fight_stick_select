namespace Image2Bytes {
    partial class OutputForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.resize_panel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.resize_panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.PlaceholderText = "Output";
            this.textBox1.Size = new System.Drawing.Size(468, 449);
            this.textBox1.TabIndex = 0;
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // resize_panel
            // 
            this.resize_panel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.resize_panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.resize_panel.Controls.Add(this.label1);
            this.resize_panel.Location = new System.Drawing.Point(448, 428);
            this.resize_panel.Name = "resize_panel";
            this.resize_panel.Size = new System.Drawing.Size(27, 28);
            this.resize_panel.TabIndex = 1;
            this.resize_panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.resize_panel_MouseDown);
            this.resize_panel.MouseEnter += new System.EventHandler(this.resize_panel_MouseEnter);
            this.resize_panel.MouseLeave += new System.EventHandler(this.resize_panel_MouseLeave);
            this.resize_panel.MouseHover += new System.EventHandler(this.resize_panel_MouseHover);
            this.resize_panel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.resize_panel_MouseMove);
            this.resize_panel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.resize_panel_MouseUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.CausesValidation = false;
            this.label1.Enabled = false;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(-1, -1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "◢";
            // 
            // OutputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 449);
            this.ControlBox = false;
            this.Controls.Add(this.resize_panel);
            this.Controls.Add(this.textBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OutputForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Output";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.OutputForm_Paint);
            this.resize_panel.ResumeLayout(false);
            this.resize_panel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox textBox1;
        private Panel resize_panel;
        private Label label1;
    }
}