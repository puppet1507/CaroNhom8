using SuperSimpleTcp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caro_Nhom8
{
    // Info
    public partial class MainForm
    {
        #region PlayerInfo
        void OpenInfo()
        {
            this.Size = new Size(755, 658);
            grb_Info.Size = new Size(726, 601);
            grb_Info.Location = new Point(6, 6);
            grb_Login.Visible = false;
            grb_SignUp.Visible = false;
            grb_Info.Visible = true;
            grb_Waiting.Visible = false;
            grb_ClientInfo.Visible = false;
            grb_BattleInfo.Visible = false;
            grb_ForgetPassword.Visible = false;
            grb_ChangePassword.Visible = false;
            panel_PlayArea.Dock = DockStyle.None;
            panel_PlayArea.Visible = false;
        }
        private void btn_PlayWithAI_Click(object sender, EventArgs e)
        {
            grs!.Clear(fpanel_Board.BackColor);
            caroChess!.StartPvC(grs!);
            tmCoolDown.Start();
            prcbCoolDown.Value = 0;
            OpenPlayArea();
        }
        private void btn_CreateRoom_Click(object sender, EventArgs e)
        {
            OpenServerInfo();
            

        }
        private void btn_JoinRoom_Click(object sender, EventArgs e)
        {
            OpenConnect();
        }
        private void btn_Info_LogOut_Click(object sender, EventArgs e)
        {
            OpenLogin();
        }
        private void btn_ChangePassword_Click(object sender, EventArgs e)
        {
            OpenChangePassword();
        }
        #endregion
    }

}
