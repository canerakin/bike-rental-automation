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
    public partial class ZimmetBisiklet : Form
    {
        MySqlConnection con = new MySqlConnection("Server=localhost; Database=bisiklet_kiralama;Uid=root;Pwd='';");

        public int bisikletId = 0;

        public ZimmetBisiklet()
        {
            InitializeComponent();
        }

        private void ZimmetBisiklet_Load(object sender, EventArgs e)
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
                MySqlDataAdapter da = new MySqlDataAdapter("SELECT bisiklet_id,cip_no,marka,model,temin_tarihi,renk,durum FROM bisiklet WHERE cip_no like '%"+textBox3.Text+"%'  ", con);
                DataSet dt = new DataSet();

                if (con.State == ConnectionState.Closed)
                    con.Open();

                da.Fill(dt, "bisiklet");
                dataGridView1.DataSource = dt.Tables["bisiklet"];
                dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;



                con.Close();
            }

            else if (textBox1.Text == "" && textBox3.Text == "")
            {
                MySqlDataAdapter da = new MySqlDataAdapter("SELECT bisiklet_id,cip_no,marka,model,temin_tarihi,renk,durum FROM bisiklet WHERE model like '%"+textBox2.Text+"%' ", con);
                DataSet dt = new DataSet();
                if (con.State == ConnectionState.Closed)
                    con.Open();

                da.Fill(dt, "bisiklet");
                dataGridView1.DataSource = dt.Tables["bisiklet"];
                dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            else if (textBox3.Text == "" && textBox2.Text == "")
            {
                MySqlDataAdapter da = new MySqlDataAdapter("SELECT bisiklet_id,cip_no,marka,model,temin_tarihi,renk,durum FROM bisiklet WHERE marka like '%"+textBox1.Text+"%' ", con);
                DataSet dt = new DataSet();
                if (con.State == ConnectionState.Closed)
                    con.Open();

                da.Fill(dt, "bisiklet");
                dataGridView1.DataSource = dt.Tables["bisiklet"];
                dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            else if (textBox3.Text == "")
            {
                MySqlDataAdapter da = new MySqlDataAdapter("SELECT bisiklet_id,cip_no,marka,model,temin_tarihi,renk,durum FROM bisiklet WHERE marka like '%"+textBox1.Text+"%'   OR  model like '%"+textBox2.Text+"%' ", con);
                DataSet dt = new DataSet();
                if (con.State == ConnectionState.Closed)
                    con.Open();

                da.Fill(dt, "bisiklet");
                dataGridView1.DataSource = dt.Tables["bisiklet"];
                dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            else if (textBox2.Text == "")
            {
                MySqlDataAdapter da = new MySqlDataAdapter("SELECT bisiklet_id,cip_no,marka,model,temin_tarihi,renk,durum FROM bisiklet WHERE cip_no like '%"+textBox3.Text+"%'  OR  marka like '%"+textBox1.Text+"%' ", con);
                DataSet dt = new DataSet();
                if (con.State == ConnectionState.Closed)
                    con.Open();

                da.Fill(dt, "bisiklet");
                dataGridView1.DataSource = dt.Tables["bisiklet"];
                dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }


            else if (textBox1.Text == "")
            {
                MySqlDataAdapter da = new MySqlDataAdapter("SELECT bisiklet_id,cip_no,marka,model,temin_tarihi,renk,durum FROM bisiklet WHERE cip_no like '%"+textBox3.Text+"%'  OR  model like '%"+textBox2.Text+"%'", con);
                DataSet dt = new DataSet();
                if (con.State == ConnectionState.Closed)
                    con.Open();

                da.Fill(dt, "bisiklet");
                dataGridView1.DataSource = dt.Tables["bisiklet"];
                dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            else

            {

                MySqlDataAdapter da = new MySqlDataAdapter("SELECT bisiklet_id,cip_no,marka,model,temin_tarihi,renk,durum FROM bisiklet WHERE cip_no like '%"+textBox3.Text+"%'  OR  marka like '%"+textBox1.Text+"%'   OR  t.model like '%"+textBox2.Text+"%'", con);
                DataSet dt = new DataSet();
                if (con.State == ConnectionState.Closed)
                    con.Open();

                da.Fill(dt, "bisiklet");
                dataGridView1.DataSource = dt.Tables["bisiklet"];
                dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;



                con.Close();
            }

            dataGridView1.ClearSelection();
            dataGridView1.Columns[0].Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            bisikletId = 0;
            this.Close();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {

            textBox4.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            bisikletId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);

        }
    }
}
