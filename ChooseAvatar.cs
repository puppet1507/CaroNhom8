using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caro_Nhom8
{
    public partial class MainForm
    {
        
        #region ChooseAvatar
        void OpenChooseAvatar()
        {
            this.Size = new Size(755, 658);
            this.MaximumSize = new Size(755, 658);
            this.MinimumSize = new Size(755, 658);
            grb_ChooseAvatar.Dock = DockStyle.Fill;
            grb_ChooseAvatar.Visible = true;
            lb_Login_Notify.Visible = false;
            grb_Login.Visible = false;
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
        private void LoadAvatars()
        {
            string[] allAvatarFiles = Directory.GetFiles(Path.Combine(Application.StartupPath, "Resources/UI_Icon"), "*.png");

            // Lọc danh sách tệp theo định dạng mong muốn
            List<string> avatarFiles = new List<string>();
            foreach (string avatarFile in allAvatarFiles)
            {
                string fileName = Path.GetFileName(avatarFile);
                avatarFiles.Add("Resources/UI_Icon/"+fileName);
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
                currentAvatarSignUp = pictureBox.Tag.ToString()!;
                OpenSignUp();

            }
            else
            {
                picbox_ChangeInfo_Avatar.Image = Image.FromFile(pictureBox.Tag.ToString()!);
                OpenChangePassword();
            }    
        }
        #endregion
    }
}
