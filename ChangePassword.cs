using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caro_Nhom8
{
    public partial class MainForm
    {
        #region ChangePassword
        void OpenChangePassword()
        {
            this.Size = new Size(755, 658);
            grb_ChangePassword.Size = new Size(726, 601);
            grb_ChangePassword.Location = new Point(6, 6);
            grb_Login.Visible = false;
            grb_SignUp.Visible = false;
            grb_Info.Visible = false;
            grb_Waiting.Visible = false;
            grb_ClientInfo.Visible = false;
            grb_BattleInfo.Visible = false;
            grb_ForgetPassword.Visible = false;
            grb_ChangePassword.Visible = true;
            panel_PlayArea.Dock = DockStyle.None;
            panel_PlayArea.Visible = false;
        }
        private void btn_CancelChangePassword_Click(object sender, EventArgs e)
        {
            OpenInfo();
        }
        #endregion
    }
}
