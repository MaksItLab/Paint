using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace TextProcessor
{
    public class Form1 : Form
    {
        private TextBox txtInput;
        private Label lblStats;
        private Button btnLoad, btnSave, btnReverseText, btnAnalyze, btnUpperCase, btnLowerCase;

        public Form1()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "Text Processor";
            this.Size = new System.Drawing.Size(600, 400);

            txtInput = new TextBox
            {
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                Location = new System.Drawing.Point(20, 20),
                Size = new System.Drawing.Size(540, 150)
            };

            btnLoad = new Button { Text = "Загрузить файл", Location = new System.Drawing.Point(20, 180) };
            btnSave = new Button { Text = "Сохранить файл", Location = new System.Drawing.Point(150, 180) };
            btnReverseText = new Button { Text = "Развернуть текст", Location = new System.Drawing.Point(280, 180) };
            btnAnalyze = new Button { Text = "Анализ текста", Location = new System.Drawing.Point(410, 180) };

            btnUpperCase = new Button { Text = "Верхний регистр", Location = new System.Drawing.Point(150, 220) };
            btnLowerCase = new Button { Text = "Нижний регистр", Location = new System.Drawing.Point(280, 220) };

            lblStats = new Label
            {
                Text = "Символов: 0 | Слов: 0 | Предложений: 0",
                Location = new System.Drawing.Point(20, 260),
                Width = 540
            };

            this.Controls.AddRange(new Control[] { txtInput, btnLoad, btnSave, btnReverseText, btnAnalyze, btnUpperCase, btnLowerCase, lblStats });

            btnLoad.Click += BtnLoad_Click;
            btnSave.Click += BtnSave_Click;
            btnReverseText.Click += BtnReverseText_Click;
            btnAnalyze.Click += BtnAnalyze_Click;
            btnUpperCase.Click += BtnUpperCase_Click;
            btnLowerCase.Click += BtnLowerCase_Click;
        }

        private void BtnLoad_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Текстовые файлы|*.txt";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtInput.Text = File.ReadAllText(openFileDialog.FileName, Encoding.UTF8);
                }
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Текстовые файлы|*.txt";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(saveFileDialog.FileName, txtInput.Text, Encoding.UTF8);
                    MessageBox.Show("Текст сохранен!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void BtnReverseText_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder(txtInput.Text);
            char[] charArray = sb.ToString().ToCharArray();
            Array.Reverse(charArray);
            txtInput.Text = new string(charArray);
        }

        private void BtnAnalyze_Click(object sender, EventArgs e)
        {
            string text = txtInput.Text;
            int charCount = text.Length;
            int wordCount = Regex.Matches(text, @"\b\w+\b").Count;
            int sentenceCount = Regex.Matches(text, @"[.!?]").Count;

            lblStats.Text = $"Символов: {charCount} | Слов: {wordCount} | Предложений: {sentenceCount}";
        }

        private void BtnUpperCase_Click(object sender, EventArgs e)
        {
            txtInput.Text = txtInput.Text.ToUpper();
        }

        private void BtnLowerCase_Click(object sender, EventArgs e)
        {
            txtInput.Text = txtInput.Text.ToLower();
        }
    }
}
