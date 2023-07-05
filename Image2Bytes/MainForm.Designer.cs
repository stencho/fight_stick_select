namespace Image2Bytes {
    partial class MainForm {
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.preview)).BeginInit();
            this.SuspendLayout();
            // 
            // preview
            // 
            this.preview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.preview.Location = new System.Drawing.Point(263, 12);
            this.preview.Name = "preview";
            this.preview.Size = new System.Drawing.Size(245, 113);
            this.preview.TabIndex = 2;
            this.preview.TabStop = false;
            this.preview.SizeChanged += new System.EventHandler(this.pictureBox1_SizeChanged);
            this.preview.Paint += new System.Windows.Forms.PaintEventHandler(this.preview_Paint);
            // 
            // output_name_textbox
            // 
            this.output_name_textbox.Location = new System.Drawing.Point(12, 43);
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
            this.bytes_rb.AutoSize = true;
            this.bytes_rb.Checked = true;
            this.bytes_rb.Location = new System.Drawing.Point(263, 174);
            this.bytes_rb.Name = "bytes_rb";
            this.bytes_rb.Size = new System.Drawing.Size(214, 19);
            this.bytes_rb.TabIndex = 4;
            this.bytes_rb.TabStop = true;
            this.bytes_rb.Text = "Byte (8bpp monochrome, 1px/byte)";
            this.bytes_rb.UseVisualStyleBackColor = true;
            // 
            // bits_rb
            // 
            this.bits_rb.AutoSize = true;
            this.bits_rb.Location = new System.Drawing.Point(263, 191);
            this.bits_rb.Name = "bits_rb";
            this.bits_rb.Size = new System.Drawing.Size(170, 19);
            this.bits_rb.TabIndex = 5;
            this.bits_rb.Text = "Packed Bit (1bpp, 8px/byte)";
            this.bits_rb.UseVisualStyleBackColor = true;
            // 
            // type_label
            // 
            this.type_label.AutoSize = true;
            this.type_label.Location = new System.Drawing.Point(263, 158);
            this.type_label.Name = "type_label";
            this.type_label.Size = new System.Drawing.Size(66, 15);
            this.type_label.TabIndex = 6;
            this.type_label.Text = "Array Type:";
            // 
            // extra_label
            // 
            this.extra_label.AutoSize = true;
            this.extra_label.Location = new System.Drawing.Point(263, 233);
            this.extra_label.Name = "extra_label";
            this.extra_label.Size = new System.Drawing.Size(76, 15);
            this.extra_label.TabIndex = 7;
            this.extra_label.Text = "Extra Output:";
            // 
            // wh_defines_check
            // 
            this.wh_defines_check.AutoSize = true;
            this.wh_defines_check.Checked = true;
            this.wh_defines_check.CheckState = System.Windows.Forms.CheckState.Checked;
            this.wh_defines_check.Location = new System.Drawing.Point(264, 251);
            this.wh_defines_check.Name = "wh_defines_check";
            this.wh_defines_check.Size = new System.Drawing.Size(147, 19);
            this.wh_defines_check.TabIndex = 8;
            this.wh_defines_check.Text = "Width/Height #defines";
            this.wh_defines_check.UseVisualStyleBackColor = true;
            // 
            // color_rb
            // 
            this.color_rb.AutoSize = true;
            this.color_rb.Location = new System.Drawing.Point(263, 208);
            this.color_rb.Name = "color_rb";
            this.color_rb.Size = new System.Drawing.Size(109, 19);
            this.color_rb.TabIndex = 9;
            this.color_rb.Text = "Integer (RGBA8)";
            this.color_rb.UseVisualStyleBackColor = true;
            // 
            // screen_demo_xybox
            // 
            this.screen_demo_xybox.Location = new System.Drawing.Point(355, 128);
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
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(263, 131);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 12;
            this.label1.Text = "Demo LCD:";
            // 
            // demo_lcd_check
            // 
            this.demo_lcd_check.AutoSize = true;
            this.demo_lcd_check.Checked = true;
            this.demo_lcd_check.CheckState = System.Windows.Forms.CheckState.Checked;
            this.demo_lcd_check.Location = new System.Drawing.Point(334, 132);
            this.demo_lcd_check.Name = "demo_lcd_check";
            this.demo_lcd_check.Size = new System.Drawing.Size(15, 14);
            this.demo_lcd_check.TabIndex = 13;
            this.demo_lcd_check.UseVisualStyleBackColor = true;
            this.demo_lcd_check.CheckedChanged += new System.EventHandler(this.demo_lcd_check_CheckedChanged);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.IntegralHeight = false;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new System.Drawing.Point(12, 72);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(245, 185);
            this.listBox1.TabIndex = 16;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button1.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button1.Location = new System.Drawing.Point(12, 263);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(245, 25);
            this.button1.TabIndex = 17;
            this.button1.Text = "Remove [Del]";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button2.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button2.Location = new System.Drawing.Point(12, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(245, 25);
            this.button2.TabIndex = 18;
            this.button2.Text = "Add Image [Ctrl+N]";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(264, 269);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(125, 19);
            this.checkBox1.TabIndex = 19;
            this.checkBox1.Text = "Drawing Functions";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(520, 297);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listBox1);
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
            this.Controls.Add(this.preview);
            this.Controls.Add(this.bits_rb);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.Text = "Image2Array";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.preview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
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
        private ListBox listBox1;
        private Button button1;
        private Button button2;
        private CheckBox checkBox1;
    }
}