using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze
{
    class Dijkstra_solve
    {
        public  int rows;
        public  int cols;
        public int start_col { get; set; }
        public static Cell[][] grid;
       
        private int nr_nodes;
        private Cell_Dijkstra[] cell_dijk;

        public Dijkstra_solve(int r, int c, Cell [][]gr, int nr_nd)
        {
            rows = r;
            cols = c;
            grid = gr;
            nr_nodes = nr_nd;

            cell_dijk = new Cell_Dijkstra[nr_nodes];

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    grid[i][j].visited = 0;
        }


        public void fill_cell_dijkstra()
        {
            Queue<Cell> que = new Queue<Cell>();

            que.Enqueue(grid[0][start_col]);
            
            int []wall= new int[4];

            while(que.Count > 0)
            {
                Cell temp = que.Dequeue();

                for (int i = 0; i < 4; i++)
                    wall[i] = temp.get_wall(i);
                
                for(int i=0; i<4; i++)
                    if(wall[i] != -1)
                    {
                        int dist = 0;

                        Cell aux_cell;

                        switch(i)
                        {
                            case 0:
                            {
                                    aux_cell = grid[temp.x - 1][temp.y];
                                    dist++;

                                    Boolean ok = new Boolean();
                                    ok = true;

                                    while(ok)
                                    {
                                        aux_cell.visited = 1;
                                        ///DE LUCARAT AICI

                                    }

                                    break;
                            }
                        }
                    }
            }
        }

    }
}
