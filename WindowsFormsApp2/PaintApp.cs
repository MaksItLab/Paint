using System;
using System.Drawing;
using System.Windows.Forms;

public class GraphicsEditor : Form
{
    private PictureBox pictureBox;
    private Bitmap canvas;
    private Graphics graphics;
    private bool isDrawing = false;
    private Point lastPoint;
    private Color currentColor = Color.Black;
    private Button btnOpen, btnSave, btnClear, btnColor, btnText;
    
    public GraphicsEditor()
    {
        this.Text = "Графический редактор";
        this.Size = new Size(800, 600);
        
        canvas = new Bitmap(700, 500);
        graphics = Graphics.FromImage(canvas);
        graphics.Clear(Color.White);
        
        pictureBox = new PictureBox()
        {
            Location = new Point(50, 50),
            Size = new Size(700, 500),
            BorderStyle = BorderStyle.Fixed3D,
            Image = canvas
        };
        pictureBox.MouseDown += StartDrawing;
        pictureBox.MouseMove += Draw;
        pictureBox.MouseUp += StopDrawing;
        
        btnOpen = CreateButton("Открыть", 10, 10, OpenImage);
        btnSave = CreateButton("Сохранить", 110, 10, SaveImage);
        btnClear = CreateButton("Очистить", 210, 10, ClearCanvas);
        btnColor = CreateButton("Выбрать цвет", 310, 10, SelectColor);
        btnText = CreateButton("Добавить текст", 410, 10, AddText);
        
        this.Controls.AddRange(new Control[] { pictureBox, btnOpen, btnSave, btnClear, btnColor, btnText });
    }
    
    private Button CreateButton(string text, int x, int y, EventHandler clickHandler)
    {
        return new Button()
        {
            Text = text,
            Location = new Point(x, y),
            Size = new Size(100, 30),
            Click = clickHandler
        };
    }
    
    private void OpenImage(object sender, EventArgs e)
    {
        OpenFileDialog ofd = new OpenFileDialog { Filter = "Image Files|*.jpg;*.png;*.bmp" };
        if (ofd.ShowDialog() == DialogResult.OK)
        {
            canvas = new Bitmap(ofd.FileName);
            graphics = Graphics.FromImage(canvas);
            pictureBox.Image = canvas;
        }
    }
    
    private void SaveImage(object sender, EventArgs e)
    {
        SaveFileDialog sfd = new SaveFileDialog { Filter = "PNG Image|*.png" };
        if (sfd.ShowDialog() == DialogResult.OK)
        {
            canvas.Save(sfd.FileName);
        }
    }
    
    private void ClearCanvas(object sender, EventArgs e)
    {
        graphics.Clear(Color.White);
        pictureBox.Refresh();
    }
    
    private void SelectColor(object sender, EventArgs e)
    {
        ColorDialog colorDialog = new ColorDialog();
        if (colorDialog.ShowDialog() == DialogResult.OK)
        {
            currentColor = colorDialog.Color;
        }
    }
    
    private void AddText(object sender, EventArgs e)
    {
        string text = Microsoft.VisualBasic.Interaction.InputBox("Введите текст:", "Добавить текст", "");
        if (!string.IsNullOrEmpty(text))
        {
            graphics.DrawString(text, new Font("Arial", 16), new SolidBrush(currentColor), new PointF(50, 50));
            pictureBox.Refresh();
        }
    }
    
    private void StartDrawing(object sender, MouseEventArgs e)
    {
        isDrawing = true;
        lastPoint = e.Location;
    }
    
    private void Draw(object sender, MouseEventArgs e)
    {
        if (isDrawing)
        {
            graphics.DrawLine(new Pen(currentColor, 2), lastPoint, e.Location);
            lastPoint = e.Location;
            pictureBox.Refresh();
        }
    }
    
    private void StopDrawing(object sender, MouseEventArgs e)
    {
        isDrawing = false;
    }
    
    [STAThread]
    static void Main()
    {
        Application.Run(new GraphicsEditor());
    }
}
