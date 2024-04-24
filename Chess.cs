using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caro_Nhom8
{
    class Chess
    {
        private int width;
        private int height;

        private int _Row;

        public int Row
        {
            get {return _Row;}
            set {_Row = value;}
        }

        private int _Column;
        public int Column
        {
            get {return _Column;}

            set {_Column = value;}
        }

        private Point _Position;
        public Point Position
        {
            get {return _Position;}
            set {_Position = value;}
        }

        public int Owner
        {
            get {return _Owner;}
            set {_Owner = value;}
        }

        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }

        private int _Owner;

        public Chess(int row, int column, Point pos, int owner)
        {
            _Row = row;
            _Column = column;
            _Position = pos;
            _Owner = owner;
        }

        public Chess()
        {

        }
    }
}
