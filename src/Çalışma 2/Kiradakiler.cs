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
    public partial class Kiradakiler : Form
    {

        MySqlConnection con = new MySqlConnection("Server=localhost; Database=bisiklet_kiralama;Uid=root;Pwd='';");

        public int bisikletId = 0;
        public string teslimtarihi;
        public string teslimsaati;

        public Kiradakiler()
        {
            InitializeComponent();
        }

        private void Kiradakiler_Load(object sender, EventArgs e)
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

                MySqlDataAdapter da = new MySqlDataAdapter("SELECT k.kiralama_id as KiraNo, CONCAT_WS('-', b1.cip_no, b1.marka, b1.model) as KiralananBisiklet, CONCAT_WS('-', ks.ad, ks.soyad) as AdiSoyadi, CONCAT_WS('-', i1.istasyon_kodu, i1.istasyon_adi) as Kiralananİstasyon, k.teslim_tarihi, k.teslim_saati, CONCAT_WS('-', g1.gorevli_tc, g1.gorevi, g1.gorevli_adi, g1.gorevli_soyadi) as TeslimAlinanGorevli  From kiralama1 k left outer join kisi ks on ks.kisi_id = k.kiralayan_kisi left outer join istasyon as i1 on i1.istasyon_id = k.kiralanan_istasyon left outer join gorevli as g1 on g1.gorevli_id = k.teslim_alan left outer join bisiklet as b1 on b1.bisiklet_id = k.kiralanan_bisiklet  WHERE b1.bisiklet_id like '%"+textBox3.Text+"%' AND k.iade_istasyon is NULL ", con);
                DataSet dt = new DataSet();

                if (con.State == ConnectionState.Closed)
                    con.Open();

                da.Fill(dt,"kiralama1");
                dataGridView1.DataSource = dt.Tables["kiralama1"];
                dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                con.Close();
            }

            else if (textBox1.Text == "" && textBox3.Text == "")
            {
                MySqlDataAdapter da = new MySqlDataAdapter("Select k.kiralama_id as KiraNo, CONCAT_WS('-', b1.cip_no, b1.marka, b1.model) as KiralananBisiklet, CONCAT_WS('-', ks.ad, ks.soyad) as AdiSoyadi, CONCAT_WS('-', i1.istasyon_kodu, i1.istasyon_adi) as Kiralananİstasyon, k.teslim_tarihi, k.teslim_saati, CONCAT_WS('-', g1.gorevli_tc, g1.gorevi, g1.gorevli_adi, g1.gorevli_soyadi) as TeslimAlinanGorevli  From kiralama1 k left outer join kisi ks on ks.kisi_id = k.kiralayan_kisi left outer join istasyon as i1 on i1.istasyon_id = k.kiralanan_istasyon left outer join gorevli as g1 on g1.gorevli_id = k.teslim_alan left outer join bisiklet as b1 on b1.bisiklet_id = k.kiralanan_bisiklet  WHERE kiralama1 like k.teslim_alan '%" + textBox2.Text + "%' AND k.iade_istasyon is NULL ", con);
                DataSet dt = new DataSet();
                if (con.State == ConnectionState.Closed)
                    con.Open();

                da.Fill(dt, "kiralama1");
                dataGridView1.DataSource = dt.Tables["kiralama1"];
                dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            else if (textBox3.Text == "" && textBox2.Text == "")
            {
                MySqlDataAdapter da = new MySqlDataAdapter("Select k.kiralama_id as KiraNo, CONCAT_WS('-', b1.cip_no, b1.marka, b1.model) as KiralananBisiklet, CONCAT_WS('-', ks.ad, ks.soyad) as AdiSoyadi, CONCAT_WS('-', i1.istasyon_kodu, i1.istasyon_adi) as Kiralananİstasyon, k.teslim_tarihi, k.teslim_saati, CONCAT_WS('-', g1.gorevli_tc, g1.gorevi, g1.gorevli_adi, g1.gorevli_soyadi) as TeslimAlinanGorevli  From kiralama1 k left outer join kisi ks on ks.kisi_id = k.kiralayan_kisi left outer join istasyon as i1 on i1.istasyon_id = k.kiralanan_istasyon left outer join gorevli as g1 on g1.gorevli_id = k.teslim_alan left outer join bisiklet as b1 on b1.bisiklet_id = k.kiralanan_bisiklet  WHERE k.kiralanan_istasyon '%" + textBox1.Text + "%' AND k.iade_istasyon is NULL ", con);
                DataSet dt = new DataSet();
                if (con.State == ConnectionState.Closed)
                    con.Open();

                da.Fill(dt, "kiralama1");
                dataGridView1.DataSource = dt.Tables["kiralama1"];
                dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            else if (textBox3.Text == "")
            {
                MySqlDataAdapter da = new MySqlDataAdapter("Select k.kiralama_id as KiraNo, CONCAT_WS('-', b1.cip_no, b1.marka, b1.model) as KiralananBisiklet, CONCAT_WS('-', ks.ad, ks.soyad) as AdiSoyadi, CONCAT_WS('-', i1.istasyon_kodu, i1.istasyon_adi) as Kiralananİstasyon, k.teslim_tarihi, k.teslim_saati, CONCAT_WS('-', g1.gorevli_tc, g1.gorevi, g1.gorevli_adi, g1.gorevli_soyadi) as TeslimAlinanGorevli  From kiralama1 k left outer join kisi ks on ks.kisi_id = k.kiralayan_kisi left outer join istasyon as i1 on i1.istasyon_id = k.kiralanan_istasyon left outer join gorevli as g1 on g1.gorevli_id = k.teslim_alan left outer join bisiklet as b1 on b1.bisiklet_id = k.kiralanan_bisiklet  WHERE k.kiralanan_istasyon like '%" + textBox1.Text + "%'   OR  k.teslim_alan like '%" + textBox2.Text + "%' AND k.iade_istasyon is NULL ", con);
                DataSet dt = new DataSet();
                if (con.State == ConnectionState.Closed)
                    con.Open();

                da.Fill(dt, "kiralama1");
                dataGridView1.DataSource = dt.Tables["kiralama1"];
                dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            else if (textBox2.Text == "")
            {
                MySqlDataAdapter da = new MySqlDataAdapter("Select k.kiralama_id as KiraNo, CONCAT_WS('-', b1.cip_no, b1.marka, b1.model) as KiralananBisiklet, CONCAT_WS('-', ks.ad, ks.soyad) as AdiSoyadi, CONCAT_WS('-', i1.istasyon_kodu, i1.istasyon_adi) as Kiralananİstasyon, k.teslim_tarihi, k.teslim_saati, CONCAT_WS('-', g1.gorevli_tc, g1.gorevi, g1.gorevli_adi, g1.gorevli_soyadi) as TeslimAlinanGorevli  From kiralama1 k left outer join kisi ks on ks.kisi_id = k.kiralayan_kisi left outer join istasyon as i1 on i1.istasyon_id = k.kiralanan_istasyon left outer join gorevli as g1 on g1.gorevli_id = k.teslim_alan left outer join bisiklet as b1 on b1.bisiklet_id = k.kiralanan_bisiklet  WHERE b1.cip_no like '%" + textBox3.Text + "%'  OR  k.kiralanan_istasyon like '%" + textBox1.Text + "%' AND k.iade_istasyon is NULL ", con);
                DataSet dt = new DataSet();
                if (con.State == ConnectionState.Closed)
                    con.Open();

                da.Fill(dt, "kiralama1");
                dataGridView1.DataSource = dt.Tables["kiralama1"];
                dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }


            else if (textBox1.Text == "")
            {
                MySqlDataAdapter da = new MySqlDataAdapter("SELECT k.kiralama_id as KiraNo, CONCAT_WS('-', b1.cip_no, b1.marka, b1.model) as KiralananBisiklet, CONCAT_WS('-', ks.ad, ks.soyad) as AdiSoyadi, CONCAT_WS('-', i1.istasyon_kodu, i1.istasyon_adi) as Kiralananİstasyon, k.teslim_tarihi, k.teslim_saati, CONCAT_WS('-', g1.gorevli_tc, g1.gorevi, g1.gorevli_adi, g1.gorevli_soyadi) as TeslimAlinanGorevli  From kiralama1 k left outer join kisi ks on ks.kisi_id = k.kiralayan_kisi left outer join istasyon as i1 on i1.istasyon_id = k.kiralanan_istasyon left outer join gorevli as g1 on g1.gorevli_id = k.teslim_alan left outer join bisiklet as b1 on b1.bisiklet_id = k.kiralanan_bisiklet  WHERE b1.cip_no like '%" + textBox3.Text + "%'  OR  k.teslim_alan like '%" + textBox2.Text + "%' AND k.iade_istasyon is NULL ", con);
                DataSet dt = new DataSet();
                if (con.State == ConnectionState.Closed)
                    con.Open();

                da.Fill(dt, "kiralama1");
                dataGridView1.DataSource = dt.Tables["kiralama1"];
                dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            else

            {

                MySqlDataAdapter da = new MySqlDataAdapter("SELECT k.kiralama_id as KiraNo, CONCAT_WS('-', b1.cip_no, b1.marka, b1.model) as KiralananBisiklet, CONCAT_WS('-', ks.ad, ks.soyad) as AdiSoyadi, CONCAT_WS('-', i1.istasyon_kodu, i1.istasyon_adi) as Kiralananİstasyon, k.teslim_tarihi, k.teslim_saati, CONCAT_WS('-', g1.gorevli_tc, g1.gorevi, g1.gorevli_adi, g1.gorevli_soyadi) as TeslimAlinanGorevli  From kiralama1 k left outer join kisi ks on ks.kisi_id = k.kiralayan_kisi left outer join istasyon as i1 on i1.istasyon_id = k.kiralanan_istasyon left outer join gorevli as g1 on g1.gorevli_id = k.teslim_alan left outer join bisiklet as b1 on b1.bisiklet_id = k.kiralanan_bisiklet WHERE b1.bisiklet.cip_no like '%" + textBox3.Text + "%'  OR  k.istasyon.kiralanan_istasyon like '%" + textBox1.Text + "%'   OR  k.teslim_alan like '%" + textBox2.Text + "%' AND iade_istasyon is NULL ", con);
                DataSet dt = new DataSet();
                if (con.State == ConnectionState.Closed)
                    con.Open();

                da.Fill(dt, "kiralama1");
                dataGridView1.DataSource = dt.Tables["kiralama1"];
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
            teslimtarihi = Convert.ToString(dataGridView1.CurrentRow.Cells[4].Value);
            teslimsaati = Convert.ToString(dataGridView1.CurrentRow.Cells[5].Value);
        }
    }
}
