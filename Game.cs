using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caro_Nhom8
{
    public class Game
    {
        public string? Game_ID { get; set; }
        public string? Player1 { get; set; }
        public int Player1_Wins { get; set; }
        public string? Player2 { get; set; }
        public int Player2_Wins { get; set; }
        public string? Size { get; set; }
        public string? StartTime { get; set; }
        public Game()
        {
            Game_ID = "";
            Player1 = "";
            Player1_Wins = 0;
            Player2 = "";
            Player2_Wins = 0;
            Size = "";
            StartTime = "";
        }
        public Game(string gameid, string pl1, int pl1w, string pl2, int pl2w, string sz, string stime)
        {
            Game_ID = gameid;
            Player1 = pl1;
            Player1_Wins = pl1w;
            Player2 = pl2;
            Player2_Wins = pl2w;
            Size = sz;
            StartTime = stime;
        }
    }
}
