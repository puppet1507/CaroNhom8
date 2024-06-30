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
using static System.Windows.Forms.DataFormats;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Media;
using WMPLib;
using System.Numerics;
using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.VisualBasic.ApplicationServices;
using System.Diagnostics.Eventing.Reader;
// Check NgocLong_Github
// Check ThoNha_Github
// Check LeNhu_Github
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
        private CaroGame caroChess;
        Board board = new Board();
        public bool isServer;
        public string currentClient = "";
        public int numofClinet = 0;
        bool isMusic = true;
        bool isSFX = true;
        bool isDark = true;
        bool isGameEnd = false;
        bool isYouFirst = false;
        bool isConnected = false;
        bool isPlayAreaOpen = false;
        public bool isChooseAvatarSignUp = false;
        public string currentAvatar = "Resources/UI_Icon/Default.png";
        public Image ImageOgreen = new Bitmap("Resources/UI_Icon/ogreen.png");
        public Image ImageXred = new Bitmap("Resources/UI_Icon/xred.png");
        public Image ImageXgreen = new Bitmap("Resources/UI_Icon/xgreen.png");
        public Image ImageOred = new Bitmap("Resources/UI_Icon/ored.png");
        public Image firstchess;
        public Image secondchess;
        public Player currentplayer = new Player();
        public string currenopponentID = "";
        public string latestgameid = "";
        public string latestchatid = "";
        WindowsMediaPlayer music = new WindowsMediaPlayer();
        WindowsMediaPlayer sfx = new WindowsMediaPlayer();
        FirebaseClient firebaseClient = new FirebaseClient("https://caronhom8-default-rtdb.firebaseio.com/");
        public List<Chat> chats;
        public Game game;
        #endregion

        #region ScreenChange_Methods

        void OpenPlayArea()
        {
            this.Size = new Size(1207, 945);
            this.MinimumSize = new Size(1207, 945);
            this.MaximumSize = new Size(9999, 9999);
            grb_Login.Visible = false;
            grb_SignUp.Visible = false;
            grb_Info.Visible = false;
            grb_Waiting.Visible = false;
            grb_ClientInfo.Visible = false;
            grb_ComputerInfo.Visible = false;
            grb_ServerInfo.Visible = false;
            grb_ForgetPassword.Visible = false;
            grb_ChangeInfo.Visible = false;
            grb_Setting.Visible = false;
            grb_ChooseAvatar.Visible = false;
            panel_PlayArea.Dock = DockStyle.Fill;
            panel_PlayArea.Visible = true;
            chats.Clear();
            panel_PlayArea_ChatArea.Controls.Clear();
            lb_PlayArea_Point1.Text = "0";
            lb_PlayArea_Point2.Text = "0";
        }

        #endregion

        #region FormMethod
        public MainForm()
        {
            InitializeComponent();
            board = new Board(16, 16);
            caroChess = new CaroGame(board);
            grs = panel_PlayArea_Board.CreateGraphics();
            firstchess = ImageXred;
            secondchess = ImageOgreen;
            chats = new List<Chat>();
            game = new Game();
            music.URL = "Resources/Sound/Music.wav";
            music.settings.setMode("loop", true);
            music.controls.stop();


        }
        private void fpanel_Board_Paint(object sender, PaintEventArgs e)
        {
            caroChess!.DrawChessBoard(grs!);
            caroChess.RepaintChess(grs!, firstchess, secondchess);
        }


        private void LoginForm_Load(object sender, EventArgs e)
        {
            DarkTheme();
            OpenLogin();
            RenewLogin();
            LoadAvatars();
            music.controls.play();
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
                if (isDark)
                {
                    txt_Msg.ForeColor = Color.White;
                }
                else
                {
                    txt_Msg.ForeColor = Color.Black;
                }
            }
        }

        private void txt_Msg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !string.IsNullOrWhiteSpace(txt_Msg.Text))
            {

                string msg = "/msg " + txt_Msg.Text;
                if (caroChess.Mode == 3)
                {
                    if (isServer)
                    {
                        server!.SendAsync(currentClient, msg);
                    }
                    else
                    {
                        client!.SendAsync(msg);
                    }
                }
                ChatBubbleRight chatBubbleRight = new ChatBubbleRight();
                chatBubbleRight.BubbleColor = Color.FromArgb(59, 198, 171);
                chatBubbleRight.ForeColor = Color.White;
                chatBubbleRight.Text = txt_Msg.Text;
                chatBubbleRight.SizeAutoW = false;
                chatBubbleRight.Width = 195;
                chats.Add(new Chat("", "", txt_Msg.Text, currentplayer.ID!, currenopponentID));
                panel_PlayArea_ChatArea.Controls.Add(chatBubbleRight);
                txt_Msg.Clear();
            }
        }
        #endregion

        #region PlayChess
        private void fpanel_Board_MouseClick(object sender, MouseEventArgs e)
        {
            if (!caroChess.Ready)
                return;
            if (caroChess.PlayChess(e.X, e.Y, grs, firstchess, secondchess))
            {
                playSFX();
                if (caroChess.Mode == 2)
                {
                    if (GameCheckWin())
                    {
                        return;
                    }
                    else
                    {
                        picbox_PlayArea_Avatar1.BorderSize = 0;
                        picbox_PlayArea_Avatar2.BorderSize = 5;
                        panel_PlayArea_Board.Enabled = false;
                        Random random = new Random();
                        tmComputer.Interval = random.Next(1000, 1500);
                        tmComputer.Start();

                    }
                }
                else if (caroChess.Mode == 3)
                {
                    panel_PlayArea_Board.Enabled = false;
                    picbox_PlayArea_Avatar1.BorderSize = 0;
                    picbox_PlayArea_Avatar2.BorderSize = 5;
                    Send("/pnt " + e.Location.X.ToString() + "," + e.Location.Y.ToString());
                    if (GameCheckWin())
                    {
                        return;
                    }
                }
                tmCoolDown.Start();
                prcbCoolDown.Value = 0;
            }
        }

        #endregion

        #region PVC_TOOLS_BUTTON
        private void btn_PVC_NewGame_Click(object sender, EventArgs e)
        {
            playSFX();
            if (isGameEnd == false)
            {
                NotifyForm nf = new NotifyForm("Đấu lại_0");
                DialogResult result = nf.ShowDialog();
                if (result == DialogResult.Yes)
                {
                    int point2 = int.Parse(lb_PlayArea_Point2.Text);
                    point2++;
                    lb_PlayArea_Point2.Text = point2.ToString();
                    grs.Clear(panel_PlayArea.BackColor);
                    caroChess.StartPvC(grs);
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
                        tmCoolDown.Start();
                        prcbCoolDown.Value = 0;
                        Random random = new Random();
                        tmComputer.Interval = random.Next(1000, 1500);
                        tmComputer.Start();
                    }
                }
                else
                {
                    return;
                }
                isGameEnd = false;

            }
            else
            {
                grs.Clear(panel_PlayArea.BackColor);
                caroChess.StartPvC(grs);
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
                    tmCoolDown.Start();
                    prcbCoolDown.Value = 0;
                    Random random = new Random();
                    tmComputer.Interval = random.Next(1000, 1500);
                    tmComputer.Start();

                }
                isGameEnd = false;
            }

        }
        private void btn_Undo_Click(object sender, EventArgs e)
        {
            playSFX();
            caroChess.Undo(grs);
            if (isGameEnd == false)
            {
                tmCoolDown.Start();
                prcbCoolDown.Value = 0;
            }
        }

        private void btn_Redo_Click(object sender, EventArgs e)
        {
            playSFX();
            caroChess.Redo(grs, firstchess, secondchess);
            if (isGameEnd == false)
            {
                tmCoolDown.Start();
                prcbCoolDown.Value = 0;
            }
        }
        private void btn_PVC_Exit_Click(object sender, EventArgs e)
        {
            playSFX();
            tmCoolDown.Stop();
            tmComputer.Stop();
            panel_PlayArea_ChatArea.Controls.Clear();
            lb_PlayArea_Point1.Text = "0";
            lb_PlayArea_Point2.Text = "0";
            OpenPlayerInfo();
        }
        #endregion

        #region PVP_TOOLS_BUTTON
        private void btn_PVP_DauHang_Click(object sender, EventArgs e)
        {
            playSFX();
            if (!isGameEnd)
            {
                NotifyForm nf = new NotifyForm("Đầu hàng_1");
                DialogResult dnf = nf.ShowDialog();
                if (dnf == DialogResult.Yes)
                {
                    Send("/ff ");
                    if (isYouFirst)
                    {
                        DoEndGame(2);
                    }
                    else
                    {
                        DoEndGame(1);
                    }
                }
            }
        }
        private void btn_PVP_HoaGiai_Click(object sender, EventArgs e)
        {
            playSFX();
            if (!isGameEnd)
            {
                NotifyForm nf = new NotifyForm("Hòa giải_1");
                DialogResult dnf = nf.ShowDialog();
                if (dnf == DialogResult.Yes)
                {
                    Send("/reconcile_request ");
                }
            }
        }

        private void btn_PVP_NewGame_Click(object sender, EventArgs e)
        {
            playSFX();
            if (!isConnected)
            {
                return;
            }
            if (isGameEnd)
            {
                NotifyForm nf = new NotifyForm("Đấu lại_1");
                DialogResult dnf = nf.ShowDialog();
                if (dnf == DialogResult.Yes)
                {
                    Send("/newgame_request ");
                }
            }
        }

        private async void btn_PVP_Exit_Click(object sender, EventArgs e)
        {
            playSFX();
            await DoExit();
        }

        #endregion

        #region Setting_Button
        private void btn_PlayArea_Music_Click(object sender, EventArgs e)
        {
            playSFX();
            isMusic = !isMusic;
            if (isMusic)
            {
                btn_Setting_Music.Image = Image.FromFile("Resources/UI_Icon/Speaker.png");
                btn_PlayArea_Music.Image = Image.FromFile("Resources/UI_Icon/Speaker.png");
                trackbar_Setting_Music.Value = 75;
            }
            else
            {
                btn_Setting_Music.Image = Image.FromFile("Resources/UI_Icon/Mute.png");
                btn_PlayArea_Music.Image = Image.FromFile("Resources/UI_Icon/Mute.png");
                trackbar_Setting_Music.Value = 0;
            }
        }

        private void btn_PlayArea_SFX_Click(object sender, EventArgs e)
        {
            playSFX();
            isSFX = !isSFX;
            if (isSFX)
            {
                btn_Setting_SFX.Image = Image.FromFile("Resources/UI_Icon/Speaker.png");
                btn_PlayArea_SFX.Image = Image.FromFile("Resources/UI_Icon/Speaker.png");
                trackbar_Setting_SFX.Value = 75;
            }
            else
            {
                btn_Setting_SFX.Image = Image.FromFile("Resources/UI_Icon/Mute.png");
                btn_PlayArea_SFX.Image = Image.FromFile("Resources/UI_Icon/Mute.png");
                trackbar_Setting_SFX.Value = 0;
            }
        }
        #endregion
    }
}