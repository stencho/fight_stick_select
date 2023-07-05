using System.Runtime.InteropServices;

namespace Image2Bytes {
    public partial class MainForm : Form {
        [DllImport("user32.dll")]
        internal static extern IntPtr SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        internal static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public static int check_size = 16;
        public static int checkerboard_size => check_size * 2;

        public static Bitmap checkerboard = new Bitmap(checkerboard_size, checkerboard_size);
        Bitmap working_bitmap;

        Size demo_screen_size = new Size(128,32);

        OutputForm output_form;

        public enum array_type {
            BYTE,
            PACKED_BIT,
            RGBA8
        }

        public struct added_image {
            public string filename;
            public string filename_short;
            public string output_name;
            public Size size;
            public Bitmap bitmap;
            public override string ToString() {
                return $"{output_name}: {filename_short}, {size.Width}x{size.Height}";
            }
        }

        added_image filename_to_ai(string file) {
            if (File.Exists(file)) {
                FileInfo fi = new FileInfo(file);
                var tb = (Bitmap)Bitmap.FromFile(file);

                return new added_image {
                    filename = file,
                    filename_short = fi.Name,
                    output_name = fi.Name.Replace(fi.Extension, ""),
                    bitmap = tb,
                    size = new Size(tb.Width, tb.Height)
                };
            } else throw new FileNotFoundException();
        }

        public MainForm() {
            InitializeComponent();
            Application.EnableVisualStyles();

            for (int y = 0; y < checkerboard_size; y++) {
                for (int x = 0; x < checkerboard_size; x++) {
                    if ((x < check_size && y < check_size) || (x >= check_size && y >= check_size))
                        checkerboard.SetPixel(x, y, Color.DarkGray);
                    else checkerboard.SetPixel(x, y, Color.LightGray);
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e) {
            output_form = new OutputForm();

            SetWindowLongPtrA(output_form.Handle, GWL_STYLE, 0x004L);

            output_form.Show();
        }

        [Serializable, StructLayout(LayoutKind.Sequential)]
        public struct RECT {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;

            public RECT(int left_, int top_, int right_, int bottom_) {
                Left = left_;
                Top = top_;
                Right = right_;
                Bottom = bottom_;
            }

            public int Height { get { return Bottom - Top; } }
            public int Width { get { return Right - Left; } }
            public Size Size { get { return new Size(Width, Height); } }

            public Point Location { get { return new Point(Left, Top); } }

            // Handy method for converting to a System.Drawing.Rectangle
            public Rectangle ToRectangle() { return Rectangle.FromLTRB(Left, Top, Right, Bottom); }

            public static RECT FromRectangle(Rectangle rectangle) {
                return new RECT(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom);
            }

            public override int GetHashCode() {
                return Left ^ ((Top << 13) | (Top >> 0x13))
                  ^ ((Width << 0x1a) | (Width >> 6))
                  ^ ((Height << 7) | (Height >> 0x19));
            }

            #region Operator overloads

            public static implicit operator Rectangle(RECT rect) {
                return rect.ToRectangle();
            }

            public static implicit operator RECT(Rectangle rect) {
                return FromRectangle(rect);
            }

            #endregion
        }


        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll")][return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool BringWindowToTop(IntPtr hWnd);

        const int GWL_STYLE = -16;

        [DllImport("user32.dll")]
        static extern IntPtr SetWindowLongPtrA(IntPtr hWnd, int nIndex, long dwNewLong);

        RECT gwr;
        RECT output_gwr;

        const int WM_MOVE = 0x0003;
        const int WM_SIZE = 0x0005;
        const int WM_MOVING = 0x0216;
        const int WM_SIZING = 0x0214;
        const int WM_ENTERSIZEMOVE = 0x0231;

        protected override void WndProc(ref Message m) {
            switch (m.Msg) {
                case WM_ENTERSIZEMOVE:
                    BringWindowToTop(output_form.Handle);
                    BringWindowToTop(Handle);
                    break;

                case WM_MOVE:
                case WM_SIZE:
                case WM_MOVING:
                case WM_SIZING:
                    GetWindowRect(this.Handle, out gwr);
                    GetWindowRect(output_form.Handle, out output_gwr);

                    MoveWindow(output_form.Handle, 
                        gwr.Right - 7, gwr.Top, 
                        output_gwr.Width, output_gwr.Height, 
                        false);

                    break;
            }

            base.WndProc(ref m);
        }


        private void preview_Paint(object sender, PaintEventArgs e) {
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

        //DRAG DROP
        private void Form1_DragDrop(object sender, DragEventArgs e) {
            if (e.Data == null) return;
            if (e.Effect == DragDropEffects.Copy) {
                string[] file = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (File.Exists(file[0])) {
                    //filename_textbox.Text = file[0];
                    SetForegroundWindow(this.Handle);
                }
            }            
        }

        private void Form1_DragEnter(object sender, DragEventArgs e) {
            if (e.Data == null) return;
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
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

        private void button2_Click(object sender, EventArgs e) {
            AddImageForm fart = new AddImageForm();
            fart.ShowDialog();
        }

    }
}