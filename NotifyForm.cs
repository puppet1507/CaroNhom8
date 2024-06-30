using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace Caro_Nhom8
{
    public partial class NotifyForm : Form
    {
        WindowsMediaPlayer sfx = new WindowsMediaPlayer();
        //int mode;
        public NotifyForm()
        {
            InitializeComponent();
        }
        public NotifyForm(string option)
        {
            InitializeComponent();
            switch (option)
            {
                case "Thắng":
                    lb_Notify.Text = "Xin chúc mừng! Bạn đã thắng";
                    One_Button();
                    break;
                case "Thua":
                    lb_Notify.Text = "Bạn đã thua! Gà!";
                    One_Button();
                    break;
                case "Hòa":
                    lb_Notify.Text = "Trận đấu kết thúc! Hòa!";
                    One_Button();
                    break;
                case "Đầu hàng_1":
                    lb_Notify.Text = "Bạn sẽ bị xử thua nếu đầu hàng lúc này! Đồng ý đầu hàng?";
                    Two_Button();
                    break;
                case "Đầu hàng_2":
                    lb_Notify.Text = "Đối phương đã đầu hàng!";
                    One_Button();
                    break;
                case "Hòa giải_1":
                    lb_Notify.Text = "Bạn muốn hòa giải với đối phương? Gửi yêu cầu hòa giải?";
                    Two_Button();
                    break;
                case "Hòa giải_2":
                    lb_Notify.Text = "Đối phương muốn hòa giải với bạn! Đồng ý hòa giải?";
                    Two_Button();
                    break;
                case "Hòa giải_3":
                    lb_Notify.Text = "Đối phương đã chấp nhận yêu cầu hòa giải!";
                    One_Button();
                    break;
                case "Đấu lại_1":
                    lb_Notify.Text = "Bạn muốn đấu lại với đối phương? Gửi yêu cầu đấu lại?";
                    Two_Button();
                    break;
                case "Đấu lại_2":
                    lb_Notify.Text = "Đối phương muốn đấu lại với bạn! Đồng ý đấu lại?";
                    Two_Button();
                    break;
                case "Đấu lại_0":
                    lb_Notify.Text = "Nếu tạo trận mới ngay lúc này, bạn sẽ bị xử thua! Vẫn tiếp tục chứ?";
                    Two_Button();
                    break;
                case "Thoát_1":
                    lb_Notify.Text = "Bạn đang trong trận, nếu thoát thì sẽ bị xử thua! Tiếp tục thoát?";
                    Two_Button();
                    break;
                case "Thoát_2":
                    lb_Notify.Text = "Đối phương đã thoát";
                    One_Button();
                    break;
                case "UpdateFireBase":
                    lb_Notify.Text = "Đang tải thông tin lên cơ sở dữ liệu";
                    One_Button();
                    break;
                default:
                    break;
            }
        }
        private void One_Button()
        {
            btn_Confirm.Text = "OK!";
            btn_Confirm.Location = new Point(219, 155);
            btn_Cancel.Visible = false;
            btn_Confirm.Visible = true;
        }
        private void Two_Button()
        {
            btn_Confirm.Text = "Đồng ý!";
            btn_Cancel.Visible = true;
            btn_Confirm.Visible = true;
            btn_Confirm.Location = new Point(103, 155);
            btn_Cancel.Location = new Point(342, 155);
        }
        private void btn_Confirm_Click(object sender, EventArgs e)
        {
            sfx.URL = "Resources/Sound/Sfx.wav";
            DialogResult = DialogResult.Yes;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            sfx.URL = "Resources/Sound/Sfx.wav";
            DialogResult = DialogResult.No;
        }
    }
}
