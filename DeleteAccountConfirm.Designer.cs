namespace Caro_Nhom8
{
    partial class DeleteAccountConfirm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label11 = new Label();
            txt_deletecode = new ReaLTaiizor.Controls.CyberTextBox();
            btn_ConfirmDelete = new ReaLTaiizor.Controls.HopeRoundButton();
            btn_CancelDelete = new ReaLTaiizor.Controls.HopeRoundButton();
            label1 = new Label();
            lb_Notify = new Label();
            SuspendLayout();
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            label11.ForeColor = Color.White;
            label11.Location = new Point(12, 32);
            label11.MaximumSize = new Size(600, 0);
            label11.Name = "label11";
            label11.Size = new Size(597, 62);
            label11.TabIndex = 34;
            label11.Text = "Nếu bạn chắc chắc muốn xóa tài khoản, hãy nhập mã xác thực được gửi tới ";
            // 
            // txt_deletecode
            // 
            txt_deletecode.Alpha = 20;
            txt_deletecode.BackColor = Color.Transparent;
            txt_deletecode.Background_WidthPen = 3F;
            txt_deletecode.BackgroundPen = true;
            txt_deletecode.ColorBackground = Color.FromArgb(12, 20, 29);
            txt_deletecode.ColorBackground_Pen = Color.FromArgb(59, 198, 171);
            txt_deletecode.ColorLighting = Color.FromArgb(59, 198, 171);
            txt_deletecode.ColorPen_1 = Color.FromArgb(12, 20, 29);
            txt_deletecode.ColorPen_2 = Color.Transparent;
            txt_deletecode.CyberTextBoxStyle = ReaLTaiizor.Enum.Cyber.StateStyle.Custom;
            txt_deletecode.Font = new Font("Arial", 11F, FontStyle.Regular, GraphicsUnit.Point);
            txt_deletecode.ForeColor = Color.White;
            txt_deletecode.Lighting = false;
            txt_deletecode.LinearGradientPen = false;
            txt_deletecode.Location = new Point(99, 133);
            txt_deletecode.Name = "txt_deletecode";
            txt_deletecode.PenWidth = 15;
            txt_deletecode.RGB = false;
            txt_deletecode.Rounding = true;
            txt_deletecode.RoundingInt = 90;
            txt_deletecode.Size = new Size(433, 45);
            txt_deletecode.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            txt_deletecode.TabIndex = 36;
            txt_deletecode.Tag = "Cyber";
            txt_deletecode.TextButton = "";
            txt_deletecode.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            txt_deletecode.Timer_RGB = 300;
            // 
            // btn_ConfirmDelete
            // 
            btn_ConfirmDelete.BorderColor = Color.FromArgb(220, 223, 230);
            btn_ConfirmDelete.ButtonType = ReaLTaiizor.Util.HopeButtonType.Primary;
            btn_ConfirmDelete.DangerColor = Color.FromArgb(245, 108, 108);
            btn_ConfirmDelete.DefaultColor = Color.FromArgb(255, 255, 255);
            btn_ConfirmDelete.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btn_ConfirmDelete.HoverTextColor = Color.FromArgb(48, 49, 51);
            btn_ConfirmDelete.InfoColor = Color.FromArgb(144, 147, 153);
            btn_ConfirmDelete.Location = new Point(99, 184);
            btn_ConfirmDelete.Name = "btn_ConfirmDelete";
            btn_ConfirmDelete.PrimaryColor = Color.FromArgb(59, 198, 171);
            btn_ConfirmDelete.Size = new Size(189, 31);
            btn_ConfirmDelete.SuccessColor = Color.FromArgb(103, 194, 58);
            btn_ConfirmDelete.TabIndex = 35;
            btn_ConfirmDelete.Text = "Xóa";
            btn_ConfirmDelete.TextColor = Color.White;
            btn_ConfirmDelete.WarningColor = Color.FromArgb(230, 162, 60);
            btn_ConfirmDelete.Click += btn_ConfirmDelete_Click;
            // 
            // btn_CancelDelete
            // 
            btn_CancelDelete.BorderColor = Color.FromArgb(220, 223, 230);
            btn_CancelDelete.ButtonType = ReaLTaiizor.Util.HopeButtonType.Primary;
            btn_CancelDelete.DangerColor = Color.FromArgb(245, 108, 108);
            btn_CancelDelete.DefaultColor = Color.FromArgb(255, 255, 255);
            btn_CancelDelete.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btn_CancelDelete.HoverTextColor = Color.FromArgb(48, 49, 51);
            btn_CancelDelete.InfoColor = Color.FromArgb(144, 147, 153);
            btn_CancelDelete.Location = new Point(343, 184);
            btn_CancelDelete.Name = "btn_CancelDelete";
            btn_CancelDelete.PrimaryColor = Color.FromArgb(245, 108, 108);
            btn_CancelDelete.Size = new Size(189, 31);
            btn_CancelDelete.SuccessColor = Color.FromArgb(103, 194, 58);
            btn_CancelDelete.TabIndex = 37;
            btn_CancelDelete.Text = "Hủy";
            btn_CancelDelete.TextColor = Color.White;
            btn_CancelDelete.WarningColor = Color.FromArgb(230, 162, 60);
            btn_CancelDelete.Click += btn_CancelDelete_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 13.8F, FontStyle.Italic, GraphicsUnit.Point);
            label1.ForeColor = Color.Lime;
            label1.Location = new Point(200, 63);
            label1.MaximumSize = new Size(600, 0);
            label1.Name = "label1";
            label1.Size = new Size(69, 31);
            label1.TabIndex = 38;
            label1.Text = "email";
            // 
            // lb_Notify
            // 
            lb_Notify.AutoSize = true;
            lb_Notify.ForeColor = Color.FromArgb(245, 108, 108);
            lb_Notify.Location = new Point(99, 229);
            lb_Notify.Name = "lb_Notify";
            lb_Notify.Size = new Size(90, 20);
            lb_Notify.TabIndex = 45;
            lb_Notify.Text = "*Thông báo:";
            lb_Notify.Visible = false;
            // 
            // DeleteAccountConfirm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(12, 20, 29);
            ClientSize = new Size(618, 307);
            Controls.Add(lb_Notify);
            Controls.Add(label1);
            Controls.Add(btn_CancelDelete);
            Controls.Add(txt_deletecode);
            Controls.Add(btn_ConfirmDelete);
            Controls.Add(label11);
            Name = "DeleteAccountConfirm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DeleteAccountConfirm";
            Load += DeleteAccountConfirm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label11;
        private ReaLTaiizor.Controls.CyberTextBox txt_deletecode;
        private ReaLTaiizor.Controls.HopeRoundButton btn_ConfirmDelete;
        private ReaLTaiizor.Controls.HopeRoundButton btn_CancelDelete;
        private Label label1;
        private Label lb_Notify;
    }
}