using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatientDBProject
{
    public partial class Form6 : Form
    {
        private VeriTabaniIslemleri var;
        private string tc;
        private string h_isim;
        private string h_soyisim;
        private string h_epos;
        private string h_cins;
        private DateTime h_dt;
        private byte[] photo;
        private DataTable table = new DataTable();
        private bool diyet_exist = false;
        private DateTime diyet_tarih;
        private string diyet_ad;
        private string diyet_eknot;
        private int d_id;
        private DateTime value;
        private List<diyet_dat> hasta_diyet = new List<diyet_dat>();
        private List<egzersiz_dat> hasta_egzersiz_ = new List<egzersiz_dat>();
        private List<kan_hepsi_Dat> kan_hepsi = new List<kan_hepsi_Dat>();
        private List<kan_hepsi_Dat> kan_tarih = new List<kan_hepsi_Dat>();
        private List<belirti_dat> belirti = new List<belirti_dat>();
        private List<insulin_dat> insu = new List<insulin_dat>();


        //Pie Chart
        private bool diyetbool;
        private bool egzbool;
        private int yuzde_diyet;
        private int yuzde_egzersiz;

        public Form6(VeriTabaniIslemleri _var, string gelenTC)
        {
            InitializeComponent();
            var = _var;
            tc = gelenTC;

            chart1.Titles.Add("Diyet Grafiği");
            chart2.Titles.Add("Egzersiz Grafiği");



        }

        private void Form6_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            GC.Collect();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            var.KullaniciGetir(tc, ref h_isim, ref h_soyisim, ref h_epos, ref h_dt, ref h_cins);
            photo = var.Resimgetir(ref tc);
            pictureBox1.Image = var.ByteArrayToBitmap(photo);
            label4.Text = tc;
            label5.Text = h_isim;
            label6.Text = h_soyisim;
            label7.Text = h_cins;

            chart_guncelle();

        }
        private void DiyetTablosu(VeriTabaniIslemleri varr, string tc)
        {
            table.Clear();
            table.Columns.Clear();
            table.Rows.Clear();
            table.Columns.Add("Diyet ID", typeof(int));
            table.Columns.Add("Diyet Adı", typeof(string));
            table.Columns.Add("Diyet Verilme Tarihi", typeof(DateTime));
            table.Columns.Add("Diyet Notu", typeof(string));
            hasta_diyet.Clear();
            varr.GuncelDiyetOnerisiGoster(tc, hasta_diyet);

            foreach (var a in hasta_diyet)
            {
                bool mevcutMu = table.AsEnumerable()
                          .Any(row => row.Field<int?>("Diyet ID") == a.diyet_id);

                if (!mevcutMu)
                {
                    table.Rows.Add(a.diyet_id, a.diyet_isim_, a.diyet_tarih_, a.diyet_eknot_);
                }
            }

            dataGridView1.DataSource = table;

        }

        private void EgzersizTablosu(VeriTabaniIslemleri varr, string tc)
        {
            table.Clear();
            table.Columns.Clear();
            table.Rows.Clear();
            table.Columns.Add("Ezgersiz ID", typeof(int));
            table.Columns.Add("Ezgersiz Adı", typeof(string));
            table.Columns.Add("Ezgersiz Verilme Tarihi", typeof(DateTime));
            table.Columns.Add("Ezgersiz Notu", typeof(string));
            hasta_egzersiz_.Clear();
            varr.GuncelEgzersizOnerisiGoster(tc, hasta_egzersiz_);

            foreach (var a in hasta_egzersiz_)
            {
                bool mevcutMu = table.AsEnumerable()
                          .Any(row => row.Field<int?>("Ezgersiz ID") == a.egzersiz_id);

                if (!mevcutMu)
                {
                    table.Rows.Add(a.egzersiz_id, a.egzersiz_ad_, a.oneri_tarih_, a.oneri_eknot_);
                }
            }

            dataGridView1.DataSource = table;

        }

        private void KanHepsiTablosu(VeriTabaniIslemleri varr, string tc)
        {
            table.Clear();
            table.Columns.Clear();
            table.Rows.Clear();
            table.Columns.Add("Girdi ID", typeof(int));
            table.Columns.Add("Kan Şeker Değeri (mg)");
            table.Columns.Add("Girdi Tipi", typeof(string));
            table.Columns.Add("Girdi Tarihi", typeof(DateTime));
            table.Columns.Add("Girdi Notu", typeof(string));

            varr.GuncelKanSekeriOlcumleriGoster(tc, kan_hepsi);

            foreach (var a in kan_hepsi)
            {
                bool mevcutMu = table.AsEnumerable()
                          .Any(row => row.Field<int?>("Girdi ID") == a.kh_id);
                if (!mevcutMu)
                {
                    table.Rows.Add(a.kh_id, a.kh_olcum, a.kh_tip, a.kh_tarih, a.kh_not);
                }
            }
            dataGridView1.DataSource = table;
        }

        private void KanTarihTablosu(VeriTabaniIslemleri varr, string tc, DateTime var)
        {
            table.Clear();
            table.Columns.Clear();
            table.Rows.Clear();
            table.Columns.Add("Girdi_ ID", typeof(int));
            table.Columns.Add("Kan Şeker Değeri (mg)");
            table.Columns.Add("Girdi Tipi", typeof(string));
            table.Columns.Add("Girdi Tarihi", typeof(DateTime));
            table.Columns.Add("Girdi Notu", typeof(string));

            kan_tarih.Clear();

            varr.TarihKanSekeriOlcumleriGoster(tc, kan_tarih, var);

            foreach (var a in kan_tarih)
            {
                bool mevcutMu = table.AsEnumerable()
                          .Any(row => row.Field<int?>("Girdi_ ID") == a.kh_id);
                if (!mevcutMu)
                {
                    table.Rows.Add(a.kh_id, a.kh_olcum, a.kh_tip, a.kh_tarih, a.kh_not);
                }
            }
            dataGridView1.DataSource = table;
        }

        private void BelirtiTablosu(VeriTabaniIslemleri varr, string tc)
        {
            table.Clear();
            table.Columns.Clear();
            table.Rows.Clear();
            table.Columns.Add("Belirti Numaraması", typeof(int));
            table.Columns.Add("Belirti Adı", typeof(string));
            table.Columns.Add("Girdi Tarihi", typeof(DateTime));

            varr.GuncelBelirtilerGoster(tc, belirti);
            varr.GecmisBelirtilerGoster(tc, belirti);
            foreach (var a in belirti)
            {
                bool mevcutMu = table.AsEnumerable()
                          .Any(row => row.Field<int?>("Belirti Numaraması") == a.belirti_id);
                if (!mevcutMu)
                {
                    table.Rows.Add(a.belirti_id, a.belirti_isim, a.belirti_kayit);
                }
            }
            dataGridView1.DataSource = table;
        }

        private void InsulinTablosu(VeriTabaniIslemleri varr, string tc)
        {
            table.Clear();
            table.Columns.Clear();
            table.Rows.Clear();
            table.Columns.Add("Giriş ID", typeof(int));
            table.Columns.Add("İnsülin Miktari (mg)", typeof(double));
            table.Columns.Add("İnsülin Verilme Tarihi", typeof(DateTime));
            table.Columns.Add("İnsülin Notu", typeof(string));
            table.Columns.Add("İnsülin Referansı", typeof(string));

            varr.GuncelInsulinOnerisiGoster(tc, insu);

            foreach (var a in insu)
            {
                bool mevcutMu = table.AsEnumerable()
                          .Any(row => row.Field<int?>("Giriş ID") == a.insulin_id);

                if (!mevcutMu)
                {
                    table.Rows.Add(a.insulin_id, a.insulin_doz, a.insulin_time, a.insulin_not, a.insulin_ref);
                }
            }

            dataGridView1.DataSource = table;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Visible = false;
            dataGridView1.DataSource = null;
            DiyetTablosu(var, tc);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Visible = false;
            dataGridView1.DataSource = null;
            EgzersizTablosu(var, tc);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Visible = false;
            dataGridView1.DataSource = null;
            KanHepsiTablosu(var, tc);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            richTextBox1.Visible = false;
            dataGridView1.DataSource = null;
            value = dateTimePicker1.Value.Date;
            KanTarihTablosu(var, tc, value);
        }

        private void chart_guncelle()
        {
            var.HastaDiyetUygulamaYuzdesi(tc, ref yuzde_diyet);
            chart1.Series["Series1"].Points.Clear();
            chart1.Series["Series1"].Points.AddXY($"Uygulandı {yuzde_diyet}", yuzde_diyet);
            chart1.Series["Series1"].Points.AddXY($"Uygulanmadı {100 - yuzde_diyet}", 100 - yuzde_diyet);
            chart1.Series["Series1"]["PieLabelStyle"] = "Disabled";
            var.HastaEgzersizUygulamaYuzdesi(tc, ref yuzde_egzersiz);
            chart2.Series["Series1"].Points.Clear();
            chart2.Series["Series1"].Points.AddXY($"Uygulandı {yuzde_egzersiz}", yuzde_egzersiz);
            chart2.Series["Series1"].Points.AddXY($"Uygulanmadı {100 - yuzde_egzersiz}", 100 - yuzde_egzersiz);
            chart2.Series["Series1"]["PieLabelStyle"] = "Disabled";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            richTextBox1.Visible = false;
            dataGridView1.DataSource = null;
            BelirtiTablosu(var, tc);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            richTextBox1.Visible = false;
            dataGridView1.DataSource = null;
            InsulinTablosu(var, tc);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            StringBuilder str = new StringBuilder();
            var.UygulamaDurumlariniGoster(tc, str);
            richTextBox1.Visible = true;
            richTextBox1.Text = str.ToString();
        }
    }

    public class diyet_dat
    {
        public string diyet_isim_;
        public DateTime diyet_tarih_;
        public string diyet_eknot_;
        public int diyet_id;
        public string diyet_durum_;
    }
    public class egzersiz_dat
    {
        public DateTime oneri_tarih_;
        public string oneri_eknot_;
        public string egzersiz_ad_;
        public int egzersiz_id;
        public string egzersiz_durum_;
    }
    public class kan_hepsi_Dat
    {
        public int kh_id;
        public int kh_olcum;
        public string kh_not;
        public string kh_tip;
        public DateTime kh_tarih;
    }
    public class belirti_dat
    {
        public int belirti_id;
        public string belirti_isim;
        public DateTime belirti_kayit;
    }
    public class insulin_dat
    {
        public int insulin_id;
        public string insulin_ref;
        public string insulin_not;
        public DateTime insulin_time;
        public double insulin_doz;
    }

}
