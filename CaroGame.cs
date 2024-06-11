using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using WMPLib;
namespace Caro_Nhom8
{
    class CaroGame
    {
        public enum END
        {
            Draw,
            Player1,
            Player2
        }
        private END _end;
        public static Pen? pen;
        public static SolidBrush? sbPnl;
        private Chess[,] arrChessPiece;
        private Board chessBoard;
        private Stack<Chess> stkChessUsed;
        private Stack<Chess> stkChessUndo;
        private int turn;
        private bool ready;

        private int mode;
        // mode = 1 => PvP 
        // mode = 2 => PvC

        //public System.Drawing.Image ImageO = new Bitmap("Resources/UI_Icon/o.png");
        //public System.Drawing.Image ImageX = new Bitmap("Resources/UI_Icon/x.png");

        public bool Ready
        {
            get { return ready; }
            set { ready = value; }
        }
        public int Turn
        {
            get { return turn; }
            
        }
    
        public int Mode
        {
            get{return mode;}
        }
        public CaroGame(Board board)
        {
            pen = new Pen(Color.FromArgb(44, 62, 80));
            //sbPnl = new SolidBrush(Color.FromArgb(12, 20, 29));
            chessBoard = board;
            arrChessPiece = new Chess[chessBoard.NumOfLines, chessBoard.NumOfColumns];
            stkChessUsed = new Stack<Chess>();
            stkChessUndo = new Stack<Chess>();
            turn = 1;
        }
        // Vẽ bàn cờ
        public void DrawChessBoard(Graphics g)
        {
            chessBoard.DrawChessBoard(g);
            
        }

        // Tạo bàn cờ bằng mảng 2 chiều
        public void CreateChessPieces()
        {
            int cellSize = 640 / chessBoard.NumOfColumns;
            for (int i = 0; i < chessBoard.NumOfLines; i++)
            {
                for (int j = 0; j < chessBoard.NumOfColumns; j++)
                {
                    arrChessPiece[i, j] = new Chess(i, j, new Point(j * cellSize, i * cellSize), 0);
                }
            }
        }

        // Phương thức đánh cờ
        public bool PlayChess(int mouseX, int mouseY, Graphics g, Image firstchess, Image secondchess)
        {
            int cellSize = 640 / chessBoard.NumOfColumns;
            if (mouseX % cellSize == 0 || mouseY % cellSize == 0)
                return false;
            int column = mouseX / cellSize;
            int row = mouseY / cellSize;

            if (arrChessPiece[row, column].Owner != 0)
                return false;
            switch (turn)
            {
                case 1:
                    arrChessPiece[row, column].Owner = 1;
                    chessBoard.DrawChess(g, arrChessPiece[row, column].Position, firstchess);
                    turn = 2;
                    break;
                case 2:
                    arrChessPiece[row, column].Owner = 2;
                    chessBoard.DrawChess(g, arrChessPiece[row, column].Position, secondchess);
                    turn = 1;
                    break;
                default:
                    MessageBox.Show("Error!!");
                    break;
            }
            stkChessUndo = new Stack<Chess>();
            Chess tmp = arrChessPiece[row, column];
            Chess cp = new Chess(tmp.Row, tmp.Column, tmp.Position, tmp.Owner);
            stkChessUsed.Push(cp);

            return true;
        }

        public void RepaintChess(Graphics g, Image firstchess, Image secondchess)
        {
            foreach (Chess cp in stkChessUsed)
            {
                if (cp.Owner == 1)
                    chessBoard.DrawChess(g, cp.Position, firstchess);
                else if (cp.Owner == 2)
                    chessBoard.DrawChess(g, cp.Position, secondchess);

            }
        }
        public void StartPvC(Graphics g)
        {
            ready = true;
            stkChessUsed = new Stack<Chess>();
            stkChessUndo = new Stack<Chess>();
            turn = 1;
            mode = 2;
            CreateChessPieces();
            DrawChessBoard(g);
        }

