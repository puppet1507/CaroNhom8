using System.Text;

namespace Caro_Nhom8
{
    // Đăng kí
    public partial class MainForm
    {
        #region SignUp
        void OpenSignUp()
        {
            this.Size = new Size(755, 658);
            this.MaximumSize = new Size(755, 658);
            this.MinimumSize = new Size(755, 658);
            grb_SignUp.Dock = DockStyle.Fill;
            lb_SignUp_Notify.Visible = false;
            grb_Login.Visible = false;
            grb_SignUp.Visible = true;
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
        private void btn_ConfirmSignUp_Click(object sender, EventArgs e)
        {
            playSFX();
        }

        private void btn_ExitSignUp_Click(object sender, EventArgs e)
        {
            playSFX();
            OpenLogin();
        }

        private void btn_GetVerifyCode_SignUp_Click(object sender, EventArgs e)
        {
            playSFX();
        }
        private void lb_ChangeAvatar_SignUp_Click(object sender, EventArgs e)
        {
            playSFX();
        }
        #endregion
    }
}
