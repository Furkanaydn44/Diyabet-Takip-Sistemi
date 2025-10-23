using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatientDBProject
{
    public partial class Form2 : Form
    {
        private string tcno;
        private string meslek;
        public string isim;
        private string soyisim;
        private string eposta;
        private DateTime dogumt;
        private string cinsiyet;
        private string profil_resim;
        private VeriTabaniIslemleri var;
        private Oturum oturum;
        private byte[] pp;
        private Kullanici giris_doktor;

        public Form2(VeriTabaniIslemleri _var, Oturum _oturum)
        {
            InitializeComponent();
            var = _var;
            oturum = _oturum;
            tcno = _oturum.GirisYapanTc;
            meslek = _oturum.KullaniciTipi;

            _var.KullaniciGetir(tcno, ref isim, ref soyisim, ref eposta, ref dogumt, ref cinsiyet);
            pp = _var.Resimgetir(ref tcno);

            if (pp != null)
            {
                pictureBox1.Image = _var.ByteArrayToBitmap(pp);
            }

            giris_doktor = new Kullanici
            {
                TcKimlikNo = tcno,
                Ad = isim,
                Soyad = soyisim,
                Eposta = eposta,
                DogumTarihi = dogumt,
                Cinsiyet = cinsiyet,
                KullaniciTipi = meslek,
                ProfilResim = null
            };

            label1.Text = $"Dr.{isim}";
            label2.Text = soyisim;
            label3.Text = eposta;
            label4.Text = cinsiyet;

        }

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            var text = "Çıkış yapmak üzeresiniz.";
            var title = "Çıkış";
            var msgbox = MessageBox.Show(text, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            switch (msgbox)
            {
                case DialogResult.Yes:
                    this.Dispose();
                    break;
                case DialogResult.No:
                    break;
                default:
                    break;
            }
        }

        private void pictureBox3_MouseClick(object sender, MouseEventArgs e)
        {
            var ui_hasta = new Form3(giris_doktor, var);
            ui_hasta.ShowDialog();
            ui_hasta = null;
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            GC.Collect();
        }

        private void pictureBox4_MouseClick(object sender, MouseEventArgs e)
        {
            var symptom = new Form4(giris_doktor, var);
            symptom.ShowDialog();
            symptom = null;
        }

        private void pictureBox6_MouseClick(object sender, MouseEventArgs e)
        {
            var butunhastalar = new Form5(giris_doktor, var);
            butunhastalar.ShowDialog();
            butunhastalar = null;
        }

        private void pictureBox5_MouseClick(object sender, MouseEventArgs e)
        {
            var uyar = new Form7(giris_doktor, var);
            uyar.ShowDialog();
            uyar = null;
        }
    }
}
