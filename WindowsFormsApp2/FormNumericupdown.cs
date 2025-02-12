using System;
using System.Windows.Forms;

namespace OrderForm
{
    public class MainForm : Form
    {
        private Label labelProduct;
        private ComboBox comboBoxProduct;
        private Label labelQuantity;
        private NumericUpDown numericUpDownQuantity;
        private Button buttonOrder;
        private Label labelTotal;
        private decimal pricePerUnit = 10.0m; // Example price per unit

        public MainForm()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "Форма заказа товаров";
            this.Width = 400;
            this.Height = 250;

            labelProduct = new Label { Text = "Выберите товар:", Top = 20, Left = 20 };
            comboBoxProduct = new ComboBox { Top = 50, Left = 20, Width = 200 };
            comboBoxProduct.Items.AddRange(new string[] { "Товар 1", "Товар 2", "Товар 3" });
            comboBoxProduct.SelectedIndex = 0;

            labelQuantity = new Label { Text = "Количество:", Top = 90, Left = 20 };
            numericUpDownQuantity = new NumericUpDown { Top = 120, Left = 20, Width = 100, Minimum = 1, Maximum = 100, Value = 1 };
            numericUpDownQuantity.ValueChanged += (s, e) => UpdateTotal();

            buttonOrder = new Button { Text = "Заказать", Top = 160, Left = 20 };
            buttonOrder.Click += (s, e) => MessageBox.Show("Заказ оформлен!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

            labelTotal = new Label { Text = "Итого: 10.00 руб.", Top = 160, Left = 120, Width = 200 };

            Controls.Add(labelProduct);
            Controls.Add(comboBoxProduct);
            Controls.Add(labelQuantity);
            Controls.Add(numericUpDownQuantity);
            Controls.Add(buttonOrder);
            Controls.Add(labelTotal);
        }

        private void UpdateTotal()
        {
            decimal total = numericUpDownQuantity.Value * pricePerUnit;
            labelTotal.Text = $"Итого: {total:F2} руб.";
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
