using Firebase.Database;

namespace Caro_Nhom8
{
    public class Player
    {
        public string? Avatar { get; set; }
        public string? Email { get; set; }
        public string? ID { get; set; }
        public int Lose { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? ProtectionCode { get; set; }
        public int Win { get; set; }
        public string? Winrate { get; set; }

        public Player()
        {
            Avatar = "";
            Email = "";
            ID = "";
            Lose = 0;
            Name = "";
            Password = "";
            ProtectionCode = "";
            Win = 0;
            Winrate = "";
        }

        public Player(string avatar, string email, string id, int lose, string name, string password, string protectionCode, int win, string winrate)
        {
            // Phương thức khởi tạo với các tham số
            // Bạn có thể thêm mã logic khởi tạo ở đây (nếu cần)
            Avatar = avatar;
            Email = email;
            ID = id;
            Lose = lose;
            Name = name;
            Password = password;
            ProtectionCode = protectionCode;
            Win = win;
            Winrate = winrate;
        }
    }
}