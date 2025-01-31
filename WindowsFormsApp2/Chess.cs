using System;
using System.Drawing;
using System.Windows.Forms;

namespace ChessBoardApp
{
    public partial class MainForm : Form
    {
        private const int TileSize = 80;
        private const int BoardSize = 8;
        private readonly ContextMenuStrip contextMenu = new ContextMenuStrip();

        public MainForm()
        {
            InitializeComponent();
            this.Text = "Chess Board App";
            this.ClientSize = new Size(TileSize * BoardSize, TileSize * BoardSize);
            this.Paint += MainForm_Paint;
            InitializeContextMenu();
        }

        private void InitializeContextMenu()
        {
            contextMenu.Items.Add("Move", null, OnMoveClick);
            contextMenu.Items.Add("Remove", null, OnRemoveClick);
        }

        private void OnMoveClick(object sender, EventArgs e)
        {
            MessageBox.Show("Move action triggered.");
        }

        private void OnRemoveClick(object sender, EventArgs e)
        {
            MessageBox.Show("Remove action triggered.");
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            DrawChessBoard(e.Graphics);
            DrawChessPieces(e.Graphics);
        }

        private void DrawChessBoard(Graphics g)
        {
            for (int row = 0; row < BoardSize; row++)
            {
                for (int col = 0; col < BoardSize; col++)
                {
                    bool isWhite = (row + col) % 2 == 0;
                    Brush brush = isWhite ? Brushes.Beige : Brushes.SaddleBrown;
                    g.FillRectangle(brush, col * TileSize, row * TileSize, TileSize, TileSize);
                }
            }
        }

        private void DrawChessPieces(Graphics g)
        {
            DrawPiece(g, "R", 0, 0); // Rook
            DrawPiece(g, "N", 1, 0); // Knight
            DrawPiece(g, "B", 2, 0); // Bishop
            DrawPiece(g, "Q", 3, 0); // Queen
            DrawPiece(g, "K", 4, 0); // King
            DrawPiece(g, "B", 5, 0); // Bishop
            DrawPiece(g, "N", 6, 0); // Knight
            DrawPiece(g, "R", 7, 0); // Rook

            for (int col = 0; col < BoardSize; col++)
            {
                DrawPiece(g, "P", col, 1); // Pawns
            }
        }

        private void DrawPiece(Graphics g, string piece, int col, int row)
        {
            Rectangle tile = new Rectangle(col * TileSize, row * TileSize, TileSize, TileSize);
            g.DrawString(piece, new Font("Arial", 24, FontStyle.Bold), Brushes.Black, tile);

            // Add context menu handling
            tile.Offset(1, 1); // Slight offset for better event detection
            Control pieceControl = new Control
            {
                Bounds = tile,
                ContextMenuStrip = contextMenu
            };
            this.Controls.Add(pieceControl);
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
