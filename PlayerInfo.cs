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
            grb_ComputerInfo.Visible = false;
            grb_ServerInfo.Visible = false;
            grb_ForgetPassword.Visible = false;
            grb_ChangePassword.Visible = false;
            panel_PlayArea.Dock = DockStyle.None;
            panel_PlayArea.Visible = false;
        }
        private void btn_PlayWithAI_Click(object sender, EventArgs e)
        {
            if (isSFX)
            {
                sfx.URL = "Resources/Sound/Sfx.wav";
            }
            OpenComputerInfo();

        }
        private void btn_CreateRoom_Click(object sender, EventArgs e)
        {
            if (isSFX)
            {
                sfx.URL = "Resources/Sound/Sfx.wav";
            }
            OpenServerInfo();


        }
        private void btn_JoinRoom_Click(object sender, EventArgs e)
        {
            if (isSFX)
            {
                sfx.URL = "Resources/Sound/Sfx.wav";
            }
            OpenConnect();
        }
        private void btn_Info_LogOut_Click(object sender, EventArgs e)
        {
            if (isSFX)
            {
                sfx.URL = "Resources/Sound/Sfx.wav";
            }
            OpenLogin();
        }
        private void btn_ChangePassword_Click(object sender, EventArgs e)
        {
            if (isSFX)
            {
                sfx.URL = "Resources/Sound/Sfx.wav";
            }
            OpenChangePassword();
        }
        private void btn_Info_Music_Click(object sender, EventArgs e)
        {
            if (isSFX)
            {
                sfx.URL = "Resources/Sound/Sfx.wav";
            }
            isMusic = !isMusic;
            if (isMusic)
            {
                music.controls.play();
                btn_Info_Music.Image = Image.FromFile("Resources/UI_Icon/Speaker.png");
            }
            else
            {
                music.controls.stop();
                btn_Info_Music.Image = Image.FromFile("Resources/UI_Icon/Mute.png");
            }
        }
       private void btn_Info_SFX_Click(object sender, EventArgs e)
       {
            if (isSFX)
            {
                sfx.URL = "Resources/Sound/Sfx.wav";
            }
            isSFX = !isSFX;
            if (isSFX)
            {
                btn_Info_SFX.Image = Image.FromFile("Resources/UI_Icon/Speaker.png");
            }
            else
            {
                btn_Info_SFX.Image = Image.FromFile("Resources/UI_Icon/Mute.png");
            }

        }
        #endregion
    }

}
