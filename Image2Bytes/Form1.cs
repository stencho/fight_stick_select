using System.Runtime.InteropServices;

namespace Image2Bytes {
    public partial class Form1 : Form {
        [DllImport("user32.dll")]
        internal static extern IntPtr SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        internal static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        static int check_size = 16;
        static int checkerboard_size => check_size * 2;

        Bitmap checkerboard = new Bitmap(checkerboard_size, checkerboard_size);
        Bitmap working_bitmap;

        Size demo_screen_size = new Size(128,32);


        public Form1() {
            InitializeComponent();

            for (int y = 0; y < checkerboard_size; y++) {
                for (int x = 0; x < checkerboard_size; x++) {
                    if ((x < check_size && y < check_size) || (x >= check_size && y >= check_size))
                        checkerboard.SetPixel(x, y, Color.DarkGray);
                    else checkerboard.SetPixel(x, y, Color.LightGray);
                }
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e) {
            e.Graphics.Clear(Color.White);

            int x_iter = (int)(preview.Width / checkerboard_size) + 1;
            int y_iter = (int)(preview.Height / checkerboard_size) + 1;

            Point center = new Point(preview.Width / 2, preview.Height / 2);

            for (int y = 0; y <= y_iter; y++) {
                for (int x = 0; x <= x_iter; x++) {
                    e.Graphics.DrawImage(checkerboard, 
                        (x * checkerboard_size), (y * checkerboard_size),
                        checkerboard_size, checkerboard_size);
                }
            }

            e.Graphics.FillRectangle(Brushes.Black, new Rectangle(center - (demo_screen_size / 2), demo_screen_size));

            if (preview.Image != null) {
                e.Graphics.DrawImage(preview.Image, center - (preview.Image.Size / 2));
            }
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e) {
            preview.Refresh();
        }


        private void load_or_clear(string file) {
            if (File.Exists(file)) {
                var fi = new FileInfo(file);
                output_name_textbox.Text = fi.Name.Replace(fi.Extension, "");


                if (   fi.Extension == ".png" 
                    || fi.Extension == ".gif"
                    || fi.Extension == ".bmp" 
                    || fi.Extension == ".jpg" 
                    || fi.Extension == ".jpeg")
                preview.Image = (Image)Bitmap.FromFile(file);
            } else {
                output_name_textbox.Text = "";
                preview.Image = null;
                preview.Refresh();
            }
        }

        private void Form1_DragDrop(object sender, DragEventArgs e) {
            if (e.Data == null) return;
            if (e.Effect == DragDropEffects.Copy) {
                string[] file = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (File.Exists(file[0])) {
                    filename_textbox.Text = file[0];
                    SetForegroundWindow(this.Handle);
                }
            }
            
        }

        private void Form1_DragEnter(object sender, DragEventArgs e) {
            if (e.Data == null) return;
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void filename_textbox_TextChanged(object sender, EventArgs e) {
            load_or_clear(filename_textbox.Text);
        }

        private Size xy_res() {
            string[] spl = screen_demo_xybox.Text.Split('x');
            int X,Y;
            if (!int.TryParse(spl[0], out X)) return Size.Empty;
            if (!int.TryParse(spl[1], out Y)) return Size.Empty;
            return new Size(X,Y);
        }

        private void screen_demo_xybox_KeyDown(object sender, KeyEventArgs e) {
            e.SuppressKeyPress = false;

            switch (e.KeyCode) {
                case Keys.D0:
                case Keys.D1:
                case Keys.D2:
                case Keys.D3:
                case Keys.D4:
                case Keys.D5:
                case Keys.D6:
                case Keys.D7:
                case Keys.D8:
                case Keys.D9:
                    if (e.Shift) e.SuppressKeyPress = true;
                    break;

                case Keys.Back:
                case Keys.Home:
                case Keys.End:
                case Keys.Left:
                case Keys.Right:
                case Keys.Up:
                case Keys.Down:
                case Keys.Enter:
                case Keys.Escape:                    
                case Keys.Control:
                case Keys.Delete:
                case Keys.Tab:                    
                    break;

                case Keys.X:
                    if (!e.Control) {
                        if (screen_demo_xybox.Text.Contains('x')) {
                            e.SuppressKeyPress = true;
                        }                        
                    }
                    break;

                case Keys.A:
                case Keys.C:
                case Keys.V:
                case Keys.Z:
                    if (!e.Control) e.SuppressKeyPress=true;
                    break;

                default: 
                    e.SuppressKeyPress = true;
                    break;
            }
        }


        private void screen_demo_xybox_TextChanged(object sender, EventArgs e) {
            screen_demo_xybox.Text = new string(screen_demo_xybox.Text.Where(c => char.IsDigit(c) || c == 'x' || c == 'X').ToArray()).ToLower();
            demo_screen_size = xy_res();
            preview.Refresh();
        }

        private void demo_lcd_check_CheckedChanged(object sender, EventArgs e) {
            if (demo_lcd_check.Checked) {
                demo_screen_size = xy_res();
                preview.Refresh();
            } else {
                demo_screen_size = Size.Empty;
                preview.Refresh();
            }
        }
        
    }
}