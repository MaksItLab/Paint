using System;
using System.Drawing;
using System.Windows.Forms;

namespace ImageEditor
{
    public partial class Form1 : Form
    {
        private Image originalImage;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Изображения|*.jpg;*.png;*.bmp";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    originalImage = Image.FromFile(openFileDialog.FileName);
                    pictureBox.Image = new Bitmap(originalImage);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "JPEG Image|*.jpg|PNG Image|*.png|Bitmap Image|*.bmp";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    pictureBox.Image.Save(saveFileDialog.FileName);
                    MessageBox.Show("Изображение сохранено успешно.");
                }
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image == null)
            {
                MessageBox.Show("Откройте изображение перед настройкой.");
                return;
            }

            using (Form2 settingsForm = new Form2((Bitmap)pictureBox.Image))
            {
                if (settingsForm.ShowDialog() == DialogResult.OK)
                {
                    pictureBox.Image = settingsForm.ModifiedImage;
                }
            }
        }
    }
}




using System;
using System.Drawing;
using System.Windows.Forms;

namespace ImageEditor
{
    public partial class Form2 : Form
    {
        public Bitmap ModifiedImage { get; private set; }
        private Bitmap originalImage;

        public Form2(Bitmap image)
        {
            InitializeComponent();
            originalImage = new Bitmap(image);
            ModifiedImage = new Bitmap(image);
        }

        private void trackBarBrightness_Scroll(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void trackBarContrast_Scroll(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            float brightness = trackBarBrightness.Value / 10.0f;
            float contrast = trackBarContrast.Value / 10.0f;

            Bitmap adjustedImage = new Bitmap(originalImage.Width, originalImage.Height);
            using (Graphics g = Graphics.FromImage(adjustedImage))
            {
                float[][] colorMatrixElements = {
                    new float[] {contrast, 0, 0, 0, 0},
                    new float[] {0, contrast, 0, 0, 0},
                    new float[] {0, 0, contrast, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {brightness, brightness, brightness, 0, 1}
                };

                System.Drawing.Imaging.ColorMatrix colorMatrix = new System.Drawing.Imaging.ColorMatrix(colorMatrixElements);
                System.Drawing.Imaging.ImageAttributes imageAttributes = new System.Drawing.Imaging.ImageAttributes();
                imageAttributes.SetColorMatrix(colorMatrix);

                g.DrawImage(originalImage, new Rectangle(0, 0, adjustedImage.Width, adjustedImage.Height),
                    0, 0, originalImage.Width, originalImage.Height, GraphicsUnit.Pixel, imageAttributes);
            }

            ModifiedImage = adjustedImage;
            pictureBoxPreview.Image = ModifiedImage;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
