namespace Image2Bytes {
    partial class Form1 {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.filename_textbox = new System.Windows.Forms.TextBox();
            this.browse_button = new System.Windows.Forms.Button();
            this.preview = new System.Windows.Forms.PictureBox();
            this.output_name_textbox = new System.Windows.Forms.TextBox();
            this.grabtab = new System.Windows.Forms.LinkLabel();
            this.bytes_rb = new System.Windows.Forms.RadioButton();
            this.bits_rb = new System.Windows.Forms.RadioButton();
            this.type_label = new System.Windows.Forms.Label();
            this.extra_label = new System.Windows.Forms.Label();
            this.wh_defines_check = new System.Windows.Forms.CheckBox();
            this.color_rb = new System.Windows.Forms.RadioButton();
            this.screen_demo_xybox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.demo_lcd_check = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.preview)).BeginInit();
            this.SuspendLayout();
            // 
            // filename_textbox
            // 
            this.filename_textbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.filename_textbox.Location = new System.Drawing.Point(261, 6);
            this.filename_textbox.Name = "filename_textbox";
            this.filename_textbox.PlaceholderText = "Input Filename";
            this.filename_textbox.Size = new System.Drawing.Size(223, 23);
            this.filename_textbox.TabIndex = 1;
            this.filename_textbox.TextChanged += new System.EventHandler(this.filename_textbox_TextChanged);
            // 
            // browse_button
            // 
            this.browse_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browse_button.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.browse_button.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.browse_button.Location = new System.Drawing.Point(482, 5);
            this.browse_button.Name = "browse_button";
            this.browse_button.Size = new System.Drawing.Size(25, 25);
            this.browse_button.TabIndex = 2;
            this.browse_button.Text = "🡅";
            this.browse_button.UseVisualStyleBackColor = true;
            // 
            // preview
            // 
            this.preview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.preview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.preview.Location = new System.Drawing.Point(261, 225);
            this.preview.Name = "preview";
            this.preview.Size = new System.Drawing.Size(245, 170);
            this.preview.TabIndex = 2;
            this.preview.TabStop = false;
            this.preview.SizeChanged += new System.EventHandler(this.pictureBox1_SizeChanged);
            this.preview.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // output_name_textbox
            // 
            this.output_name_textbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.output_name_textbox.Location = new System.Drawing.Point(261, 35);
            this.output_name_textbox.Name = "output_name_textbox";
            this.output_name_textbox.PlaceholderText = "Output Name";
            this.output_name_textbox.Size = new System.Drawing.Size(245, 23);
            this.output_name_textbox.TabIndex = 3;
            // 
            // grabtab
            // 
            this.grabtab.AutoSize = true;
            this.grabtab.Location = new System.Drawing.Point(143, -51);
            this.grabtab.Name = "grabtab";
            this.grabtab.Size = new System.Drawing.Size(88, 15);
            this.grabtab.TabIndex = 0;
            this.grabtab.TabStop = true;
            this.grabtab.Text = "the tab grabber";
            // 
            // bytes_rb
            // 
            this.bytes_rb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bytes_rb.AutoSize = true;
            this.bytes_rb.Checked = true;
            this.bytes_rb.Location = new System.Drawing.Point(261, 77);
            this.bytes_rb.Name = "bytes_rb";
            this.bytes_rb.Size = new System.Drawing.Size(214, 19);
            this.bytes_rb.TabIndex = 4;
            this.bytes_rb.TabStop = true;
            this.bytes_rb.Text = "Byte (8bpp monochrome, 1px/byte)";
            this.bytes_rb.UseVisualStyleBackColor = true;
            // 
            // bits_rb
            // 
            this.bits_rb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bits_rb.AutoSize = true;
            this.bits_rb.Location = new System.Drawing.Point(261, 94);
            this.bits_rb.Name = "bits_rb";
            this.bits_rb.Size = new System.Drawing.Size(170, 19);
            this.bits_rb.TabIndex = 5;
            this.bits_rb.Text = "Packed Bit (1bpp, 8px/byte)";
            this.bits_rb.UseVisualStyleBackColor = true;
            // 
            // type_label
            // 
            this.type_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.type_label.AutoSize = true;
            this.type_label.Location = new System.Drawing.Point(261, 61);
            this.type_label.Name = "type_label";
            this.type_label.Size = new System.Drawing.Size(66, 15);
            this.type_label.TabIndex = 6;
            this.type_label.Text = "Array Type:";
            // 
            // extra_label
            // 
            this.extra_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.extra_label.AutoSize = true;
            this.extra_label.Location = new System.Drawing.Point(261, 136);
            this.extra_label.Name = "extra_label";
            this.extra_label.Size = new System.Drawing.Size(76, 15);
            this.extra_label.TabIndex = 7;
            this.extra_label.Text = "Extra Output:";
            // 
            // wh_defines_check
            // 
            this.wh_defines_check.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.wh_defines_check.AutoSize = true;
            this.wh_defines_check.Checked = true;
            this.wh_defines_check.CheckState = System.Windows.Forms.CheckState.Checked;
            this.wh_defines_check.Location = new System.Drawing.Point(262, 154);
            this.wh_defines_check.Name = "wh_defines_check";
            this.wh_defines_check.Size = new System.Drawing.Size(147, 19);
            this.wh_defines_check.TabIndex = 8;
            this.wh_defines_check.Text = "Width/Height #defines";
            this.wh_defines_check.UseVisualStyleBackColor = true;
            // 
            // color_rb
            // 
            this.color_rb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.color_rb.AutoSize = true;
            this.color_rb.Location = new System.Drawing.Point(261, 111);
            this.color_rb.Name = "color_rb";
            this.color_rb.Size = new System.Drawing.Size(109, 19);
            this.color_rb.TabIndex = 9;
            this.color_rb.Text = "Integer (RGBA8)";
            this.color_rb.UseVisualStyleBackColor = true;
            // 
            // screen_demo_xybox
            // 
            this.screen_demo_xybox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.screen_demo_xybox.Location = new System.Drawing.Point(283, 196);
            this.screen_demo_xybox.Name = "screen_demo_xybox";
            this.screen_demo_xybox.PlaceholderText = "XxY";
            this.screen_demo_xybox.Size = new System.Drawing.Size(54, 23);
            this.screen_demo_xybox.TabIndex = 11;
            this.screen_demo_xybox.Text = "128x32";
            this.screen_demo_xybox.TextChanged += new System.EventHandler(this.screen_demo_xybox_TextChanged);
            this.screen_demo_xybox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.screen_demo_xybox_KeyDown);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(261, 178);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 12;
            this.label1.Text = "Demo LCD:";
            // 
            // demo_lcd_check
            // 
            this.demo_lcd_check.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.demo_lcd_check.AutoSize = true;
            this.demo_lcd_check.Checked = true;
            this.demo_lcd_check.CheckState = System.Windows.Forms.CheckState.Checked;
            this.demo_lcd_check.Location = new System.Drawing.Point(262, 200);
            this.demo_lcd_check.Name = "demo_lcd_check";
            this.demo_lcd_check.Size = new System.Drawing.Size(15, 14);
            this.demo_lcd_check.TabIndex = 13;
            this.demo_lcd_check.UseVisualStyleBackColor = true;
            this.demo_lcd_check.CheckedChanged += new System.EventHandler(this.demo_lcd_check_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(6, 401);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(500, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.IntegralHeight = false;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new System.Drawing.Point(6, 6);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(250, 389);
            this.listBox1.TabIndex = 16;
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 429);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.demo_lcd_check);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.screen_demo_xybox);
            this.Controls.Add(this.color_rb);
            this.Controls.Add(this.wh_defines_check);
            this.Controls.Add(this.extra_label);
            this.Controls.Add(this.type_label);
            this.Controls.Add(this.bytes_rb);
            this.Controls.Add(this.grabtab);
            this.Controls.Add(this.output_name_textbox);
            this.Controls.Add(this.filename_textbox);
            this.Controls.Add(this.browse_button);
            this.Controls.Add(this.preview);
            this.Controls.Add(this.bits_rb);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Name = "Form1";
            this.Text = "Image2Array";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.preview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox filename_textbox;
        private Button browse_button;
        private PictureBox preview;
        private TextBox output_name_textbox;
        private LinkLabel linkLabel1;
        private LinkLabel grabtab;
        private RadioButton bytes_rb;
        private RadioButton bits_rb;
        private Label type_label;
        private Label extra_label;
        private CheckBox wh_defines_check;
        private RadioButton color_rb;
        private LinkLabel linkLabel2;
        private TextBox screen_demo_xybox;
        private Label label1;
        private CheckBox demo_lcd_check;
        private Button button1;
        private ListBox listBox1;
    }
}