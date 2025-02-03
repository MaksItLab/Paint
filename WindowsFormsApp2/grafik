using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ChartApp
{
    public class MainForm : Form
    {
        private Chart chart;
        private Button addButton;
        private TextBox valueTextBox;
        private TextBox labelTextBox;
        private Label valueLabel;
        private Label labelLabel;

        public MainForm()
        {
            // Настройка формы
            this.Text = "Pie Chart Application";
            this.Size = new System.Drawing.Size(800, 600);

            // Инициализация компонентов
            chart = new Chart { Dock = DockStyle.Top, Height = 400 };
            var chartArea = new ChartArea("MainArea");
            chart.ChartAreas.Add(chartArea);
            var series = new Series("Data")
            {
                ChartType = SeriesChartType.Pie
            };
            chart.Series.Add(series);

            addButton = new Button { Text = "Add Data Point", Top = 420, Left = 600, Width = 120 };
            addButton.Click += AddButton_Click;

            valueLabel = new Label { Text = "Value:", Top = 420, Left = 50 };
            valueTextBox = new TextBox { Top = 420, Left = 110, Width = 100 };

            labelLabel = new Label { Text = "Label:", Top = 420, Left = 250 };
            labelTextBox = new TextBox { Top = 420, Left = 310, Width = 100 };

            // Добавление компонентов на форму
            this.Controls.Add(chart);
            this.Controls.Add(addButton);
            this.Controls.Add(valueLabel);
            this.Controls.Add(valueTextBox);
            this.Controls.Add(labelLabel);
            this.Controls.Add(labelTextBox);
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (double.TryParse(valueTextBox.Text, out double value) && !string.IsNullOrWhiteSpace(labelTextBox.Text))
            {
                chart.Series["Data"].Points.AddXY(labelTextBox.Text, value);
                valueTextBox.Clear();
                labelTextBox.Clear();
            }
            else
            {
                MessageBox.Show("Please enter a valid numeric value and a label.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
