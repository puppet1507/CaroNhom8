using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Caro_Nhom8
{
    #region ComputerInfo
    public partial class MainForm
    {
       
        void OpenComputerInfo()
        {
            this.Size = new Size(755, 658);
            this.MaximumSize = new Size(755, 658);
            this.MinimumSize = new Size(755, 658);
            grb_ComputerInfo.Dock = DockStyle.Fill;
            grb_Login.Visible = false;
            grb_SignUp.Visible = false;
            grb_Info.Visible = false;
            grb_Waiting.Visible = false;
            grb_ClientInfo.Visible = false;
            grb_ComputerInfo.Visible = true;
            grb_ServerInfo.Visible = false;
            grb_ForgetPassword.Visible = false;
            grb_ChangeInfo.Visible = false;
            grb_Setting.Visible = false;
            grb_ChooseAvatar.Visible = false;
            panel_PlayArea.Dock = DockStyle.None;
            panel_PlayArea.Visible = false;
        }
        private void btn_PVC_Start_Click(object sender, EventArgs e)
        {
            playSFX();
            picbox_PlayArea_Avatar1.Image = Image.FromFile(currentplayer.Avatar!);
            picbox_PlayArea_Avatar2.Image = Image.FromFile("Resources/UI_Icon/Bot.png");
            lb_PlayArea_Name1.Text = currentplayer.Name;
            lb_PlayArea_Name2.Text = "Bot";
            string boardsize = cbx_PVC_BoardSize.SelectedItem.ToString()!;
            string turntime = cbx_PVC_TurnTime.SelectedItem.ToString()!;
            string chess = cbx_PVC_Chess.SelectedItem.ToString()!;
            string first = cbx_PVC_FirstTurn.SelectedItem.ToString()!;
            switch(boardsize)
            {
                case "10x10":
                    board = new Board(10, 10);
                    break;
                case "16x16":
                    board = new Board(16, 16);
                    
                    break;
                case "20x20":
                    board = new Board(20, 20);
                    break;
                default:
                    break;
            }
            switch (turntime)
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
                default:
                    break;
            }
            caroChess = new CaroGame(board);
            grs = panel_PlayArea_Board.CreateGraphics();
            grs!.Clear(panel_PlayArea_Board.BackColor);
            caroChess!.StartPvC(grs!);
            OpenPlayArea();
            grb_PVC_Tools.Location = new Point(14, 310);
            grb_PVC_Tools.Visible = true;
            grb_PVP_Tools.Visible = false;

            switch (first)
            {
                case "Bạn đánh trước":
                    switch (chess)
                    {
                        case "x":
                            picbox_PlayArea_Chess1.Image = ImageXgreen;
                            picbox_PlayArea_Chess2.Image = ImageOred;
                            firstchess = ImageXgreen;
                            secondchess = ImageOred;
                            break;
                        case "o":
                            picbox_PlayArea_Chess1.Image = ImageOgreen;
                            picbox_PlayArea_Chess2.Image = ImageXred;
                            firstchess = ImageOgreen;
                            secondchess = ImageXred;
                            break;
                        default:
                            break;
                    }
                    isYouFirst = true;
                    picbox_PlayArea_Avatar1.BorderSize = 5;
                    picbox_PlayArea_Avatar2.BorderSize = 0;
                    break;
                case "Máy đánh trước":
                    switch (chess)
                    {
                        case "x":
                            picbox_PlayArea_Chess1.Image = ImageXgreen;
                            picbox_PlayArea_Chess2.Image = ImageOred;
                            firstchess = ImageOred;
                            secondchess = ImageXgreen;
                            break;
                        case "o":
                            picbox_PlayArea_Chess1.Image = ImageOgreen;
                            picbox_PlayArea_Chess2.Image = ImageXred;
                            firstchess = ImageXred;
                            secondchess = ImageOgreen;
                            break;
                        default:
                            break;
                    }
                    isYouFirst = false;
                    panel_PlayArea_Board.Enabled = false;
                    picbox_PlayArea_Avatar1.BorderSize = 0;
                    picbox_PlayArea_Avatar2.BorderSize = 5;
                    Random random = new Random();
                    tmComputer.Interval = random.Next(1000, 1500);
                    tmComputer.Start();
                    break;
                default:
                    break;
            }
            prcbCoolDown.Value = 0;
            tmCoolDown.Start();
        }

        private void btn_PVC_Cancel_Click(object sender, EventArgs e)
        {
            playSFX();
            OpenPlayerInfo();
        }
        
    }
    #endregion
}
