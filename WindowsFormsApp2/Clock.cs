using System;
using System.Drawing;
using System.Windows.Forms;

namespace ClockApp
{
    public partial class ClockForm : Form
    {
        private Timer timer;

        public ClockForm()
        {
            InitializeComponent();
            this.Text = "Analog Clock";
            this.ClientSize = new Size(400, 400);
            this.DoubleBuffered = true;
            this.Paint += ClockForm_Paint;

            timer = new Timer();
            timer.Interval = 1000; // Update every second
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            this.Invalidate(); // Redraw the clock
        }

        private void ClockForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Center and radius of the clock
            int centerX = this.ClientSize.Width / 2;
            int centerY = this.ClientSize.Height / 2;
            int clockRadius = Math.Min(centerX, centerY) - 10;

            // Draw clock face
            g.DrawEllipse(Pens.Black, centerX - clockRadius, centerY - clockRadius, clockRadius * 2, clockRadius * 2);

            // Get current time
            DateTime now = DateTime.Now;
            int hours = now.Hour;
            int minutes = now.Minute;
            int seconds = now.Second;

            // Draw hour, minute, and second hands
            DrawHand(g, centerX, centerY, clockRadius * 0.5f, (hours % 12 + minutes / 60f) * 30, 6, Brushes.Black); // Hour hand
            DrawHand(g, centerX, centerY, clockRadius * 0.7f, (minutes + seconds / 60f) * 6, 4, Brushes.Black); // Minute hand
            DrawHand(g, centerX, centerY, clockRadius * 0.9f, seconds * 6, 2, Brushes.Red); // Second hand
        }

        private void DrawHand(Graphics g, int centerX, int centerY, float length, float angle, int thickness, Brush color)
        {
            double radians = (Math.PI / 180) * (angle - 90);
            int x = centerX + (int)(length * Math.Cos(radians));
            int y = centerY + (int)(length * Math.Sin(radians));

            using (Pen pen = new Pen(color, thickness))
            {
                pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                g.DrawLine(pen, centerX, centerY, x, y);
            }
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ClockForm());
        }
    }
}
