using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using icSzERo.Properties;
using System.Net.WebSockets;
using System.Net.Http;

namespace icSzERo
{
    public partial class MainFrm : Form
    {
        public MainFrm()
        {
            InitializeComponent();
        }

        /**
         * Server should:
         * > HTTP GET id
         * > UPGRADE TO WebSocket
         * > SEND MESSAGE WITH THE FOLLOWING SYNTAX: "{id}|{name}|{id}|{name}" FOR MATCHING PLAYERS 
         * > ACCEPT MESSAGE WITH THE FOLLOWING SYNTAX: "move {id},{x},{y}" AND BROADCAST IT
         */

        /**
         * Client should:
         * HTTP GET id
         * UPGRADE TO WebSocket
         * MESSAGE: "iam {id}"
         * MESSAGE: "match {id}"
         * WAIT FOR A MATCHING PLAYER(MESSAGE "{id}|{name}|{id}|{name}")
         * START GAME
         * BROADCAST MOVES
         * ACCEPT MOVES: "{id},{x},{y}"
         * END GAME WITH MESSAGE: "end {id}"
         */

        Status conStatus;

        public string user;
        private int id, idop;
        private ClientWebSocket cws;
        private bool isX;

        private void Form1_Load(object sender, EventArgs e)
        {
            PVPButton.Enabled = false;
            PVAIButton.Enabled = false;
        }

        private void QuitButton_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            PVPButton.Show();
            PVPButton.Enabled = true;
            PVAIButton.Show();
            PVAIButton.Enabled = true;
            PVPOnlineBtn.Show();
            PVPOnlineBtn.Enabled = true;
        }

        private void PVPButton_Click(object sender, EventArgs e)
        {
            PVPGame GameWindow = new PVPGame();
            GameWindow.Show();
        }

        private void PVAIButton_Click(object sender, EventArgs e)
        {
            PVAIGame GameWindow = new PVAIGame();
            GameWindow.Show();
        }

        private int nrcif(int x)
        {
            int rez = 0;
            do
            {
                rez++;
                x /= 10;
            } while (x > 0);
            return rez;
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

        private async void Connect()
        {
            try
            {
                // <---- HTTP GET id ----
                try
                {
                    conStatus = Status.CONNECTING;
                    HttpClient httpc = new HttpClient();
                    string postcontent = "{\"user\": \"" + user + "\", \"pass\": \"" + Resources.HTTPPass + "\" }";
                    HttpResponseMessage response = await httpc.PostAsync(Resources.HTTPString,
                        new StringContent(postcontent, Encoding.UTF8, "application/json"));
                    if (!response.IsSuccessStatusCode)
                    {
                        conStatus = Status.ERROR;
                        return;
                    }
                    string s = await response.Content.ReadAsStringAsync();
                    id = int.Parse(s);
                } catch(HttpRequestException)
                {
                    MessageBox.Show("Error while connecting");
                    conStatus = Status.ERROR;
                    return;
                }
                
                // ---- HTTP GET id ----/>

                // <---- UPGRADE TO WebSocket ----
                conStatus = Status.UPGRADING;
                cws = new ClientWebSocket();
                await cws.ConnectAsync(new Uri(Resources.WSString), CancellationToken.None);
                // ---- UPGRADE TO WebSocket ----/>

                await SendMessage(cws, "iam " + id.ToString());

                // <---- MESSAGE: "match {id}" ----
                await SendMessage(cws, "match " + id.ToString());
                // ---- MESSAGE: "match {id}" ----/>

                // <---- WAIT FOR MATCHING PLAYER ----
                conStatus = Status.MATCHING;
                bool searching = true;
                idop = 0;
                isX = false;
                while(searching)
                {
                    ArraySegment<byte> receivedMessage = new ArraySegment<byte>(new byte[64]);
                    WebSocketReceiveResult res = await cws.ReceiveAsync(receivedMessage, CancellationToken.None);
                    string message = "";
                    for (int i = 0; i < res.Count; i++)
                    {
                        message += (char)receivedMessage.Array[i];
                    }
                    char[] sep = { '|' };
                    string[] splitted = message.Split(sep);
                    if(splitted.Length == 4)
                    {
                        int id1 = int.Parse(splitted[0]);
                        string user1 = splitted[1];
                        int id2 = int.Parse(splitted[2]);
                        string user2 = splitted[3];
                        if(id1 == id)
                        {
                            searching = false;
                            idop = id2;
                            isX = true;
                        }
                        else if(id2 == id)
                        {
                            searching = false;
                            idop = id1;
                            isX = false;
                        }
                    }
                }
                // ---- WAIT FOR MATCHING PLAYER ----/>

                conStatus = Status.DONE;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                conStatus = Status.ERROR;
                return;
            }
        }

        public void StartConnecting()
        {
            Thread t = new Thread(new ThreadStart(Connect));
            t.Start();
        }

        private void PVPOnlineBtn_Click(object sender, EventArgs e)
        {
            lblStatus.Visible = true;
            conStatus = Status.CONNECTING;
            UsernameFrm userFrm = new UsernameFrm();
            userFrm.Tag = this;
            Enabled = false;
            userFrm.Show();
        }

        private PVPOnlineGame pvpo = null;
        public static bool matchEnded = false;

        private void StatusUpdater_Tick(object sender, EventArgs e)
        {
            switch(conStatus)
            {
                case Status.CONNECTING:
                    lblStatus.Text = "Pinging server...";
                    break;
                case Status.UPGRADING:
                    lblStatus.Text = "Connecting to server...";
                    break;
                case Status.MATCHING:
                    lblStatus.Text = "Finding another player...";
                    break;
                case Status.DONE:
                    lblStatus.Text = "";
                    if(pvpo == null)
                    {
                        matchEnded = false;
                        pvpo = new PVPOnlineGame
                        {
                            Tag = Tuple.Create(cws, id, idop, isX)
                        };
                        pvpo.Show();
                    }
                    if(matchEnded)
                    {
                        pvpo.Dispose();
                        pvpo = null;
                        Enabled = true;
                    }
                    break;
                case Status.ERROR:
                    lblStatus.Text = "Connection error";
                    Enabled = true;
                    break;
            }
        }
    }
}