        public void StartLAN(Graphics g)
        {
            ready = true;
            stkChessUsed = new Stack<Chess>();
            stkChessUndo = new Stack<Chess>();
            turn = 1;
            mode = 3;
            CreateChessPieces();
            DrawChessBoard(g);
        }
        #region Undo, Redo
        public void Undo(Graphics g)
        {
            if (mode == 2)
            {
                if (stkChessUsed.Count != 0)
                {
                    if(stkChessUsed.Count == 1)
                    {
                        return;

                    }
                    else
                    {
                        Chess cpC = stkChessUsed.Pop();
                        Chess cpP = stkChessUsed.Pop();
                        stkChessUndo.Push(new Chess(cpP.Row, cpP.Column, cpP.Position, cpP.Owner));
                        stkChessUndo.Push(new Chess(cpC.Row, cpC.Column, cpC.Position, cpC.Owner));
                        arrChessPiece[cpP.Row, cpP.Column].Owner = 0;
                        arrChessPiece[cpC.Row, cpC.Column].Owner = 0;
                        chessBoard.RemoveChess(g, cpP.Position, sbPnl!);
                        chessBoard.RemoveChess(g, cpC.Position, sbPnl!);
                    }    
                    
                }
                else
                    MessageBox.Show("Bạn chưa đánh nước cờ nào!!");
            }
        }
        public void Redo(Graphics g, Image firstchess, Image secondchess)
        {
            if (mode == 2)
            {
                if (stkChessUndo.Count != 0)
                {
                    Chess cpC = stkChessUndo.Pop();
                    Chess cpP = stkChessUndo.Pop();
                    stkChessUsed.Push(new Chess(cpP.Row, cpP.Column, cpP.Position, cpP.Owner));
                    stkChessUsed.Push(new Chess(cpC.Row, cpC.Column, cpC.Position, cpC.Owner));
                    arrChessPiece[cpC.Row, cpC.Column].Owner = cpC.Owner;
                    arrChessPiece[cpP.Row, cpP.Column].Owner = cpP.Owner;
                    chessBoard.DrawChess(g, cpP.Position, cpP.Owner == 1 ? firstchess : secondchess);
                    chessBoard.DrawChess(g, cpC.Position, cpC.Owner == 1 ? firstchess : secondchess);
                }
            }
        }
        #endregion

        #region Check Winer

        public int CheckWin()
        {
            if (stkChessUsed.Count == chessBoard.NumOfColumns * chessBoard.NumOfLines)
            {
                return 22;
            }
            foreach (Chess cp in stkChessUsed)
            {
                if (CheckVertical(cp.Row, cp.Column, cp.Owner) || CheckHorizontal(cp.Row, cp.Column, cp.Owner) || CheckCross(cp.Row, cp.Column, cp.Owner) || CheckCrossBackwards(cp.Row, cp.Column, cp.Owner))
                {
                    if (cp.Owner == 1)
                        return 1;
                    else if (cp.Owner == 2)
                        return 2;
                }
            }
            return 0;
        }

        private bool CheckVertical(int currRow, int currColumn, int currOwner)
        {
            if (currRow > chessBoard.NumOfLines - 5)
                return false;
            int count;
            for (count = 1; count < 5; count++)
            {
                if (arrChessPiece[currRow + count, currColumn].Owner != currOwner)
                    return false;
            }
            if (currRow == 0 || currRow + count == chessBoard.NumOfLines)
                return true;
            if (arrChessPiece[currRow - 1, currColumn].Owner == 0 || arrChessPiece[currRow + count, currColumn].Owner == 0)
                return true;
            return false;
        }

        private bool CheckHorizontal(int currRow, int currColumn, int currOwner)
        {
            if (currColumn > chessBoard.NumOfColumns - 5)
                return false;
            int count;
            for (count = 1; count < 5; count++)
            {
                if (arrChessPiece[currRow, currColumn + count].Owner != currOwner)
                    return false;
            }
            if (currColumn == 0 || currColumn + count == chessBoard.NumOfColumns)
                return true;
            if (arrChessPiece[currRow, currColumn - 1].Owner == 0 || arrChessPiece[currRow, currColumn + count].Owner == 0)
                return true;
            return false;
        }

        private bool CheckCross(int currRow, int currColumn, int currOwner)
        {
            if (currRow > chessBoard.NumOfLines - 5 || currColumn > chessBoard.NumOfColumns - 5)
                return false;
            int count;
            for (count = 1; count < 5; count++)
            {
                if (arrChessPiece[currRow + count, currColumn + count].Owner != currOwner)
                    return false;
            }
            if (currColumn == 0 || currColumn + count == chessBoard.NumOfColumns || currRow == 0 || currRow + count == chessBoard.NumOfLines)
                return true;
            if (arrChessPiece[currRow - 1, currColumn - 1].Owner == 0 || arrChessPiece[currRow + count, currColumn + count].Owner == 0)
                return true;
            return false;
        }

