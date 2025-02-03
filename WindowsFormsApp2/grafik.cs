using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        private Chart chart;
        private Button addButton;
        private TextBox xValueTextBox;
        private TextBox yValueTextBox;
        private Label xLabel;
        private Label yLabel;

        public Form1()
        {

            // Настройка формы
            this.Text = "Chart Application";
            this.Size = new System.Drawing.Size(800, 600);

            // Инициализация компонентов
            chart = new Chart { Dock = DockStyle.Top, Height = 400 };
            var chartArea = new ChartArea("MainArea");
            chart.ChartAreas.Add(chartArea);
            var series = new Series("Data")
            {
                ChartType = SeriesChartType.Line
            };
            chart.Series.Add(series);

            addButton = new Button { Text = "Add Data Point", Top = 420, Left = 600, Width = 120 };
            addButton.Click += AddButton_Click;

            xLabel = new Label { Text = "X Value:", Top = 420, Left = 50 };
            xValueTextBox = new TextBox { Top = 420, Left = 110, Width = 100 };

            yLabel = new Label { Text = "Y Value:", Top = 420, Left = 250 };
            yValueTextBox = new TextBox { Top = 420, Left = 310, Width = 100 };

            // Добавление компонентов на форму
            this.Controls.Add(chart);
            this.Controls.Add(addButton);
            this.Controls.Add(xLabel);
            this.Controls.Add(xValueTextBox);
            this.Controls.Add(yLabel);
            this.Controls.Add(yValueTextBox);

        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (double.TryParse(xValueTextBox.Text, out double xValue) &&
                double.TryParse(yValueTextBox.Text, out double yValue))
            {
                chart.Series["Data"].Points.AddXY(xValue, yValue);
                xValueTextBox.Clear();
                yValueTextBox.Clear();
            }
            else
            {
                MessageBox.Show("Please enter valid numeric values for both X and Y.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
