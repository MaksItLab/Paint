using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace TextEditor
{
    public class MainForm : Form
    {
        private MenuStrip menuStrip;
        private ToolStrip toolbar;
        private RichTextBox textBox;
        private List<string> recentFiles = new List<string>();

        public MainForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            // Main form properties
            this.Text = "Текстовый редактор";
            this.Width = 800;
            this.Height = 600;

            // Menu strip
            menuStrip = new MenuStrip();
            var fileMenu = new ToolStripMenuItem("Файл");
            var editMenu = new ToolStripMenuItem("Редактирование");
            var helpMenu = new ToolStripMenuItem("Помощь");

            // File menu items
            var openItem = new ToolStripMenuItem("Открыть", null, OpenFile) { ShortcutKeys = Keys.Control | Keys.O };
            var saveItem = new ToolStripMenuItem("Сохранить", null, SaveFile) { ShortcutKeys = Keys.Control | Keys.S };
            var exitItem = new ToolStripMenuItem("Выход", null, (s, e) => Close()) { ShortcutKeys = Keys.Control | Keys.Q };
            var recentFilesMenu = new ToolStripMenuItem("Недавние файлы");

            fileMenu.DropDownItems.AddRange(new ToolStripItem[] { openItem, saveItem, new ToolStripSeparator(), recentFilesMenu, exitItem });

            // Edit menu items
            var copyItem = new ToolStripMenuItem("Копировать", null, (s, e) => textBox.Copy()) { ShortcutKeys = Keys.Control | Keys.C };
            var pasteItem = new ToolStripMenuItem("Вставить", null, (s, e) => textBox.Paste()) { ShortcutKeys = Keys.Control | Keys.V };

            editMenu.DropDownItems.AddRange(new ToolStripItem[] { copyItem, pasteItem });

            // Help menu items
            var aboutItem = new ToolStripMenuItem("О программе", null, ShowAbout);
            helpMenu.DropDownItems.Add(aboutItem);

            menuStrip.Items.AddRange(new ToolStripItem[] { fileMenu, editMenu, helpMenu });

            // Toolbar
            toolbar = new ToolStrip();
            toolbar.Items.Add(new ToolStripButton("Открыть", null, OpenFile) { ToolTipText = "Открыть файл" });
            toolbar.Items.Add(new ToolStripButton("Сохранить", null, SaveFile) { ToolTipText = "Сохранить файл" });
            toolbar.Items.Add(new ToolStripButton("Копировать", null, (s, e) => textBox.Copy()) { ToolTipText = "Копировать текст" });
            toolbar.Items.Add(new ToolStripButton("Вставить", null, (s, e) => textBox.Paste()) { ToolTipText = "Вставить текст" });

            // RichTextBox
            textBox = new RichTextBox { Dock = DockStyle.Fill, ContextMenuStrip = CreateContextMenu() };

            // Layout
            Controls.Add(textBox);
            Controls.Add(toolbar);
            Controls.Add(menuStrip);
            MainMenuStrip = menuStrip;
        }

        private ContextMenuStrip CreateContextMenu()
        {
            var contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add("Копировать", null, (s, e) => textBox.Copy());
            contextMenu.Items.Add("Вставить", null, (s, e) => textBox.Paste());
            return contextMenu;
        }

        private void OpenFile(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    textBox.Text = File.ReadAllText(openFileDialog.FileName);
                    AddToRecentFiles(openFileDialog.FileName);
                }
            }
        }

        private void SaveFile(object sender, EventArgs e)
        {
            using (var saveFileDialog = new SaveFileDialog())
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(saveFileDialog.FileName, textBox.Text);
                }
            }
        }

        private void AddToRecentFiles(string filePath)
        {
            if (!recentFiles.Contains(filePath))
            {
                recentFiles.Add(filePath);
                UpdateRecentFilesMenu();
            }
        }

        private void UpdateRecentFilesMenu()
        {
            var fileMenu = menuStrip.Items[0] as ToolStripMenuItem;
            var recentFilesMenu = fileMenu.DropDownItems[3] as ToolStripMenuItem;
            recentFilesMenu.DropDownItems.Clear();

            foreach (var file in recentFiles)
            {
                recentFilesMenu.DropDownItems.Add(new ToolStripMenuItem(file, null, (s, e) => LoadRecentFile(file)));
            }
        }

        private void LoadRecentFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                textBox.Text = File.ReadAllText(filePath);
            }
            else
            {
                MessageBox.Show("Файл не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowAbout(object sender, EventArgs e)
        {
            MessageBox.Show("Простой текстовый редактор с меню и тулбаром.", "О программе", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
