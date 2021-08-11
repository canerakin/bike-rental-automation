using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Çalışma_2
{
    public partial class Form1 : Form
    {

        

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();

            frm.Show();
            this.Hide();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            zimmet frm = new zimmet();

            frm.Show();
            this.Hide();

        }

        private void button3_Click(object sender, EventArgs e)
        {


            kiralama frm = new kiralama();

            frm.Show();
            this.Hide();

        }
    }
}
