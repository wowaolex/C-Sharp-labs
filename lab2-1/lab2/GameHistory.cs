using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    class GameHistory
    {
        public Stack<FieldMemento> History { get; private set; }

        public GameHistory()
        {
            History = new Stack<FieldMemento>();
        }
    }
}
