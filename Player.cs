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
        public int Winrate { get; set; }

    }
}