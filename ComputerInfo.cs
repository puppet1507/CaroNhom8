using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caro_Nhom8
{
    #region ComputerInfo
    public partial class MainForm
    {
        void OpenComputerInfo()
        {
            this.Size = new Size(755, 658);
            this.MaximumSize = new Size(755, 658);
            this.MinimumSize = new Size(755, 658);
            grb_ComputerInfo.Dock = DockStyle.Fill;
            grb_Login.Visible = false;
            grb_SignUp.Visible = false;
            grb_Info.Visible = false;
            grb_Waiting.Visible = false;
            grb_ClientInfo.Visible = false;
            grb_ComputerInfo.Visible = true;
            grb_ServerInfo.Visible = false;
            grb_ForgetPassword.Visible = false;
            grb_ChangeInfo.Visible = false;
            grb_Setting.Visible = false;
            panel_PlayArea.Dock = DockStyle.None;
            panel_PlayArea.Visible = false;
        }
        private void btn_PVC_Start_Click(object sender, EventArgs e)
        {
            playSFX();
            grs!.Clear(panel_PlayArea_Board.BackColor);
            caroChess!.StartPvC(grs!);
            tmCoolDown.Start();
            prcbCoolDown.Value = 0;
            OpenPlayArea();
        }

        private void btn_PVC_Cancel_Click(object sender, EventArgs e)
        {
            playSFX();
            OpenInfo();
        }
    }
    #endregion
}
