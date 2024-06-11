using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using System.Xml.Linq;

namespace Caro_Nhom8
{
    public class Chat
    {
        public string? Chat_ID { get; set; }
        public string? Game_ID { get; set; }
        public string? Message { get; set; }
        public string? Sender { get; set; }
        public string? Receiver { get; set; }
        public Chat()
        {
            Chat_ID = "";
            Game_ID = "";
            Message = "";
            Sender = "";
            Receiver = "";
        }
        public Chat(string chatid, string gameid, string msg, string sdr, string rcvr)
        {
            Chat_ID = chatid;
            Game_ID = gameid;
            Message = msg;
            Sender = sdr;
            Receiver = rcvr;
        }
    }
}
