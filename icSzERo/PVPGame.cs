using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace icSzERo
{
    public partial class PVPGame : Form
    {
        public PVPGame()
        {
            InitializeComponent();
            System.Timers.Timer UpdateTimer = new System.Timers.Timer();
            UpdateTimer.Elapsed += new System.Timers.ElapsedEventHandler(UpdateTimer_Tick);
            UpdateTimer.Interval = 10;
            UpdateTimer.Enabled = true;
        }

        PictureBox Canvas;

        char[,] Playground = new char[3, 3];

        private bool b = true;

        private const int TileSize = 200;

        private const int Size0 = 80 * TileSize / 100;
        private const int SizeX = Size0 / 2;

        private void Draw0(Graphics g, Point loc) /// loc = top-left
        {
            Pen pen = new Pen(Color.Black, 5);
            g.DrawEllipse(pen, loc.X, loc.Y, Size0, Size0);
        }

        private void DrawX(Graphics g, Point loc) /// loc = center
        {
            Pen pen = new Pen(Color.Black, 10);
            g.DrawLine(pen, loc.X - SizeX, loc.Y - SizeX, loc.X + SizeX, loc.Y + SizeX);
            g.DrawLine(pen, loc.X - SizeX, loc.Y + SizeX, loc.X + SizeX, loc.Y - SizeX);
        }

        private void Update(object sender, PaintEventArgs e)
        {
            const int width = 5;
            Pen pen = new Pen(Color.Black, width);
            e.Graphics.DrawLine(pen, 0, TileSize - width, Width, TileSize - width);
            e.Graphics.DrawLine(pen, 0, 2 * TileSize - width, Width, 2 * TileSize - width);
            e.Graphics.DrawLine(pen, TileSize - width, 0, TileSize - width, Height);
            e.Graphics.DrawLine(pen, 2 * TileSize - width, 0, 2 * TileSize - width, Height);

            for (int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    if(Playground[i, j] == 'X')
                    {
                        DrawX(e.Graphics, new Point(i * TileSize + TileSize / 2, j * TileSize + TileSize / 2));
                    }
                    else if(Playground[i, j] == '0')
                    {
                        Point p = new Point(i * TileSize + ((TileSize - Size0) / 2), j * TileSize + ((TileSize - Size0) / 2));
                        Draw0(e.Graphics, p);
                    }
                }
            }
        }

        private void ShowWinMessage()
        {
            if (b)
            {
                MessageBox.Show("Castiga 0!");
            }
            else
            {
                MessageBox.Show("Castiga X!");
            }
            Dispose();
        }

        private void CheckWin()
        {
            for(int i = 0; i < 3; i++)
            {
                if(Playground[i, 0] > 0 && Playground[i, 0] == Playground[i, 1] && Playground[i, 1] == Playground[i, 2])
                {
                    ShowWinMessage();
                }
                if(Playground[0, i] > 0 && Playground[0, i] == Playground[1, i] && Playground[1, i] == Playground[2, i])
                {
                    ShowWinMessage();
                }
            }
            if(Playground[0, 0] > 0 && Playground[0, 0] == Playground[1, 1] && Playground[1, 1] == Playground[2, 2])
            {
                ShowWinMessage();
            }
            if(Playground[1, 1] > 0 && Playground[0, 2] == Playground[1, 1] && Playground[1, 1] == Playground[2, 0])
            {
                ShowWinMessage();
            }
        }

        private void UserClick(object sender, MouseEventArgs e)
        {
            int x = e.Location.X / TileSize;
            int y = e.Location.Y / TileSize;
            if (Playground[x, y] != 'X' && Playground[x, y] != '0')
            {
                if (b)
                {
                    Playground[x, y] = 'X';
                }
                else
                {
                    Playground[x, y] = '0';
                }
                b = !b;
            }
            CheckWin();
        }


        private void PVPGame_Load(object sender, EventArgs e)
        {
            Size = new Size(TileSize * 3, TileSize * 3);
            Canvas = new PictureBox()
            {
                Location = new Point(0, 0),
                Size = new Size(TileSize * 3, TileSize * 3)
            };
            Controls.Add(Canvas);
            Canvas.Paint += new PaintEventHandler(Update);
            Canvas.MouseClick += new MouseEventHandler(UserClick);
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            Canvas.Invalidate();
        }
    }
}
