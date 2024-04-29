using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caro_Nhom8
{
    // Đăng nhập
    public partial class MainForm
    {
        #region Login
        void OpenLogin()
        {
            this.Size = new Size(755, 658);
            this.MaximumSize = new Size(755, 658);
            this.MinimumSize = new Size(755, 658);
            grb_Login.Dock = DockStyle.Fill;
            grb_ChooseAvatar.Visible = false;
            lb_Login_Notify.Visible = false;
            grb_Login.Visible = true;
            grb_SignUp.Visible = false;
            grb_Info.Visible = false;
            grb_Waiting.Visible = false;
            grb_ClientInfo.Visible = false;
            grb_ComputerInfo.Visible = false;
            grb_ServerInfo.Visible = false;
            grb_ForgetPassword.Visible = false;
            grb_ChangeInfo.Visible = false;
            grb_Setting.Visible = false;
            panel_PlayArea.Dock = DockStyle.None;
            panel_PlayArea.Visible = false;
        }
        private void btn_Login_Click_1(object sender, EventArgs e)
        {
            playSFX();

            OpenInfo();
            lb_Welcome.Text = "Welcome " + txt_Login_ID.TextButton;
        }
        private void btn_Open_SignUp_Click(object sender, EventArgs e)
        {
            playSFX();
            isChooseAvatarSignUp = true;
            OpenSignUp();
            


        }
        private void lb_Open_ForgetPW_Click(object sender, EventArgs e)
        {
            playSFX();

            OpenForgetPassword();


        }
        
        #endregion
    }
}
