﻿using SuperSimpleTcp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caro_Nhom8
{
    public partial class MainForm
    {
        #region BattleInfo
        void OpenBattleInfo()
        {
            this.Size = new Size(755, 658);
            grb_BattleInfo.Size = new Size(726, 601);
            grb_BattleInfo.Location = new Point(6, 6);
            grb_Login.Visible = false;
            grb_SignUp.Visible = false;
            grb_Info.Visible = false;
            grb_Waiting.Visible = false;
            grb_ClientInfo.Visible = false;
            grb_BattleInfo.Visible = true;
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
                server = new SimpleTcpServer("127.0.0.1", int.Parse(txt_ServerPort.TextButton));
                server.Start();
                server.Events.ClientConnected += Server_Events_ClientConnected;
                server.Events.ClientDisconnected += Server_Events_ClientDisconnected;
                server.Events.DataSent += Server_Events_DataSent;
                server.Events.DataReceived += Server_Events_DataReceived;
                isServer = true;

            });
        }

        private void btn_CancelBattleInfo_Click(object sender, EventArgs e)
        {
            OpenInfo();
        }
        
        #endregion
    }
}
