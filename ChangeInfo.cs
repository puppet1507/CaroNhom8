using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Caro_Nhom8
{
    public partial class MainForm
    {
        #region ChangeInfo
        void OpenChangePassword()
        {
            this.Size = new Size(755, 658);
            this.MaximumSize = new Size(755, 658);
            this.MinimumSize = new Size(755, 658);
            grb_ChangeInfo.Dock = DockStyle.Fill;
            lb_ChangeInfo_Notify.Visible = false;
            grb_Login.Visible = false;
            grb_SignUp.Visible = false;
            grb_Info.Visible = false;
            grb_Waiting.Visible = false;
            grb_ClientInfo.Visible = false;
            grb_ComputerInfo.Visible = false;
            grb_ServerInfo.Visible = false;
            grb_ForgetPassword.Visible = false;
            grb_ChangeInfo.Visible = true;
            grb_Setting.Visible = false;
            grb_ChooseAvatar.Visible = false;
            panel_PlayArea.Dock = DockStyle.None;
            panel_PlayArea.Visible = false;
        }
      
        private void btn_CancelChangeInfo_Click(object sender, EventArgs e)
        {
            playSFX();
            RenewPlayerInfo();
            currentAvatar = currentplayer.Avatar!;
            OpenInfo();
            LoadRanking(currentplayer.ID!);
        }
        private async void btn_ConfirmChangeInfo_Click(object sender, EventArgs e)
        {
            playSFX();
            lb_ChangeInfo_Notify.ForeColor = Color.White;
            lb_ChangeInfo_Notify.Text = "*Đang xử lí";
            lb_ChangeInfo_Notify.Visible = true;
            string name = txt_ChangeInfo_Name.TextButton.Trim();
            string oldpassword = currentplayer.Password!;
            string newpassword = txt_ChangeInfo_NewPW.TextButton.Trim();
            string avatar = currentAvatar;
            if (!ValidateName(name))
            {
                lb_ChangeInfo_Notify.ForeColor = Color.FromArgb(245, 108, 108);
                lb_ChangeInfo_Notify.Text = "*Thông báo: Tên không hợp lệ!";
            }
            else if(txt_ChangeInfo_ProtectionCode.TextButton != currentplayer.ProtectionCode)
            {
                lb_ChangeInfo_Notify.ForeColor = Color.FromArgb(245, 108, 108);
                lb_ChangeInfo_Notify.Text = "*Thông báo: Mã bảo vệ không chính xác!";
            }
            else
            {
                if (string.IsNullOrEmpty(newpassword))
                {
                    newpassword = oldpassword;
                }
                else if (!ValidatePassword(newpassword))
                {
                    lb_ChangeInfo_Notify.ForeColor = Color.FromArgb(245, 108, 108);
                    lb_ChangeInfo_Notify.Text = "*Thông báo: Mật khẩu không hợp lệ!";
                    return;
                }

                // Cập nhật thông tin của player
                currentplayer = new Player
                {
                    ID = currentplayer.ID,
                    Name = name,
                    Avatar = avatar,
                    Email = currentplayer.Email,
                    Password = newpassword,
                    ProtectionCode = currentplayer.ProtectionCode,
                    Win = currentplayer.Win,
                    Lose = currentplayer.Lose,
                    Winrate = currentplayer.Winrate,
                };
                await firebaseClient.Child("Users").Child("User_" + currentplayer.ID).PutAsync(currentplayer);
                lb_ChangeInfo_Notify.ForeColor = Color.FromArgb(59, 198, 171);
                lb_ChangeInfo_Notify.Text = "*Thông báo: Cập nhật thành công!";
            }    
        }
        
        private void lb_ChangeInfo_Avatar_Click(object sender, EventArgs e)
        {
            OpenChooseAvatar();
            playSFX();
        }

        #endregion
    }
}
