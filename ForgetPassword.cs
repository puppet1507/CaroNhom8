using SuperSimpleTcp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caro_Nhom8
{
    public partial class MainForm
    {
        #region ForgetPassword
        void OpenForgetPassword()
        {
            this.Size = new Size(755, 658);
            this.MaximumSize = new Size(755, 658);
            this.MinimumSize = new Size(755, 658);
            grb_ForgetPassword.Dock = DockStyle.Fill;
            lb_ForgetPassword_Notify.Visible = false;
            grb_Login.Visible = false;
            grb_SignUp.Visible = false;
            grb_Info.Visible = false;
            grb_Waiting.Visible = false;
            grb_ClientInfo.Visible = false;
            grb_ComputerInfo.Visible = false;
            grb_ServerInfo.Visible = false;
            grb_ForgetPassword.Visible = true;
            grb_ChangeInfo.Visible = false;
            grb_Setting.Visible = false;
            panel_PlayArea.Dock = DockStyle.None;
            panel_PlayArea.Visible = false;
        }
        private void btn_GetVerifyCode_ForgetPassword_Click(object sender, EventArgs e)
        {
            playSFX();
        }

        private void btn_ConfirmForgetPassword_Click(object sender, EventArgs e)
        {
            playSFX();
        }

        private void btn_CancelForgetPassword_Click(object sender, EventArgs e)
        {
            playSFX();
            OpenLogin();
        }
        #endregion
    }
}
