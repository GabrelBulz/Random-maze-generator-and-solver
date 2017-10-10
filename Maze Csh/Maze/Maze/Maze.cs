using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze
{
    class Maze
    {
        public int rows { get; set; }
        public int cols { get; set; }

        public Maze()
        {
            rows = 0;
            cols = 0;
        }


        public Maze(int a, int b)
        {
            this.rows = a;
            this.cols = b;
        }

    }
}
