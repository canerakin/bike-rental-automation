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
    public partial class zimmet : Form
    {

        MySqlConnection con = new MySqlConnection("Server=localhost; Database=bisiklet_kiralama;Uid=root;Pwd='';");

        public zimmet()
        {
            InitializeComponent();
           
        }

        private void Form8_Load(object sender, EventArgs e)
        {
            tablodoldur();
          

            
                  

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
                        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();

            frm.Show();
            this.Hide();

        }

        private void button2_Click(object sender, EventArgs e)
        {
                try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                string ekle = "INSERT INTO zimmet(istasyon,bisiklet,istasyona_gelis,gelis_saati,teslim_alan) values (@istasyon,@bisiklet,@istasyona_gelis,@gelis_saati,@teslim_alan)";
                MySqlCommand komut = new MySqlCommand(ekle, con);


                komut.Parameters.AddWithValue("@istasyon", textBox1.Text);
                komut.Parameters.AddWithValue("@bisiklet", textBox5.Text);
                komut.Parameters.AddWithValue("@istasyona_gelis", dateTimePicker1.Value);
                komut.Parameters.AddWithValue("@gelis_saati", dateTimePicker2.Value);
                komut.Parameters.AddWithValue("@teslim_alan", textBox2.Text);

                komut.ExecuteNonQuery();
                con.Close();
                tablodoldur();
            

            }
            catch (Exception )
            {
                
                DialogResult Soru;

                Soru = MessageBox.Show("Seçilen bisiklet bir istasyona zimmetli. Zimmeti değiştirmek ister misiniz ?", "Uyarı",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                if (Soru == DialogResult.Yes)
                {

                    string ekle = "UPDATE zimmet SET istasyon=@istasyon ,istasyona_gelis=@istasyona_gelis ,gelis_saati=@gelis_saati ,teslim_alan=@teslim_alan WHERE  bisiklet=@bisiklet";

                    MySqlCommand komut = new MySqlCommand(ekle, con);

                    komut.Parameters.AddWithValue("@istasyon", textBox1.Text);
                    komut.Parameters.AddWithValue("@bisiklet", textBox5.Text);
                    komut.Parameters.AddWithValue("@istasyona_gelis", dateTimePicker1.Value);
                    komut.Parameters.AddWithValue("@gelis_saati", dateTimePicker2.Value);
                    komut.Parameters.AddWithValue("@teslim_alan", textBox2.Text);

                    komut.ExecuteNonQuery();

                    MySqlDataAdapter da = new MySqlDataAdapter("SELECT zimmet.bisiklet as Cip_No,bisiklet.marka as Bisikletin_Markasi, istasyon.istasyon_adi as İstasyon_Adi ,gorevli.gorevli_adi as Teslim_Alan_Gorevli,zimmet.istasyona_gelis as Gelis_Tarihi,zimmet.gelis_saati as Gelis_Saati from bisiklet,gorevli,zimmet,istasyon where zimmet.istasyon=istasyon.istasyon_id AND zimmet.bisiklet=bisiklet.bisiklet_id AND zimmet.teslim_alan=gorevli.gorevli_id  ", con);
                    DataSet dt = new DataSet();


                    da.Fill(dt, "zimmet");
                    dataGridView1.DataSource = dt.Tables["zimmet"];
                    dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                    con.Close();
                    tablodoldur();
                    MessageBox.Show("Zimmet değiştirildi.");
                   

                }

                if (Soru == DialogResult.No)
                {
                    MessageBox.Show("Zimmet değiştirilmedi.");
                }
            }
        }


        void tablodoldur()
        {

             MySqlDataAdapter da = new MySqlDataAdapter("SELECT bisiklet.cip_no as 'Cip Numarası',bisiklet.marka as 'Bisikletin Markasi', istasyon.istasyon_adi as 'İstasyon Adi' ,gorevli.gorevli_adi as 'Teslim Alan Gorevli',zimmet.istasyona_gelis as 'Gelis Tarihi',zimmet.gelis_saati as 'Gelis Saati' from bisiklet,gorevli,zimmet,istasyon where zimmet.istasyon=istasyon.istasyon_id AND zimmet.bisiklet=bisiklet.bisiklet_id AND zimmet.teslim_alan=gorevli.gorevli_id  ", con);
             DataSet dt = new DataSet();

             con.Open();

             da.Fill(dt, "zimmet");
             dataGridView1.DataSource = dt.Tables["zimmet"];
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            con.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow drow in dataGridView1.SelectedRows)
            {
                int bisiklet = Convert.ToInt32(drow.Cells[0].Value);
                KayıtSil(bisiklet);
            }
            tablodoldur();

        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            int currentMouseOverRow = dataGridView1.HitTest(e.X, e.Y).RowIndex;

        }

        void KayıtSil(int bisiklet)
        {
            string sql = "DELETE FROM zimmet WHERE bisiklet=@bisiklet";
            MySqlCommand komut = new MySqlCommand(sql, con);
            komut.Parameters.AddWithValue("@bisiklet", bisiklet);
            con.Open();
            komut.ExecuteNonQuery();
            con.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            comboBox3.Items.Clear();
                string sql = "select istasyon_id from istasyon where istasyon_adi ='" + comboBox1.Text + "'";
                con.Open();

                MySqlCommand komut = new MySqlCommand(sql, con);

                String a = Convert.ToString(komut.ExecuteScalar());

                con.Close();

                textBox1.Text = a;

            MySqlCommand komut2 = new MySqlCommand();
            komut2.CommandText = "SELECT *FROM gorevli WHERE gorev_yeri = " + textBox1.Text + "";
            komut2.Connection = con;
            komut2.CommandType = CommandType.Text;

            MySqlDataReader dr2;
            con.Open();
            dr2 = komut2.ExecuteReader();

            while (dr2.Read())
            {

                comboBox3.Items.Add(dr2["gorevli_adi"]);
            }

            con.Close();

        }

    

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

            string sql = "select gorevli_id from gorevli where gorevli_adi ='" + comboBox3.Text + "'";
            con.Open();

            MySqlCommand komut = new MySqlCommand(sql, con);

            String c = Convert.ToString(komut.ExecuteScalar());

            con.Close();

            textBox2.Text = c;

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            ZimmetBisiklet zimmetBisiklet = new ZimmetBisiklet();
            zimmetBisiklet.ShowDialog();


            if (zimmetBisiklet.bisikletId != 0)
            {

                int x = zimmetBisiklet.bisikletId;
                string sorgu = " SELECT cip_no FROM bisiklet WHERE bisiklet_id=" + x + "";

                con.Open();

                MySqlCommand komutf = new MySqlCommand(sorgu, con);

                String aa = Convert.ToString(komutf.ExecuteScalar());

                textBox3.Text = aa;


                string sqlaa = "select marka from bisiklet where cip_no=" + textBox3.Text + "";


                MySqlCommand komutaa = new MySqlCommand(sqlaa, con);

                String c = Convert.ToString(komutaa.ExecuteScalar());

                con.Close();

                textBox4.Text = c;
            }
            string sql = "select bisiklet_id from bisiklet where marka ='" + textBox4.Text + "'";
            con.Open();

            MySqlCommand komut = new MySqlCommand(sql, con);

            String b = Convert.ToString(komut.ExecuteScalar());

            con.Close();

            textBox5.Text = b;


        }

        private void zimmet_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 frm = new Form1();

            frm.Show();
            this.Hide();

        }
    }
}
