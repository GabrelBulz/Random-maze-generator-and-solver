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
    public partial class Maze_diplay_prim : Form
    {
        Cell[][] grid;
        int rows;
        int cols;
        static int height;
        static int width;
        private Random rnd = new Random(); //i've created this rand here because otherwise the seed necesarry for rand w;ll be the same if a create a random into a loop (the time between object initilisation is too small)
        private int nr_nodes;
        public Maze_diplay_prim()
        {
            InitializeComponent();

            int start_col;

            height = Form1.rows;
            width = Form1.cols;

            cal_matrix();
            Cell.rows = rows;
            Cell.cols = cols;
            grid = new Cell[rows][];
            start_col = rnd.Next(0, cols);



            for (int i = 0; i < rows; i++)
            {
                grid[i] = new Cell[cols];
                for (int j = 0; j < cols; j++)
                    grid[i][j] = new Cell(i, j);

            }
            create_maze(start_col);

            Bitmap bit = new Bitmap(height, width);
            create_bitmap(ref bit);
            //REZOLVARE--------------------------------------------------------------------
            Dijkstra_solve dij = new Dijkstra_solve(rows, cols, grid, nr_nodes, start_col);

            Stack<KeyValuePair<int, int>> road = dij.get_road();

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

                if (x1 == x2)
                {
                    for (int k = 0; k < Math.Abs(y1 - y2); k++)
                    {
                        bit.SetPixel(x1 * 3 + 1, (Math.Min(y1, y2) + k) * 3 + 1, Color.FromArgb(t, q, w, e));
                        bit.SetPixel(x1 * 3 + 1, (Math.Min(y1, y2) + k) * 3 + 2, Color.FromArgb(t, q, w, e));
                        bit.SetPixel(x1 * 3 + 2, (Math.Min(y1, y2) + k) * 3 + 1, Color.FromArgb(t, q, w, e));
                        bit.SetPixel(x1 * 3 + 2, (Math.Min(y1, y2) + k) * 3 + 2, Color.FromArgb(t, q, w, e));
                        bit.SetPixel(x1 * 3 + 1, (Math.Min(y1, y2) + k) * 3 + 3, Color.FromArgb(t, q, w, e));
                        bit.SetPixel(x1 * 3 + 2, (Math.Min(y1, y2) + k) * 3 + 3, Color.FromArgb(t, q, w, e));
                    }
                    bit.SetPixel(x1 * 3 + 1, Math.Max(y1, y2) * 3 + 1, Color.FromArgb(t, q, w, e));
                    bit.SetPixel(x1 * 3 + 2, Math.Max(y1, y2) * 3 + 2, Color.FromArgb(t, q, w, e));
                    bit.SetPixel(x1 * 3 + 1, Math.Max(y1, y2) * 3 + 2, Color.FromArgb(t, q, w, e));
                    bit.SetPixel(x1 * 3 + 2, Math.Max(y1, y2) * 3 + 1, Color.FromArgb(t, q, w, e));
                }
                else
                {
                    for (int k = 0; k < Math.Abs(x1 - x2); k++)
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

            if (height < pictureBox1.Height && width < pictureBox1.Width)
            {   ///INTERPOLATION IF FOR HOW TO MAXIMIZE THE BITMAP IT WAS USING BLEN WUHC BLEND WITHE WITH BLACK -> GREY
                Bitmap resized = new Bitmap(bit.Height * (int)(pictureBox1.Height / height), bit.Width * (int)(pictureBox1.Width / width));
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

        private void create_maze(int start_col)
        {
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    grid[i][j].visited = 0;

            List<Cell> stk = new List<Cell>();
           grid[0][start_col].visited = 1;

            

            //push the neighbour of the first node and mark it as visited
            stk.Add(grid[1][start_col]);//down neighbour
            grid[1][start_col].son_of = new KeyValuePair<int, int>(grid[0][start_col].x, grid[0][start_col].y);

            if (start_col > 0)//right
            {

                grid[0][start_col - 1].son_of = new KeyValuePair<int, int>(grid[0][start_col].x, grid[0][start_col].y);
                stk.Add(grid[0][start_col - 1]);
            }
            if (start_col < cols - 1)//left
            {
                grid[0][start_col + 1].son_of = new KeyValuePair<int, int>(grid[0][start_col].x, grid[0][start_col].y);
                stk.Add(grid[0][start_col + 1]);
            }




            int[] wall = new int[4];
            int cont_walls = 0;

            int cont = 0;
            while(stk.Count > 0)
            {
                Cell temp = stk.ElementAt(rnd.Next(0, stk.Count));

                int wall_to_parent_to_be_deleted;

                //determine what type of child is
                if(temp.x == temp.son_of.Key)
                {
                    if (temp.y > temp.son_of.Value)
                        wall_to_parent_to_be_deleted = 2;
                    else
                        wall_to_parent_to_be_deleted = 3;
                }
                else
                {
                    if (temp.x > temp.son_of.Key)
                        wall_to_parent_to_be_deleted = 0;
                    else
                        wall_to_parent_to_be_deleted = 1;
                }

                int type_of_son = type_son(wall_to_parent_to_be_deleted);

                temp.delete_wall(wall_to_parent_to_be_deleted);
                grid[temp.son_of.Key][temp.son_of.Value].delete_wall(type_of_son);


                get_available_walls_of_cell(wall, ref cont_walls, temp);

                for (int i=0; i<cont_walls; i++)
                {
                    if(wall[i] == 0)
                    {
                        if (stk.Contains(grid[temp.x - 1][temp.y]) == false && grid[temp.x - 1][temp.y].visited == 0)
                        {
                            grid[temp.x - 1][temp.y].son_of = new KeyValuePair<int, int>(temp.x, temp.y);
                            stk.Add(grid[temp.x - 1][temp.y]);
                        }
                    }
                    if(wall[i] == 1)
                    {
                        if (stk.Contains(grid[temp.x + 1][temp.y]) == false && grid[temp.x + 1][temp.y].visited == 0)
                        {
                            grid[temp.x + 1][temp.y].son_of = new KeyValuePair<int, int>(temp.x, temp.y);
                            stk.Add(grid[temp.x + 1][temp.y]);
                        }
                    }
                    if(wall[i] == 2)
                    {
                        if (stk.Contains(grid[temp.x][temp.y - 1]) == false && grid[temp.x][temp.y - 1].visited == 0)
                        {
                            grid[temp.x][temp.y - 1].son_of = new KeyValuePair<int, int>(temp.x, temp.y);
                            stk.Add(grid[temp.x][temp.y - 1]);
                        }
                    }
                    if(wall[i] == 3)
                    {
                        if (stk.Contains(grid[temp.x][temp.y + 1]) == false && grid[temp.x][temp.y + 1].visited == 0)
                        {
                            grid[temp.x][temp.y + 1].son_of = new KeyValuePair<int, int>(temp.x, temp.y);
                            stk.Add(grid[temp.x][temp.y + 1]);
                        }
                    }
                }

                temp.visited = 1;
                stk.Remove(temp);
                cont++;
            }

            for(int i=0; i<rows; i++)
                for(int j=0; j<cols; j++)
                {
                    if (grid[i][j].get_wall(0) == 1)
                        grid[i][j].block_wall(0);
                    if (grid[i][j].get_wall(2) == 1)
                        grid[i][j].block_wall(2);
                    if (grid[i][j].get_wall(1) == 1)
                        grid[i][j].block_wall(1);
                    if (grid[i][j].get_wall(3) == 1)
                        grid[i][j].block_wall(3);
                }
        }

        private int type_son(int i)
        {
            if (i == 0) return 1;
            if (i == 1) return 0;
            if (i == 2) return 3;
            else return 2;
        }
        private void get_available_walls_of_cell(int[] wall, ref int cont, Cell x)
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
                if (x.get_wall(i) == 0)
                    wall[cont++] = i;
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
            int size_wall_arr = 0;


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

                        //bit.SetPixel(i * 3 + 1, j * 3 + 1, Color.FromArgb(a, r, g, b));
                        //bit.SetPixel(i * 3 + 1, j * 3 + 2, Color.FromArgb(a, r, g, b));
                        //bit.SetPixel(i * 3 + 2, j * 3 + 1, Color.FromArgb(a, r, g, b));
                        //bit.SetPixel(i * 3 + 2, j * 3 + 2, Color.FromArgb(a, r, g, b));

                        nr_nodes++;

                    }

                }

        }

        private void Maze_diplay_prim_Load(object sender, EventArgs e)
        {

        }

        private void Maze_diplay_prim_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(1);
        }
    }
}
