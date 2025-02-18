using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OthelloLogic
{
    public class Stone
    {
        public Player Color { get; set;}

        public Stone(Player color)
        {
            Color = color;
        }
    }
}