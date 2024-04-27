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
                        if(controlchild is SkyComboBox comboBox)
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
        #endregion
    }
}
