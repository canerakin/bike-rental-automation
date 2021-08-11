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
    public partial class kiralama : Form
    {


       // public static int degisken;


        MySqlConnection con = new MySqlConnection("Server=localhost; Database=bisiklet_kiralama;Uid=root;Pwd='';");

        public kiralama()
        {
            InitializeComponent();

            List<string> sayilar = new List<string>();
        }

       

        private void Form9_Load(object sender, EventArgs e)
        {

            KisiSecme kisiSecme = new KisiSecme();
            BisikletSecme bisikletSecme = new BisikletSecme();
            tablodoldur();

            
      


            MySqlCommand komuta = new MySqlCommand();
            komuta.CommandText = "SELECT *FROM istasyon";
            komuta.Connection = con;
            komuta.CommandType = CommandType.Text;

            MySqlDataReader dra;

            con.Open();
            dra = komuta.ExecuteReader();


            while (dra.Read())
            {
                comboBox6.Items.Add(dra["istasyon_adi"]);
            }
            con.Close();


        }

        private void button1_Click(object sender, EventArgs e)
        {

            Form1 frm = new Form1();

            frm.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }
        
        private void button2_Click(object sender, EventArgs e) //Kiralama Butonu
        {
            con.Open();

            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
              
                string ekle = "INSERT INTO kiralama1(kiralanan_bisiklet,kiralayan_kisi,kiralanan_istasyon,teslim_tarihi,teslim_saati,teslim_alan) values (@kiralanan_bisiklet,@kiralayan_kisi,@kiralanan_istasyon,@teslim_tarihi,@teslim_saati,@teslim_alan)";
                MySqlCommand komut = new MySqlCommand(ekle, con);

              
                komut.Parameters.AddWithValue("@kiralanan_bisiklet", textBox2.Text);
                komut.Parameters.AddWithValue("@kiralayan_kisi", textBox18.Text);
                komut.Parameters.AddWithValue("@kiralanan_istasyon", textBox17.Text);
                komut.Parameters.AddWithValue("@teslim_tarihi", dateTimePicker1.Value);
                komut.Parameters.AddWithValue("@teslim_saati", dateTimePicker2.Value);
                komut.Parameters.AddWithValue("@teslim_alan", textBox4.Text);

                string sorgu = "UPDATE bisiklet SET durum='kirada' where bisiklet_id=" + textBox2.Text + "";
                MySqlCommand komuta = new MySqlCommand(sorgu, con);

                string a = "kirada";

                komuta.Parameters.AddWithValue("@durum", a);


                komuta.ExecuteNonQuery();
                komut.ExecuteNonQuery();
                con.Close();
                tablodoldur();

               
               

            }
            catch (Exception )
            {
                string sorgu = "UPDATE bisiklet SET durum='kiradadegil' where bisiklet_id=" + textBox2.Text + "";
                MySqlCommand komuta = new MySqlCommand(sorgu, con);

                string a = "kiradadegil";

                komuta.Parameters.AddWithValue("@durum", a);

                komuta.ExecuteNonQuery();
                MessageBox.Show("Bilgileri kontrol edip tekrar deneyin." );

            }

        }


        void tablodoldur()
        {


               MySqlDataAdapter da = new MySqlDataAdapter("Select k.kiralama_id as KiraNo, CONCAT_WS('-', b1.cip_no, b1.marka, b1.model) as KiralananBisiklet, CONCAT_WS('-', ks.ad, ks.soyad) as AdiSoyadi, CONCAT_WS('-', i1.istasyon_kodu, i1.istasyon_adi) as Kiralananİstasyon, k.teslim_tarihi, k.teslim_saati, CONCAT_WS('-', g1.gorevli_tc, g1.gorevi, g1.gorevli_adi, g1.gorevli_soyadi) as TeslimAlinanGorevli, k.iade_tarihi, k.iade_saati, CONCAT_WS('-', i2.istasyon_kodu, i2.istasyon_adi) as İadeEdilenİstasyon, CONCAT_WS('-', g2.gorevli_tc, g2.gorevi, g2.gorevli_adi, g2.gorevli_soyadi) as IadeEdilenGorevli, k.kiralama_suresi, k.kira_ucret From kiralama1 k left outer join kisi ks on ks.kisi_id = k.kiralayan_kisi left outer join istasyon as i1 on i1.istasyon_id = k.kiralanan_istasyon left outer join gorevli as g1 on g1.gorevli_id = k.teslim_alan left outer join gorevli as g2 on g2.gorevli_id = k.iade_gorevli left outer join istasyon as i2 on i2.istasyon_id = k.iade_istasyon left outer join bisiklet as b1 on b1.bisiklet_id = k.kiralanan_bisiklet ", con);
                   



            DataSet dt = new DataSet();
           
            con.Open();
            da.Fill(dt, "kiralama1");

                
                dataGridView1.DataSource = dt.Tables["kiralama1"];
          
            con.Close();

        }


        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            int currentMouseOverRow = dataGridView1.HitTest(e.X, e.Y).RowIndex;

        }

        private void button3_Click(object sender, EventArgs e)  //teslim butonu
        {


            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                string kiralananBisiklet = textBox5.Text;
                string ekle = "UPDATE  kiralama1 SET iade_tarihi=@iade_tarihi ,iade_saati= @iade_saati,iade_istasyon=@iade_istasyon,iade_gorevli=@iade_gorevli,kiralama_suresi=@kiralama_suresi,kira_ucret=@kira_ucret WHERE  kiralama_id="+textBox19.Text+" ";
                string ekler = "UPDATE zimmet SET istasyon=@istasyon ,istasyona_gelis=@istasyona_gelis ,gelis_saati=@gelis_saati ,teslim_alan=@teslim_alan WHERE  bisiklet=@bisiklet";
                MySqlCommand komut = new MySqlCommand(ekle, con);
                MySqlCommand komutr = new MySqlCommand(ekler, con);

                komut.Parameters.AddWithValue("@kiralama_id", textBox19.Text);
                komut.Parameters.AddWithValue("@kiralanan_bisiklet", textBox5.Text);
                komut.Parameters.AddWithValue("@iade_tarihi", dateTimePicker3.Value);
                komut.Parameters.AddWithValue("@iade_saati", dateTimePicker4.Value);
                komut.Parameters.AddWithValue("@iade_istasyon", textBox6.Text); 
                komut.Parameters.AddWithValue("@iade_gorevli", textBox8.Text);
                komut.Parameters.AddWithValue("@kiralama_suresi", textBox10.Text);
                komut.Parameters.AddWithValue("@kira_ucret", textBox7.Text);

                komut.ExecuteNonQuery();


                komutr.Parameters.AddWithValue("@istasyon", textBox6.Text);
                komutr.Parameters.AddWithValue("@bisiklet", textBox5.Text);
                komutr.Parameters.AddWithValue("@istasyona_gelis", dateTimePicker3.Value);
                komutr.Parameters.AddWithValue("@gelis_saati", dateTimePicker4.Value);
                komutr.Parameters.AddWithValue("@teslim_alan", textBox8.Text);

                komutr.ExecuteNonQuery();


                string sorgu = "UPDATE bisiklet SET durum='kiradadegil' where bisiklet_id=" + textBox5.Text + "";
                MySqlCommand komuta = new MySqlCommand(sorgu, con);

                string a = "kiradadegil";

                komuta.Parameters.AddWithValue("@durum", a);

                komuta.ExecuteNonQuery();

                con.Close();

                tablodoldur();
             

            }

            catch (Exception hata)
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
            }
         

        }

       


      

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql = "select gorevli_id from gorevli where gorevli_adi ='" + comboBox5.Text + "'";
            con.Open();

            MySqlCommand komut = new MySqlCommand(sql, con);

            String c = Convert.ToString(komut.ExecuteScalar());

            con.Close();

            textBox4.Text = c;
        }

       

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {

            comboBox7.Items.Clear();
            string sql = "select istasyon_id from istasyon where istasyon_adi ='" + comboBox6.Text + "'";
            con.Open();

            MySqlCommand komut = new MySqlCommand(sql, con);

            String a = Convert.ToString(komut.ExecuteScalar());

            con.Close();

            textBox6.Text = a;


            MySqlCommand komutb = new MySqlCommand();
            komutb.CommandText = "SELECT *FROM gorevli WHERE gorev_yeri = "+textBox6.Text+"";
            komutb.Connection = con;
            komutb.CommandType = CommandType.Text;

            MySqlDataReader drb;

            con.Open();
            drb = komutb.ExecuteReader();


            while (drb.Read())
            {
                comboBox7.Items.Add(drb["gorevli_adi"]);
            }
            con.Close();

        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sql = "select gorevli_id from gorevli where gorevli_adi ='" + comboBox7.Text + "'";
            con.Open();

            MySqlCommand komut = new MySqlCommand(sql, con);

            String c = Convert.ToString(komut.ExecuteScalar());

            con.Close();

            textBox8.Text = c;
        }


 

        private  void button5_Click(object sender, EventArgs e)
        {
            KisiSecme kisiSecme = new KisiSecme();
            kisiSecme.ShowDialog();

            if (kisiSecme.kisiId != 0)
                {

                int y = kisiSecme.kisiId;
                string sorgu = " SELECT tc FROM kisi WHERE kisi_id="+y+"";

                con.Open();

                MySqlCommand komutf = new MySqlCommand(sorgu, con);

                String a = Convert.ToString(komutf.ExecuteScalar());

                textBox15.Text = a;
               

                string sql = "select ad from kisi where tc="+textBox15.Text+"";


                MySqlCommand komut = new MySqlCommand(sql, con);

                String c = Convert.ToString(komut.ExecuteScalar());


                textBox1.Text = c;

                string sqlxx = "select kisi_id from kisi where tc=" + textBox15.Text + "";


                MySqlCommand komutxx = new MySqlCommand(sqlxx, con);

                String cxx = Convert.ToString(komutxx.ExecuteScalar());

                con.Close();

                textBox18.Text = cxx;


            }
            
            kisiSecme.Dispose();
            
        }

        private  void button6_Click(object sender, EventArgs e)
        {
            BisikletSecme bisikletSecme = new BisikletSecme();
            bisikletSecme.ShowDialog();

            if (bisikletSecme.bisikletId != 0)
            {

                int x = bisikletSecme.bisikletId;
                string sorgu = " SELECT cip_no FROM bisiklet WHERE bisiklet_id="+x+"";

                con.Open();

                MySqlCommand komutf = new MySqlCommand(sorgu, con);

                String aa = Convert.ToString(komutf.ExecuteScalar());

                textBox16.Text = aa;


                string sqlaa = "select marka from bisiklet where cip_no=" + textBox16.Text + "";


                MySqlCommand komutaa = new MySqlCommand(sqlaa, con);

                String c = Convert.ToString(komutaa.ExecuteScalar());

                con.Close();

                textBox13.Text = c;

                string sql = "select bisiklet_id from bisiklet where marka ='" + textBox13.Text + "'";
                con.Open();

                MySqlCommand komut = new MySqlCommand(sql, con);

                String b = Convert.ToString(komut.ExecuteScalar());

                con.Close();

                textBox2.Text = b;


                MySqlCommand komut1 = new MySqlCommand();
                komut1.CommandText = "SELECT *FROM zimmet where bisiklet= " + textBox16.Text + "";
                komut1.Connection = con;
                komut1.CommandType = CommandType.Text;

                MySqlDataReader dr1;

                con.Open();
                dr1 = komut1.ExecuteReader();

                while (dr1.Read())
                {
                    textBox17.Text = dr1["istasyon"].ToString();

                }

                con.Close();

                string sqla = "select istasyon_adi from istasyon where istasyon_id ='" + textBox17.Text + "'";
                con.Open();

                MySqlCommand komuta = new MySqlCommand(sqla, con);

                String a = Convert.ToString(komuta.ExecuteScalar());

                con.Close();

                textBox3.Text = a;


                comboBox5.Items.Clear();



                MySqlCommand komut5 = new MySqlCommand();
                komut5.CommandText = "SELECT *FROM gorevli WHERE gorev_yeri = " + textBox17.Text + "";
                komut5.Connection = con;
                komut5.CommandType = CommandType.Text;

                MySqlDataReader dr5;
                con.Open();
                dr5 = komut5.ExecuteReader();

                while (dr5.Read())
                {

                    comboBox5.Items.Add(dr5["gorevli_adi"]);

                }

                con.Close();

            }


          
            
       




            bisikletSecme.Dispose();


        }

        private void button7_Click(object sender, EventArgs e)
        {
            Kiradakiler kiradakiler = new Kiradakiler();
            kiradakiler.ShowDialog();

            if (kiradakiler.bisikletId != 0)
            {

                int x = kiradakiler.bisikletId;
                string teslimsaati = kiradakiler.teslimsaati;
                string teslimtarihi = kiradakiler.teslimtarihi;





                string sorgu = " SELECT kiralanan_bisiklet FROM kiralama1 WHERE kiralama_id=" + x + "";

                con.Open();

                MySqlCommand komutf = new MySqlCommand(sorgu, con);

                String aa = Convert.ToString(komutf.ExecuteScalar());

                textBox5.Text = aa;


                string sqlaa = "select marka from bisiklet where Bisiklet_id=" + textBox5.Text + "";


                MySqlCommand komutaa = new MySqlCommand(sqlaa, con);

                String c = Convert.ToString(komutaa.ExecuteScalar());

                con.Close();

                textBox14.Text = c;

                string sqldd = "select kiralama_id from kiralama1 where kiralanan_bisiklet ='" + textBox5.Text + "'  AND iade_tarihi is NULL ";
                con.Open();

                MySqlCommand komutdd = new MySqlCommand(sqldd, con);

                String bdd = Convert.ToString(komutdd.ExecuteScalar());

                con.Close();

                textBox19.Text = bdd;


                string sorguss = " SELECT saatlik_ucret FROM ucret ORDER BY ucret_baslangic DESC LIMIT 1";

                con.Open();


                MySqlCommand komutfss = new MySqlCommand(sorguss, con);

                String a = Convert.ToString(komutfss.ExecuteScalar());

                con.Close();

                DateTime bTarih = Convert.ToDateTime(dateTimePicker4.Text);
                DateTime kTarih = Convert.ToDateTime(teslimsaati);
                TimeSpan Sonuc = bTarih - kTarih;
                Console.WriteLine("" + Sonuc);

                DateTime xTarih = Convert.ToDateTime(dateTimePicker3.Text);
                DateTime yTarih = Convert.ToDateTime(teslimtarihi);
                TimeSpan SonucGun = xTarih - yTarih;
                Console.WriteLine("" + SonucGun);

                int dakika = int.Parse(Sonuc.Minutes.ToString());
                int gun = int.Parse(SonucGun.TotalHours.ToString());
                int saat = int.Parse(Sonuc.Hours.ToString());
                textBox12.Text = Convert.ToString(dakika);
                int toplam = gun + saat;

                textBox10.Text = Convert.ToString(toplam);

                float ucret;
                if (toplam <= 1)
                {
                    ucret = float.Parse(a);

                }

                else
                {
                    ucret = (toplam * float.Parse(a));
                }

                textBox7.Text = Convert.ToString(ucret);

            }


            
        }

        private void kiralama_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 frm = new Form1();

            frm.Show();
            this.Hide();
        }
      
    }
    
}
