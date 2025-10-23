using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PatientDBProject
{
    public partial class Form4 : Form
    {
        private Kullanici doktor;
        private string hasta_tc;
        private VeriTabaniIslemleri var;
        private List<BelirtiTurleri> belirti;
        private List<DiyetTurleri> diyet;
        private List<EgzersizTurleri> egzersiz;
        private bool err = true;
        private string h_isim;
        private string h_soyisim;
        private string h_epos;
        private string h_cins;
        private DateTime h_dt;

        public Form4(Kullanici _doktor, VeriTabaniIslemleri _var)
        {
            InitializeComponent();
            doktor = _doktor;
            var = _var;
            diyet = new List<DiyetTurleri>();
            belirti = new List<BelirtiTurleri>();
            egzersiz = new List<EgzersizTurleri>();



            var.VerileriYukle(diyet, egzersiz, belirti);

        }

        private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
            GC.Collect();
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string giris_tc = textBox1.Text.Trim();

            if (giris_tc == "")
            {
                ListBoxSifirla();
            }
            else if (giris_tc.Length != 11 || !giris_tc.All(char.IsDigit))
            {
                MessageBox.Show("Geçersiz TC Kimlik numarası",
                    "Eksik Giriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ListBoxSifirla();
            }

            else if (var.HastaTCKontrol(doktor.TcKimlikNo, giris_tc))
            {
                foreach (var belir in belirti)
                {
                    if (!checkedListBox1.Items.Contains(belir.belirti_adi))
                    {
                        checkedListBox1.Items.Add(belir.belirti_adi);
                    }
                }
            }
            else
            {
                MessageBox.Show("TC Kimlik numarası veritabanında bulunamadı",
               "Hatalı Giriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ListBoxSifirla();
            }


            if (checkedListBox1.Items.Count > 0 && var.HastaTCKontrol(doktor.TcKimlikNo, giris_tc))
            {
                hasta_tc = giris_tc.ToString();

                byte[] a = var.Resimgetir(ref hasta_tc);
                pictureBox2.Image = var.ByteArrayToBitmap(a);

                var.KullaniciGetir(giris_tc, ref h_isim, ref h_soyisim, ref h_epos, ref h_dt, ref h_cins);

                label4.Text = h_isim;
                label5.Text = h_soyisim;
                label6.Text = h_epos;
                label7.Text = h_cins;

                button2.Enabled = true;
                button2.Visible = true;
                checkedListBox1.Enabled = true;
                checkedListBox1.Visible = true;
                textBox2.Visible = true;
                textBox2.Enabled = true;
                label2.Enabled = true;
                label2.Visible = true;
                label3.Visible = true;
                label3.Enabled = true;
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                label7.Visible = true;
                panel3.Visible = true;
                panel4.Visible = true;
                panel5.Visible = true;
                panel6.Visible = true;
                pictureBox2.Visible = true;

            }
        }

        public void ListBoxSifirla()
        {
            button2.Enabled = false;
            button2.Visible = false;
            checkedListBox1.Enabled = false;
            checkedListBox1.Visible = false;
            textBox2.Visible = false;
            textBox2.Enabled = false;
            label2.Enabled = false;
            label2.Visible = false;
            label3.Visible = false;
            label3.Enabled = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            pictureBox2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            panel6.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            List<string> secilenler = new List<string>();
            double kan_seker;

            foreach (var item in checkedListBox1.CheckedItems)
            {
                if (!secilenler.Contains(item.ToString()))
                {
                    secilenler.Add(item.ToString());
                }
            }


            if (textBox2.Text == "" || !textBox2.Text.All(char.IsDigit))
            {
                MessageBox.Show("Ortalama Kan şekeri seviyesi eksik/hatalı girildi!", "Eksik Girdi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (secilenler.Count == 0)
            {
                MessageBox.Show("En az bir belirti girilmek zorunda !", "Eksik Seçim", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                kan_seker = double.Parse(textBox2.Text);
                var.OneriYap(doktor.TcKimlikNo, hasta_tc, kan_seker, secilenler, diyet, egzersiz, ref err);

                if (!err)
                {
                    var.KayitYap(hasta_tc, kan_seker, secilenler);
                    MessageBox.Show("Hastanın diyet/öneri kaydı yapıldı.", "Kaydedildi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Dispose();
                }

            }

            /*else if (var.HastaDiyetKontrol(giris_tc))
            {
                MessageBox.Show("Hasta TC Kimlik numarası veritabanında kayıtlı",
              "Hatalı Giriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ListBoxSifirla();
            }*/
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
