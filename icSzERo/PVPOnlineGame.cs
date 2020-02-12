using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace icSzERo
{
    public partial class PVPOnlineGame : Form
    {
        public PVPOnlineGame()
        {
            InitializeComponent();
            Size = new Size(TileSize * 3, TileSize * 3);
            Canvas = new PictureBox()
            {
                Location = new Point(0, 0),
                Size = new Size(TileSize * 3, TileSize * 3)
            };
            Controls.Add(Canvas);
            Canvas.Paint += new PaintEventHandler(Update);
            Canvas.MouseClick += new MouseEventHandler(UserClick);
            System.Timers.Timer UpdateTimer = new System.Timers.Timer();
            UpdateTimer.Elapsed += new System.Timers.ElapsedEventHandler(UpdateTimer_Tick);
            UpdateTimer.Interval = 10;
            UpdateTimer.Enabled = true;
        }

        PictureBox Canvas;

        char[,] Playground = new char[3, 3];
        ClientWebSocket cws;

        private bool playerturn = true;
        private bool isX = true;

        private const int TileSize = 200;

        private const int Size0 = 80 * TileSize / 100;
        private const int SizeX = Size0 / 2;
        private int id, idop;

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

            e.Graphics.DrawString("Tu esti " + (isX ? "X" : "0"), DefaultFont, Brushes.Red, 0, 0);
        }

        private async void ShowWinMessage(bool winX)
        {
            if (!winX)
            {
                MessageBox.Show("Castiga 0!");
            }
            else
            {
                MessageBox.Show("Castiga X!");
            }
            await SendMessage(cws, "end " + id.ToString() + " " + idop.ToString());
            MainFrm.matchEnded = true;
            Dispose();
        }

        private void CheckWin()
        {
            for (int i = 0; i < 3; i++)
            {
                if (Playground[i, 0] > 0 && Playground[i, 0] == Playground[i, 1] && Playground[i, 1] == Playground[i, 2])
                {
                    ShowWinMessage(Playground[i, 0] == 'X');
                }
                if (Playground[0, i] > 0 && Playground[0, i] == Playground[1, i] && Playground[1, i] == Playground[2, i])
                {
                    ShowWinMessage(Playground[0, i] == 'X');
                }
            }
            if (Playground[0, 0] > 0 && Playground[0, 0] == Playground[1, 1] && Playground[1, 1] == Playground[2, 2])
            {
                ShowWinMessage(Playground[0, 0] == 'X');
            }
            if (Playground[1, 1] > 0 && Playground[0, 2] == Playground[1, 1] && Playground[1, 1] == Playground[2, 0])
            {
                ShowWinMessage(Playground[1, 1] == 'X');
            }
        }

        private async Task SendMessage(ClientWebSocket cws, string toSendMsg)
        {
            byte[] rawBuffer = new byte[toSendMsg.Length];
            for (int i = 0; i < toSendMsg.Length; i++)
            {
                rawBuffer[i] = (byte)toSendMsg[i];
            }
            ArraySegment<byte> toSend = new ArraySegment<byte>(rawBuffer);
            await cws.SendAsync(toSend, WebSocketMessageType.Text, true, CancellationToken.None);
        }

        private async void BroadcastMove(int x, int y)
        {
            await SendMessage(cws, "move " + id.ToString() + "," + x.ToString() + "," + y.ToString());
            WaitForMove();
        }

        private async void WaitForMove()
        {
            bool waiting = true;
            while(waiting)
            {
                ArraySegment<byte> message = new ArraySegment<byte>(new byte[64]);
                WebSocketReceiveResult res = await cws.ReceiveAsync(message, CancellationToken.None);
                string msg = "";
                for(int i = 0; i < res.Count; i++)
                {
                    msg += (char)message.Array[i];
                }
                char[] sep = { ',' };
                string[] splitted = msg.Split(sep);
                int idm = int.Parse(splitted[0]), 
                    xm = int.Parse(splitted[1]), 
                    ym = int.Parse(splitted[2]);
                if(idm == idop)
                {
                    if(isX)
                    {
                        Playground[xm, ym] = '0';
                        CheckWin();
                        playerturn = true;
                        waiting = false;
                    }
                    else
                    {
                        Playground[xm, ym] = 'X';
                        CheckWin();
                        playerturn = true;
                        waiting = false;
                    }
                }
            }
        }

        private void UserClick(object sender, MouseEventArgs e)
        {
            int x = e.Location.X / TileSize;
            int y = e.Location.Y / TileSize;
            if (Playground[x, y] != 'X' && Playground[x, y] != '0')
            {
                if (playerturn)
                {
                    Playground[x, y] = isX ? 'X' : '0';
                    BroadcastMove(x, y);
                    playerturn = false;
                    CheckWin();
                }
            }
        }

        private void PVPGame_Load(object sender, EventArgs e)
        {
            cws = (Tag as Tuple<ClientWebSocket, int, int, bool>).Item1;
            id = (Tag as Tuple<ClientWebSocket, int, int, bool>).Item2;
            idop = (Tag as Tuple<ClientWebSocket, int, int, bool>).Item3;
            isX = (Tag as Tuple<ClientWebSocket, int, int, bool>).Item4;
            if(!isX)
            {
                playerturn = false;
                WaitForMove();
            }
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            Canvas.Invalidate();
        }
    }
}
