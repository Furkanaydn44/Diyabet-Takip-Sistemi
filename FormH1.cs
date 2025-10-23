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
    public partial class FormH1 : Form
    {
        private Oturum oturum;
        private VeriTabaniIslemleri var;
        private byte[] pp;
        private string tcno;
        private string meslek;
        public string isim;
        private string soyisim;
        private string eposta;
        private DateTime dogumt;
        private string cinsiyet;
        private string profil_resim;
        private Kullanici giris_hasta;

        public FormH1(VeriTabaniIslemleri _var, Oturum _oturum)
        {
            InitializeComponent();
            oturum = _oturum;
            var = _var;
            tcno = _oturum.GirisYapanTc;
            meslek = _oturum.KullaniciTipi;
            _var.KullaniciGetir(tcno, ref isim, ref soyisim, ref eposta, ref dogumt, ref cinsiyet);
            pp = _var.Resimgetir(ref tcno);

            if (pp != null)
            {
                pictureBox1.Image = _var.ByteArrayToBitmap(pp);
            }

            giris_hasta = new Kullanici
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
            label1.Text = isim;
            label2.Text = soyisim;
            label6.Text = eposta;
            label4.Text = cinsiyet;


        }

        private void FormH1_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            GC.Collect();
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
            var ui_hasta = new FormH2(giris_hasta, var);
            ui_hasta.ShowDialog();
            ui_hasta = null;
        }

        private void pictureBox4_MouseClick(object sender, MouseEventArgs e)
        {
            var ui_hasta = new FormH3(giris_hasta, var);
            ui_hasta.ShowDialog();
            ui_hasta = null;
        }

        private void pictureBox5_MouseClick(object sender, MouseEventArgs e)
        {
            var ui_hasta = new FormH4(giris_hasta, var);
            ui_hasta.ShowDialog();
            ui_hasta = null;
        }
    }
}
