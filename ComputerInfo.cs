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
            grb_ComputerInfo.Size = new Size(726, 601);
            grb_ComputerInfo.Location = new Point(6, 6);
            grb_Login.Visible = false;
            grb_SignUp.Visible = false;
            grb_Info.Visible = false;
            grb_Waiting.Visible = false;
            grb_ClientInfo.Visible = false;
            grb_ComputerInfo.Visible = true;
            grb_ServerInfo.Visible = false;
            grb_ForgetPassword.Visible = false;
            grb_ChangePassword.Visible = false;
            panel_PlayArea.Dock = DockStyle.None;
            panel_PlayArea.Visible = false;
        }
        private void btn_PVC_Start_Click(object sender, EventArgs e)
        {
            grs!.Clear(fpanel_Board.BackColor);
            caroChess!.StartPvC(grs!);
            tmCoolDown.Start();
            prcbCoolDown.Value = 0;
            OpenPlayArea();
        }

        private void btn_PVC_Cancel_Click(object sender, EventArgs e)
        {
            OpenInfo();
        }
    }
    #endregion
}
