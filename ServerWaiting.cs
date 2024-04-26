namespace Caro_Nhom8
{
    // Đăng kí
    public partial class MainForm
    {
        #region ServerWaiting
        void OPenWaiting()
        {
            this.Size = new Size(755, 658);
            grb_Waiting.Size = new Size(726, 601);
            grb_Waiting.Location = new Point(6, 6);
            grb_Login.Visible = false;
            grb_SignUp.Visible = false;
            grb_Info.Visible = false;
            grb_Waiting.Visible = true;
            grb_ClientInfo.Visible = false;
            grb_ComputerInfo.Visible = false;
            grb_ServerInfo.Visible = false;
            grb_ForgetPassword.Visible = false;
            grb_ChangePassword.Visible = false;
            panel_PlayArea.Dock = DockStyle.None;
            panel_PlayArea.Visible = false;
        }
        private void btn_CancelWaitingPlayer_Click(object sender, EventArgs e)
        {
            if (isSFX)
            {
                sfx.URL = "Resources/Sound/Sfx.wav";
            }
            this.Invoke((MethodInvoker)delegate
            {
                try
                {
                    server!.Stop();
                    server.Dispose();
                }
                catch (Exception)
                {
                }
                OpenServerInfo();

            });
        }
        #endregion
    }
}
