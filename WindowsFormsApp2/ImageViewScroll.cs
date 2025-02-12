using System;
using System.Drawing;
using System.Windows.Forms;

namespace ImageViewer
{
    public class MainForm : Form
    {
        private PictureBox pictureBox;
        private VScrollBar vScrollBar;
        private HScrollBar hScrollBar;
        private Image image;

        public MainForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "Просмотрщик изображений";
            this.Width = 800;
            this.Height = 600;

            pictureBox = new PictureBox
            {
                Location = new Point(0, 0),
                SizeMode = PictureBoxSizeMode.AutoSize
            };

            vScrollBar = new VScrollBar
            {
                Dock = DockStyle.Right,
                Visible = false
            };
            vScrollBar.Scroll += (s, e) => UpdateScroll();

            hScrollBar = new HScrollBar
            {
                Dock = DockStyle.Bottom,
                Visible = false
            };
            hScrollBar.Scroll += (s, e) => UpdateScroll();

            var panel = new Panel
            {
                AutoScroll = false,
                Dock = DockStyle.Fill
            };
            panel.Controls.Add(pictureBox);

            Controls.Add(panel);
            Controls.Add(vScrollBar);
            Controls.Add(hScrollBar);

            LoadImage();
        }

        private void LoadImage()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Изображения|*.png;*.jpg;*.jpeg;*.bmp;*.gif";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    image = Image.FromFile(openFileDialog.FileName);
                    pictureBox.Image = image;
                    AdjustScrollBars();
                }
            }
        }

        private void AdjustScrollBars()
        {
            if (image == null) return;

            vScrollBar.Visible = image.Height > ClientSize.Height;
            hScrollBar.Visible = image.Width > ClientSize.Width;

            if (vScrollBar.Visible)
            {
                vScrollBar.Maximum = image.Height - ClientSize.Height;
                vScrollBar.LargeChange = ClientSize.Height / 10;
            }
            if (hScrollBar.Visible)
            {
                hScrollBar.Maximum = image.Width - ClientSize.Width;
                hScrollBar.LargeChange = ClientSize.Width / 10;
            }
        }

        private void UpdateScroll()
        {
            pictureBox.Location = new Point(-hScrollBar.Value, -vScrollBar.Value);
        }

        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
