namespace Image2Bytes {
    partial class AddImageForm {
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
            this.filename_textbox = new System.Windows.Forms.TextBox();
            this.browse_button = new System.Windows.Forms.Button();
            this.preview = new System.Windows.Forms.PictureBox();
            this.add_button = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.preview)).BeginInit();
            this.SuspendLayout();
            // 
            // filename_textbox
            // 
            this.filename_textbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filename_textbox.Location = new System.Drawing.Point(4, 12);
            this.filename_textbox.Name = "filename_textbox";
            this.filename_textbox.PlaceholderText = "Input Filename";
            this.filename_textbox.Size = new System.Drawing.Size(234, 23);
            this.filename_textbox.TabIndex = 4;
            this.filename_textbox.TextChanged += new System.EventHandler(this.filename_textbox_TextChanged);
            // 
            // browse_button
            // 
            this.browse_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browse_button.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.browse_button.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.browse_button.Location = new System.Drawing.Point(235, 11);
            this.browse_button.Name = "browse_button";
            this.browse_button.Size = new System.Drawing.Size(25, 25);
            this.browse_button.TabIndex = 5;
            this.browse_button.Text = "🡅";
            this.browse_button.UseVisualStyleBackColor = true;
            // 
            // preview
            // 
            this.preview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.preview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.preview.Location = new System.Drawing.Point(4, 41);
            this.preview.Name = "preview";
            this.preview.Size = new System.Drawing.Size(256, 128);
            this.preview.TabIndex = 7;
            this.preview.TabStop = false;
            this.preview.Paint += new System.Windows.Forms.PaintEventHandler(this.preview_Paint);
            // 
            // add_button
            // 
            this.add_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.add_button.Enabled = false;
            this.add_button.Location = new System.Drawing.Point(4, 175);
            this.add_button.Name = "add_button";
            this.add_button.Size = new System.Drawing.Size(127, 23);
            this.add_button.TabIndex = 8;
            this.add_button.Text = "Add";
            this.add_button.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(133, 175);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(127, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // AddImageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(264, 203);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.add_button);
            this.Controls.Add(this.preview);
            this.Controls.Add(this.filename_textbox);
            this.Controls.Add(this.browse_button);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "AddImageForm";
            this.Text = "Add Image";
            ((System.ComponentModel.ISupportInitialize)(this.preview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private TextBox filename_textbox;
        private Button browse_button;
        private PictureBox preview;
        private Button add_button;
        private Button button2;
    }
}