        private bool CheckCrossBackwards(int currRow, int currColumn, int currOwner)
        {
            if (currRow < 4 || currColumn > chessBoard.NumOfColumns - 5)
                return false;
            int count;
            for (count = 1; count < 5; count++)
            {
                if (arrChessPiece[currRow - count, currColumn + count].Owner != currOwner)
                    return false;
            }
            if (currRow == 4 || currRow == chessBoard.NumOfLines - 1 || currColumn == 0 || currColumn + count == chessBoard.NumOfColumns)
                return true;
            if (arrChessPiece[currRow + 1, currColumn - 1].Owner == 0 || arrChessPiece[currRow - count, currColumn + count].Owner == 0)
                return true;
            return false;
        }
        #endregion

        #region CheckwinwithoutBlock
        public bool CheckWinwithoutBlock()
        {
            if (stkChessUsed.Count == chessBoard.NumOfColumns * chessBoard.NumOfLines)
            {
                _end = END.Draw;
                return true;
            }
            foreach (Chess cp in stkChessUsed)
            {
                if (CheckVerticalWithoutBlock(cp.Row, cp.Column, cp.Owner) || CheckHorizontalWithoutBlock(cp.Row, cp.Column, cp.Owner) || CheckCrossWithoutBlock(cp.Row, cp.Column, cp.Owner) || CheckCrossBackwardsWithoutBlock(cp.Row, cp.Column, cp.Owner))
                {
                    _end = cp.Owner == 1 ? END.Player1 : END.Player2;
                    return true;
                }
            }
            return false;
        }
        private bool CheckVerticalWithoutBlock(int currRow, int currColumn, int currOwner)
        {
            if (currRow > chessBoard.NumOfLines - 5)
                return false;
            int count;
            for (count = 1; count < 5; count++)
            {
                if (arrChessPiece[currRow + count, currColumn].Owner != currOwner)
                    return false;
            }
            return true;
        }

        private bool CheckHorizontalWithoutBlock(int currRow, int currColumn, int currOwner)
        {
            if (currColumn > chessBoard.NumOfColumns - 5)
                return false;
            int count;
            for (count = 1; count < 5; count++)
            {
                if (arrChessPiece[currRow, currColumn + count].Owner != currOwner)
                    return false;
            }
            return true;
        }

        private bool CheckCrossWithoutBlock(int currRow, int currColumn, int currOwner)
        {
            if (currRow > chessBoard.NumOfLines - 5 || currColumn > chessBoard.NumOfColumns - 5)
                return false;
            int count;
            for (count = 1; count < 5; count++)
            {
                if (arrChessPiece[currRow + count, currColumn + count].Owner != currOwner)
                    return false;
            }
            return true;
        }

        private bool CheckCrossBackwardsWithoutBlock(int currRow, int currColumn, int currOwner)
        {
            if (currRow < 4 || currColumn > chessBoard.NumOfColumns - 5)
                return false;
            int count;
            for (count = 1; count < 5; count++)
            {
                if (arrChessPiece[currRow - count, currColumn + count].Owner != currOwner)
                    return false;
            }
            return true;
        }
        #endregion

        #region AI Computer
        private long[] AttackPoint = new long[7] { 0, 9, 54, 162, 1458, 13112, 118008 };
        private long[] DefensePoint = new long[7] { 0, 3, 27, 99, 729, 6561, 59049 };
        public void LaunchComputer(Graphics g, Image firstchess, Image secondchess)
        {
            int cellSize = 640 / chessBoard.NumOfColumns;
            if (stkChessUsed.Count == 0)
            {
                PlayChess(chessBoard.NumOfColumns / 2 * cellSize + 1, chessBoard.NumOfLines / 2 * cellSize + 1, g, firstchess, secondchess);
            }
            else
            {

                Chess cp = FindMove();
                PlayChess(cp.Position.X + 1, cp.Position.Y + 1, g,firstchess,secondchess);

            }
        }
        private Chess FindMove()
        {
            Chess cpResult = new Chess();
            long maxPoint = 0;
            for (int i = 0; i < chessBoard.NumOfLines; i++)
            {
                for (int j = 0; j < chessBoard.NumOfColumns; j++)
                {
                    if (arrChessPiece[i, j].Owner == 0)
                    {
                        long attackkPoint = AtkPoint_CheckHorizontal(i, j) + AtkPoint_CheckVertical(i, j) + AtkPoint_CheckCross(i, j) + AtkPoint_CheckCrossBackward(i, j);
                        long defensePoint = DefPoint_CheckHorizontal(i, j) + DefPoint_CheckVertical(i, j) + DefPoint_CheckCross(i, j) + DefPoint_CheckCrossBackward(i, j);
                        long tmpPoint = attackkPoint > defensePoint ? attackkPoint : defensePoint;
                        if (maxPoint < tmpPoint)
                        {
                            maxPoint = tmpPoint;
                            cpResult = new Chess(arrChessPiece[i, j].Row, arrChessPiece[i, j].Column, arrChessPiece[i, j].Position, arrChessPiece[i, j].Owner);

                        }
                    }
                }
            }

            return cpResult;
        }

