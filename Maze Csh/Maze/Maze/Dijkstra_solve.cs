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
        private int start_col;
        public static Cell[][] grid;
       
        private int nr_nodes;
        private Cell_Dijkstra[] cell_dijk;
        private int cont_noduri;
        private Stack<KeyValuePair<int, int>> road;

        public Dijkstra_solve(int r, int c, Cell [][]gr, int nr_nd, int str)
        {
            rows = r;
            cols = c;
            grid = gr;
            start_col = str;

            nr_nodes = nr_nd;
            road = new Stack<KeyValuePair<int, int>>();

            cell_dijk = new Cell_Dijkstra[nr_nodes];

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    grid[i][j].visited = 0;

            cont_noduri = 0;

            fill_cell_dijkstra();
            create_cost();
            create_road();

            //while (road.Count > 0)
            //{
            //    KeyValuePair<int, int> temp;
            //    temp = road.Pop();

            //    Console.Write(temp.Key + " " + temp.Value + "\n");
            //}
        }


        public void fill_cell_dijkstra()
        {
            Queue<Cell> que = new Queue<Cell>();
            
            que.Enqueue(grid[0][start_col]);
            
            int []wall= new int[4];

            while(que.Count > 0)
            {
                Cell temp = que.Dequeue();

                KeyValuePair<int, int> vecin_sus = new KeyValuePair<int, int>(-1, -1);
                KeyValuePair<int, int> vecin_jos = new KeyValuePair<int, int>(-1, -1);
                KeyValuePair<int, int> vecin_st = new KeyValuePair<int, int>(-1, -1);
                KeyValuePair<int, int> vecin_dr = new KeyValuePair<int, int>(-1, -1);

                int dist_sus = -1;
                int dist_jos = -1;
                int dist_st = -1;
                int dist_dr = -1;


                for (int i = 0; i < 4; i++)
                   wall[i] = temp.get_wall(i);

                for (int i = 0; i < 4; i++)
                    if (wall[i] != -1)
                    {
                        int dist = 0;

                        Cell aux_cell;

                        switch (i)
                        {
                            case 0:
                                {
                                    aux_cell = grid[temp.x - 1][temp.y];
                                    dist++;

                                    Boolean ok = new Boolean();
                                    ok = true;

                                    while (ok)
                                    {
                                        //caz blocat in directia de mers
                                        if (aux_cell.get_wall(0) == -1)
                                            ok = false;
                                        //caz deschis in stanga
                                        if (aux_cell.get_wall(2) != -1)
                                            ok = false;
                                        //caz deschis in dreapta
                                        if (aux_cell.get_wall(3) != -1)
                                            ok = false;

                                        if (ok == true)
                                        {
                                            aux_cell = grid[aux_cell.x - 1][aux_cell.y];
                                            dist++;
                                        }

                                    }

                                    if (aux_cell.visited != 1)
                                        que.Enqueue(aux_cell);

                                    dist_sus = dist;
                                    vecin_sus = new KeyValuePair<int, int>(aux_cell.x, aux_cell.y);

                                        break;
                                }
                            case 1:
                                {
                                    aux_cell = grid[temp.x + 1][temp.y];
                                    dist++;

                                    Boolean ok = new Boolean();
                                    ok = true;

                                    while (ok)
                                    {
                                        //caz blocat in directia de mers
                                        if (aux_cell.get_wall(1) == -1)
                                            ok = false;
                                        //caz deschis in stanga
                                        if (aux_cell.get_wall(2) != -1)
                                            ok = false;
                                        //caz deschis in dreapta
                                        if (aux_cell.get_wall(3) != -1)
                                            ok = false;

                                        if (ok == true)
                                        {
                                            aux_cell = grid[aux_cell.x + 1][aux_cell.y];
                                            dist++;
                                        }

                                    }

                                    if (aux_cell.visited != 1)
                                        que.Enqueue(aux_cell);

                                    dist_jos = dist;
                                    vecin_jos = new KeyValuePair<int, int>(aux_cell.x, aux_cell.y);

                                    break;
                                }
                            case 2:
                                {
                                    aux_cell = grid[temp.x][temp.y - 1];
                                    dist++;

                                    Boolean ok = new Boolean();
                                    ok = true;

                                    while (ok)
                                    {
                                        //caz blocat in directia de mers
                                        if (aux_cell.get_wall(2) == -1)
                                            ok = false;
                                        //caz deschis in sus
                                        if (aux_cell.get_wall(0) != -1)
                                            ok = false;
                                        //caz deschis in jos
                                        if (aux_cell.get_wall(1) != -1)
                                            ok = false;

                                        if (ok == true)
                                        {
                                            aux_cell = grid[aux_cell.x][aux_cell.y - 1];
                                            dist++;
                                        }

                                    }

                                    if (aux_cell.visited != 1)
                                        que.Enqueue(aux_cell);

                                    dist_st = dist;
                                    vecin_st = new KeyValuePair<int, int>(aux_cell.x, aux_cell.y);

                                    break;
                                }
                            case 3:
                                {
                                    aux_cell = grid[temp.x][temp.y + 1];
                                    dist++;

                                    Boolean ok = new Boolean();
                                    ok = true;

                                    while (ok)
                                    {
                                        //caz blocat in directia de mers
                                        if (aux_cell.get_wall(3) == -1)
                                            ok = false;
                                        //caz deschis in sus
                                        if (aux_cell.get_wall(0) != -1)
                                            ok = false;
                                        //caz deschis in jos
                                        if (aux_cell.get_wall(1) != -1)
                                            ok = false;

                                        if (ok == true)
                                        {
                                            aux_cell = grid[aux_cell.x][aux_cell.y + 1];
                                            dist++;
                                        }

                                    }

                                    if (aux_cell.visited != 1)
                                        que.Enqueue(aux_cell);

                                    dist_dr = dist;
                                    vecin_dr = new KeyValuePair<int, int>(aux_cell.x, aux_cell.y);

                                    break;
                                }
                        }
                    }

                //mark cell as visite
                temp.visited = 1;
                //Console.Write(temp.x + " " + temp.y + "\n");
                //create it as disj cell
                cell_dijk[cont_noduri] = new Cell_Dijkstra(temp.x, temp.y);
                //set neighbours
                cell_dijk[cont_noduri].set_cell_neighbour(vecin_sus, 0);
                cell_dijk[cont_noduri].set_cell_neighbour(vecin_jos, 1);
                cell_dijk[cont_noduri].set_cell_neighbour(vecin_st, 2);
                cell_dijk[cont_noduri].set_cell_neighbour(vecin_dr, 3);
                //set distance to neighbours
                cell_dijk[cont_noduri].set_dist_to_neighbour(dist_sus, 0);
                cell_dijk[cont_noduri].set_dist_to_neighbour(dist_jos, 1);
                cell_dijk[cont_noduri].set_dist_to_neighbour(dist_st, 2);
                cell_dijk[cont_noduri].set_dist_to_neighbour(dist_dr, 3);

                cont_noduri++;

                if (temp.x == rows - 1 && temp.y == cols - 1)
                    break;
            }
        }

        private void create_cost()
        {
            for (int i = 0; i < cont_noduri; i++)
            {
                cell_dijk[i].visit = 0;
                cell_dijk[i].cost_from = new KeyValuePair<int, int>(-1, -1);
            }

            //setez costul start 0
            cell_dijk[0].set_cur_dist(0);

            Queue<Cell_Dijkstra> que = new Queue<Cell_Dijkstra>();
            que.Enqueue(cell_dijk[0]);

            while(que.Count > 0)
            {
                Cell_Dijkstra temp = que.Dequeue();

                Cell_Dijkstra vecin_sus = null;
                Cell_Dijkstra vecin_jos = null;
                Cell_Dijkstra vecin_st = null;
                Cell_Dijkstra vecin_dr = null;

                for(int i=0; i<4; i++)
                    if(temp.get_dist_to_neighbour(i) != -1)
                        switch(i)
                        {
                            case 0:
                                {
                                    Boolean ok = new Boolean();
                                    ok = true;

                                    for(int j=0; j<cont_noduri && ok; j++)
                                        if(cell_dijk[j].x == temp.get_cell_neighbour(0).Key && cell_dijk[j].y == temp.get_cell_neighbour(0).Value)
                                        {
                                            if (cell_dijk[j].get_cur_dist() > temp.get_cur_dist() + temp.get_dist_to_neighbour(0))
                                            {
                                                cell_dijk[j].set_cur_dist(temp.get_cur_dist() + temp.get_dist_to_neighbour(0));
                                                cell_dijk[j].cost_from = new KeyValuePair<int, int>(temp.x, temp.y);
                                            }
                                            ok = false;

                                            vecin_sus = cell_dijk[j];
                                        }
                                    break;
                                }
                            case 1:
                                {
                                    Boolean ok = new Boolean();
                                    ok = true;

                                    for (int j = 0; j < cont_noduri && ok; j++)
                                        if (cell_dijk[j].x == temp.get_cell_neighbour(1).Key && cell_dijk[j].y == temp.get_cell_neighbour(1).Value)
                                        {
                                            if (cell_dijk[j].get_cur_dist() > temp.get_cur_dist() + temp.get_dist_to_neighbour(1))
                                            {
                                                cell_dijk[j].set_cur_dist(temp.get_cur_dist() + temp.get_dist_to_neighbour(1));
                                                cell_dijk[j].cost_from = new KeyValuePair<int, int>(temp.x, temp.y);
                                            }
                                            ok = false;

                                            vecin_jos = cell_dijk[j];
                                        }
                                    break;
                                }
                            case 2:
                                {
                                    Boolean ok = new Boolean();
                                    ok = true;

                                    for (int j = 0; j < cont_noduri && ok; j++)
                                        if (cell_dijk[j].x == temp.get_cell_neighbour(2).Key && cell_dijk[j].y == temp.get_cell_neighbour(2).Value)
                                        {
                                            if (cell_dijk[j].get_cur_dist() > temp.get_cur_dist() + temp.get_dist_to_neighbour(2))
                                            {
                                                cell_dijk[j].set_cur_dist(temp.get_cur_dist() + temp.get_dist_to_neighbour(2));
                                                cell_dijk[j].cost_from = new KeyValuePair<int, int>(temp.x, temp.y);
                                            }
                                            ok = false;

                                            vecin_st = cell_dijk[j];
                                        }
                                    break;
                                }
                            case 3:
                                {
                                    Boolean ok = new Boolean();
                                    ok = true;

                                    for (int j = 0; j < cont_noduri && ok; j++)
                                        if (cell_dijk[j].x == temp.get_cell_neighbour(3).Key && cell_dijk[j].y == temp.get_cell_neighbour(3).Value)
                                        {
                                            if (cell_dijk[j].get_cur_dist() > temp.get_cur_dist() + temp.get_dist_to_neighbour(3))
                                            {
                                                cell_dijk[j].set_cur_dist(temp.get_cur_dist() + temp.get_dist_to_neighbour(3));
                                                cell_dijk[j].cost_from = new KeyValuePair<int, int>(temp.x, temp.y);
                                            }

                                            ok = false;

                                            vecin_dr = cell_dijk[j];
                                        }
                                    break;
                                }
                        }

                temp.visit = 1;

                if (vecin_sus != null && vecin_sus.visit != 1)
                    que.Enqueue(vecin_sus);
                if (vecin_jos != null && vecin_jos.visit != 1)
                    que.Enqueue(vecin_jos);
                if (vecin_st != null && vecin_st.visit != 1)
                    que.Enqueue(vecin_st);
                if (vecin_dr != null && vecin_dr.visit != 1)
                    que.Enqueue(vecin_dr);

                if (temp.x == rows - 1 && temp.y == cols - 1)
                    break;
            }
        }

        private void create_road()
        {
            Cell_Dijkstra temp = null;

            for(int i=0; i< cont_noduri; i++)
                if(cell_dijk[i].x == rows-1 && cell_dijk[i].y == cols-1)
                {
                    temp = cell_dijk[i];
                    break;
                }

            int cont = 0;

            //while didn't reach the start
            while (temp.get_cur_dist() != 0)
            {
                road.Push(new KeyValuePair<int, int>(temp.x, temp.y));

                Boolean ok = new Boolean();
                ok = true;

                int i;
                for (i = 0; i < cont_noduri && ok; i++)
                    if (cell_dijk[i].x == temp.cost_from.Key && cell_dijk[i].y == temp.cost_from.Value)
                        ok = false;

                temp = cell_dijk[i-1];

                cont++;
            }

            road.Push(new KeyValuePair<int, int>(0, start_col));
        }

        public Stack<KeyValuePair<int, int>> get_road()
        {
            return road;
        }
    }
}
