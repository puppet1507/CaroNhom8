using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace Caro_Nhom8
{
    public partial class DeleteAccountConfirm : Form
    {
        string vcode = "";
        WindowsMediaPlayer sfx = new WindowsMediaPlayer();
        public DeleteAccountConfirm()
        {
            InitializeComponent();
        }
        public DeleteAccountConfirm(string email, string code)
        {
            InitializeComponent();
            label1.Text = email;
            vcode = code;
        }
        private async void DeleteAccountConfirm_Load(object sender, EventArgs e)
        {
            vcode = GenerateVerificationCode(6);
            bool get = await GetVerifyCodeAsync(label1.Text, vcode);

        }
        private void btn_ConfirmDelete_Click(object sender, EventArgs e)
        {
            sfx.URL = "Resources/Sound/Sfx.wav";
            if (txt_deletecode.TextButton != vcode)
            {
                lb_Notify.Visible = true;
                lb_Notify.ForeColor = Color.FromArgb(245, 108, 108);
                lb_Notify.Text = "*Thông báo: Mã xác thực không chính xác!";
            }
            else
            {
                DialogResult = DialogResult.Yes;
            }

        }

        private void btn_CancelDelete_Click(object sender, EventArgs e)
        {
            sfx.URL = "Resources/Sound/Sfx.wav";
            DialogResult = DialogResult.No;
        }
        public string GenerateVerificationCode(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            StringBuilder result = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                result.Append(chars[random.Next(chars.Length)]);
            }

            return result.ToString();
        }
        private async Task<bool> GetVerifyCodeAsync(string email, string verificationCode)
        {
            string code = verificationCode;
            string htmlTemplate = File.ReadAllText("Templates/Delete.html");
            string emailBody = htmlTemplate.Replace("[[CODE]]", code);
            string to = email;
            string from = "carogroup8@gmail.com";
            string password = "gmet uyro ltev vqhe";

            using (MailMessage message = new MailMessage())
            {
                message.To.Add(to);
                message.From = new MailAddress(from);
                message.Subject = "Verification code - Caro Group8";
                message.Body = emailBody;
                message.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Credentials = new NetworkCredential(from, password);

                    try
                    {
                        await smtp.SendMailAsync(message);
                        return true;
                    }
                    catch (SmtpException smtpEx)
                    {
                        lb_Notify.ForeColor = Color.FromArgb(245, 108, 108);
                        lb_Notify.Text = "*Thông báo: " + smtpEx.Message;
                        return false;
                    }
                    catch (Exception ex)
                    {
                        lb_Notify.ForeColor = Color.FromArgb(245, 108, 108);
                        lb_Notify.Text = "*Thông báo: " + ex.Message;
                        return false;
                    }
                }
            }
        }

        
    }
}
