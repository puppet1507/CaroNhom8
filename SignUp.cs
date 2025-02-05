﻿using Firebase.Database;
using Firebase.Database.Query;
using System.Text;
using System.Text.RegularExpressions;
using System;
using System.Net.Mail;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using Microsoft.VisualBasic;
using System.Numerics;
using Microsoft.VisualBasic.ApplicationServices;

namespace Caro_Nhom8
{
    // Đăng kí
    public partial class MainForm
    {
        #region SignUp
        public string verifycode = "";
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
        private async void btn_ConfirmSignUp_Click(object sender, EventArgs e)
        {
            playSFX();
            lb_SignUp_Notify.Visible = true;
            lb_SignUp_Notify.ForeColor = Color.White;
            lb_SignUp_Notify.Text = "*Đang xử lí";
            string email = txt_SignUp_Email.TextButton.Trim();
            string id = txt_SignUp_ID.TextButton.Trim();
            bool isExists = await IsIdExists(id);
            string name = txt_SignUp_Name.TextButton.Trim();
            string password = txt_SignUp_PW.TextButton.Trim();
            string protecode = txt_SignUp_ProtectionCode.TextButton.Trim();
            
            if (!IsValidID(id))
            {
                lb_SignUp_Notify.ForeColor = Color.FromArgb(245, 108, 108);
                lb_SignUp_Notify.Text = "*Thông báo: ID không hợp lệ!";
            }    
            else if(isExists)
            {
                lb_SignUp_Notify.ForeColor = Color.FromArgb(245, 108, 108);
                lb_SignUp_Notify.Text = "*Thông báo: ID đã tồn tại!";
            }
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
            else if(verifycode == "none")
            {
                lb_SignUp_Notify.ForeColor = Color.FromArgb(245, 108, 108);
                lb_SignUp_Notify.Text = "*Thông báo: Hãy tạo mã xác thực!";
            }
            else if(txt_Signup_Verifycode.TextButton != verifycode)
            {
                lb_SignUp_Notify.ForeColor = Color.FromArgb(245, 108, 108);
                lb_SignUp_Notify.Text = "*Thông báo: Mã xác thực không đúng!";
            }    
            else if (!ValidatePassword(password))
            {
                lb_SignUp_Notify.ForeColor = Color.FromArgb(245, 108, 108);
                lb_SignUp_Notify.Text = "*Thông báo: Mật khẩu không hợp lệ!";
            }
            else if (!ValidateProtectionCode(protecode))
            {
                lb_SignUp_Notify.ForeColor = Color.FromArgb(245, 108, 108);
                lb_SignUp_Notify.Text = "*Thông báo: Mã bảo vệ chỉ được chứa số!";
            }
            else
            {
                Player newUser = new Player
                {
                    ID = id,
                    Name = name,
                    Avatar = currentAvatar,
                    Email = email,
                    Password = password,
                    ProtectionCode = protecode,
                    Win = 0,
                    Lose = 0,
                    Winrate = "0%",
                };
                await firebaseClient.Child("Users").Child("User_"+id).PutAsync(newUser);
                lb_SignUp_Notify.ForeColor = Color.FromArgb(59, 198, 171);
                lb_SignUp_Notify.Text = "*Thông báo: Đăng kí thành công!";
                verifycode = "";
            }    
        }
        
        private void btn_ExitSignUp_Click(object sender, EventArgs e)
        {
            playSFX();
            isChooseAvatarSignUp = false;
            OpenLogin();
            RenewLogin();
        }

        private void lb_ChangeAvatar_SignUp_Click(object sender, EventArgs e)
        {
            playSFX();
            OpenChooseAvatar();
        }
        private async void btn_Getverifycode_Click(object sender, EventArgs e)
        {
            playSFX();
            lb_SignUp_Notify.Visible = true;
            lb_SignUp_Notify.ForeColor = Color.White;
            lb_SignUp_Notify.Text = "*Đang xử lí";
            if (!ValidateEmail(txt_SignUp_Email.TextButton))
            {
                lb_SignUp_Notify.ForeColor = Color.FromArgb(245, 108, 108);
                lb_SignUp_Notify.Text = "*Thông báo: Email không hợp lệ!";
                
            }
            else
            {
                verifycode = GenerateVerificationCode(6);
                bool get = await GetVerifyCodeAsync(txt_SignUp_Email.TextButton, verifycode, "register");
                if (get)
                {
                    lb_SignUp_Notify.ForeColor = Color.FromArgb(59, 198, 171);
                    lb_SignUp_Notify.Text = "*Thông báo: Gửi mã xác thực thành công";
                }
            }    
        }
        #endregion
    }
}
