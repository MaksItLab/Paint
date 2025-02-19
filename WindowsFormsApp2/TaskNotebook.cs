using System;
using System.Windows.Forms;

namespace TaskPlannerApp
{
    public class MainForm : Form
    {
        private Label labelPriority, labelCompleted, labelStartDate, labelDeadline, labelTasks;
        private RadioButton radioLow, radioMedium, radioHigh;
        private CheckBox checkBoxCompleted;
        private DateTimePicker dateTimePicker;
        private MonthCalendar monthCalendar;
        private ListBox listBoxTasks;
        private Button buttonAddTask;

        public MainForm()
        {
            // Настройка формы
            this.Text = "Планировщик задач";
            this.Size = new System.Drawing.Size(420, 500);

            // Приоритет задачи
            labelPriority = new Label() { Text = "Приоритет:", Location = new System.Drawing.Point(20, 20) };
            radioLow = new RadioButton() { Text = "Низкий", Location = new System.Drawing.Point(20, 45) };
            radioMedium = new RadioButton() { Text = "Средний", Location = new System.Drawing.Point(100, 45) };
            radioHigh = new RadioButton() { Text = "Высокий", Location = new System.Drawing.Point(180, 45) };

            // Статус выполнения
            labelCompleted = new Label() { Text = "Статус:", Location = new System.Drawing.Point(20, 80) };
            checkBoxCompleted = new CheckBox() { Text = "Задача выполнена", Location = new System.Drawing.Point(20, 105) };

            // Дата начала
            labelStartDate = new Label() { Text = "Дата начала:", Location = new System.Drawing.Point(20, 140) };
            dateTimePicker = new DateTimePicker() { Location = new System.Drawing.Point(20, 165), Format = DateTimePickerFormat.Short };

            // Дедлайн (окончание)
            labelDeadline = new Label() { Text = "Выберите дедлайн:", Location = new System.Drawing.Point(20, 200) };
            monthCalendar = new MonthCalendar() { Location = new System.Drawing.Point(20, 225) };

            // Список задач
            labelTasks = new Label() { Text = "Список задач:", Location = new System.Drawing.Point(220, 20) };
            listBoxTasks = new ListBox() { Location = new System.Drawing.Point(220, 45), Size = new System.Drawing.Size(160, 250) };

            // Кнопка "Добавить задачу"
            buttonAddTask = new Button() { Text = "Добавить", Location = new System.Drawing.Point(20, 400) };
            buttonAddTask.Click += new EventHandler(ButtonAddTask_Click);

            // Добавляем элементы в форму
            this.Controls.Add(labelPriority);
            this.Controls.Add(radioLow);
            this.Controls.Add(radioMedium);
            this.Controls.Add(radioHigh);
            this.Controls.Add(labelCompleted);
            this.Controls.Add(checkBoxCompleted);
            this.Controls.Add(labelStartDate);
            this.Controls.Add(dateTimePicker);
            this.Controls.Add(labelDeadline);
            this.Controls.Add(monthCalendar);
            this.Controls.Add(labelTasks);
            this.Controls.Add(listBoxTasks);
            this.Controls.Add(buttonAddTask);
        }

        private void ButtonAddTask_Click(object sender, EventArgs e)
        {
            // Определяем приоритет
            string priority = radioLow.Checked ? "Низкий" : (radioMedium.Checked ? "Средний" : (radioHigh.Checked ? "Высокий" : "Не указан"));

            // Определяем статус выполнения
            string status = checkBoxCompleted.Checked ? "[✔] Выполнено" : "[ ] В процессе";

            // Получаем даты
            string startDate = dateTimePicker.Value.ToShortDateString();
            string deadline = monthCalendar.SelectionStart.ToShortDateString();

            // Добавляем задачу в список
            string task = $"{status} | Приоритет: {priority} | {startDate} → {deadline}";
            listBoxTasks.Items.Add(task);
        }

        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new MainForm());
        }
    }
}
