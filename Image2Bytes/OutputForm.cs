using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Image2Bytes.MainForm;

namespace Image2Bytes {
    public partial class OutputForm : Form {
        public OutputForm() {
            InitializeComponent();
            build_resize_handles();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e) {

        }

        [DllImport("user32.dll")] static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);
        [DllImport("user32.dll")] static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
        [DllImport("user32.dll")] static extern bool GetCursorPos(out Point lpPoint);

        RECT gwr;

        bool mouseover = false;
        bool resizing = false;

        bool mouseover_right = false;
        bool mouseover_bottom = false;

        const int WM_MOUSEMOVE = 0x0200;

        Rectangle right_rect = Rectangle.Empty;
        Rectangle bottom_rect = Rectangle.Empty;

        int resize_handle_size = 10;

        void build_resize_handles() {
            GetWindowRect(Handle, out gwr);
            right_rect = new Rectangle(Size.Width - resize_handle_size, 0, resize_handle_size, Size.Height);
            bottom_rect = new Rectangle(0, Size.Height - resize_handle_size, Size.Width, resize_handle_size);
        }

        void invalidate_handles() {
            Invalidate(right_rect);
            Invalidate(bottom_rect);
        }

        Point mouse_point;
        Point mouse_point_relative;
        Point last_mouse_point;

        bool point_intersects_rect(Point P, Rectangle R) {
            return point_intersects_rect(P, R.X, R.Y, R.Width, R.Height);
        }
            
        bool point_intersects_rect(Point P, int X, int Y, int W, int H) {
            if ((P.X >= X && P.X <= X + W) && (P.Y >= Y && P.Y <= Y + H)) {
                return true;
            }

            return false;
        }

        void resize() {

            GetCursorPos(out mouse_point);

            GetWindowRect(Handle, out gwr);

            mouse_point_relative.X = mouse_point.X - gwr.Left;
            mouse_point_relative.Y = mouse_point.Y - gwr.Top;


            if (resizing) {
                MoveWindow(Handle,
                    gwr.Left, gwr.Top,
                    gwr.Width + (mouse_point.X - last_mouse_point.X),
                    gwr.Height + (mouse_point.Y - last_mouse_point.Y),
                    true);
                


                build_resize_handles();

                Refresh();
                
                //invalidate_handles();

                last_mouse_point = mouse_point;
            }
        }

        protected override void WndProc(ref Message m) {
            if (m.Msg == WM_MOUSEMOVE) {}

            base.WndProc(ref m);
        }

        private void OutputForm_Paint(object sender, PaintEventArgs e) {
            if (mouseover) {

                if (point_intersects_rect(mouse_point_relative, right_rect)) {
                    e.Graphics.FillRectangle(Brushes.Black, right_rect);
                }
                if (point_intersects_rect(mouse_point_relative, bottom_rect)) {
                    e.Graphics.FillRectangle(Brushes.Black, bottom_rect);
                }
            }
        }

        private void resize_panel_MouseDown(object sender, MouseEventArgs e) {
            resizing = true;
            GetWindowRect(Handle, out gwr);
            GetCursorPos(out last_mouse_point);            
        }

        private void resize_panel_MouseUp(object sender, MouseEventArgs e) {
            resizing = false;
            mouseover = false;
        }

        private void resize_panel_MouseMove(object sender, MouseEventArgs e) {
            resize();
        }

        private void resize_panel_MouseEnter(object sender, EventArgs e) {
            mouseover = true;
            resize_panel.BackColor = Color.Black;
        }

        private void resize_panel_MouseLeave(object sender, EventArgs e) {
            mouseover = false;
            resize_panel.BackColor = Color.White;
        }

        private void resize_panel_MouseHover(object sender, EventArgs e) {
            mouseover = true;
            resize_panel.BackColor = Color.Black;

        }
    }
}
