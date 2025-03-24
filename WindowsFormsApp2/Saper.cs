using System;
using System.Drawing;
using System.Windows.Forms;

public partial class Minesweeper : Form
{
    private const int gridSize = 5;
    private const int cellSize = 50;
    private const int mineCount = 5;
    private Button[,] buttons = new Button[gridSize, gridSize];
    private bool[,] mines = new bool[gridSize, gridSize];
    private Label lblStatus;

    public Minesweeper()
    {
        this.Text = "Сапер";
        this.ClientSize = new Size(gridSize * cellSize, gridSize * cellSize + 30);
        InitGame();
    }

    private void InitGame()
    {
        Random rand = new Random();
        int placedMines = 0;
        
        while (placedMines < mineCount)
        {
            int x = rand.Next(gridSize);
            int y = rand.Next(gridSize);
            if (!mines[x, y])
            {
                mines[x, y] = true;
                placedMines++;
            }
        }
        
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                Button btn = new Button
                {
                    Size = new Size(cellSize, cellSize),
                    Location = new Point(i * cellSize, j * cellSize),
                    Tag = new Point(i, j)
                };
                btn.Click += CellClick;
                buttons[i, j] = btn;
                this.Controls.Add(btn);
            }
        }
        
        lblStatus = new Label
        {
            Text = "Найдите все безопасные клетки!",
            AutoSize = true,
            Location = new Point(10, gridSize * cellSize + 5)
        };
        this.Controls.Add(lblStatus);
    }

    private void CellClick(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        Point pos = (Point)btn.Tag;
        int x = pos.X, y = pos.Y;

        if (mines[x, y])
        {
            btn.BackColor = Color.Red;
            MessageBox.Show("Вы попали на мину! Игра окончена.", "Поражение");
            ResetGame();
        }
        else
        {
            int mineCount = CountMinesAround(x, y);
            btn.Text = mineCount > 0 ? mineCount.ToString() : "";
            btn.BackColor = Color.LightGray;
            btn.Enabled = false;
        }
    }

    private int CountMinesAround(int x, int y)
    {
        int count = 0;
        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                int nx = x + dx, ny = y + dy;
                if (nx >= 0 && nx < gridSize && ny >= 0 && ny < gridSize && mines[nx, ny])
                {
                    count++;
                }
            }
        }
        return count;
    }

    private void ResetGame()
    {
        this.Controls.Clear();
        buttons = new Button[gridSize, gridSize];
        mines = new bool[gridSize, gridSize];
        InitGame();
    }

    [STAThread]
    public static void Main()
    {
        Application.Run(new Minesweeper());
    }
}
