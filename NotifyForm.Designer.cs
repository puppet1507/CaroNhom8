namespace Caro_Nhom8
{
    partial class NotifyForm
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
            btn_Cancel = new ReaLTaiizor.Controls.HopeRoundButton();
            btn_Confirm = new ReaLTaiizor.Controls.HopeRoundButton();
            lb_Notify = new Label();
            SuspendLayout();
            // 
            // btn_Cancel
            // 
            btn_Cancel.BorderColor = Color.FromArgb(220, 223, 230);
            btn_Cancel.ButtonType = ReaLTaiizor.Util.HopeButtonType.Primary;
            btn_Cancel.DangerColor = Color.FromArgb(245, 108, 108);
            btn_Cancel.DefaultColor = Color.FromArgb(255, 255, 255);
            btn_Cancel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btn_Cancel.HoverTextColor = Color.FromArgb(48, 49, 51);
            btn_Cancel.InfoColor = Color.FromArgb(144, 147, 153);
            btn_Cancel.Location = new Point(341, 165);
            btn_Cancel.Name = "btn_Cancel";
            btn_Cancel.PrimaryColor = Color.FromArgb(245, 108, 108);
            btn_Cancel.Size = new Size(189, 31);
            btn_Cancel.SuccessColor = Color.FromArgb(103, 194, 58);
            btn_Cancel.TabIndex = 40;
            btn_Cancel.Text = "Hủy";
            btn_Cancel.TextColor = Color.White;
            btn_Cancel.WarningColor = Color.FromArgb(230, 162, 60);
            btn_Cancel.Click += btn_Cancel_Click;
            // 
            // btn_Confirm
            // 
            btn_Confirm.BorderColor = Color.FromArgb(220, 223, 230);
            btn_Confirm.ButtonType = ReaLTaiizor.Util.HopeButtonType.Primary;
            btn_Confirm.DangerColor = Color.FromArgb(245, 108, 108);
            btn_Confirm.DefaultColor = Color.FromArgb(255, 255, 255);
            btn_Confirm.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btn_Confirm.HoverTextColor = Color.FromArgb(48, 49, 51);
            btn_Confirm.InfoColor = Color.FromArgb(144, 147, 153);
            btn_Confirm.Location = new Point(103, 165);
            btn_Confirm.Name = "btn_Confirm";
            btn_Confirm.PrimaryColor = Color.FromArgb(59, 198, 171);
            btn_Confirm.Size = new Size(189, 31);
            btn_Confirm.SuccessColor = Color.FromArgb(103, 194, 58);
            btn_Confirm.TabIndex = 39;
            btn_Confirm.Text = "Đồng ý";
            btn_Confirm.TextColor = Color.White;
            btn_Confirm.WarningColor = Color.FromArgb(230, 162, 60);
            btn_Confirm.Click += btn_Confirm_Click;
            // 
            // lb_Notify
            // 
            lb_Notify.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point);
            lb_Notify.ForeColor = Color.White;
            lb_Notify.Location = new Point(103, 51);
            lb_Notify.MaximumSize = new Size(427, 0);
            lb_Notify.Name = "lb_Notify";
            lb_Notify.Size = new Size(427, 62);
            lb_Notify.TabIndex = 38;
            lb_Notify.Text = "Bạn sẽ bị xử thua nếu đầu hàng lúc này! Đồng ý đầu hàng?";
            lb_Notify.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // NotifyForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(12, 20, 29);
            ClientSize = new Size(630, 281);
            Controls.Add(btn_Cancel);
            Controls.Add(btn_Confirm);
            Controls.Add(lb_Notify);
            Name = "NotifyForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Thông báo";
            ResumeLayout(false);
        }

        #endregion

        private ReaLTaiizor.Controls.HopeRoundButton btn_Cancel;
        private ReaLTaiizor.Controls.HopeRoundButton btn_Confirm;
        private Label lb_Notify;
    }
}