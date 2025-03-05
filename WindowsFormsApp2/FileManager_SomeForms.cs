using System;
using System.IO;
using System.Windows.Forms;

namespace FileManagerApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    listBoxFiles.Items.Clear();
                    string[] files = Directory.GetFiles(folderDialog.SelectedPath);
                    listBoxFiles.Items.AddRange(files);
                }
            }
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    listBoxFiles.Items.Add(openFileDialog.FileName);
                }
            }
        }

        private void btnSaveFile_Click(object sender, EventArgs e)
        {
            if (listBoxFiles.SelectedItem == null)
            {
                MessageBox.Show("Выберите файл для сохранения.");
                return;
            }

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.FileName = Path.GetFileName(listBoxFiles.SelectedItem.ToString());
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    File.Copy(listBoxFiles.SelectedItem.ToString(), saveFileDialog.FileName);
                    MessageBox.Show("Файл сохранен успешно.");
                }
            }
        }

        private void btnProperties_Click(object sender, EventArgs e)
        {
            if (listBoxFiles.SelectedItem == null)
            {
                MessageBox.Show("Выберите файл для просмотра свойств.");
                return;
            }

            string filePath = listBoxFiles.SelectedItem.ToString();
            Form2 propertiesForm = new Form2(filePath);
            propertiesForm.ShowDialog();
        }
    }
}



using System;
using System.IO;
using System.Windows.Forms;

namespace FileManagerApp
{
    public partial class Form2 : Form
    {
        public Form2(string filePath)
        {
            InitializeComponent();

            FileInfo fileInfo = new FileInfo(filePath);
            lblFileName.Text = "Файл: " + fileInfo.Name;
            lblSize.Text = "Размер: " + fileInfo.Length + " байт";
            lblCreated.Text = "Создан: " + fileInfo.CreationTime.ToString();
            lblModified.Text = "Изменен: " + fileInfo.LastWriteTime.ToString();
        }
    }
}
