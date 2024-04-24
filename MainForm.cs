using ReaLTaiizor.Controls;
using SuperSimpleTcp;
using System;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.DataFormats;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Caro_Nhom8
{

    public partial class MainForm : Form
    {
        #region Properties
        SimpleTcpClient? client;
        SimpleTcpServer? server;
        //private string name = "";
        //private string IP = "127.0.0.1";
        private Graphics grs;
        public static int cdStep = 100;
        public static int cdTime = 15000;
        public static int cdInterval = 100;
        private CaroGame caroChess;
        public bool isServer;
        public string currentClient = "";
        #endregion

        #region ScreenChange_Methods

        void OpenPlayArea()
        {
            this.Size = new Size(1207, 945);
            grb_Login.Visible = false;
            grb_Info.Visible = false;
            grb_Waiting.Visible = false;
            grb_ClientInfo.Visible = false;
            panel_PlayArea.Dock = DockStyle.Fill;
            panel_PlayArea.Visible = true;
        }

        #endregion

        #region LoginForm_Btn_Click_Methods










        private void btn_Undo_Click(object sender, EventArgs e)
        {
            caroChess.Undo(grs);
            tmCoolDown.Start();
            prcbCoolDown.Value = 0;
        }

        private void btn_Redo_Click(object sender, EventArgs e)
        {
            caroChess.Redo(grs);
            tmCoolDown.Start();
            prcbCoolDown.Value = 0;
        }
        #endregion

        #region Server

        private void Server_Events_DataReceived(object? sender, DataReceivedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                string data = Encoding.UTF8.GetString(e.Data);

                // Kiểm tra nếu đó là yêu cầu gửi tên của client
                if (data.StartsWith("/msg "))
                {
                    string msg = data.Substring(5);
                    ChatBubbleLeft chatBubbleLeft = new ChatBubbleLeft();
                    chatBubbleLeft.BubbleColor = Color.FromArgb(238, 102, 119);
                    chatBubbleLeft.ForeColor = Color.White;
                    chatBubbleLeft.SizeAutoW = false;
                    chatBubbleLeft.Width = 195;
                    chatBubbleLeft.Text = msg;
                    fpanel_Chat.Controls.Add(chatBubbleLeft);

                }
                else if (data.StartsWith("/pnt "))
                {
                    string point = data.Substring(5);
                    string[] coordinates = point.Split(',');
                    int x = int.Parse(coordinates[0]); // Lấy giá trị tọa độ x từ mảng
                    int y = int.Parse(coordinates[1]); // Lấy giá trị tọa độ y từ mảng
                    Point point2 = new Point(x, y);
                    prcbCoolDown.Value = 0;
                    tmCoolDown.Start();
                    OtherPlayerMark(point2);
                }

            });
        }

        private void Server_Events_DataSent(object? sender, DataSentEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {

            });
        }

        private void Server_Events_ClientDisconnected(object? sender, ConnectionEventArgs e)
        {

        }

        private void Server_Events_ClientConnected(object? sender, ConnectionEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                OpenPlayArea();
                currentClient = e.IpPort;
                caroChess.StartLAN(grs);

            });
        }



        #endregion

        #region Client
        private void Client_Events_DataReceived(object? sender, DataReceivedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                string data = Encoding.UTF8.GetString(e.Data);

                // Kiểm tra nếu đó là yêu cầu gửi tên của client
                if (data.StartsWith("/msg "))
                {
                    string msg = data.Substring(5); // Lấy phần tên sau chuỗi "NAME: "
                    ChatBubbleLeft chatBubbleLeft = new ChatBubbleLeft();
                    chatBubbleLeft.BubbleColor = Color.FromArgb(238, 102, 119);
                    chatBubbleLeft.ForeColor = Color.White;
                    chatBubbleLeft.SizeAutoW = false;
                    chatBubbleLeft.Width = 195;
                    chatBubbleLeft.Text = msg;
                    fpanel_Chat.Controls.Add(chatBubbleLeft);

                }
                else if (data.StartsWith("/pnt "))
                {
                    string point = data.Substring(5);
                    string[] coordinates = point.Split(',');
                    int x = int.Parse(coordinates[0]); // Lấy giá trị tọa độ x từ mảng
                    int y = int.Parse(coordinates[1]); // Lấy giá trị tọa độ y từ mảng
                    Point point2 = new Point(x, y);
                    prcbCoolDown.Value = 0;
                    tmCoolDown.Start();
                    OtherPlayerMark(point2);
                }

            });
        }

        private void Client_Events_DataSent(object? sender, DataSentEventArgs e)
        {
        }

        private void Client_Events_Disconnected(object? sender, ConnectionEventArgs e)
        {
        }

        private void Client_Events_Connected(object? sender, ConnectionEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                OpenPlayArea();
                caroChess.StartLAN(grs);
            });
        }
        #endregion

        #region FormMethod
        public MainForm()
        {
            InitializeComponent();
            Board board = new Board(16, 16);
            caroChess = new CaroGame(board);
            caroChess.CreateChessPieces();
            grs = fpanel_Board.CreateGraphics();
            prcbCoolDown.Maximum = 15000;
            prcbCoolDown.Value = 0;
            tmCoolDown.Interval = cdInterval;

        }
        private void fpanel_Board_Paint(object sender, PaintEventArgs e)
        {
            caroChess!.DrawChessBoard(grs!);
            caroChess.RepaintChess(grs!);
        }


        private void LoginForm_Load(object sender, EventArgs e)
        {


            OpenLogin();
            dtg_Ranking.Rows.Add(
                new object[]
                {
                    22520922,
                    100,
                    0,
                    "100%"

                });
            dtg_Ranking.Rows.Add(
                new object[]
                {
                    22520922,
                    100,
                    0,
                    "100%"

                });
            dtg_Ranking.Rows.Add(
                new object[]
                {
                    22520922,
                    100,
                    0,
                    "100%"

                });
            dtg_Ranking.Rows.Add(
                new object[]
                {
                    22520922,
                    100,
                    0,
                    "100%"

                });
            dtg_Ranking.Rows.Add(
                new object[]
                {
                    22520922,
                    100,
                    0,
                    "99%"

                });
            dtg_Ranking.Rows.Add(
                new object[]
                {
                    22520922,
                    100,
                    0,
                    "99%"

                });
            dtg_Ranking.Rows.Add(
                new object[]
                {
                    22520922,
                    100,
                    0,
                    "99%"

                });
            dtg_Ranking.Rows.Add(
                new object[]
                {
                    22520922,
                    100,
                    0,
                    "99%"

                });
        }
        #endregion


        #region Chat

        private void txt_Msg_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_Msg.Text))
            {
                txt_Msg.Text = "Nhập tin nhắn ...";
                txt_Msg.ForeColor = Color.Gray;
            }
        }

        private void txt_Msg_Enter(object sender, EventArgs e)
        {
            if (txt_Msg.Text == "Nhập tin nhắn ...")
            {
                txt_Msg.Text = "";
                txt_Msg.ForeColor = Color.White;
            }
        }

        private void txt_Msg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(txt_Msg.Text))
            {
                string msg = "/msg " + txt_Msg.Text;
                if (isServer)
                {
                    server!.SendAsync(currentClient, msg);
                }
                else
                {
                    client!.SendAsync(msg);
                }
                ChatBubbleRight chatBubbleRight = new ChatBubbleRight();
                chatBubbleRight.BubbleColor = Color.FromArgb(59, 198, 171);
                chatBubbleRight.ForeColor = Color.White;
                chatBubbleRight.Text = txt_Msg.Text;
                chatBubbleRight.SizeAutoW = false;
                chatBubbleRight.Width = 195;
                fpanel_Chat.Controls.Add(chatBubbleRight);
            }
        }
        #endregion


        private void fpanel_Board_MouseClick(object sender, MouseEventArgs e)
        {
            if (!caroChess.Ready)
                return;
            if (caroChess.PlayChess(e.X, e.Y, grs))
            {
                if (caroChess.Mode == 1)
                {
                    if (caroChess.CheckWin())
                    {
                        tmCoolDown.Stop();
                        caroChess.EndGame();
                        return;
                    }
                }
                else if (caroChess.Mode == 2)
                {
                    if (caroChess.CheckWin())
                    {
                        tmCoolDown.Stop();
                        caroChess.EndGame();
                        return;
                    }
                    else
                    {
                        caroChess.LaunchComputer(grs);
                        if (caroChess.CheckWin())
                        {
                            tmCoolDown.Stop();
                            caroChess.EndGame();
                            return;
                        }
                    }
                }
                else if (caroChess.Mode == 3)
                {
                    fpanel_Board.Enabled = false;

                    if (isServer)
                    {
                        server!.SendAsync(currentClient, "/pnt " + e.Location.X.ToString() + "," + e.Location.Y.ToString());
                    }
                    else
                    {
                        client!.SendAsync("/pnt " + e.Location.X.ToString() + "," + e.Location.Y.ToString());
                    }
                    if (caroChess.CheckWin())
                    {
                        tmCoolDown.Stop();
                        caroChess.EndGame();
                        return;
                    }
                }
                tmCoolDown.Start();
                prcbCoolDown.Value = 0;
            }
        }
        public void OtherPlayerMark(Point point)
        {
            if (!caroChess.Ready)
                return;
            if (caroChess.PlayChess(point.X, point.Y, grs))
            {
                fpanel_Board.Enabled = true;
                if (caroChess.CheckWin())
                {
                    tmCoolDown.Stop();
                    caroChess.EndGame();
                }
            }
        }

        private void tmCoolDown_Tick(object sender, EventArgs e)
        {
            prcbCoolDown.Value = prcbCoolDown.Value + 100;

            if (prcbCoolDown.Value >= prcbCoolDown.Maximum)
            {
                tmCoolDown.Stop();
                caroChess!.EndGame();

            }


        }

        
    }


}