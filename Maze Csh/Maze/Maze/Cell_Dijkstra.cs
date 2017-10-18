using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze
{
    class Cell_Dijkstra
    {
        public int x { get; set; }
        public int y { get; set; }

        private KeyValuePair<int, int>[] cell_neighbour;
        private int[] dist_to_neighbour;
        private int cur_dist;
        public int visit { get; set; }
        public KeyValuePair<int,int> cost_from { get; set; }

        public Cell_Dijkstra(int a, int b)
        {
            x = a;
            y = b;

            cell_neighbour = new KeyValuePair<int, int>[4];

            for (int i = 0; i < 4; i++)
                cell_neighbour[i] = new KeyValuePair<int, int>(-1, -1);

            dist_to_neighbour = new int[4];
            for (int i = 0; i < 4; i++)
                dist_to_neighbour[i] = -1;

            cur_dist = int.MaxValue;
        }


        public KeyValuePair<int, int> get_cell_neighbour(int pos)
        {
            return cell_neighbour[pos];
        }

        public void set_cell_neighbour(KeyValuePair<int, int> neig, int pos)
        {
            cell_neighbour[pos] = neig;
        }

        public void set_dist_to_neighbour(int value, int pos)
        {
            dist_to_neighbour[pos] = value;
        }

        public int get_dist_to_neighbour(int pos)
        {
            return dist_to_neighbour[pos];
        }

        public void set_cur_dist(int value)
        {
            cur_dist = value;
        }

        public int get_cur_dist()
        {
            return cur_dist;
        }
    }
}
