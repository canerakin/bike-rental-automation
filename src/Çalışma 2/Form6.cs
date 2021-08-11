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
    public partial class Form6 : Form
    {
        MySqlConnection con = new MySqlConnection("Server=localhost; Database=bisiklet_kiralama;Uid=root;Pwd='';");


        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {


           


            griddoldur();
            dataGridView1.Columns[0].Visible = false;
            MySqlCommand komut1 = new MySqlCommand();
            komut1.CommandText = "SELECT *FROM istasyon";
            komut1.Connection = con;
            komut1.CommandType = CommandType.Text;

            MySqlDataReader dr1;

            con.Open();
            dr1 = komut1.ExecuteReader();

            while (dr1.Read())
            {
                comboBox1.Items.Add(dr1["istasyon_adi"]);

            }

            con.Close();

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 frm1 = new Form2();
            frm1.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                if (textBox6.Text == string.Empty)
                {

                    string ekle = "INSERT INTO gorevli(gorevli_tc,gorevli_adi,gorevli_soyadi,gorevi,gorev_yeri) values (@gorevli_tc,@gorevli_adi,@gorevli_soyadi,@gorevi,@gorev_yeri)";
                    MySqlCommand komut = new MySqlCommand(ekle, con);

                    komut.Parameters.AddWithValue("@gorevli_tc", textBox1.Text);
                    komut.Parameters.AddWithValue("@gorevli_adi", textBox2.Text);
                    komut.Parameters.AddWithValue("@gorevli_soyadi", textBox3.Text);
                    komut.Parameters.AddWithValue("@gorevi", textBox4.Text);
                    komut.Parameters.AddWithValue("@gorev_yeri", textBox5.Text);





                    komut.ExecuteNonQuery();
                    con.Close();
                    griddoldur();
                }


                else
                {

                    string ekle = "UPDATE gorevli SET gorevli_id=@gorevli_id, gorevli_tc=@gorevli_tc , gorevli_adi=@gorevli_adi, gorevli_soyadi=@gorevli_soyadi , gorevi=@gorevi , gorev_yeri=@gorev_yeri WHERE  gorevli_id=@gorevli_id";

                    MySqlCommand komut = new MySqlCommand(ekle, con);

                    komut.Parameters.AddWithValue("@gorevli_id", textBox6.Text);
                    komut.Parameters.AddWithValue("@gorevli_tc", textBox1.Text);
                    komut.Parameters.AddWithValue("@gorevli_adi", textBox2.Text);
                    komut.Parameters.AddWithValue("@gorevli_soyadi", textBox3.Text);
                    komut.Parameters.AddWithValue("@gorevi", textBox4.Text);
                    komut.Parameters.AddWithValue("@gorev_yeri", textBox5.Text);

                    komut.ExecuteNonQuery();

                    MySqlDataAdapter da = new MySqlDataAdapter("SELECT* from gorevli", con);
                    DataSet dt = new DataSet();


                    da.Fill(dt, "gorevli");
                    dataGridView1.DataSource = dt.Tables["gorevli"];
                    dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                    MessageBox.Show("Görevli GÜncellendi."); 

                    con.Close();


                }

            }
            catch (Exception )
            {
                DialogResult Soru;

                Soru = MessageBox.Show("Seçilen görevli tc ile bir kayıt var.Bilgileri güncellemek istar misiniz ? ?", "Uyarı",
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



        void griddoldur()
        {

            MySqlDataAdapter da = new MySqlDataAdapter("SELECT * from gorevli", con);
            DataSet dt = new DataSet();

            con.Open();

            da.Fill(dt, "gorevli");
            dataGridView1.DataSource = dt.Tables["gorevli"];
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;



            con.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow drow in dataGridView1.SelectedRows)
            {
                int gorevli_id = Convert.ToInt32(drow.Cells[0].Value);
                KayıtSil(gorevli_id);
            }
            griddoldur();
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            int currentMouseOverRow = dataGridView1.HitTest(e.X, e.Y).RowIndex;


        }
        

        void KayıtSil(int gorevli_id)
        {
            try
            {

                string sql = "DELETE FROM gorevli WHERE gorevli_id=@gorevli_id";
                MySqlCommand komut = new MySqlCommand(sql, con);
                komut.Parameters.AddWithValue("@gorevli_id", gorevli_id);
                con.Open();
                komut.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception hata)
            {
                MessageBox.Show("Seçilen görevli üzerine istasyondan biziklet zimmetlenmiş. Önce zimmeti kaldırın." + hata.Message);
                con.Close();

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            string sql = "select istasyon_kodu from istasyon where istasyon_adi ='" + comboBox1.Text + "'";
            con.Open();

            MySqlCommand komut = new MySqlCommand(sql, con);

            String a = Convert.ToString(komut.ExecuteScalar());

            con.Close();

            textBox5.Text = a;


        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            textBox6.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
           
        }

        private void Form6_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form2 frm1 = new Form2();
            frm1.Show();
            this.Hide();
        }
    }
}