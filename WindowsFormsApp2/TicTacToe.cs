using System;
using System.Drawing;
using System.Windows.Forms;

namespace TicTacToe
{
    public class GameForm : Form
    {
        private Button[,] buttons = new Button[3, 3];
        private bool isXturn = true;

        public GameForm()
        {
            this.Text = "Крестики-нолики";
            this.Size = new Size(320, 360);
            this.StartPosition = FormStartPosition.CenterScreen;

            InitializeBoard();
        }

        private void InitializeBoard()
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    buttons[row, col] = new Button
                    {
                        Size = new Size(100, 100),
                        Location = new Point(col * 100, row * 100),
                        Font = new Font("Arial", 24, FontStyle.Bold),
                        Tag = new Point(row, col)
                    };
                    buttons[row, col].Click += OnButtonClick;
                    this.Controls.Add(buttons[row, col]);
                }
            }
        }

        private void OnButtonClick(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null || !string.IsNullOrEmpty(btn.Text))
                return;

            btn.Text = isXturn ? "X" : "O";
            isXturn = !isXturn;

            CheckForWinner();
        }

        private void CheckForWinner()
        {
            string winner = null;

            // Проверяем строки и столбцы
            for (int i = 0; i < 3; i++)
            {
                if (buttons[i, 0].Text != "" && buttons[i, 0].Text == buttons[i, 1].Text && buttons[i, 1].Text == buttons[i, 2].Text)
                    winner = buttons[i, 0].Text;
                if (buttons[0, i].Text != "" && buttons[0, i].Text == buttons[1, i].Text && buttons[1, i].Text == buttons[2, i].Text)
                    winner = buttons[0, i].Text;
            }

            // Проверяем диагонали
            if (buttons[0, 0].Text != "" && buttons[0, 0].Text == buttons[1, 1].Text && buttons[1, 1].Text == buttons[2, 2].Text)
                winner = buttons[0, 0].Text;
            if (buttons[0, 2].Text != "" && buttons[0, 2].Text == buttons[1, 1].Text && buttons[1, 1].Text == buttons[2, 0].Text)
                winner = buttons[0, 2].Text;

            // Победитель найден
            if (winner != null)
            {
                MessageBox.Show($"Победил {winner}!", "Игра окончена");
                ResetBoard();
                return;
            }

            // Проверяем на ничью
            bool draw = true;
            foreach (var btn in buttons)
            {
                if (btn.Text == "")
                {
                    draw = false;
                    break;
                }
            }

            if (draw)
            {
                MessageBox.Show("Ничья!", "Игра окончена");
                ResetBoard();
            }
        }

        private void ResetBoard()
        {
            foreach (var btn in buttons)
            {
                btn.Text = "";
            }
            isXturn = true;
        }

        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new GameForm());
        }
    }
}
