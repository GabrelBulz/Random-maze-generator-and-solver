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
    
    public partial class Form1 : Form
    {
        public static int rows;
        public static int cols;
        public Form1()
        {
            InitializeComponent();

           

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string a = textBox1.Text;
            string b = textBox2.Text;


            ///////////NU UITA SA IMPLEMENTEZI PT CAZU IN CARE BAGA DIM PREA MICI!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            //////////!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            if (a.Length<=0 || b.Length<=0)
                MessageBox.Show("You should enter a bigger size than 0");
            else
            {
                rows = Convert.ToInt32(a);
                cols = Convert.ToInt32(b);




                Form form2 = new Maze_display_depth();
                form2.Show();
                form2.Focus();
                this.Hide();
            }
            

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            string a = textBox1.Text;
            string b = textBox2.Text;


            ///////////NU UITA SA IMPLEMENTEZI PT CAZU IN CARE BAGA DIM PREA MICI!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            //////////!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            if (a.Length <= 0 || b.Length <= 0)
                MessageBox.Show("You should enter a bigger size than 0");
            else
            {
                rows = Convert.ToInt32(a);
                cols = Convert.ToInt32(b);




                Form form2 = new Maze_diplay_prim();
                form2.Show();
                form2.Focus();
                this.Hide();
            }


        }
    }
}
