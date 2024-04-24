using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caro_Nhom8
{
    class Board
    {
        private int _NumOfLines;
        private int _NumOfColumns;
        public int NumOfLines
        {
            get { return _NumOfLines; }
            set {_NumOfLines = value;}
        }
        public int NumOfColumns
        {
            get {return _NumOfColumns;}
            set{_NumOfColumns = value;}
        }
        public Board()
        {
            NumOfColumns = 0;
            NumOfLines =  0;
        }
        public Board(int numOfLines, int numOFColumns)
        {
            NumOfColumns = numOFColumns;
            NumOfLines = numOfLines;
        }
        public void DrawChessBoard(Graphics g)
        {
            int cellSize = 640 / NumOfColumns; // Kích thước của mỗi ô

            using (Pen pen = new Pen(Color.FromArgb(44, 62, 80)))
            {
                // Vẽ nền
                g.Clear(Color.FromArgb(12, 20, 29));

                // Vẽ đường kẻ
                for (int i = 0; i <= NumOfColumns; i++)
                {
                    g.DrawLine(pen, i * cellSize, 0, i * cellSize, 640);
                }

                for (int j = 0; j <= NumOfLines; j++)
                {
                    g.DrawLine(pen, 0, j * cellSize, 640, j * cellSize);
                }
            }
            
        }

        // Vẽ quân cờ
        public void DrawChess(Graphics g, Point point, Image img)
        {
            int cellSize = 640 / NumOfColumns; // Kích thước của mỗi ô

            g.DrawImage(img, point.X +1, point.Y +1, cellSize-2, cellSize-2);
        }

        // Xóa quân cờ
        public void RemoveChess(Graphics g, Point point, SolidBrush sb)
        {
            int cellSize = 640 / NumOfColumns; // Kích thước của mỗi ô

            g.FillRectangle(sb, point.X +1, point.Y +1, cellSize-2, cellSize-2);
        }
       
    }
}