        #region Attack
        // Duyệt dọc
        private long AtkPoint_CheckVertical(int currRow, int currColumn)
        {
            long total = 0;
            int ally = 0;
            int enemy = 0;
            for (int count = 1; count < 6 && currRow + count < chessBoard.NumOfLines; count++)
            {
                if (arrChessPiece[currRow + count, currColumn].Owner == 1)
                    ally++;
                else if (arrChessPiece[currRow + count, currColumn].Owner == 2)
                {
                    enemy++;
                    total -= 9;
                    break;
                }
                else
                {
                    break;
                }
            }
            for (int count = 1; count < 6 && currRow - count >= 0; count++)
            {
                if (arrChessPiece[currRow - count, currColumn].Owner == 1)
                    ally++;
                else if (arrChessPiece[currRow - count, currColumn].Owner == 2)
                {
                    enemy++;
                    total -= 9;
                    break;
                }
                else
                {
                    break;
                }
            }
            if (enemy == 2) return 0;
            total -= DefensePoint[enemy];
            total += AttackPoint[ally];
            return total;
        }
        // Duyệt ngang
        private long AtkPoint_CheckHorizontal(int currRow, int currColumn)
        {
            long total = 0;
            int ally = 0;
            int enemy = 0;
            for (int count = 1; count < 6 && currColumn + count < chessBoard.NumOfColumns; count++)
            {
                if (arrChessPiece[currRow, currColumn + count].Owner == 1)
                    ally++;
                else if (arrChessPiece[currRow, currColumn + count].Owner == 2)
                {
                    enemy++;
                    total -= 9;
                    break;
                }
                else
                {
                    break;
                }
            }
            for (int count = 1; count < 6 && currColumn - count >= 0; count++)
            {
                if (arrChessPiece[currRow, currColumn - count].Owner == 1)
                    ally++;
                else if (arrChessPiece[currRow, currColumn - count].Owner == 2)
                {
                    enemy++;
                    total -= 9;
                    break;
                }
                else
                {
                    break;
                }
            }
            if (enemy == 2) return 0;
            total -= DefensePoint[enemy];
            total += AttackPoint[ally];
            return total;
        }
        // Duyệt chéo
        private long AtkPoint_CheckCross(int currRow, int currColumn)
        {
            long total = 0;
            int ally = 0;
            int enemy = 0;
            for (int count = 1; count < 6 && currRow + count < chessBoard.NumOfLines && currColumn + count < chessBoard.NumOfColumns; count++)
            {
                if (arrChessPiece[currRow + count, currColumn + count].Owner == 1)
                    ally++;
                else if (arrChessPiece[currRow + count, currColumn + count].Owner == 2)
                {
                    enemy++;
                    total -= 9;
                    break;
                }
                else
                {
                    break;
                }
            }
            for (int count = 1; count < 6 && currRow - count >= 0 && currColumn - count >= 0; count++)
            {
                if (arrChessPiece[currRow - count, currColumn - count].Owner == 1)
                    ally++;
                else if (arrChessPiece[currRow - count, currColumn - count].Owner == 2)
                {
                    enemy++;
                    total -= 9;
                    break;
                }
                else
                {
                    break;
                }
            }
            if (enemy == 2) return 0;
            total -= DefensePoint[enemy];
            total += AttackPoint[ally];
            return total;
        }

        // Duyệt chéo ngược
        private long AtkPoint_CheckCrossBackward(int currRow, int currColumn)
        {
            long total = 0;
            int ally = 0;
            int enemy = 0;
            for (int count = 1; count < 6 && currRow - count >= 0 && currColumn + count < chessBoard.NumOfColumns; count++)
            {
                if (arrChessPiece[currRow - count, currColumn + count].Owner == 1)
                    ally++;
                else if (arrChessPiece[currRow - count, currColumn + count].Owner == 2)
                {
                    enemy++;
                    total -= 9;
                    break;
                }
                else
                {
                    break;
                }
            }
            for (int count = 1; count < 6 && currRow + count < chessBoard.NumOfLines && currColumn - count >= 0; count++)
            {
                if (arrChessPiece[currRow + count, currColumn - count].Owner == 1)
                    ally++;
                else if (arrChessPiece[currRow + count, currColumn - count].Owner == 2)
                {
                    enemy++;
                    total -= 9;
                    break;
                }
                else
                {
                    break;
                }
            }
            if (enemy == 2) return 0;
            total -= DefensePoint[enemy];
            total += AttackPoint[ally];
            return total;
        }
        #endregion

