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
    public partial class Form3 : Form
    {

        MySqlConnection con = new MySqlConnection("Server=localhost; Database=bisiklet_kiralama;Uid=root;Pwd='';");

        public Form3()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 frm1 = new Form2();
            frm1.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {


                if (con.State == ConnectionState.Closed)
                    con.Open();


                if (textBox5.Text == String.Empty) { 

                string ekle = "INSERT INTO bisiklet(cip_no,marka,model,temin_tarihi,renk) values (@cip_no,@marka,@model,@temin_tarihi,@renk)";
                MySqlCommand komut = new MySqlCommand(ekle, con);

                komut.Parameters.AddWithValue("@cip_no", textBox1.Text);
                komut.Parameters.AddWithValue("@marka", textBox2.Text);
                komut.Parameters.AddWithValue("@model", textBox3.Text);
                komut.Parameters.AddWithValue("@renk", textBox4.Text);
                komut.Parameters.AddWithValue("@temin_tarihi", dateTimePicker1.Value);




                komut.ExecuteNonQuery();
                con.Close();
                griddoldur();
            }

                else
                {


                    string ekle = "UPDATE bisiklet SET bisiklet_id=@bisiklet_id, cip_no=@cip_no ,marka=@marka ,model=@model ,temin_tarihi=@temin_tarihi , renk=@renk WHERE  bisiklet_id=@bisiklet_id";

                    MySqlCommand komut = new MySqlCommand(ekle, con);

                    komut.Parameters.AddWithValue("@bisiklet_id", textBox5.Text);
                    komut.Parameters.AddWithValue("@cip_no", textBox1.Text);
                    komut.Parameters.AddWithValue("@marka", textBox2.Text);
                    komut.Parameters.AddWithValue("@model", textBox3.Text);
                    komut.Parameters.AddWithValue("@temin_tarihi", dateTimePicker1.Value);
                    komut.Parameters.AddWithValue("@renk", textBox4.Text);

                    komut.ExecuteNonQuery();

                    MySqlDataAdapter da = new MySqlDataAdapter("SELECT * from bisiklet", con);
                    DataSet dt = new DataSet();


                    da.Fill(dt, "bisiklet");
                    dataGridView1.DataSource = dt.Tables["bisiklet"];
                    dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                    con.Close();
                    griddoldur();
                    MessageBox.Show("Bisiklet Güncellendi.");


                }

            }
            catch (Exception)
            {
                DialogResult Soru;

                Soru = MessageBox.Show("Seçilen çip numarasına kayıtlı bir bisiklet var. Güncellemek istar misiniz ? ?", "Uyarı",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (Soru == DialogResult.Yes)
                {


                }

                if (Soru == DialogResult.No)
                {
                    MessageBox.Show("Güncelleme Gerçekleşmedi.");
                }
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {

            griddoldur();
            dataGridView1.ClearSelection();
            dataGridView1.Columns[0].Visible = false;

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();

        }


       public void griddoldur()
        {
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter("SELECT *from bisiklet", con);
                DataSet dt = new DataSet();

                con.Open();

                da.Fill(dt, "bisiklet");
                dataGridView1.DataSource = dt.Tables["bisiklet"];
                dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;



                con.Close();
            }

            catch(Exception hata)
            {

                MessageBox.Show(" " + hata.Message);
                con.Close();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow drow in dataGridView1.SelectedRows)
            {
                int bisiklet_id = Convert.ToInt32(drow.Cells[0].Value);
                KayıtSil(bisiklet_id);
            }
            griddoldur();

        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            int currentMouseOverRow = dataGridView1.HitTest(e.X, e.Y).RowIndex;

        }

     
        void KayıtSil(int bisiklet_id)
        {

            try
            {
                string sql = "DELETE FROM bisiklet WHERE bisiklet_id=@bisiklet_id";
                MySqlCommand komut = new MySqlCommand(sql, con);
              
                komut.Parameters.AddWithValue("@bisiklet_id", bisiklet_id);
                con.Open();
              
                
                komut.ExecuteNonQuery();
                con.Close();
            }

            catch(Exception )
            {

                MessageBox.Show("Seçilen bisiklet bir istasyona zimmetli. Önce zimmeti kaldırın.");
                con.Close();

            }
        }



        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {


           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();

            
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {

            if (dataGridView1.RowCount > 0)
                if (dataGridView1.CurrentRow != null)
                {
                    textBox5.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                    textBox4.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                    dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                }
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form2 frm1 = new Form2();
            frm1.Show();
            this.Hide();
        }
    }
}
