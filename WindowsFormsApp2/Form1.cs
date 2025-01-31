using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        private List<Shape> shapes = new List<Shape>();
        private ShapeType currentShapeType = ShapeType.Rectangle;
        private Color currentColor = Color.Black;
        private bool drawing = false;
        private Point startPoint;
        public Form1()
        {
            InitializeComponent();
            SetupUI();
        }

        private void SetupUI()
        {
            // Create ToolStrip
            ToolStrip toolStrip = new ToolStrip();
            this.Controls.Add(toolStrip);

            // Buttons for shapes
            ToolStripButton rectangleButton = new ToolStripButton("Rectangle");
            rectangleButton.Click += (s, e) => currentShapeType = ShapeType.Rectangle;
            toolStrip.Items.Add(rectangleButton);

            ToolStripButton ellipseButton = new ToolStripButton("Ellipse");
            ellipseButton.Click += (s, e) => currentShapeType = ShapeType.Ellipse;
            toolStrip.Items.Add(ellipseButton);

            ToolStripButton lineButton = new ToolStripButton("Line");
            lineButton.Click += (s, e) => currentShapeType = ShapeType.Line;
            toolStrip.Items.Add(lineButton);

            // Color picker
            ToolStripButton colorButton = new ToolStripButton("Pick Color");
            colorButton.Click += (s, e) => PickColor();
            toolStrip.Items.Add(colorButton);

            // Save button
            ToolStripButton saveButton = new ToolStripButton("Save");
            saveButton.Click += (s, e) => SaveCanvas();
            toolStrip.Items.Add(saveButton);

            this.Text = "Drawing App";
            this.DoubleBuffered = true;
            this.BackColor = Color.White;
            this.WindowState = FormWindowState.Maximized;
            this.Paint += Form1_Paint;
            this.MouseDown += Form1_MouseDown;
            this.MouseUp += Form1_MouseUp;
            this.MouseMove += Form1_MouseMove;
        }

        private void PickColor()
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    currentColor = colorDialog.Color;
                }
            }
        }

        private void SaveCanvas()
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "PNG Image|*.png|JPEG Image|*.jpg|Bitmap Image|*.bmp";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (Bitmap bitmap = new Bitmap(this.ClientSize.Width, this.ClientSize.Height))
                    {
                        this.DrawToBitmap(bitmap, this.ClientRectangle);
                        bitmap.Save(saveFileDialog.FileName);
                    }
                }
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            foreach (var shape in shapes)
            {
                shape.Draw(g);
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            drawing = true;
            startPoint = e.Location;
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (drawing)
            {
                Point endPoint = e.Location;
                shapes.Add(ShapeFactory.CreateShape(currentShapeType, startPoint, endPoint, currentColor));
                drawing = false;
                this.Invalidate();
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (drawing)
            {
                this.Invalidate();
            }
        }
    }

    public enum ShapeType
    {
        Rectangle,
        Ellipse,
        Line
    }

    public abstract class Shape
    {
        public Color Color { get; set; }
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }

        public Shape(Point startPoint, Point endPoint, Color color)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
            Color = color;
        }

        public abstract void Draw(Graphics g);
    }

    public class RectangleShape : Shape
    {
        public RectangleShape(Point startPoint, Point endPoint, Color color) : base(startPoint, endPoint, color) { }

        public override void Draw(Graphics g)
        {
            using (Pen pen = new Pen(Color))
            {
                g.DrawRectangle(pen, GetRectangle());
            }
        }

        private Rectangle GetRectangle()
        {
            return new Rectangle(
                Math.Min(StartPoint.X, EndPoint.X),
                Math.Min(StartPoint.Y, EndPoint.Y),
                Math.Abs(StartPoint.X - EndPoint.X),
                Math.Abs(StartPoint.Y - EndPoint.Y)
            );
        }
    }

    public class EllipseShape : Shape
    {
        public EllipseShape(Point startPoint, Point endPoint, Color color) : base(startPoint, endPoint, color) { }

        public override void Draw(Graphics g)
        {
            using (Pen pen = new Pen(Color))
            {
                g.DrawEllipse(pen, GetRectangle());
            }
        }

        private Rectangle GetRectangle()
        {
            return new Rectangle(
                Math.Min(StartPoint.X, EndPoint.X),
                Math.Min(StartPoint.Y, EndPoint.Y),
                Math.Abs(StartPoint.X - EndPoint.X),
                Math.Abs(StartPoint.Y - EndPoint.Y)
            );
        }
    }

    public class LineShape : Shape
    {
        public LineShape(Point startPoint, Point endPoint, Color color) : base(startPoint, endPoint, color) { }

        public override void Draw(Graphics g)
        {
            using (Pen pen = new Pen(Color))
            {
                g.DrawLine(pen, StartPoint, EndPoint);
            }
        }
    }

    public static class ShapeFactory
    {
        public static Shape CreateShape(ShapeType shapeType, Point startPoint, Point endPoint, Color color)
        {
            switch (shapeType)
            {
                case ShapeType.Rectangle:
                    return new RectangleShape(startPoint, endPoint, color);
                case ShapeType.Ellipse:
                    return new EllipseShape(startPoint, endPoint, color);
                case ShapeType.Line:
                    return new LineShape(startPoint, endPoint, color);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
