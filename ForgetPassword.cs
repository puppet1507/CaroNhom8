using Firebase.Database.Query;
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
            grb_ChooseAvatar.Visible = false;
            panel_PlayArea.Dock = DockStyle.None;
            panel_PlayArea.Visible = false;
            
        }

        private async void btn_ConfirmForgetPassword_Click(object sender, EventArgs e)
        {
            playSFX();
            lb_ForgetPassword_Notify.ForeColor = Color.White;
            lb_ForgetPassword_Notify.Text = "*Đang xử lí";
            lb_ForgetPassword_Notify.Visible = true;
            string id = txt_ForgetPW_ID.TextButton.Trim();
            string pw = txt_ForgetPW_NewPW.TextButton.Trim();
            bool isExists = await IsIdExists(id);
            if (!isExists)
            {
                lb_ForgetPassword_Notify.ForeColor = Color.FromArgb(245, 108, 108);
                lb_ForgetPassword_Notify.Text = "*Thông báo: ID không tồn tại!";
            }
            else
            {
                var dataSnapshot = await firebaseClient.Child("Users").OrderByKey().EqualTo("User_" + id).OnceAsync<Player>();
                foreach (var item in dataSnapshot)
                {
                    var user = item.Object;
                    if (txt_ForgetPW_UserEmail.TextButton != user.Email)
                    {
                        lb_ForgetPassword_Notify.ForeColor = Color.FromArgb(245, 108, 108);
                        lb_ForgetPassword_Notify.Text = "*Thông báo: Email không chính xác!";
                    }
                    else
                    {
                        if(txt_FortgetPW_Code.TextButton != verifycode)
                        {
                            lb_ForgetPassword_Notify.ForeColor = Color.FromArgb(245, 108, 108);
                            lb_ForgetPassword_Notify.Text = "*Thông báo: Mã xác thực không chính xác!";
                        }    
                        else if(!ValidatePassword(pw))
                        {
                            lb_ForgetPassword_Notify.ForeColor = Color.FromArgb(245, 108, 108);
                            lb_ForgetPassword_Notify.Text = "*Thông báo: Mật khẩu không hợp lệ!";
                        }
                        else
                        {
                            user.Password = pw;
                            await firebaseClient.Child("Users").Child("User_" + id).PutAsync(user);
                            lb_ForgetPassword_Notify.ForeColor = Color.FromArgb(59, 198, 171);
                            lb_ForgetPassword_Notify.Text = "*Thông báo: Đổi mật khẩu mới thành công!";
                            verifycode = "";
                            RenewForgetPassword();
                        }    
                    }    
                }
            }
        }

        private void btn_CancelForgetPassword_Click(object sender, EventArgs e)
        {
            playSFX();
            OpenLogin();
        }
        private async void btn_ForgetPW_GetCode_Click(object sender, EventArgs e)
        {
            playSFX();
            lb_ForgetPassword_Notify.ForeColor = Color.White;
            lb_ForgetPassword_Notify.Text = "*Đang xử lí";
            lb_ForgetPassword_Notify.Visible = true;
            string id = txt_ForgetPW_ID.TextButton.Trim();
            bool isExists = await IsIdExists(id);
            if (!isExists)
            {
                lb_ForgetPassword_Notify.ForeColor = Color.FromArgb(245, 108, 108);
                lb_ForgetPassword_Notify.Text = "*Thông báo: ID không tồn tại!";
            }
            else
            {
                var dataSnapshot = await firebaseClient.Child("Users").OrderByKey().EqualTo("User_" + id).OnceAsync<Player>();
                foreach (var item in dataSnapshot)
                {
                    var user = item.Object;
                    if (txt_ForgetPW_UserEmail.TextButton != user.Email)
                    {
                        lb_ForgetPassword_Notify.ForeColor = Color.FromArgb(245, 108, 108);
                        lb_ForgetPassword_Notify.Text = "*Thông báo: Email không chính xác!";
                    }
                    else
                    {
                        verifycode = GenerateVerificationCode(6);
                        bool get = await GetVerifyCodeAsync(txt_ForgetPW_UserEmail.TextButton, verifycode, "forgetpw");
                        if (get)
                        {
                            lb_ForgetPassword_Notify.ForeColor = Color.FromArgb(59, 198, 171);
                            lb_ForgetPassword_Notify.Text = "*Thông báo: Gửi mã xác thực thành công";
                        }

                    }    
                }
            }
        }
        #endregion
    }
}