        #region Defense
        private long DefPoint_CheckVertical(int currRow, int currColumn)
        {
            long total = 0;
            int ally = 0;
            int enemy = 0;
            for (int count = 1; count < 6 && currRow + count < chessBoard.NumOfLines; count++)
            {
                if (arrChessPiece[currRow + count, currColumn].Owner == 1)
                {
                    ally++;
                    break;
                }
                else if (arrChessPiece[currRow + count, currColumn].Owner == 2)
                {
                    enemy++;
                }
                else
                {
                    break;
                }
            }
            for (int count = 1; count < 6 && currRow - count >= 0; count++)
            {
                if (arrChessPiece[currRow - count, currColumn].Owner == 1)
                {
                    ally++;
                    break;
                }
                else if (arrChessPiece[currRow - count, currColumn].Owner == 2)
                {
                    enemy++;
                }
                else
                {
                    break;
                }
            }
            if (ally == 2) return 0;
            total += DefensePoint[enemy];
            if (enemy > 0)
                total -= AttackPoint[ally] * 2;
            return total;
        }
        private long DefPoint_CheckHorizontal(int currRow, int currColumn)
        {
            long total = 0;
            int ally = 0;
            int enemy = 0;
            for (int count = 1; count < 6 && currColumn + count < chessBoard.NumOfColumns; count++)
            {
                if (arrChessPiece[currRow, currColumn + count].Owner == 1)
                {
                    ally++;
                    break;
                }
                else if (arrChessPiece[currRow, currColumn + count].Owner == 2)
                {
                    enemy++;
                }
                else
                {
                    break;
                }
            }
            for (int count = 1; count < 6 && currColumn - count >= 0; count++)
            {
                if (arrChessPiece[currRow, currColumn - count].Owner == 1)
                {
                    ally++;
                    break;
                }
                else if (arrChessPiece[currRow, currColumn - count].Owner == 2)
                {
                    enemy++;
                }
                else
                {
                    break;
                }
            }
            if (ally == 2) return 0;
            total += DefensePoint[enemy];
            if (enemy > 0)
                total -= AttackPoint[ally] * 2;

            return total;
        }
        private long DefPoint_CheckCross(int currRow, int currColumn)
        {
            long total = 0;
            int ally = 0;
            int enemy = 0;
            for (int count = 1; count < 6 && currRow + count < chessBoard.NumOfLines && currColumn + count < chessBoard.NumOfColumns; count++)
            {
                if (arrChessPiece[currRow + count, currColumn + count].Owner == 1)
                {
                    ally++;
                    break;
                }
                else if (arrChessPiece[currRow + count, currColumn + count].Owner == 2)
                {
                    enemy++;

                }
                else
                {
                    break;
                }
            }
            for (int count = 1; count < 6 && currRow - count >= 0 && currColumn - count >= 0; count++)
            {
                if (arrChessPiece[currRow - count, currColumn - count].Owner == 1)
                {
                    ally++;
                    break;
                }
                else if (arrChessPiece[currRow - count, currColumn - count].Owner == 2)
                {
                    enemy++;

                }
                else
                {
                    break;
                }
            }
            if (ally == 2) return 0;
            total += DefensePoint[enemy];
            if (enemy > 0)
                total -= AttackPoint[ally] * 2;

            return total;
        }
        private long DefPoint_CheckCrossBackward(int currRow, int currColumn)
        {
            long total = 0;
            int ally = 0;
            int enemy = 0;
            for (int count = 1; count < 6 && currRow - count >= 0 && currColumn + count < chessBoard.NumOfColumns; count++)
            {
                if (arrChessPiece[currRow - count, currColumn + count].Owner == 1)
                {
                    ally++;
                    break;
                }
                else if (arrChessPiece[currRow - count, currColumn + count].Owner == 2)
                {
                    enemy++;

                }
                else
                {
                    break;
                }
            }
            for (int count = 1; count < 6 && currRow + count < chessBoard.NumOfLines && currColumn - count >= 0; count++)
            {
                if (arrChessPiece[currRow + count, currColumn - count].Owner == 1)
                {
                    ally++;
                    break;
                }
                else if (arrChessPiece[currRow + count, currColumn - count].Owner == 2)
                {
                    enemy++;

                }
                else
                {
                    break;
                }
            }
            if (ally == 2) return 0;
            total += DefensePoint[enemy];
            if (enemy > 0)
                total -= AttackPoint[ally] * 2;

            return total;
        }
        #endregion

        #endregion
    }
}
