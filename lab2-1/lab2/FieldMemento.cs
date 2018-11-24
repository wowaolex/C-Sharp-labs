using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    class FieldMemento
    {
        public int Size { get; private set; }
        public string[,] Field { get; private set; }
        public Point Current { get; private set; }

        public FieldMemento(string[,] field, int size, Point current)
        {
            Size = size;
            Field = (string[,])field.Clone();
            Current = current;
        }
    }
}
