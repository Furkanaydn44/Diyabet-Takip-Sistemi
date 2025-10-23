using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatientDBProject
{
    public partial class Form3 : Form
    {

        private Kullanici girdi_doktor;
        private VeriTabaniIslemleri var;
        private String imageloc = "";
        private byte[] profilepic;
        private bool error = false;
        private string sifre;
        public Form3(Kullanici doktor, VeriTabaniIslemleri _var)
        {
            InitializeComponent();
            var = _var;
            girdi_doktor = doktor;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "jpg files(*.jpg)|*.jpg| PNG files(*.png)|*.png| All Files(*.*)|*.*";

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    imageloc = dialog.FileName;

                    pictureBox2.ImageLocation = imageloc;
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Hatalı Konum veya Dosya.\nBelirtilen yol bulunamadı.", "Hata", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox2.Checked = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox1.Checked = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string trim_isim = textBox1.Text.Trim();
            string trim_soyisim = textBox2.Text.Trim();
            string cinsiyett = "";

            if (error)
            {
                MessageBox.Show("TC Kimlik Numarası veritabanında bulunuyor.\nLütfen farklı 11 haneli bir TC kimlik numarası Girin",
                    "Hatalı Giriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (!checkBox1.Checked && !checkBox2.Checked)
            {
                MessageBox.Show("Lütfen geçerli bir cinsiyet girin.",
                   "Eksik Giriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("İsim/Soyisim boş bırakılamaz.",
                   "Eksik Giriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (textBox3.Text == "")
            {
                MessageBox.Show("TC kimlik numarası boş bırakılamaz.",
                   "Eksik Giriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (textBox4.Text == "")
            {
                MessageBox.Show("E-posta boş bırakılamaz.",
                   "Eksik Giriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (checkBox1.Checked)
                { cinsiyett = "Erkek"; }
                else if (checkBox2.Checked)
                { cinsiyett = "Kadın"; }

                if (imageloc != "")
                {
                    profilepic = var.ResimDonusum(imageloc);
                }
                else
                {
                    profilepic = var.ResimToByteArray(Properties.Resources.Unknown_person);
                }
                sifre = var.SifreUret();
                var girhasta = new Kullanici
                {
                    TcKimlikNo = textBox3.Text,
                    SifreHash = sifre,
                    Ad = textBox1.Text,
                    Soyad = textBox2.Text,
                    Eposta = textBox4.Text,
                    DogumTarihi = dateTimePicker1.Value,
                    Cinsiyet = cinsiyett,
                    KullaniciTipi = "Hasta",
                    ProfilResim = profilepic
                };

                var.HastaKaydet(girhasta, girdi_doktor);
                var.MailGonder(textBox4.Text, textBox1.Text, $"{textBox2.Text}\nSisteme kaydınız başarıyla yapılmıştır. Şifreniz:{sifre}");
                MessageBox.Show("Hasta başarıyla kaydedildi.\nŞifre E-posta hesabına iletilmiştir.",
                   "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Dispose();
            }
        }

        private void textBox4_Validating(object sender, CancelEventArgs e)
        {
            string email = textBox4.Text.Trim();


            string pattern = @"^[a-zA-Z0-9._%+-]+@(gmail\.com|([a-zA-Z0-9.-]+\.)?edu\.tr|hotmail\.com|outlook\.com\.tr)$";

            if (email == "")
            { }
            else if (!Regex.IsMatch(email, pattern))
            {
                e.Cancel = true;
                MessageBox.Show("Lütfen geçerli bir E-posta adresi girin.",
                    "Hatalı Giriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textBox3_Validating(object sender, CancelEventArgs e)
        {
            string giris_tc = textBox3.Text.Trim();

            if (giris_tc == "")
            { }
            else if (giris_tc.Length != 11 || !giris_tc.All(char.IsDigit))
            {
                e.Cancel = true;
                MessageBox.Show("TC Kimlik Numarası hatalı girildi.\nLütfen 11 haneli bir TC kimlik numarası Girin",
                    "Hatalı Giriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void textBox3_Validated(object sender, EventArgs e)
        {
            string giris_tc = textBox3.Text.Trim();
            if (var.TCKontrol(ref giris_tc))
            {
                MessageBox.Show("TC Kimlik Numarası veritabanında bulunuyor.\nLütfen farklı 11 haneli bir TC kimlik numarası Girin",
                    "Hatalı Giriş", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                error = true;
            }
            else
            {
                error = false;
            }
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            GC.Collect();
        }
    }
}
