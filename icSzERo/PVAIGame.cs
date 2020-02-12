using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace icSzERo
{
    public partial class PVAIGame : Form
    {
        public PVAIGame()
        {
            InitializeComponent();
        }

        private const int TileSize = 200;

        private const int Size0 = 80 * TileSize / 100;
        private const int SizeX = Size0 / 2;

        PictureBox Canvas;

        char[,] Playground = new char[3, 3];

        private bool b = false;

        private const char gol = (char)0;
        private const char player = '0';
        private const char ai = 'X';


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
                for (int j = 0; j < 3; j++)
                {
                    if (Playground[i, j] == 'X')
                    {
                        DrawX(e.Graphics, new Point(i * TileSize + TileSize / 2, j * TileSize + TileSize / 2));
                    }
                    else if (Playground[i, j] == '0')
                    {
                        Point p = new Point(i * TileSize + ((TileSize - Size0) / 2), j * TileSize + ((TileSize - Size0) / 2));
                        Draw0(e.Graphics, p);
                    }
                }
            }
        }

        private bool NoMovesLeft(char[,] PG)
        {
            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    if(PG[i, j] != gol)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private int Evaluate(char[,] PG)
        {
            for(int i = 0; i < 3; i++)
            {
                if(PG[i, 0] != gol && PG[i, 0] == PG[i, 1] && PG[i, 1] == PG[i, 2])
                {
                    if(PG[i, 0] == player)
                    {
                        return -100;
                    }
                    else
                    {
                        return 100;
                    }
                }
                if(PG[0, i] != gol && PG[0, i] == PG[1, i] && PG[1, i] == PG[2, i])
                {
                    if (PG[0, i] == player)
                    {
                        return -100;
                    }
                    else
                    {
                        return 100;
                    }
                }
            }
            if(PG[0, 0] != gol && PG[0, 0] == PG[1, 1] && PG[1, 1] == PG[2, 2])
            {
                if (PG[0, 0] == player)
                {
                    return -100;
                }
                else
                {
                    return 100;
                }
            }
            if(PG[1, 1] != gol && PG[0, 2] == PG[1, 1] && PG[1, 1] == PG[2, 0])
            {
                if (PG[1, 1] == player)
                {
                    return -100;
                }
                else
                {
                    return 100;
                }
            }
            return 0;
        }

        int minimax(char[,] PG, int depth, bool isMax)
        {
            int score = Evaluate(PG);
            if(score == -100 || score == 100)
            {
                return score;
            }

            if(NoMovesLeft(PG))
            {
                return 0;
            }

            if(isMax)
            {
                int maxval = -9999;

                for(int i = 0; i < 3; i++)
                {
                    for(int j = 0; j < 3; j++)
                    {
                        if(PG[i, j] == gol)
                        {
                            PG[i, j] = ai;

                            int rez = minimax(PG, depth + 1, !isMax);

                            if(rez > maxval)
                            {
                                maxval = rez;
                            }

                            PG[i, j] = gol;
                        }
                    }
                }

                return maxval - depth;
            }
            else
            {
                int minval = 9999;

                for(int i = 0; i < 3; i++)
                {
                    for(int j = 0; j < 3; j++)
                    {
                        if(PG[i, j] == gol)
                        {
                            PG[i, j] = player;

                            int rez = minimax(PG, depth + 1, !isMax);

                            if (rez < minval)
                            {
                                minval = rez;
                            }

                            PG[i, j] = gol;
                        }
                    }
                }

                return minval + depth;
            }
        }

        private Move GetBestMove(char[,] PG)
        {
            int best = -99999;
            Move BestMove = new Move(-1, -1);

            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    if(PG[i, j] == gol)
                    {
                        PG[i, j] = ai;

                        int moveval = minimax(PG, 0, false);

                        PG[i, j] = gol;

                        if(moveval > best)
                        {
                            best = moveval;
                            BestMove.x = i;
                            BestMove.y = j;
                        }
                    }
                }
            }

            return BestMove;
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
                int rez = Evaluate(Playground);
                if(rez == -100)
                {
                    MessageBox.Show(player + " a castigat");
                    Dispose();
                    Close();
                    return;
                }
                if(NoMovesLeft(Playground))
                {
                    MessageBox.Show("REMIZA!");
                    Dispose();
                    Close();
                    return;
                }

                Move best = GetBestMove(Playground);
                if (best.x != -1 && best.y != -1)
                {
                    Playground[best.x, best.y] = ai;
                }
                rez = Evaluate(Playground);
                if (rez == -100)
                {
                    MessageBox.Show(player + " a castigat");
                    Dispose();
                    Close();
                    return;
                }
                else if (NoMovesLeft(Playground))
                {
                    MessageBox.Show("REMIZA!");
                    Dispose();
                    Close();
                    return;
                }
                else if (rez == 100)
                {
                    MessageBox.Show(ai + " a castigat");
                    Dispose();
                    Close();
                    return;
                }
            }
        }

        private void PVAIGame_Load(object sender, EventArgs e)
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
            UpdateTimer.Start();

            Random RNG = new Random();

            if(!b)
            {
                Playground[RNG.Next(0, 3), RNG.Next(0, 3)] = 'X';
            }
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            Canvas.Invalidate();
        }
    }
}
