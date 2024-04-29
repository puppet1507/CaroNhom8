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
            this.MaximumSize = new Size(755, 658);
            this.MinimumSize = new Size(755, 658);
            grb_Info.Dock = DockStyle.Fill;
            grb_Login.Visible = false;
            grb_SignUp.Visible = false;
            grb_Info.Visible = true;
            grb_Waiting.Visible = false;
            grb_ClientInfo.Visible = false;
            grb_ComputerInfo.Visible = false;
            grb_ServerInfo.Visible = false;
            grb_ForgetPassword.Visible = false;
            grb_ChangeInfo.Visible = false;
            grb_Setting.Visible = false;
            grb_ChooseAvatar.Visible = false;
            panel_PlayArea.Dock = DockStyle.None;
            panel_PlayArea.Visible = false;
        }
        private void btn_PlayWithAI_Click(object sender, EventArgs e)
        {
            playSFX();
            OpenComputerInfo();

        }
        private void btn_CreateRoom_Click(object sender, EventArgs e)
        {
            playSFX();
            OpenServerInfo();


        }
        private void btn_JoinRoom_Click(object sender, EventArgs e)
        {
            playSFX();
            OpenConnect();
        }
        private void btn_Info_LogOut_Click(object sender, EventArgs e)
        {
            playSFX();
            OpenLogin();
        }
        private void btn_ChangePassword_Click(object sender, EventArgs e)
        {
            playSFX();
            OpenChangePassword();
        }
        private void btn_Info_OpenSetting_Click(object sender, EventArgs e)
        {
            playSFX();
            OpenSetting();
        }
        #endregion
    }

}
