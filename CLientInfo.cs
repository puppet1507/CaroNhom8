﻿using SuperSimpleTcp;

namespace Caro_Nhom8
{
    //Client kết nối với server
    public partial class MainForm
    {
        #region ClientInfo
        void OpenConnect()
        {
            this.Size = new Size(755, 658);
            this.MaximumSize = new Size(755, 658);
            this.MinimumSize = new Size(755, 658);
            grb_ClientInfo.Dock = DockStyle.Fill;
            lb_ClientInfo_Notify.Visible = false;
            grb_Login.Visible = false;
            grb_SignUp.Visible = false;
            grb_Info.Visible = false;
            grb_Waiting.Visible = false;
            grb_ClientInfo.Visible = true;
            grb_ComputerInfo.Visible = false;
            grb_ServerInfo.Visible = false;
            grb_ForgetPassword.Visible = false;
            grb_ChangeInfo.Visible = false;
            grb_Setting.Visible = false;
            panel_PlayArea.Dock = DockStyle.None;
            panel_PlayArea.Visible = false;
        }
        private void btn_Connect_Click(object sender, EventArgs e)
        {
            playSFX();
            this.Invoke((MethodInvoker)delegate
            {
                client = new SimpleTcpClient(txt_ClientInfo_IP.TextButton,int.Parse(txt_ClientInfo_Port.TextButton));
                client.Events.Connected += Client_Events_Connected;
                client.Events.Disconnected += Client_Events_Disconnected;
                client.Events.DataSent += Client_Events_DataSent;
                client.Events.DataReceived += Client_Events_DataReceived;
                client.Connect();

            });
        }
        private void btn_CancelJoinRoom_Click(object sender, EventArgs e)
        {
            playSFX();
            OpenInfo();
        }
        #endregion
    }
}
