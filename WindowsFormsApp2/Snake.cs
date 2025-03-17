using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SnakeGame
{
    public class GameForm : Form
    {
        private Timer gameTimer;
        private List<Point> snake = new List<Point>();
        private Point food;
        private int cellSize = 20;
        private string direction = "Right";
        private bool isGameOver = false;
        private Random random = new Random();

        public GameForm()
        {
            this.Text = "Змейка";
            this.Size = new Size(400, 400);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.DoubleBuffered = true;

            snake.Add(new Point(5, 5)); // Начальная позиция змейки
            GenerateFood();

            gameTimer = new Timer { Interval = 150 };
            gameTimer.Tick += GameLoop;
            gameTimer.Start();

            this.KeyDown += OnKeyDown;
            this.Paint += OnPaint;
        }

        private void GameLoop(object sender, EventArgs e)
        {
            if (isGameOver) return;

            MoveSnake();
            CheckCollision();
            this.Invalidate();
        }

        private void MoveSnake()
        {
            Point head = snake[0];
            Point newHead = head;

            switch (direction)
            {
                case "Up": newHead = new Point(head.X, head.Y - 1); break;
                case "Down": newHead = new Point(head.X, head.Y + 1); break;
                case "Left": newHead = new Point(head.X - 1, head.Y); break;
                case "Right": newHead = new Point(head.X + 1, head.Y); break;
            }

            snake.Insert(0, newHead);

            if (newHead == food)
            {
                GenerateFood();
            }
            else
            {
                snake.RemoveAt(snake.Count - 1);
            }
        }

        private void CheckCollision()
        {
            Point head = snake[0];

            // Столкновение со стенами
            if (head.X < 0 || head.Y < 0 || head.X >= this.ClientSize.Width / cellSize || head.Y >= this.ClientSize.Height / cellSize)
            {
                GameOver();
            }

            // Столкновение с самой собой
            for (int i = 1; i < snake.Count; i++)
            {
                if (snake[i] == head)
                {
                    GameOver();
                }
            }
        }

        private void GenerateFood()
        {
            int maxX = this.ClientSize.Width / cellSize;
            int maxY = this.ClientSize.Height / cellSize;
            food = new Point(random.Next(0, maxX), random.Next(0, maxY));

            // Проверяем, чтобы еда не появилась на змейке
            while (snake.Contains(food))
            {
                food = new Point(random.Next(0, maxX), random.Next(0, maxY));
            }
        }

        private void GameOver()
        {
            isGameOver = true;
            gameTimer.Stop();
            MessageBox.Show("Игра окончена!", "Змейка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Рисуем змейку
            foreach (Point p in snake)
            {
                g.FillRectangle(Brushes.Green, p.X * cellSize, p.Y * cellSize, cellSize, cellSize);
            }

            // Рисуем еду
            g.FillRectangle(Brushes.Red, food.X * cellSize, food.Y * cellSize, cellSize, cellSize);
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up when direction != "Down": direction = "Up"; break;
                case Keys.Down when direction != "Up": direction = "Down"; break;
                case Keys.Left when direction != "Right": direction = "Left"; break;
                case Keys.Right when direction != "Left": direction = "Right"; break;
            }
        }

        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new GameForm());
        }
    }
}
