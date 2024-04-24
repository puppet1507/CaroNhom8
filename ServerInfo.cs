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
        #region ServerInfo
        void OpenServerInfo()
        {
            this.Size = new Size(755, 658);
            grb_ServerInfo.Size = new Size(726, 601);
            grb_ServerInfo.Location = new Point(6, 6);
            lb_BattleInfo_Notify.Visible = false;
            grb_Login.Visible = false;
            grb_SignUp.Visible = false;
            grb_Info.Visible = false;
            grb_Waiting.Visible = false;
            grb_ClientInfo.Visible = false;
            grb_ComputerInfo.Visible = false;
            grb_ServerInfo.Visible = true;
            grb_ForgetPassword.Visible = false;
            grb_ChangePassword.Visible = false;
            panel_PlayArea.Dock = DockStyle.None;
            panel_PlayArea.Visible = false;
        }
        private void btn_ContinueCreateServer_Click(object sender, EventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                OPenWaiting();
                server = new SimpleTcpServer("127.0.0.1", int.Parse(txt_PVP_ServerPort.TextButton));
                server.Start();
                server.Events.ClientConnected += Server_Events_ClientConnected;
                server.Events.ClientDisconnected += Server_Events_ClientDisconnected;
                server.Events.DataSent += Server_Events_DataSent;
                server.Events.DataReceived += Server_Events_DataReceived;
                isServer = true;

            });
        }
        private void btn_CancelCreateServer_Click(object sender, EventArgs e)
        {
            OpenInfo();

        }
        #endregion
    }
}
