using ReaLTaiizor.Controls;
using SuperSimpleTcp;
using System.Text;

namespace Caro_Nhom8
{
    //Client kết nối với server
    public partial class MainForm
    {
        #region ClientInfo
        void OpenConnect()
        {
            this.Size = new Size(755, 658);
            this.MaximumSize = new Size(755, 658);
            this.MinimumSize = new Size(755, 658);
            grb_ClientInfo.Dock = DockStyle.Fill;
            lb_ClientInfo_Notify.Visible = false;
            grb_Login.Visible = false;
            grb_SignUp.Visible = false;
            grb_Info.Visible = false;
            grb_Waiting.Visible = false;
            grb_ClientInfo.Visible = true;
            grb_ComputerInfo.Visible = false;
            grb_ServerInfo.Visible = false;
            grb_ForgetPassword.Visible = false;
            grb_ChangeInfo.Visible = false;
            grb_Setting.Visible = false;
            grb_ChooseAvatar.Visible = false;
            panel_PlayArea.Dock = DockStyle.None;
            panel_PlayArea.Visible = false;
            lb_ClientInfo_Notify.Visible = false;
            txt_ClientInfo_ServerCode.TextButton = "";
        }
        private async void btn_Connect_Click(object sender, EventArgs e)
        {
            playSFX();
            lb_ClientInfo_Notify.ForeColor = Color.White;
            lb_ClientInfo_Notify.Visible = true;
            lb_ClientInfo_Notify.Text = "*Đang xử lí";
            latestgameid = await GetLatestGameID();
            latestchatid = await GetLatestChatID();
            try
            {
                string info = DecodeServerInfo(txt_ClientInfo_ServerCode.TextButton);
                string[] parts = info.Split(';');
                string ip = parts[0];
                int port = int.Parse(parts[1]);
                string boardSize = parts[2];
                string turnTime = parts[3];
                string chess = parts[4];
                string first = parts[5];
                client = new SimpleTcpClient(ip, port);
                client.Events.Connected += Client_Events_Connected;
                client.Events.Disconnected += Client_Events_Disconnected;
                client.Events.DataSent += Client_Events_DataSent;
                client.Events.DataReceived += Client_Events_DataReceived;
                client.Connect();
                isServer = false;
                switch (boardSize)
                {
                    case "10x10":
                        board = new Board(10, 10);
                        game.Size = "10x10";
                        break;
                    case "16x16":
                        board = new Board(16, 16);
                        game.Size = "16x16";
                        break;
                    case "20x20":
                        board = new Board(20, 20);
                        game.Size = "20x20";
                        break;
                }
                switch (turnTime)
                {
                    case "10 giây":
                        prcbCoolDown.Maximum = 10000;
                        break;
                    case "16 giây":
                        prcbCoolDown.Maximum = 16000;
                        break;
                    case "20 giây":
                        prcbCoolDown.Maximum = 20000;
                        break;
                }
                caroChess = new CaroGame(board);
                grs = panel_PlayArea_Board.CreateGraphics();
                grs.Clear(panel_PlayArea_Board.BackColor);
                caroChess.StartLAN(grs);
                switch (first)
                {
                    case "Bạn đánh trước":
                        switch (chess)
                        {
                            case "x":
                                picbox_PlayArea_Chess1.Image = ImageOgreen;
                                picbox_PlayArea_Chess2.Image = ImageXred; 
                                firstchess = ImageXred;
                                secondchess = ImageOgreen;
                                break;
                            case "o":
                                picbox_PlayArea_Chess1.Image = ImageXgreen;
                                picbox_PlayArea_Chess2.Image = ImageOred;
                                firstchess = ImageOred;
                                secondchess = ImageXgreen;
                                break;
                            default:
                                break;
                        }
                        isYouFirst = false;
                        panel_PlayArea_Board.Enabled = false;
                        picbox_PlayArea_Avatar1.BorderSize = 0;
                        picbox_PlayArea_Avatar2.BorderSize = 5;
                        break;
                    case "Địch đánh trước":
                        switch (chess)
                        {
                            case "x":
                                picbox_PlayArea_Chess1.Image = ImageOgreen;
                                picbox_PlayArea_Chess2.Image = ImageXred;
                                firstchess = ImageOgreen;
                                secondchess = ImageXred;
                                break;
                            case "o":
                                picbox_PlayArea_Chess1.Image = ImageXgreen;
                                picbox_PlayArea_Chess2.Image = ImageOred;
                                firstchess = ImageXgreen;
                                secondchess = ImageOred;
                                break;
                            default:
                                break;
                        }
                        isYouFirst = true;
                        panel_PlayArea_Board.Enabled = true;
                        picbox_PlayArea_Avatar1.BorderSize = 5;
                        picbox_PlayArea_Avatar2.BorderSize = 0;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                lb_ClientInfo_Notify.ForeColor = Color.FromArgb(245, 108, 108);
                lb_ClientInfo_Notify.Visible = true;
                lb_ClientInfo_Notify.Text = "*Thông báo: " + ex.Message;
            }
            

        }
        private void btn_CancelJoinRoom_Click(object sender, EventArgs e)
        {
            playSFX();
            OpenPlayerInfo();
        }
        private void Client_Events_DataReceived(object? sender, DataReceivedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                string data = Encoding.UTF8.GetString(e.Data);
                
                if (data.StartsWith("/msg "))
                {
                    string msg = data.Substring(5);
                    ChatBubbleLeft chatBubbleLeft = new ChatBubbleLeft();
                    chatBubbleLeft.BubbleColor = Color.FromArgb(238, 102, 119);
                    chatBubbleLeft.ForeColor = Color.White;
                    chatBubbleLeft.SizeAutoW = false;
                    chatBubbleLeft.Width = 195;
                    chatBubbleLeft.Text = msg;
                    chats.Add(new Chat("", "", msg, currenopponentID, currentplayer.ID!));
                    panel_PlayArea_ChatArea.Controls.Add(chatBubbleLeft);

                }
                else if (data.StartsWith("/pnt "))
                {
                    string point = data.Substring(5);
                    string[] coordinates = point.Split(',');
                    int x = int.Parse(coordinates[0]);
                    int y = int.Parse(coordinates[1]);
                    Point point2 = new Point(x, y);
                    prcbCoolDown.Value = 0;
                    tmCoolDown.Start();
                    OtherPlayerMark(point2);
                }
                else if (data.StartsWith("/nme "))
                {
                    string[] parts = data.Split('\n');
                    foreach (string part in parts)
                    {
                        if (part.StartsWith("/nme "))
                        {
                            lb_PlayArea_Name2.Text = part.Substring(5);
                            
                        }
                        else if (part.StartsWith("/ava "))
                        {
                            picbox_PlayArea_Avatar2.Image = Image.FromFile(part.Substring(5));
                        }
                        else if (part.StartsWith("/id "))
                        {
                            currenopponentID = part.Substring(4);
                        }
                    }
                }
                else if (data.StartsWith("/ff "))
                {
                    tmCoolDown.Stop();
                    NotifyForm nf = new NotifyForm("Đầu hàng_2");
                    DialogResult dnf = nf.ShowDialog();
                    if (isYouFirst)
                    {
                        DoEndGame(1);
                    }
                    else
                    {
                        DoEndGame(2);
                    }
                }
                else if (data.StartsWith("/reconcile_request "))
                {
                    NotifyForm nf = new NotifyForm("Hòa giải_2");
                    DialogResult dnf = nf.ShowDialog();
                    if (dnf == DialogResult.Yes)
                    {
                        Send("/reconcile_accept ");
                        DoEndGame(22);
                    }
                }
                else if (data.StartsWith("/reconcile_accept "))
                {
                    tmCoolDown.Stop();
                    NotifyForm nf = new NotifyForm("Hòa giải_3");
                    DialogResult dnf = nf.ShowDialog();
                    DoEndGame(22);

                }
                else if (data.StartsWith("/newgame_request "))
                {
                    NotifyForm nf = new NotifyForm("Đấu lại_2");
                    DialogResult dnf = nf.ShowDialog();
                    if (dnf == DialogResult.Yes)
                    {
                        Send("/newgame_accept ");
                        DoNewGame_PVP();
                    }
                }
                else if (data.StartsWith("/newgame_accept "))
                {
                    DoNewGame_PVP();
                }
                else if (data.StartsWith("/exit "))
                {
                    tmCoolDown.Stop();
                    isConnected = false;
                    client!.DisconnectAsync();
                    NotifyForm nf = new NotifyForm("Thoát_2");
                    nf.ShowDialog();
                    if (!isGameEnd)
                    {
                        if (isYouFirst)
                        {
                            DoEndGame(1);
                        }
                        else
                        {
                            DoEndGame(2);
                        }
                    }
                }
                
            });
        }

        private void Client_Events_DataSent(object? sender, DataSentEventArgs e)
        {
        }

        private void Client_Events_Disconnected(object? sender, ConnectionEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                
            });
        }

        private void Client_Events_Connected(object? sender, ConnectionEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                DateTime now = DateTime.Now;
                game.StartTime = now.ToString();
                string info = $"/nme {currentplayer.Name}\n/ava {currentplayer.Avatar}\n/id {currentplayer.ID}";
                client!.SendAsync(info);
                lb_PlayArea_Name1.Text = currentplayer.Name;
                picbox_PlayArea_Avatar1.Image = Image.FromFile(currentplayer.Avatar!);
                OpenPlayArea();
                isConnected = true;
                isGameEnd = false;
                isPlayAreaOpen = true;
                grb_PVP_Tools.Location = new Point(14, 310);
                grb_PVP_Tools.Visible = true;
                grb_PVC_Tools.Visible = false;
                prcbCoolDown.Value = 0;
                tmCoolDown.Start();
            });
        }
        public string DecodeServerInfo(string encodedInfo)
        {
            byte[] infoBytes = Convert.FromBase64String(encodedInfo);
            string info = Encoding.UTF8.GetString(infoBytes);
            return info;
           
            
        }
        #endregion
    }
}
