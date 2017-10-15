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
        Random rnd = new Random();
        int nr_nodes;
        

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
            grid = new Cell[rows][];



            for (int i = 0; i < rows; i++)
            {
                grid[i] = new Cell[cols];
                for (int j = 0; j < cols; j++)
                   grid[i][j] = new Cell(i, j);
                
            }
        
            Stack<Cell> stk = new Stack<Cell>();

            //i'll pick the start on the top row, but on a random coloumn;
            int start_col;
            start_col = rnd.Next(0, cols);
            grid[0][start_col].visited = 1;

            //push the start on the stack
            stk.Push(grid[0][start_col]);



            //button1.Click += delegate {

            //    for(int i=0; i<rows; i++)
            //        for(int j=0; j<cols; j++)
            //            MessageBox.Show(grid[rows - 1 ][0].get_wall(1).ToString() + grid[0][0].get_wall(1).ToString() + grid[0][0].get_wall(2).ToString() + grid[0][0].get_wall(3).ToString()); };



            //initiate_vect(wall);
            //cell_update(ref grid[0][start_col]);
            //get_available_walls_of_cell(wall, ref size_wall_array, grid[0][start_col]);

            //int test = get_random_wall(wall, size_wall_array);

            //for (int h = 0; h < size_wall_array; h++)
            //    Console.Write(wall[h].ToString()+" ");

            //Console.Write("picked wall "+test.ToString()+"\n");

            //int temp_cont = 0;


            while (stk.Count > 0)
            {
                Cell temp = stk.Peek();
                initiate_vect(wall);
                cell_update(ref temp);
                get_available_walls_of_cell(wall, ref size_wall_array, temp);

                int rand_wall = get_random_wall(wall, size_wall_array);
                //if(temp_cont < 30)
                //{
                //    Console.Write("i: " + temp.x.ToString() + " j: " + temp.y.ToString() + " ");

                //    for (int h = 0; h < size_wall_array; h++)
                //    Console.Write(wall[h].ToString() + " ");

                //    Console.Write("picked wall " + rand_wall.ToString() + "\n");
                //}
                

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

            //for (int i = 0; i < rows; i++)
            //    for (int j = 0; j < cols; j++)
            //        Console.Write("R:" + grid[i][j].x.ToString() + " C:" + grid[i][j].y.ToString() + " top:" + grid[i][j].get_wall(0).ToString() + " bot:" + grid[i][j].get_wall(1).ToString() + " left:" + grid[i][j].get_wall(2).ToString() + " right:" + grid[i][j].get_wall(3).ToString() + "\n");
            Bitmap bit = new Bitmap(height, width);
            
            create_bitmap(ref bit);


            ///color start box
            int t = 255;
            int q = 0;
            int w = 255;
            int e = 0;

            bit.SetPixel(1, start_col * 3 + 1, Color.FromArgb(t, q, w, e));
            bit.SetPixel(1, start_col * 3 + 2, Color.FromArgb(t, q, w, e));
            bit.SetPixel(2, start_col * 3 + 1, Color.FromArgb(t, q, w, e));
            bit.SetPixel(2, start_col * 3 + 2, Color.FromArgb(t, q, w, e));

            ///set finish colour
            t = 255;
            q = 255;
            w = 0;
            e = 0; 

            bit.SetPixel(height-3, width - 3, Color.FromArgb(t, q, w, e));
            bit.SetPixel(height-2, width - 3, Color.FromArgb(t, q, w, e));
            bit.SetPixel(height-2, width - 2, Color.FromArgb(t, q, w, e));
            bit.SetPixel(height-3, width - 2, Color.FromArgb(t, q, w, e));

            //for (int temp1 = 0; temp1 < height; temp1++)
            //    for (int temp2 = 0; temp2 < width; temp2++)
            //        if (temp1 > 0 && temp2 > 0)
            //            if (bit.GetPixel(temp1 - 1, temp2) == bit.GetPixel(temp1, temp2 - 1))
            //                bit.SetPixel(temp1, temp2, bit.GetPixel(temp1 - 1, temp2));

            bit.RotateFlip(RotateFlipType.Rotate90FlipNone);


            bit.Save("D:\\Img_maze\\Img.png");

            pictureBox1.Image = bit;

            Console.Write(nr_nodes.ToString()+"\n");
            Dijkstra_solve dij = new Dijkstra_solve(rows, cols, grid, nr_nodes);

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


        private void get_unavailable_walls_of_cell(int[] wall, ref int cont, Cell x)
        {
            cont = 0;

            for (int i = 0; i < 4; i++)
                if (x.get_wall(i) == 0 || x.get_wall(i) == 1)
                    wall[cont++] = i;
        }

        private int get_random_wall(int []wall, int cont)
        {
            if (cont == 0)
                return -1;
            else
            {
                return wall[rnd.Next(0, cont)];
            }
        }


        private void create_bitmap(ref Bitmap bit)
        {

            nr_nodes = 0;

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                {
                    if (grid[i][j].get_wall(0) == -1)
                    {
                        int a = 125;
                        int r = 137;
                        int g = 0;
                        int b = 255;

                        for (int k = 0; k < 3; k++)
                            bit.SetPixel(i * 3, j * 3 + k, Color.FromArgb(a, r, g, b));
                    }

                    if (grid[i][j].get_wall(1) == -1)
                    {
                        int a = 125;
                        int r = 137;
                        int g = 0;
                        int b = 255;

                        for (int k = 0; k <= 3; k++)
                            bit.SetPixel((i + 1) * 3, j * 3 + k, Color.FromArgb(a, r, g, b));
                    }

                    if (grid[i][j].get_wall(2) == -1)
                    {
                        int a = 125;
                        int r = 137;
                        int g = 0;
                        int b = 255;

                        for (int k = 0; k < 3; k++)
                            bit.SetPixel(i * 3 + k, j * 3, Color.FromArgb(a, r, g, b));
                    }

                    if (grid[i][j].get_wall(3) == -1)
                    {
                        int a = 125;
                        int r = 137;
                        int g = 0;
                        int b = 255;

                        for (int k = 0; k < 3; k++)
                            bit.SetPixel(i * 3 + k, (j + 1) * 3, Color.FromArgb(a, r, g, b));
                    }

                    //set box
                    {
                        int a = 125;
                        int r = 255;
                        int g = 255;
                        int b = 255;

                        bit.SetPixel(i * 3 + 1, j * 3 + 1, Color.FromArgb(a, r, g, b));
                        bit.SetPixel(i * 3 + 1, j * 3 + 2, Color.FromArgb(a, r, g, b));
                        bit.SetPixel(i * 3 + 2, j * 3 + 1, Color.FromArgb(a, r, g, b));
                        bit.SetPixel(i * 3 + 2, j * 3 + 2, Color.FromArgb(a, r, g, b));
                    }
                }

            int[] wall = new int[4];
            int size_wall_arr=0;

            //Cell test_1 = new Cell(1001, 1001);
            //test_1.block_wall(0);
            //test_1.block_wall(2);

            //Boolean ok = false;

            //get_unavailable_walls_of_cell(wall, ref size_wall_arr, test_1);

            //for (int k = 0; k < size_wall_arr - 1; k++)
            //    for (int z = k + 1; z < size_wall_arr; z++)
            //    {
            //        if (Math.Abs(wall[z] - wall[k]) >= 2)
            //            ok = true;
            //        Console.Write(wall[k].ToString() + " " + wall[z].ToString() + "\n");
            //    }

            //Console.Write(ok.ToString());



            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                {
                    Boolean ok = false;

                    get_unavailable_walls_of_cell(wall, ref size_wall_arr, grid[i][j]);

                    for (int k = 0; k < size_wall_arr - 1; k++)
                        for (int z = k + 1; z < size_wall_arr; z++)
                            if (Math.Abs(wall[z] - wall[k]) >= 2)
                                ok = true;

                    ///caz special 1
                    for (int k = 0; k < size_wall_arr; k++)
                        if (wall[k] == 1 && k < size_wall_arr - 1)
                            if (wall[k + 1] == 2)
                                ok = true;

                    //caz 2
                    if (size_wall_arr == 1)
                        ok = true;

                    ///// caz special 2
                    //for (int k = 0; k < size_wall_arr; k++)
                    //    if (wall[k] == 1)
                    //        for(int z=k+1; z < size_wall_arr; z++)
                    //            if(wall[z] == )



                    if (ok == true)
                    {

                        int a = 255;
                        int r = 0;
                        int g = 0;
                        int b = 0;

                        bit.SetPixel(i * 3 + 1, j * 3 + 1, Color.FromArgb(a, r, g, b));
                        bit.SetPixel(i * 3 + 1, j * 3 + 2, Color.FromArgb(a, r, g, b));
                        bit.SetPixel(i * 3 + 2, j * 3 + 1, Color.FromArgb(a, r, g, b));
                        bit.SetPixel(i * 3 + 2, j * 3 + 2, Color.FromArgb(a, r, g, b));

                        nr_nodes++;

                        Console.Write(nr_nodes.ToString() + "\n");
                    }

                }

        }

        private void button1_Click(object sender, EventArgs e)
        {
           // MessageBox.Show((grid[1][1].y).ToString());
        }
    }
}
