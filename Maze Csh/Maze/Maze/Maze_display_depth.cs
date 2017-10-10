using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Maze
{
    public partial class Maze_display_depth : Form
    {
        static int height;
        static int width;
        int rows; //actual nr of cells per row
        int cols;//actual nr of cells per column
        Cell[][] grid;

        public Maze_display_depth()
        {
            InitializeComponent();

            height = Form1.rows;
            width = Form1.cols;

            int []wall= new int[4];
            int size_wall_array=0;

            cal_matrix();
            Cell.rows = rows;
            Cell.cols = cols;
            grid = new Cell[cols][];



            for (int i = 0; i < rows; i++)
            {
                grid[i] = new Cell[cols];
                for (int j = 0; j < cols; j++)
                    grid[i][j] = new Cell(i, j);
            }
        
            Stack<Cell> stk = new Stack<Cell>();

            //i'll pick the start on the top row, but on a random coloumn;
            int start_col;
            Random rnd = new Random();
            start_col = rnd.Next(0, cols);
            grid[0][start_col].visited = 1;

            //push the start on the stack
            stk.Push(grid[0][start_col]);



            //button1.Click += delegate {

            //    for(int i=0; i<rows; i++)
            //        for(int j=0; j<cols; j++)
            //            MessageBox.Show(grid[rows - 1 ][0].get_wall(1).ToString() + grid[0][0].get_wall(1).ToString() + grid[0][0].get_wall(2).ToString() + grid[0][0].get_wall(3).ToString()); };








            while (stk.Count > 0)
            {
                Cell temp = stk.Peek();
                initiate_vect(wall);
                cell_update(ref temp);
                get_available_walls_of_cell(wall, ref size_wall_array, temp);

                int rand_wall = get_random_wall(wall, size_wall_array);
                if (rand_wall == -1)
                    stk.Pop();
                else
                {
                    switch (rand_wall)
                    {
                        //up
                        case 0:
                            {
                                temp.delete_wall(0);
                                Cell to_go = grid[temp.x - 1][temp.y];
                                to_go.delete_wall(1);
                                to_go.visited = 1;
                                stk.Push(to_go);
                            }
                            break;

                        //down
                        case 1:
                            {
                                temp.delete_wall(1);
                                Cell to_go = grid[temp.x + 1][temp.y];
                                to_go.delete_wall(0);
                                to_go.visited = 1;
                                stk.Push(to_go);
                            }
                            break;
                        //left
                        case 2:
                            {
                                temp.delete_wall(2);
                                Cell to_go = grid[temp.x][temp.y - 1];
                                to_go.delete_wall(3);
                                to_go.visited = 1;
                                stk.Push(to_go);
                            }
                            break;

                        //right
                        case 3:
                            {
                                temp.delete_wall(3);
                                Cell to_go = grid[temp.x][temp.y + 1];
                                to_go.delete_wall(2);
                                to_go.visited = 1;
                                stk.Push(to_go);
                            }
                            break;
                    }
                }
            }

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    Console.Write("R:" + grid[i][j].x.ToString() + " C:" + grid[i][j].y.ToString() + " top:" + grid[i][j].get_wall(0).ToString() + " bot:" + grid[i][j].get_wall(1).ToString() + " left:" + grid[i][j].get_wall(2).ToString() + " right:" + grid[i][j].get_wall(3).ToString() + "\n");

        }


        private void cal_matrix()
        {
            /**i've choosed that each cell of the maze should be 2x2 pixels,
             * and the number of total pixels should be x%3=1
             */

            int result;
            result = height % 3;
            switch (result)
            {
                case 0:
                    height += 1;
                    break;
                case 2:
                    height += 2;
                    break;
            }

            result = width % 3;
            switch (result)
            {
                case 0:
                    width += 1;
                    break;
                case 2:
                    width += 2;
                    break;
            }

            rows = height / 3;
            cols = width / 3;
        }

        private void initiate_vect(int []x)
        {
            for (int i = 0; i < 4; i++)
                x[i] = 5;
        }

        private void cell_update(ref Cell cell)
        {
            int i = cell.x;
            int j = cell.y;
            
            for(int k=0; k<4; k++)
                if(cell.get_wall(k) == 1)
                {
                    switch(k)
                    {
                        case 0:
                            if (grid[i - 1][j].visited == 1) cell.block_wall(0);
                            break;
                        case 1:
                            if (grid[i + 1][j].visited == 1) cell.block_wall(1);
                            break;
                        case 2:
                            if (grid[i][j - 1].visited == 1) cell.block_wall(2);
                            break;
                        case 3:
                            if (grid[i][j + 1].visited == 1) cell.block_wall(3);
                            break;
                    }
                }
           

        }


        private void get_available_walls_of_cell(int []wall, ref int cont, Cell x)
        {
            cont = 0;

            for (int i = 0; i < 4; i++)
                if (x.get_wall(i) == 1)
                    wall[cont++] = i;
        }

        private int get_random_wall(int []wall, int cont)
        {
            if (cont == 0)
                return -1;
            else
            {
                Random rnd = new Random();
                return wall[rnd.Next(0, cont)];
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           // MessageBox.Show((grid[1][1].y).ToString());
        }
    }
}
