using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze
{
    class Cell
    {

        public int x { get; set; }
        public int y { get; set; }
        public int visited { get; set; }
        //represent the walls of the cell 
        //vales: -1: can't be remov
        //        0: the wall is remove;
        //        1: might be remove in the future
        private int[] walls;
        //0-up
        //1-down
        //2-left
        //3-right
        public static int rows { get; set;}
        public static int cols { get; set; }


        public Cell()
        {
            x = 0;
            y = 0;
            visited = 0;
            walls = new int[4];

            for (int i = 0; i < 4; i++)
                walls[i] = 1;
        }


        public Cell(int a, int b)
        {
            this.x = a;
            this.y = b;
            visited = 0;
            walls = new int[4];

            for (int i = 0; i < 4; i++)
                walls[i] = 1;

            if (a == 0)
               walls[0] = -1;
            if (a == rows - 1)
                walls[1] = -1;

            if (b == 0)
                walls[2] = -1;
            if (b == cols - 1)
                walls[3] = -1;
        }

        public void delete_wall(int x)
        {
            walls[x] = 0;
        }

        //return the nr el elemets of the available walls and it take's as input a array of 4 empty elements
        public int vect_walls(int []wall)
        {
            int cont = 0;

            for (int i = 0; i < 4; i++)
                if (walls[i] == 1)
                    wall[cont++] = i;

            return cont;
        }


        public int get_wall(int i)
        {
            if (i >= 0 && i < 4)
                return walls[i];
            else
                return 0;
        }


        public void block_wall(int nr_wall)
        {
            if (nr_wall >= 0 && nr_wall < 4)
                walls[nr_wall] = -1;
        }

        //private void check_walls()
        //{

        //}
    }
}
