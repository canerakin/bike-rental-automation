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
    public partial class Form4 : Form


    {

        MySqlConnection con = new MySqlConnection("Server=localhost; Database=bisiklet_kiralama;Uid=root;Pwd='';");

        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 frm1 = new Form2();
            frm1.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                if (textBox7.Text == String.Empty)
                {

                    string ekle = "INSERT INTO kisi(tc,ad,soyad,baba_adi,dogum_yeri,dogum_tarihi,ikamet_adresi) values (@tc,@ad,@soyad,@baba_adi,@dogum_yeri,@dogum_tarihi,@ikamet_adresi)";
                    MySqlCommand komut = new MySqlCommand(ekle, con);

                    komut.Parameters.AddWithValue("@tc", textBox1.Text);
                    komut.Parameters.AddWithValue("@ad", textBox2.Text);
                    komut.Parameters.AddWithValue("@soyad", textBox3.Text);
                    komut.Parameters.AddWithValue("@baba_adi", textBox4.Text);
                    komut.Parameters.AddWithValue("@dogum_yeri", textBox5.Text);
                    komut.Parameters.AddWithValue("@ikamet_adresi", textBox6.Text);
                    komut.Parameters.AddWithValue("@dogum_tarihi", dateTimePicker1.Value);




                    komut.ExecuteNonQuery();
                    con.Close();
                    griddoldur();
                }
                else 
                {
                    string ekle = "UPDATE kisi SET  kisi_id = @kisi_id ,tc=@tc ,ad=@ad ,soyad=@soyad ,baba_adi=@baba_adi , dogum_yeri=@dogum_yeri, dogum_tarihi=@dogum_tarihi, ikamet_adresi=@ikamet_adresi WHERE  kisi_id=@kisi_id";

                    MySqlCommand komut = new MySqlCommand(ekle, con);

                    komut.Parameters.AddWithValue("@kisi_id", textBox7.Text);
                    komut.Parameters.AddWithValue("@tc", textBox1.Text);
                    komut.Parameters.AddWithValue("@ad", textBox2.Text);
                    komut.Parameters.AddWithValue("@soyad", textBox3.Text);
                    komut.Parameters.AddWithValue("@baba_adi", textBox4.Text);
                    komut.Parameters.AddWithValue("@dogum_yeri", textBox5.Text);
                    komut.Parameters.AddWithValue("@ikamet_adresi", textBox6.Text);
                    komut.Parameters.AddWithValue("@dogum_tarihi", dateTimePicker1.Value);

                    komut.ExecuteNonQuery();

                    MySqlDataAdapter da = new MySqlDataAdapter("SELECT * from kisi", con);
                    DataSet dt = new DataSet();


                    da.Fill(dt, "kisi");
                    dataGridView1.DataSource = dt.Tables["kisi"];
                    dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                    con.Close();
                    griddoldur();
                    MessageBox.Show("Kişi Bilgileri Güncellendi.");

                }
            }
            catch (Exception )
            {
                DialogResult Soru;

                Soru = MessageBox.Show("Seçilen TC kimlik numarasına kayıtlı bir kişi var.Bilgileri güncellemek istar misiniz ? ?", "Uyarı",
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

        private void Form4_Load(object sender, EventArgs e)
        {
            griddoldur();
            dataGridView1.ClearSelection();
            dataGridView1.Columns[0].Visible = false;
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();

        }

        void griddoldur()
        {
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT * from kisi", con);
            DataSet dt = new DataSet();

            con.Open();

            da.Fill(dt, "kisi");
            dataGridView1.DataSource = dt.Tables["kisi"];
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;



            con.Close();


        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow drow in dataGridView1.SelectedRows)
            {
                int kisi_id = Convert.ToInt32(drow.Cells[0].Value);
                KayıtSil(kisi_id);
            }
            griddoldur();
        }

        
        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            int currentMouseOverRow = dataGridView1.HitTest(e.X, e.Y).RowIndex;


        }



        void KayıtSil(int kisi_id)
        {

            
                string sql = "DELETE FROM kisi WHERE kisi_id=@kisi_id";
                MySqlCommand komut = new MySqlCommand(sql, con);
                komut.Parameters.AddWithValue("@kisi_id", kisi_id);
                con.Open();
                komut.ExecuteNonQuery();
                con.Close();
            
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
            textBox6.Clear();
            textBox7.Clear();

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount > 0)
                if (dataGridView1.CurrentRow != null)
                {
                    textBox7.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                    textBox4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                    textBox5.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                    textBox6.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                    dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                }
        }

        private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form2 frm1 = new Form2();
            frm1.Show();
            this.Hide();
        }
    }
}
