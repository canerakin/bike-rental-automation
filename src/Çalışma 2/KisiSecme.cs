using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Çalışma_2
{
    public partial class KisiSecme : Form
    {
        

        MySqlConnection con = new MySqlConnection("Server=localhost; Database=bisiklet_kiralama;Uid=root;Pwd='';");

        public int kisiId = 0;

        public KisiSecme()
        {
            InitializeComponent();
        }

        private void KisiSecme_Load(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();

        
        }

        private void button1_Click(object sender, EventArgs e)
        {

            dataGridView1.Columns.Clear();
            griddoldur();


        }

        public void griddoldur()
        {
            if (textBox1.Text == "" && textBox2.Text == "")
            {
                MySqlDataAdapter da = new MySqlDataAdapter("SELECT * FROM kisi WHERE tc like '%" + textBox3.Text + "%'", con);
                DataSet dt = new DataSet();

                if (con.State == ConnectionState.Closed)
                    con.Open();

                da.Fill(dt, "kisi");
                dataGridView1.DataSource = dt.Tables["kisi"];
                dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;



                con.Close();
            }

            else if (textBox1.Text == "" && textBox3.Text == "")
            {
                MySqlDataAdapter da = new MySqlDataAdapter("SELECT * FROM kisi WHERE soyad like '%"+textBox2.Text+"%'", con);
                DataSet dt = new DataSet();
                if (con.State == ConnectionState.Closed)
                    con.Open();

                da.Fill(dt, "kisi");
                dataGridView1.DataSource = dt.Tables["kisi"];
                dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            else if (textBox3.Text == "" && textBox2.Text == "")
            {
                MySqlDataAdapter da = new MySqlDataAdapter("SELECT * FROM kisi WHERE ad like '%"+textBox1.Text+"%'", con);
                DataSet dt = new DataSet();
                if (con.State == ConnectionState.Closed)
                    con.Open();

                da.Fill(dt, "kisi");
                dataGridView1.DataSource = dt.Tables["kisi"];
                dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            else if (textBox3.Text == "")
            {
                MySqlDataAdapter da = new MySqlDataAdapter("SELECT * FROM kisi WHERE ad like '%"+textBox1.Text+"%'   OR  soyad like '%"+textBox2.Text+"%'", con);
                DataSet dt = new DataSet();
                if (con.State == ConnectionState.Closed)
                    con.Open();

                da.Fill(dt, "kisi");
                dataGridView1.DataSource = dt.Tables["kisi"];
                dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            else if (textBox2.Text == "")
            {
                MySqlDataAdapter da = new MySqlDataAdapter("SELECT * FROM kisi WHERE tc like '%"+textBox3.Text+"%'  OR  ad like '%"+textBox1.Text+"%' ", con);
                DataSet dt = new DataSet();
                if (con.State == ConnectionState.Closed)
                    con.Open();

                da.Fill(dt, "kisi");
                dataGridView1.DataSource = dt.Tables["kisi"];
                dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }


            else if (textBox1.Text == "")
            {
                MySqlDataAdapter da = new MySqlDataAdapter("SELECT * FROM kisi WHERE tc like '%"+textBox3.Text+"%'  OR  soyad like '%"+textBox2.Text+"%'", con);
                DataSet dt = new DataSet();
                if (con.State == ConnectionState.Closed)
                    con.Open();

                da.Fill(dt, "kisi");
                dataGridView1.DataSource = dt.Tables["kisi"];
                dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            else

            {

                MySqlDataAdapter da = new MySqlDataAdapter("SELECT * FROM kisi WHERE tc like '%"+textBox3.Text+"%'  OR  ad like '%"+textBox1.Text+"%'   OR  soyad like '%"+textBox2.Text+"%'", con);
                DataSet dt = new DataSet();
                if (con.State == ConnectionState.Closed)
                    con.Open();

                da.Fill(dt, "kisi");
                dataGridView1.DataSource = dt.Tables["kisi"];
                dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;



                con.Close();
            }
            dataGridView1.ClearSelection();
            dataGridView1.Columns[0].Visible = false;
        }

        public void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            textBox4.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            kisiId= Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            kisiId = 0;
            this.Close();
        }
    }
}
