using Firebase.Database.Query;
using ReaLTaiizor.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Caro_Nhom8
{
    public partial class MainForm
    {
        #region AvatarMethod
        private void LoadAvatars()
        {
            string[] allAvatarFiles = Directory.GetFiles(Path.Combine(Application.StartupPath, "Resources/UI_Icon"), "*.png");

            // Lọc danh sách tệp theo định dạng mong muốn
            List<string> avatarFiles = new List<string>();
            foreach (string avatarFile in allAvatarFiles)
            {
                string fileName = Path.GetFileName(avatarFile);
                avatarFiles.Add("Resources/UI_Icon/" + fileName);
            }
            foreach (string avatarFile in avatarFiles)
            {
                RJCircularPictureBox pictureBox = new RJCircularPictureBox();
                pictureBox.Image = System.Drawing.Image.FromFile(avatarFile);
                pictureBox.Width = 50;
                pictureBox.Height = 50;
                pictureBox.BorderColor = Color.FromArgb(59, 198, 171);
                pictureBox.BorderColor2 = Color.FromArgb(59, 198, 171);
                pictureBox.BorderSize = 1;
                pictureBox.Tag = avatarFile.ToString();
                pictureBox.Margin = new Padding(5);
                pictureBox.Click += PictureBox_Click;

                panel_ChooseAvatar.Controls.Add(pictureBox);
            }
        }
        private void PictureBox_Click(object? sender, EventArgs e)
        {
            playSFX();
            RJCircularPictureBox pictureBox = (RJCircularPictureBox)sender!;
            pictureBox.Cursor = Cursors.Hand;
            if (isChooseAvatarSignUp)
            {
                picbox_SignUp_Avatar.Image = Image.FromFile(pictureBox.Tag.ToString()!);
                currentAvatar = pictureBox.Tag.ToString()!;
                OpenSignUp();

            }
            else
            {
                picbox_ChangeInfo_Avatar.Image = Image.FromFile(pictureBox.Tag.ToString()!);
                currentAvatar = pictureBox.Tag.ToString()!;
                OpenChangePassword();
            }
        }
        #endregion
        #region LoginMethod
        void RenewLogin()
        {
            txt_Login_ID.TextButton = currentplayer.ID;
            txt_Login_PW.TextButton = "";
        }
        #endregion

        #region SignUpMethod
        void RenewSignUp()
        {
            txt_SignUp_ID.TextButton = "";
            txt_SignUp_Name.TextButton = "";
            txt_SignUp_Email.TextButton = "";
            txt_SignUp_PW.TextButton = "";
            txt_SignUp_ProtectionCode.TextButton = "";
            picbox_SignUp_Avatar.Image = Image.FromFile("Resources/UI_Icon/Default.png");
            currentAvatar = "Resources/UI_Icon/Default.png";
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
        public static bool ValidateProtectionCode(string password)
        {
            string pattern = @"^[0-9]+$";
            bool isMatch = Regex.IsMatch(password, pattern);
            return isMatch;
        }
        private async Task<bool> IsIdExists(string id)
        {
            var data = await firebaseClient.Child("Users").Child("User_" + id).OnceAsync<object>();
            return data.Any();
        }
        private async Task<bool> IsPasswordTrue(string id, string pw)
        {
            var dataSnapshot = await firebaseClient.Child("Users").OrderByKey().EqualTo("User_" + id).OnceAsync<Player>();
            foreach (var item in dataSnapshot)
            {
                var user = item.Object;
                if (pw == user.Password)
                {
                    currentplayer!.ID = user.ID;
                    currentplayer.Password = user.Password;
                    currentplayer.Name = user.Name;
                    currentplayer.Win = user.Win;
                    currentplayer.Lose = user.Lose;
                    currentplayer.Avatar = user.Avatar;
                    currentplayer.Winrate = user.Winrate;
                    currentplayer.ProtectionCode = user.ProtectionCode;
                    currentplayer.Email = user.Email;
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region PlayerInfoMethod
        void RenewPlayerInfo()
        {
            picbox_PlayerInfo_Avatar.Image = Image.FromFile(currentplayer.Avatar!);
            lb_Win.ForeColor = Color.FromArgb(59, 198, 171);
            lb_Win.Text = "Win: " + currentplayer!.Win.ToString();
            lb_Lose.ForeColor = Color.FromArgb(245, 108, 108);
            lb_Lose.Text = "Lose: " + currentplayer.Lose.ToString();
            lb_Welcome.Text = currentplayer.Name;
        }
        #endregion

        #region ChangeInfoMethod
        void RenewChangeInfo()
        {
            txt_ChangeInfo_Name.TextButton = currentplayer!.Name;
            txt_ChangeInfo_ID.TextButton = currentplayer.ID;
            txt_ChangeInfo_Email.TextButton = currentplayer.Email;
            picbox_ChangeInfo_Avatar.Image = Image.FromFile(currentplayer.Avatar!);
            txt_ChangeInfo_NewPW.TextButton = "";
            txt_ChangeInfo_ProtectionCode.TextButton = "";
        }
        #endregion

        #region SettingMethod
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
                        if (controlchild is SkyComboBox comboBox)
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
