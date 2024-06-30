using Firebase.Database.Query;
using ReaLTaiizor.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.CompilerServices;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Caro_Nhom8
{
    public partial class MainForm
    {
        #region AvatarMethod
        private void LoadAvatars()
        {
            string[] allAvatarFiles = Directory.GetFiles(Path.Combine(Application.StartupPath, "Resources/UI_Icon"), "*.png");

            // Lọc danh sách tệp theo định dạng mong muốn
            List<string> avatarFiles = new List<string>();
            foreach (string avatarFile in allAvatarFiles)
            {
                string fileName = Path.GetFileName(avatarFile);
                avatarFiles.Add("Resources/UI_Icon/" + fileName);
            }
            foreach (string avatarFile in avatarFiles)
            {
                RJCircularPictureBox pictureBox = new RJCircularPictureBox();
                pictureBox.Image = System.Drawing.Image.FromFile(avatarFile);
                pictureBox.Width = 50;
                pictureBox.Height = 50;
                pictureBox.BorderColor = Color.FromArgb(59, 198, 171);
                pictureBox.BorderColor2 = Color.FromArgb(59, 198, 171);
                pictureBox.BorderSize = 1;
                pictureBox.Tag = avatarFile.ToString();
                pictureBox.Margin = new Padding(5);
                pictureBox.Click += PictureBox_Click;

                panel_ChooseAvatar.Controls.Add(pictureBox);
            }
        }
        private void PictureBox_Click(object? sender, EventArgs e)
        {
            playSFX();
            RJCircularPictureBox pictureBox = (RJCircularPictureBox)sender!;
            pictureBox.Cursor = Cursors.Hand;
            if (isChooseAvatarSignUp)
            {
                picbox_SignUp_Avatar.Image = Image.FromFile(pictureBox.Tag.ToString()!);
                currentAvatar = pictureBox.Tag.ToString()!;
                OpenSignUp();

            }
            else
            {
                picbox_ChangeInfo_Avatar.Image = Image.FromFile(pictureBox.Tag.ToString()!);
                currentAvatar = pictureBox.Tag.ToString()!;
                OpenChangePassword();
            }
        }
        #endregion

        #region LoginMethod
        void RenewLogin()
        {
            txt_Login_ID.TextButton = currentplayer.ID;
            txt_Login_PW.TextButton = "";
        }
        #endregion

        #region ForgetPassowrdMethod
        void RenewForgetPassword()
        {
            txt_ForgetPW_ID.TextButton = "";
            txt_ForgetPW_NewPW.TextButton = "";
            txt_ForgetPW_UserEmail.TextButton = "";
            txt_FortgetPW_Code.TextButton = "";
        }
        #endregion

        #region SignUpMethod
        void RenewSignUp()
        {
            txt_SignUp_ID.TextButton = "";
            txt_SignUp_Name.TextButton = "";
            txt_SignUp_Email.TextButton = "";
            txt_SignUp_PW.TextButton = "";
            txt_SignUp_ProtectionCode.TextButton = "";
            txt_Signup_Verifycode.TextButton = "";
            verifycode = "none";
            picbox_SignUp_Avatar.Image = Image.FromFile("Resources/UI_Icon/Default.png");
            currentAvatar = "Resources/UI_Icon/Default.png";
        }
        public static bool ValidateEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            bool isMatch = Regex.IsMatch(email, pattern);

            return isMatch;
        }
        public static bool IsValidID(string id)
        {
            string pattern = @"^[a-zA-Z0-9!@#$%^&*()_+{}\[\]:;<>,.?~\\|]+$";
            bool isMatch = Regex.IsMatch(id, pattern);

            return isMatch;
        }
        public static bool ValidateName(string name)
        {
            string pattern = @"^[\p{L}\s]+$";
            bool isMatch = Regex.IsMatch(name, pattern) && !Regex.IsMatch(name, @"[!@#$%^&*()_+{}\[\]:;<>,.?~\\|0-9]");
            return isMatch;
        }
        public static bool ValidatePassword(string password)
        {
            string pattern = @"^[a-zA-Z0-9!@#$%^&*()_+{}\[\]:;<>,.?~\\|]+$";
            bool isMatch = Regex.IsMatch(password, pattern);
            return isMatch;
        }
        public static bool ValidateProtectionCode(string password)
        {
            string pattern = @"^[0-9]+$";
            bool isMatch = Regex.IsMatch(password, pattern);
            return isMatch;
        }
        
        private async Task<bool> GetVerifyCodeAsync(string email, string verificationCode, string option)
        {

            string code = verificationCode;
            string htmlTemplate = "";
            if (option == "register")
            {
                htmlTemplate = File.ReadAllText("Templates/Register.html");
            }
            else if(option == "forgetpw")
            {
                htmlTemplate = File.ReadAllText("Templates/ForgetPW.html");
            }    
            string emailBody = htmlTemplate.Replace("[[CODE]]", code);
            string to = email;
            string from = "carogroup8@gmail.com";
            string password = "gmet uyro ltev vqhe";

            using (MailMessage message = new MailMessage())
            {
                message.To.Add(to);
                message.From = new MailAddress(from);
                message.Subject = "Verification code - Caro Group8";
                message.Body = emailBody;
                message.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Credentials = new NetworkCredential(from, password);

                    try
                    {
                        await smtp.SendMailAsync(message);
                        return true;
                    }
                    catch (SmtpException smtpEx)
                    {
                        lb_SignUp_Notify.ForeColor = Color.FromArgb(245, 108, 108);
                        lb_SignUp_Notify.Text = "*Thông báo: " + smtpEx.Message;
                        return false;
                    }
                    catch (Exception ex)
                    {
                        lb_SignUp_Notify.ForeColor = Color.FromArgb(245, 108, 108);
                        lb_SignUp_Notify.Text = "*Thông báo: " + ex.Message;
                        return false;
                    }
                }
            }
        }
        public string GenerateVerificationCode(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            StringBuilder result = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                result.Append(chars[random.Next(chars.Length)]);
            }

            return result.ToString();
        }
        #endregion

        #region PlayerInfoMethod
        void RenewCurrentPlayer()
        {
            currentplayer = new Player();
        }
        void RenewPlayerInfo()
        {
            picbox_PlayerInfo_Avatar.Image = Image.FromFile(currentplayer.Avatar!);
            lb_Win.ForeColor = Color.FromArgb(59, 198, 171);
            lb_Win.Text = "Win: " + currentplayer!.Win.ToString();
            lb_Lose.ForeColor = Color.FromArgb(245, 108, 108);
            lb_Lose.Text = "Lose: " + currentplayer.Lose.ToString();
            lb_Welcome.Text = currentplayer.Name;
            
        }
        private async void LoadRanking(string id)
        {
            dtg_YourRanking.Rows.Clear();
            dtg_Ranking.Rows.Clear();
            var dataSnapshot = await firebaseClient.Child("Users").OrderByKey().OnceAsync<Player>();
            int i = 0;
            // Sắp xếp theo winrate giảm dần
            var sortedData = dataSnapshot.OrderByDescending(item => item.Object.Winrate);

            foreach (var item in sortedData)
            {
                i++;
                var user = item.Object;
                dtg_Ranking.Rows.Add(
                new object[]
                {
                    i,
                    user.Name!,
                    user.Win,
                    user.Lose,
                    user.Winrate!
                });
                if(user.ID == id)
                {
                    dtg_YourRanking.Rows.Add(
                new object[]
                {
                    i,
                    user.Name!,
                    user.Win,
                    user.Lose,
                    user.Winrate!
                });
                }    
            }
        }
        #endregion

        #region ChangeInfoMethod
        void RenewChangeInfo()
        {
            txt_ChangeInfo_Name.TextButton = currentplayer!.Name;
            txt_ChangeInfo_ID.TextButton = currentplayer.ID;
            txt_ChangeInfo_Email.TextButton = currentplayer.Email;
            picbox_ChangeInfo_Avatar.Image = Image.FromFile(currentplayer.Avatar!);
            txt_ChangeInfo_NewPW.TextButton = "";
            txt_ChangeInfo_ProtectionCode.TextButton = "";
        }
        #endregion
       
        #region SettingMethod
        void LightTheme()
        {
            foreach (Control control in Controls)
            {
                if (control is System.Windows.Forms.GroupBox groupBox)
                {
                    groupBox.BackColor = Color.White;
                    groupBox.ForeColor = Color.Black;
                    foreach (Control controlchild in groupBox.Controls)
                    {
                        if (controlchild is System.Windows.Forms.Button button)
                        {
                            button.BackColor = Color.White;
                        }
                        if (controlchild is Label label)
                        {
                            label.ForeColor = Color.Black;
                        }
                        if (controlchild is PoisonDataGridView dataGridView)
                        {
                            dataGridView.BackgroundColor = Color.White;
                            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
                            dataGridView.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.White;
                            dataGridView.DefaultCellStyle.BackColor = Color.White;
                            dataGridView.DefaultCellStyle.ForeColor = Color.Black;
                            dataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(211, 211, 211);
                            dataGridView.DefaultCellStyle.SelectionForeColor = Color.Black;

                        }
                        if (controlchild is CyberTextBox cybertextbox)
                        {
                            cybertextbox.ColorBackground = Color.White;
                            cybertextbox.ForeColor = Color.Black;
                        }
                        if (controlchild is SkyComboBox comboBox)
                        {
                            comboBox.BGColorA = Color.White;
                            comboBox.BGColorB = Color.White;
                            comboBox.ForeColor = Color.Black;
                            comboBox.ItemHighlightColor = Color.White;
                            comboBox.ListBackColor = Color.White;
                            comboBox.ListForeColor = Color.Black;
                            comboBox.ListSelectedBackColorA = Color.FromArgb(223, 223, 224);
                            comboBox.ListSelectedBackColorB = Color.FromArgb(223, 223, 224);
                        }
                    }
                }
            }
            CaroGame.sbPnl = new SolidBrush(Color.FromArgb(255, 255, 255));
            panel_PlayArea.BackColor = Color.FromArgb(248, 249, 250);
            panel_PlayArea_ChatArea.BackColor = Color.FromArgb(241, 243, 245);
            panel_PLayArea_Tool.BackColor = Color.FromArgb(241, 243, 245);
            panel_PlayArea_MsgArea.BackColor = Color.FromArgb(241, 243, 245);
            panel_PlayArea_Player1.BackColor = Color.FromArgb(241, 243, 245);
            panel_PlayArea_Player2.BackColor = Color.FromArgb(241, 243, 245);
            panel_PlayArea_PlayerInfo.BackColor = Color.FromArgb(241, 243, 245);
            panel.BackColor = Color.FromArgb(12, 20, 29);
            Clock.TimeColor = Color.FromArgb(12, 20, 29);
            txt_Msg.BackColor = Color.FromArgb(241, 243, 245);
            prcbCoolDown.ColorBackground = Color.FromArgb(223, 223, 224);
            lb_PlayArea_Point1.ForeColor = Color.Black;
            lb_PlayArea_Point2.ForeColor = Color.Black;
            lb_PlayArea_Name1.ForeColor = Color.Black;
            lb_PlayArea_Name2.ForeColor = Color.Black;
        }
        void DarkTheme()
        {
            foreach (Control control in Controls)
            {
                if (control is System.Windows.Forms.GroupBox groupBox)
                {
                    groupBox.BackColor = Color.FromArgb(12, 20, 29);
                    groupBox.ForeColor = Color.White;
                    foreach (Control controlchild in groupBox.Controls)
                    {
                        if (controlchild is System.Windows.Forms.Button button)
                        {
                            button.BackColor = Color.FromArgb(12, 20, 29);
                        }
                        if (controlchild is Label label)
                        {
                            label.ForeColor = Color.White;
                        }
                        if (controlchild is PoisonDataGridView dataGridView)
                        {
                            dataGridView.BackgroundColor = Color.FromArgb(12, 20, 29);
                            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(12, 20, 29);
                            dataGridView.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(12, 20, 29);
                            dataGridView.DefaultCellStyle.BackColor = Color.FromArgb(12, 20, 29);
                            dataGridView.DefaultCellStyle.ForeColor = Color.White;
                            dataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(27, 40, 55);
                            dataGridView.DefaultCellStyle.SelectionForeColor = Color.White;

                        }
                        if (controlchild is CyberTextBox cybertextbox)
                        {
                            cybertextbox.ColorBackground = Color.FromArgb(12, 20, 29);
                            cybertextbox.ForeColor = Color.White;
                        }
                        if (controlchild is SkyComboBox comboBox)
                        {
                            comboBox.BGColorA = Color.FromArgb(12, 20, 29);
                            comboBox.BGColorB = Color.FromArgb(12, 20, 29);
                            comboBox.ForeColor = Color.White;
                            comboBox.ItemHighlightColor = Color.FromArgb(12, 20, 29);
                            comboBox.ListBackColor = Color.FromArgb(12, 20, 29);
                            comboBox.ListForeColor = Color.White;
                            comboBox.ListSelectedBackColorA = Color.FromArgb(27, 40, 55);
                            comboBox.ListSelectedBackColorB = Color.FromArgb(27, 40, 55);
                        }

                    }
                }
            }
            CaroGame.sbPnl = new SolidBrush(Color.FromArgb(12, 20, 29));
            panel_PlayArea.BackColor = Color.FromArgb(18, 26, 37);
            panel_PlayArea_ChatArea.BackColor = Color.FromArgb(12, 20, 29);
            panel_PLayArea_Tool.BackColor = Color.FromArgb(12, 20, 29);
            panel_PlayArea_MsgArea.BackColor = Color.FromArgb(12, 20, 29);
            panel_PlayArea_Player1.BackColor = Color.FromArgb(12, 20, 29);
            panel_PlayArea_Player2.BackColor = Color.FromArgb(12, 20, 29);
            panel_PlayArea_PlayerInfo.BackColor = Color.FromArgb(12, 20, 29);
            panel.BackColor = Color.White;
            Clock.TimeColor = Color.White;
            txt_Msg.BackColor = Color.FromArgb(12, 20, 29);
            prcbCoolDown.ColorBackground = Color.FromArgb(37, 52, 68);
            lb_PlayArea_Point1.ForeColor = Color.White;
            lb_PlayArea_Point2.ForeColor = Color.White;
            lb_PlayArea_Name1.ForeColor = Color.White;
            lb_PlayArea_Name2.ForeColor = Color.White;
        }
        public void playSFX()
        {
            if (isSFX)
            {
                sfx.URL = "Resources/Sound/Sfx.wav";
            }
        }
        #endregion

        #region GameMethod
        public bool GameCheckWin()
        {
            if(caroChess.CheckWin() == 0)
            {
                return false;
            }    
            else if(caroChess.CheckWin() == 1)
            {
                DoEndGame(1);
                return true;
            }
            else if(caroChess.CheckWin() == 2)
            {
                DoEndGame(2);
                return true;
            }
            else if(caroChess.CheckWin() == 22)
            {
                DoEndGame(22);
                return true;
            }    
            return false;
        }

        public void DoEndGame(int x)
        {
            NotifyForm nfw = new NotifyForm("Thắng");
            NotifyForm nfl = new NotifyForm("Thua");
            NotifyForm nfd = new NotifyForm("Hòa");
            DialogResult dnf = new DialogResult();
            switch (x)
            {
                case 22:
                    
                    tmCoolDown.Stop();
                    dnf = nfd.ShowDialog();
                    break;
                case 1:
                    if (isYouFirst)
                    {
                        int point = int.Parse(lb_PlayArea_Point1.Text);
                        point++;
                        lb_PlayArea_Point1.Text = point.ToString();
                        tmCoolDown.Stop();
                        dnf = nfw.ShowDialog();

                    }
                    else
                    {
                        int point = int.Parse(lb_PlayArea_Point2.Text);
                        point++;
                        lb_PlayArea_Point2.Text = point.ToString();
                        tmCoolDown.Stop();
                        dnf = nfl.ShowDialog();

                    }
                    break;
                case 2:
                    if (isYouFirst)
                    {
                        int point = int.Parse(lb_PlayArea_Point2.Text);
                        point++;
                        lb_PlayArea_Point2.Text = point.ToString();
                        tmCoolDown.Stop();
                        dnf = nfl.ShowDialog();

                    }
                    else
                    {
                        int point = int.Parse(lb_PlayArea_Point1.Text);
                        point++;
                        lb_PlayArea_Point1.Text = point.ToString();
                        tmCoolDown.Stop();
                        dnf = nfw.ShowDialog();
                    }
                    break;
                default:
                    break;
            }
            isGameEnd = true;
            caroChess.Ready = false;
        }
        private void DoNewGame_PVP()
        {
            grs.Clear(panel_PlayArea.BackColor);
            caroChess.StartLAN(grs);
            Image temp = firstchess;
            firstchess = secondchess;
            secondchess = temp;
            isYouFirst = !isYouFirst;
            if (isYouFirst)
            {
                panel_PlayArea_Board.Enabled = true;
                picbox_PlayArea_Avatar1.BorderSize = 5;
                picbox_PlayArea_Avatar2.BorderSize = 0;
                prcbCoolDown.Value = 0;
                tmCoolDown.Start();
            }
            else
            {
                panel_PlayArea_Board.Enabled = false;
                picbox_PlayArea_Avatar1.BorderSize = 0;
                picbox_PlayArea_Avatar2.BorderSize = 5;
                prcbCoolDown.Value = 0;
                tmCoolDown.Start();
            }
            isGameEnd = false;
        }
        private void tmCoolDown_Tick(object sender, EventArgs e)
        {
            if (prcbCoolDown.Value < prcbCoolDown.Maximum / 2)
            {
                prcbCoolDown.ColorProgressBar = Color.FromArgb(59, 198, 171);
            }
            if (prcbCoolDown.Value >= prcbCoolDown.Maximum / 2)
            {
                prcbCoolDown.ColorProgressBar = Color.FromArgb(253, 203, 102);
            }
            if (prcbCoolDown.Value >= prcbCoolDown.Maximum * 3 / 4)
            {
                prcbCoolDown.ColorProgressBar = Color.FromArgb(245, 108, 108);

            }
            prcbCoolDown.Value = prcbCoolDown.Value + 100;
            if (prcbCoolDown.Value >= prcbCoolDown.Maximum)
            {
                if (caroChess.Turn == 1)
                {
                    DoEndGame(2);
                }
                else if (caroChess.Turn == 2)
                {
                    DoEndGame(1);
                }
            }
        }

        private void tmComputer_Tick(object sender, EventArgs e)
        {
            tmComputer.Stop();
            playSFX();
            caroChess.LaunchComputer(grs, firstchess, secondchess);
            picbox_PlayArea_Avatar1.BorderSize = 5;
            picbox_PlayArea_Avatar2.BorderSize = 0;
            if (GameCheckWin())
            {
                tmCoolDown.Stop();
                return;
            }
            else
            {
                tmCoolDown.Start();
                prcbCoolDown.Value = 0;
                panel_PlayArea_Board.Enabled = true;
            }
        }
        public void OtherPlayerMark(Point point)
        {
            if (!caroChess.Ready)
                return;
            if (caroChess.PlayChess(point.X, point.Y, grs, firstchess, secondchess))
            {
                playSFX();
                picbox_PlayArea_Avatar1.BorderSize = 5;
                picbox_PlayArea_Avatar2.BorderSize = 0;
                panel_PlayArea_Board.Enabled = true;
                if (GameCheckWin())
                {
                    tmCoolDown.Stop();
                }
            }
        }
        #endregion

        #region SocketMethod
        public string GetLocalIPv4(NetworkInterfaceType _type)
        {
            string output = "";
            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (item.NetworkInterfaceType == _type && item.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            output = ip.Address.ToString();
                        }
                    }
                }
            }
            return output;
        }
        private void Send(string mess)
        {
            if (isServer)
            {
                server!.SendAsync(currentClient, mess);
            }
            else
            {
                client!.SendAsync(mess);
            }
        }
        #endregion

        #region FireBaseMethod
        public async Task DoExit()
        {

            if (isConnected && isPlayAreaOpen)
            {
                if (isGameEnd)
                {
                    Send("/exit ");
                    if (isServer)
                    {
                        server!.DisconnectClient(currentClient);
                        server.Stop();
                    }
                    else
                    {
                        await client!.DisconnectAsync();
                    }
                    isConnected = false;
#pragma warning disable CS4014
                    NotifyForm nf = new NotifyForm("UpdateFireBase");
                    Task.Run(() =>
                    {
                        nf.ShowDialog();
                    });
                    await UpGameToFireBase(latestgameid, game);
                    await UpChatToFireBase(latestgameid, latestchatid, chats);
                    await UpPlayerToFireBase(currentplayer);
                    if (nf.InvokeRequired)
                    {
                        nf.Invoke((MethodInvoker)delegate { nf.Close(); });
                    }
                    else
                    {
                        nf.Close();
                    }
                    OpenPlayerInfo();
                    isPlayAreaOpen = false;
                }
                else
                {
                    NotifyForm nf = new NotifyForm("Thoát_1");
                    DialogResult dnf = nf.ShowDialog();
                    if (dnf == DialogResult.Yes)
                    {
                        Send("/exit ");
                        if (isServer)
                        {
                            server!.DisconnectClient(currentClient);
                            server.Stop();
                        }
                        else
                        {
                            await client!.DisconnectAsync();
                        }
                        isConnected = false;
                        if (isYouFirst)
                        {
                            DoEndGame(2);
                        }
                        else
                        {
                            DoEndGame(1);
                        }
#pragma warning disable CS4014
                        NotifyForm nf2 = new NotifyForm("UpdateFireBase");
                        Task.Run(() =>
                        {
                            nf2.ShowDialog();
                        });
                        await UpGameToFireBase(latestgameid, game);
                        await UpChatToFireBase(latestgameid, latestchatid, chats);
                        await UpPlayerToFireBase(currentplayer);
                        if (nf2.InvokeRequired)
                        {
                            nf2.Invoke((MethodInvoker)delegate { nf2.Close(); });
                        }
                        else
                        {
                            nf2.Close();
                        }
                        OpenPlayerInfo();
                        isPlayAreaOpen = false;
                    }
                }

            }
            else if (!isConnected && isPlayAreaOpen)
            {
#pragma warning disable CS4014
                NotifyForm nf2 = new NotifyForm("UpdateFireBase");
                Task.Run(() =>
                {
                    nf2.ShowDialog();
                });
                await UpGameToFireBase(latestgameid, game);
                await UpChatToFireBase(latestgameid, latestchatid, chats);
                await UpPlayerToFireBase(currentplayer);
                if (nf2.InvokeRequired)
                {
                    nf2.Invoke((MethodInvoker)delegate { nf2.Close(); });
                }
                else
                {
                    nf2.Close();
                }
                OpenPlayerInfo();
                isPlayAreaOpen = false;
            }
            else if (!isConnected && !isPlayAreaOpen)
            {
                return;
            }
        }
        public async Task UpPlayerToFireBase(Player player)
        {
            int numswin = int.Parse(lb_PlayArea_Point1.Text);
            int numslose = int.Parse(lb_PlayArea_Point2.Text);
            player.Win = player.Win + numswin;
            player.Lose = player.Lose + numslose;
            player.Winrate = ((double)player.Win / (player.Win + player.Lose) * 100).ToString("0.00") + "%";
            await firebaseClient.Child("Users").Child("User_" + player.ID).PutAsync(player);
        }
        public async Task UpChatToFireBase(string latestgameid, string latestchatid, List<Chat> chatss)
        {
            int start = int.Parse(latestchatid);
            int gameid = int.Parse(latestgameid) + 1;
            string chat_gameid = gameid.ToString();
            start++;
            if (chatss.Count == 0)
            {
                return;
            }
            foreach (var chat in chatss)
            {
                chat.Chat_ID = start.ToString();
                chat.Game_ID = chat_gameid;
                start++;
                await firebaseClient.Child("Chats").Child("Chat_" + chat.Chat_ID).PutAsync(chat);
            }
        }
        public async Task UpGameToFireBase(string latestgameid, Game game)
        {
            int gameid = int.Parse(latestgameid) + 1;
            game.Game_ID = gameid.ToString();
            if (isServer)
            {
                game.Player1 = currentplayer.ID;
                game.Player1_Wins = int.Parse(lb_PlayArea_Point1.Text);
                game.Player2 = currenopponentID;
                game.Player2_Wins = int.Parse(lb_PlayArea_Point2.Text);
            }
            else
            {
                game.Player1 = currenopponentID;
                game.Player1_Wins = int.Parse(lb_PlayArea_Point2.Text);
                game.Player2 = currentplayer.ID;
                game.Player2_Wins = int.Parse(lb_PlayArea_Point1.Text);
            }
            await firebaseClient.Child("Games").Child("Game_" + game.Game_ID).PutAsync(game);
        }

        private async Task<string> GetLatestGameID()
        {
            var games = await firebaseClient.Child("Games").OrderByKey().OnceAsync<Game>();
            var latestGame = games.OrderByDescending(g => int.Parse(g.Object.Game_ID!)).FirstOrDefault();
            return latestGame?.Object.Game_ID!;
        }
        private async Task<string> GetLatestChatID()
        {
            var chats = await firebaseClient.Child("Chats").OrderByKey().OnceAsync<Chat>();
            var latestChat = chats.OrderByDescending(g => int.Parse(g.Object.Chat_ID!)).FirstOrDefault();
            return latestChat?.Object.Chat_ID!;
        }

        private async void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            await DoExit();
            e.Cancel = false;
            Application.Exit();

        }
        private async Task<bool> IsIdExists(string id)
        {
            var data = await firebaseClient.Child("Users").Child("User_" + id).OnceAsync<object>();
            return data.Any();
        }
        private async Task<bool> IsPasswordTrue(string id, string pw)
        {
            var dataSnapshot = await firebaseClient.Child("Users").OrderByKey().EqualTo("User_" + id).OnceAsync<Player>();
            foreach (var item in dataSnapshot)
            {
                var user = item.Object;
                if (pw == user.Password)
                {
                    currentplayer!.ID = user.ID;
                    currentplayer.Password = user.Password;
                    currentplayer.Name = user.Name;
                    currentplayer.Win = user.Win;
                    currentplayer.Lose = user.Lose;
                    currentplayer.Avatar = user.Avatar;
                    currentplayer.Winrate = user.Winrate;
                    currentplayer.ProtectionCode = user.ProtectionCode;
                    currentplayer.Email = user.Email;
                    currentAvatar = user.Avatar!;
                    return true;
                }
            }
            return false;
        }
        #endregion
    }
}
