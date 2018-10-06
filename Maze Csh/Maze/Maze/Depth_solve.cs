using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze
{
    class Depth_solve
    {
        public int rows;
        public int cols;
        private int start_col;
        public static Cell[][] grid;

        private int nr_nodes;
        private Cell_Depth[] cell_depth;
        private int cont_noduri;
        private Stack<KeyValuePair<int, int>> road;
        private Stack<Cell_Depth> stk_depth;

        public Boolean solved;


        public Depth_solve(int r, int c, Cell[][] gr, int nr_nd, int str)
        {
            solved = false;
            rows = r;
            cols = c;
            grid = gr;
            start_col = str;

            nr_nodes = nr_nd;
            road = new Stack<KeyValuePair<int, int>>();

            cell_depth = new Cell_Depth[nr_nodes];

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    grid[i][j].visited = 0;

            cont_noduri = 0;

            fill_cell_depth();
            //initiate_visit_list_cell();
            solve();
            calculate_road();
        }


        public void fill_cell_depth()
        {
            Queue<Cell> que = new Queue<Cell>();

            que.Enqueue(grid[0][start_col]);

            int[] wall = new int[4];

            while (que.Count > 0)
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
                cell_depth[cont_noduri] = new Cell_Depth(temp.x, temp.y);
                //set neighbours
                cell_depth[cont_noduri].set_cell_neighbour(vecin_sus, 0);
                cell_depth[cont_noduri].set_cell_neighbour(vecin_jos, 1);
                cell_depth[cont_noduri].set_cell_neighbour(vecin_st, 2);
                cell_depth[cont_noduri].set_cell_neighbour(vecin_dr, 3);
                //set distance to neighbours
                cell_depth[cont_noduri].set_dist_to_neighbour(dist_sus, 0);
                cell_depth[cont_noduri].set_dist_to_neighbour(dist_jos, 1);
                cell_depth[cont_noduri].set_dist_to_neighbour(dist_st, 2);
                cell_depth[cont_noduri].set_dist_to_neighbour(dist_dr, 3);

                cont_noduri++;

                if (temp.x == rows - 1 && temp.y == cols - 1)
                    break;
            }
        }

        public void solve()
        {
            stk_depth = new Stack<Cell_Depth>();

            stk_depth.Push(get_cell_from_list(0,start_col));

            int contor = 0;

            while (stk_depth.Count != 0)
            {
                Cell_Depth temp_cell = stk_depth.Pop();
                temp_cell.visit = 1;

                if (temp_cell.x == rows - 1 && temp_cell.y == cols - 1)
                {
                    solved = true;
                    break;
                }


                KeyValuePair<int, int>[] arr_neighbor = temp_cell.get_neighbor_arr();


                for (int i = 0; i < arr_neighbor.Length; i++)
                    if (arr_neighbor[i].Key != -1 && get_cell_from_list(arr_neighbor[i].Key, arr_neighbor[i].Value).visit != 1)
                    {
                        Cell_Depth neighbor_to_be_added = get_cell_from_list(arr_neighbor[i].Key, arr_neighbor[i].Value);
                        neighbor_to_be_added.added_by = new KeyValuePair<int, int>(temp_cell.x, temp_cell.y);
                        stk_depth.Push(neighbor_to_be_added);
                    }
            }
        }

        public void calculate_road()
        {
            if(solved == true)
            {
                Cell_Depth current = get_cell_from_list(rows - 1, cols - 1);  //start from the end then go back using the added_by field

                while(true)
                {
                    road.Push(new KeyValuePair<int, int>(current.x, current.y));

                    if ((current.added_by).Key == -1)
                        break;

                    current = get_cell_from_list(current.added_by.Key, current.added_by.Value);
                }
            }
        }

        public Cell_Depth get_cell_from_list(int i, int j)
        {
            for (int cont = 0; cont < cont_noduri; cont++)
                if (cell_depth[cont].x == i && cell_depth[cont].y == j)
                    return cell_depth[cont];

            
            Cell_Depth null_cell = new Cell_Depth(-1, -1);
            null_cell.visit = 1;
            return null_cell;
        }

        public void initiate_visit_list_cell()
        {
            for (int i = 0; i < cont_noduri; i++)
                cell_depth[i].visit = 0;
        }

        public Stack<KeyValuePair<int, int>> get_road()
        {
            return road;
        }
    }
}
