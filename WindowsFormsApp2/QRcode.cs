using QRCoder;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace qr
{
    public partial class Form1 : Form
    {
        private TextBox txtInput;
        private Button btnGenerate;
        private PictureBox pictureBox;
        public Form1()
        {
            this.Text = "Генератор QR-кодов";
            this.Width = 400;
            this.Height = 500;

            Label lbl = new Label()
            {
                Text = "Введите текст:",
                Location = new Point(20, 20),
                AutoSize = true
            };

            txtInput = new TextBox()
            {
                Location = new Point(20, 50),
                Width = 340
            };

            btnGenerate = new Button()
            {
                Text = "Сгенерировать QR-код",
                Location = new Point(20, 90),
                Width = 200
            };
            btnGenerate.Click += BtnGenerate_Click;

            pictureBox = new PictureBox()
            {
                Location = new Point(20, 140),
                Width = 300,
                Height = 300,
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            Controls.Add(lbl);
            Controls.Add(txtInput);
            Controls.Add(btnGenerate);
            Controls.Add(pictureBox);
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            string inputText = txtInput.Text.Trim();

            if (string.IsNullOrEmpty(inputText))
            {
                MessageBox.Show("Введите текст для генерации QR-кода.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(inputText, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);

            pictureBox.Image = qrCodeImage;
        }
    }
}
