using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Image2Bytes {
    public partial class AddImageForm : Form {

        public string filename = "";
        
        
        public AddImageForm() {
            InitializeComponent();
        }

        private void load_or_clear(string file) {
            if (File.Exists(file)) {
                var fi = new FileInfo(file);

                if (fi.Extension == ".png"
                    || fi.Extension == ".gif"
                    || fi.Extension == ".bmp"
                    || fi.Extension == ".jpg"
                    || fi.Extension == ".jpeg") {
                    filename = file;
                    preview.Image = (Image)Bitmap.FromFile(file);
                    add_button.Enabled = true;
                }
            } else {
                preview.Image = null;
                preview.Refresh();

                add_button.Enabled = false;
            }
        }

        private void filename_textbox_TextChanged(object sender, EventArgs e) {
            load_or_clear(filename_textbox.Text);
        }

        private void preview_Paint(object sender, PaintEventArgs e) {
            e.Graphics.Clear(Color.White);

            int x_iter = (int)(preview.Width / MainForm.checkerboard_size) + 1;
            int y_iter = (int)(preview.Height / MainForm.checkerboard_size) + 1;

            Point center = new Point(preview.Width / 2, preview.Height / 2);

            for (int y = 0; y <= y_iter; y++) {
                for (int x = 0; x <= x_iter; x++) {
                    e.Graphics.DrawImage(MainForm.checkerboard,
                        (x * MainForm.checkerboard_size), (y * MainForm.checkerboard_size),
                        MainForm.checkerboard_size, MainForm.checkerboard_size);
                }
            }

            if (preview.Image != null) {
                e.Graphics.DrawImage(preview.Image, center - (preview.Image.Size / 2));
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
