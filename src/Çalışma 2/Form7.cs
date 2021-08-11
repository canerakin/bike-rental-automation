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
    public partial class Form7 : Form
    {
        MySqlConnection con = new MySqlConnection("Server=localhost; Database=bisiklet_kiralama;Uid=root;Pwd='';");

        public Form7()
        {
            InitializeComponent();
        }
        private void Form7_Load(object sender, EventArgs e)
        {

            griddoldur();

            textBox1.Clear();
            textBox2.Clear();
            dataGridView1.Columns[0].Visible = false;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                if (textBox2.Text == string.Empty)
                {
                    string ekle = "INSERT INTO ucret(ucret_baslangic,saatlik_ucret) values (@ucret_baslangic,@saatlik_ucret)";
                    MySqlCommand komut = new MySqlCommand(ekle, con);

                    komut.Parameters.AddWithValue("@saatlik_ucret", textBox1.Text);
                    komut.Parameters.AddWithValue("@ucret_baslangic", dateTimePicker1.Value);



                    komut.ExecuteNonQuery();
                    con.Close();
                    griddoldur();

                }

                else
                {

                    string ekle = "UPDATE ucret SET ucret_id=@ucret_id ,saatlik_ucret=@saatlik_ucret , ucret_baslangic=@ucret_baslangic WHERE  ucret_id=@ucret_id";

                    MySqlCommand komut = new MySqlCommand(ekle, con);

                    komut.Parameters.AddWithValue("@ucret_id", textBox2.Text);
                    komut.Parameters.AddWithValue("@saatlik_ucret", textBox1.Text);
                    komut.Parameters.AddWithValue("@ucret_baslangic", dateTimePicker1.Value);

                    komut.ExecuteNonQuery();

                    MySqlDataAdapter da = new MySqlDataAdapter("SELECT* from ucret", con);
                    DataSet dt = new DataSet();


                    da.Fill(dt, "ucret");
                    dataGridView1.DataSource = dt.Tables["ucret"];
                    dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                    con.Close();

                }
            }
            catch (Exception)
            {
                DialogResult Soru;

                Soru = MessageBox.Show("Seçilen güne kayıtlı bir tarife var.Bilgileri güncellemek istar misiniz ? ?", "Uyarı",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (Soru == DialogResult.Yes)
                {

                   
                    MessageBox.Show("Görevli Bilgileri Güncellendi.");

                }

                if (Soru == DialogResult.No)
                {
                    MessageBox.Show("Güncelleme Gerçekleşmedi.");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Form2 frm1 = new Form2();
            frm1.Show();
            this.Hide();

        }

        void griddoldur()
        {

            MySqlDataAdapter da = new MySqlDataAdapter("SELECT * from ucret", con);
            DataSet dt = new DataSet();

            con.Open();

            da.Fill(dt, "ucret");
            dataGridView1.DataSource = dt.Tables["ucret"];
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;



            con.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow drow in dataGridView1.SelectedRows)
            {
                int ucret_id = Convert.ToInt32(drow.Cells[0].Value);
                KayıtSil(ucret_id);
            }
            griddoldur();

        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            int currentMouseOverRow = dataGridView1.HitTest(e.X, e.Y).RowIndex;


        }

        void KayıtSil(int ucret_id)
        {
       
                string sql = "DELETE FROM ucret WHERE ucret_id=@ucret_id";
                MySqlCommand komut = new MySqlCommand(sql, con);
                komut.Parameters.AddWithValue("@ucret_id", ucret_id);
                con.Open();
                komut.ExecuteNonQuery();
                con.Close();
         
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            textBox1.Clear();
        }

        private void Form7_FormClosed(object sender, FormClosedEventArgs e)
        {

            Form2 frm1 = new Form2();
            frm1.Show();
            this.Hide();
        }
    }
}
