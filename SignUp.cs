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
            grb_SignUp.Size = new Size(726, 601);
            grb_SignUp.Location = new Point(6, 6);
            lb_SignUp_Notify.Visible = false;
            grb_Login.Visible = false;
            grb_SignUp.Visible = true;
            grb_Info.Visible = false;
            grb_Waiting.Visible = false;
            grb_ClientInfo.Visible = false;
            grb_ComputerInfo.Visible = false;
            grb_ServerInfo.Visible = false;
            grb_ForgetPassword.Visible = false;
            grb_ChangePassword.Visible = false;
            panel_PlayArea.Dock = DockStyle.None;
            panel_PlayArea.Visible = false;
        }
        private void btn_ConfirmSignUp_Click(object sender, EventArgs e)
        {

        }

        private void btn_ExitSignUp_Click(object sender, EventArgs e)
        {
            OpenLogin();
        }

        private void btn_GetVerifyCode_SignUp_Click(object sender, EventArgs e)
        {

        }
        private void lb_ChangeAvatar_SignUp_Click(object sender, EventArgs e)
        {

        }
        #endregion
    }
}
