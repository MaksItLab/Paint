using System;
using System.Windows.Forms;

namespace UserFormApp
{
    public class MainForm : Form
    {
        private Label labelGender, labelNews, labelBirthDate, labelMeeting;
        private RadioButton radioMale, radioFemale;
        private CheckBox checkBoxNews;
        private DateTimePicker dateTimePicker;
        private MonthCalendar monthCalendar;
        private Button buttonSubmit;
        private Label labelResult;

        public MainForm()
        {
            // Настройка формы
            this.Text = "Анкета пользователя";
            this.Size = new System.Drawing.Size(400, 500);

            // Выбор пола
            labelGender = new Label() { Text = "Выберите пол:", Location = new System.Drawing.Point(20, 20) };
            radioMale = new RadioButton() { Text = "Мужской", Location = new System.Drawing.Point(20, 45) };
            radioFemale = new RadioButton() { Text = "Женский", Location = new System.Drawing.Point(120, 45) };

            // Получать новости?
            labelNews = new Label() { Text = "Получать новости?", Location = new System.Drawing.Point(20, 80) };
            checkBoxNews = new CheckBox() { Text = "Да, хочу получать", Location = new System.Drawing.Point(20, 105) };

            // Дата рождения
            labelBirthDate = new Label() { Text = "Дата рождения:", Location = new System.Drawing.Point(20, 140) };
            dateTimePicker = new DateTimePicker() { Location = new System.Drawing.Point(20, 165), Format = DateTimePickerFormat.Short };

            // Выбор дня встречи
            labelMeeting = new Label() { Text = "Выберите день встречи:", Location = new System.Drawing.Point(20, 200) };
            monthCalendar = new MonthCalendar() { Location = new System.Drawing.Point(20, 225) };

            // Кнопка "Отправить"
            buttonSubmit = new Button() { Text = "Отправить", Location = new System.Drawing.Point(20, 400) };
            buttonSubmit.Click += new EventHandler(ButtonSubmit_Click);

            // Вывод данных
            labelResult = new Label() { Location = new System.Drawing.Point(20, 430), Size = new System.Drawing.Size(350, 40) };

            // Добавляем элементы в форму
            this.Controls.Add(labelGender);
            this.Controls.Add(radioMale);
            this.Controls.Add(radioFemale);
            this.Controls.Add(labelNews);
            this.Controls.Add(checkBoxNews);
            this.Controls.Add(labelBirthDate);
            this.Controls.Add(dateTimePicker);
            this.Controls.Add(labelMeeting);
            this.Controls.Add(monthCalendar);
            this.Controls.Add(buttonSubmit);
            this.Controls.Add(labelResult);
        }

        private void ButtonSubmit_Click(object sender, EventArgs e)
        {
            // Получаем значения
            string gender = radioMale.Checked ? "Мужской" : (radioFemale.Checked ? "Женский" : "Не выбран");
            string news = checkBoxNews.Checked ? "Подписан на новости" : "Без подписки";
            string birthDate = dateTimePicker.Value.ToShortDateString();
            string meetingDate = monthCalendar.SelectionStart.ToShortDateString();

            // Вывод результата
            labelResult.Text = $"Пол: {gender}\n{news}\nДата рождения: {birthDate}\nВстреча: {meetingDate}";
        }

        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new MainForm());
        }
    }
}
