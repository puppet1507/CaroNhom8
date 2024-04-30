using ReaLTaiizor.Controls;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caro_Nhom8
{
    public partial class MainForm
    {
        #region Setting
        void OpenSetting()
        {
            this.Size = new Size(755, 658);
            this.MaximumSize = new Size(755, 658);
            this.MinimumSize = new Size(755, 658);
            grb_Setting.Dock = DockStyle.Fill;
            grb_Setting.Visible = true;
            grb_Login.Visible = false;
            grb_SignUp.Visible = false;
            grb_Info.Visible = false;
            grb_Waiting.Visible = false;
            grb_ClientInfo.Visible = false;
            grb_ComputerInfo.Visible = false;
            grb_ServerInfo.Visible = false;
            grb_ForgetPassword.Visible = false;
            grb_ChangeInfo.Visible = false;
            grb_ChooseAvatar.Visible = false;
            panel_PlayArea.Dock = DockStyle.None;
            panel_PlayArea.Visible = false;
        }
        
        private void btn_Setting_Music_Click(object sender, EventArgs e)
        {
            playSFX();
            isMusic = !isMusic;
            if (isMusic)
            {
                btn_Setting_Music.Image = Image.FromFile("Resources/UI_Icon/Speaker.png");
                trackbar_Setting_Music.Value = 10;
            }
            else
            {
                btn_Setting_Music.Image = Image.FromFile("Resources/UI_Icon/Mute.png");
                trackbar_Setting_Music.Value = 0;
            }
        }

        private void btn_Setting_SFX_Click(object sender, EventArgs e)
        {

            playSFX();
            isSFX = !isSFX;
            if (isSFX)
            {
                btn_Setting_SFX.Image = Image.FromFile("Resources/UI_Icon/Speaker.png");
                trackbar_Setting_SFX.Value = 10;
            }
            else
            {
                btn_Setting_SFX.Image = Image.FromFile("Resources/UI_Icon/Mute.png");
                trackbar_Setting_SFX.Value = 0;
            }
        }
        private void btn_Setting_Exit_Click(object sender, EventArgs e)
        {
            playSFX();
            OpenInfo();
        }
        private void trackbar_Setting_Music_ValueChanged()
        {
            int volume = trackbar_Setting_Music.Value;
            if(volume == 0)
            {
                isMusic = false;
                btn_Setting_Music.Image = Image.FromFile("Resources/UI_Icon/Mute.png");
            }
            else
            {
                isMusic = true;
                btn_Setting_Music.Image = Image.FromFile("Resources/UI_Icon/Speaker.png");
            }
            music.settings.volume = volume;

        }

        private void trackbar_Setting_SFX_ValueChanged()
        {
            int volume = trackbar_Setting_SFX.Value;
            if (volume == 0)
            {
                isSFX = false;
                btn_Setting_SFX.Image = Image.FromFile("Resources/UI_Icon/Mute.png");
            }
            else
            {
                isSFX = true;
                btn_Setting_SFX.Image = Image.FromFile("Resources/UI_Icon/Speaker.png");
            }
            sfx.settings.volume = volume;

        }
        private void btn_Setting_Theme_Click(object sender, EventArgs e)
        {
            playSFX();
            isDark = !isDark;
            if(isDark)
            {
                btn_Setting_Theme.Image = Image.FromFile("Resources/UI_Icon/Moon.png");
                DarkTheme();
            }
            else
            {
                btn_Setting_Theme.Image = Image.FromFile("Resources/UI_Icon/Sun.png");
                LightTheme();
            }    
        }
       
        #endregion
    }
}
