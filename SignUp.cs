using System.Text;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

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
            grb_ChooseAvatar.Visible = false;
            panel_PlayArea.Dock = DockStyle.None;
            panel_PlayArea.Visible = false;
        }
        private void btn_ConfirmSignUp_Click(object sender, EventArgs e)
        {
            playSFX();
            lb_SignUp_Notify.Visible = true;

            string email = txt_SignUp_Email.TextButton.Trim();
            string id = txt_SignUp_ID.TextButton.Trim();
            string name = txt_SignUp_Name.TextButton.Trim();
            string password = txt_SignUp_PW.TextButton.Trim();
            string verifycode = txt_SignUp_VerifyCode.TextButton.Trim();
            if(!IsValidID(id))
            {
                lb_SignUp_Notify.ForeColor = Color.FromArgb(245, 108, 108);
                lb_SignUp_Notify.Text = "*Thông báo: ID không hợp lệ!";
            }    
            /*else if(xử lí khi id đã tồn tại)
            {
                lb_SignUp_Notify.ForeColor = Color.FromArgb(245, 108, 108);
                lb_SignUp_Notify.Text = "*Thông báo: ID đã tồn tại!";
            }*/
            else if (!ValidateName(name))
            {
                lb_SignUp_Notify.ForeColor = Color.FromArgb(245, 108, 108);
                lb_SignUp_Notify.Text = "*Thông báo: Tên không hợp lệ!";
            }    
            else if (!ValidateEmail(email))
            {
                lb_SignUp_Notify.ForeColor = Color.FromArgb(245, 108, 108);
                lb_SignUp_Notify.Text = "*Thông báo: Email không hợp lệ!";
            }
            else if (!ValidatePassword(password))
            {
                lb_SignUp_Notify.ForeColor = Color.FromArgb(245, 108, 108);
                lb_SignUp_Notify.Text = "*Thông báo: Mật khẩu không hợp lệ!";
            }
            else if (string.IsNullOrWhiteSpace(verifycode))
            {
                lb_SignUp_Notify.ForeColor = Color.FromArgb(245, 108, 108);
                lb_SignUp_Notify.Text = "*Thông báo: Mã xác thực không hợp lệ!";
            }
            /*else if(xử lí khi mã xác thực không chính xác)
            {
                lb_SignUp_Notify.ForeColor = Color.FromArgb(245, 108, 108);
                lb_SignUp_Notify.Text = "*Thông báo: Mã xác thực không chính xác!";
            }*/
            else
            {
                // Xử lí khi các điều kiện phù hợp
                lb_SignUp_Notify.ForeColor = Color.FromArgb(59, 198, 171);
                lb_SignUp_Notify.Text = "*Thông báo: Đăng kí thành công!";
            }    
        }

        private void btn_ExitSignUp_Click(object sender, EventArgs e)
        {
            playSFX();
            isChooseAvatarSignUp = false;
            OpenLogin();
        }

        private void btn_GetVerifyCode_SignUp_Click(object sender, EventArgs e)
        {
            playSFX();
        }
        private void lb_ChangeAvatar_SignUp_Click(object sender, EventArgs e)
        {
            playSFX();
            OpenChooseAvatar();
        }
        public static bool ValidateEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            bool isMatch = Regex.IsMatch(email, pattern);

            return isMatch;
        }
        public static bool IsValidID(string id)
        {
            string pattern = @"^[a-zA-Z0-9!@#$%^&*()_+{}\[\]:;<>,.?~\\|]+$";
            bool isMatch = Regex.IsMatch(id, pattern);

            return isMatch;
        }
        public static bool ValidateName(string name)
        {
            string pattern = @"^[\p{L}\s]+$";
            bool isMatch = Regex.IsMatch(name, pattern) && !Regex.IsMatch(name, @"[!@#$%^&*()_+{}\[\]:;<>,.?~\\|0-9]");
            return isMatch;
        }
        public static bool ValidatePassword(string password)
        {
            string pattern = @"^[a-zA-Z0-9!@#$%^&*()_+{}\[\]:;<>,.?~\\|]+$";
            bool isMatch = Regex.IsMatch(password, pattern);
            return isMatch;
        }
        #endregion
    }
}
