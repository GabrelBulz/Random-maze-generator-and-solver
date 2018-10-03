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

            Bitmap bit = new Bitmap(height, width);
            
            create_bitmap(ref bit);

            //REZOLVARE--------------------------------------------------------------------
            Depth_solve depth_solve = new Depth_solve(rows, cols, grid, nr_nodes, start_col);
            //Dijkstra_solve dij = new Dijkstra_solve(rows, cols, grid, nr_nodes,start_col);

            Stack<KeyValuePair<int, int>> road = depth_solve.get_road();

            int t = 255;
            int q = 0;
            int w = 255;
            int e = 0;

            double rate_of_change_colour = (double)255 / road.Count;
            double cont_rate_change = rate_of_change_colour;

            KeyValuePair<int, int> temp1 = road.Pop();
            
            while (road.Count > 0)
            {
                KeyValuePair<int, int> temp = road.Pop();

                int x1 = temp.Key;
                int x2 = temp1.Key;
                int y1 = temp.Value;
                int y2 = temp1.Value;

                if(x1 == x2)
                {
                    for(int k=0; k<Math.Abs(y1-y2); k++)
                    {
                        bit.SetPixel(x1 * 3 + 1, (Math.Min(y1, y2) + k) * 3 + 1, Color.FromArgb(t, q, w, e));
                        bit.SetPixel(x1 * 3 + 1, (Math.Min(y1, y2) + k) * 3 + 2, Color.FromArgb(t, q, w, e));
                        bit.SetPixel(x1 * 3 + 2, (Math.Min(y1, y2) + k) * 3 + 1, Color.FromArgb(t, q, w, e));
                        bit.SetPixel(x1 * 3 + 2, (Math.Min(y1 ,y2) + k) * 3 + 2, Color.FromArgb(t, q, w, e));
                        bit.SetPixel(x1 * 3 + 1, (Math.Min(y1, y2) + k) * 3 + 3, Color.FromArgb(t, q, w, e));
                        bit.SetPixel(x1 * 3 + 2, (Math.Min(y1, y2) + k) * 3 + 3, Color.FromArgb(t, q, w, e));
                    }
                    bit.SetPixel(x1 * 3 + 1, Math.Max(y1, y2)  * 3 + 1, Color.FromArgb(t, q, w, e));
                    bit.SetPixel(x1 * 3 + 2, Math.Max(y1, y2)  * 3 + 2, Color.FromArgb(t, q, w, e));
                    bit.SetPixel(x1 * 3 + 1, Math.Max(y1, y2) * 3 + 2, Color.FromArgb(t, q, w, e));
                    bit.SetPixel(x1 * 3 + 2, Math.Max(y1, y2) * 3 + 1, Color.FromArgb(t, q, w, e));
                }
                else
                {
                    for (int k = 0; k < Math.Abs(x1-x2); k++)
                    {
                        bit.SetPixel((Math.Min(x1, x2) + k) * 3 + 1, y2 * 3 + 1, Color.FromArgb(t, q, w, e));
                        bit.SetPixel((Math.Min(x1, x2) + k) * 3 + 1, y2 * 3 + 2, Color.FromArgb(t, q, w, e));
                        bit.SetPixel((Math.Min(x1, x2) + k) * 3 + 2, y2 * 3 + 1, Color.FromArgb(t, q, w, e));
                        bit.SetPixel((Math.Min(x1, x2) + k) * 3 + 2, y2 * 3 + 2, Color.FromArgb(t, q, w, e));
                        bit.SetPixel((Math.Min(x1, x2) + k) * 3 + 3, y2 * 3 + 2, Color.FromArgb(t, q, w, e));
                        bit.SetPixel((Math.Min(x1, x2) + k) * 3 + 3, y2 * 3 + 1, Color.FromArgb(t, q, w, e));
                    }

                }


                temp1 = temp;

                if (rate_of_change_colour > 1)
                {
                    q += (int)rate_of_change_colour;
                    w -= (int)rate_of_change_colour;
                }
                else
                {
                    if (cont_rate_change > 1)
                        cont_rate_change %= 1;
                    cont_rate_change += rate_of_change_colour;
                    q += (int)cont_rate_change;
                    w -= (int)cont_rate_change;
                }

                //rate_of_change_colour += rate_of_change_colour;
                if (q > 255)
                    q = 255;
                if (w < 0)
                    w = 0;
            }
            //END HERE--------------------------------------------------------------------

            ///color start box
            t = 255;
            q = 0;
            w = 255;
            e = 0;

            bit.SetPixel(1, start_col * 3 + 1, Color.FromArgb(t, q, w, e));
            bit.SetPixel(1, start_col * 3 + 2, Color.FromArgb(t, q, w, e));
            bit.SetPixel(2, start_col * 3 + 1, Color.FromArgb(t, q, w, e));
            bit.SetPixel(2, start_col * 3 + 2, Color.FromArgb(t, q, w, e));

            ///set finish colour
            t = 255;
            q = 255;
            w = 0;
            e = 0;

            bit.SetPixel(height - 3, width - 3, Color.FromArgb(t, q, w, e));
            bit.SetPixel(height - 2, width - 3, Color.FromArgb(t, q, w, e));
            bit.SetPixel(height - 2, width - 2, Color.FromArgb(t, q, w, e));
            bit.SetPixel(height - 3, width - 2, Color.FromArgb(t, q, w, e));

            if(height<pictureBox1.Height && width<pictureBox1.Width)
            {   ///INTERPOLATION IF FOR HOW TO MAXIMIZE THE BITMAP IT WAS USING BLEN WUHC BLEND WITHE WITH BLACK -> GREY
                Bitmap resized = new Bitmap(bit.Height * (int)(pictureBox1.Height/height), bit.Width * (int)(pictureBox1.Width / width));
                Graphics g = Graphics.FromImage(resized);
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                g.DrawImage(bit, 0, 0, bit.Height * (int)(pictureBox1.Height / height), bit.Width * (int)(pictureBox1.Width / width));
                bit = new Bitmap(resized.Height, resized.Width);
                bit = resized;
            }


            bit.RotateFlip(RotateFlipType.Rotate90FlipNone);


            bit.Save("D:\\Img_maze\\Img.png");

            pictureBox1.Image = bit;
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

                    if (ok == true)
                    {

                        int a = 255;
                        int r = 0;
                        int g = 0;
                        int b = 0;

                        nr_nodes++;
                        
                    }

                }

        }

        private void button1_Click(object sender, EventArgs e)
        {
           // MessageBox.Show((grid[1][1].y).ToString());
        }

        private void Maze_display_depth_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(1);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
