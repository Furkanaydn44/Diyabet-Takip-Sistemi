
namespace PatientDBProject
{
    public partial class Form1 : Form
    {
        public string tcno;
        public string passw;

        private VeriTabaniIslemleri _var;
        private Oturum _oturum;

        public Form1(VeriTabaniIslemleri var, Oturum oturum)
        {
            InitializeComponent();
            _var = var;
            _oturum = oturum;

        }

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = false;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = true;
            textBox4.Visible = true;
            label2.Enabled = true;
            label3.Enabled = true;
            button1.Enabled = false;
            button2.Enabled = true;
            button1.Visible = false;
            button2.Visible = true;

            label2.Text = "Kullanıcı adı";
            label3.Text = "Şifre";
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = false;
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox1.Visible = true;
            textBox2.Visible = true;
            textBox3.Visible = false;
            textBox4.Visible = false;
            label2.Enabled = true;
            label3.Enabled = true;
            button1.Enabled = true;
            button2.Enabled = false;
            button1.Visible = true;
            button2.Visible = false;

            label2.Text = "Doktor Kullanıcı adı";
            label3.Text = "Şifre";
        }

        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            tcno = textBox1.Text;
            passw = textBox2.Text;
            if (_var.GirisYap(tcno, passw, "Doktor"))
            {
                _oturum.GirisYapanTc = tcno;
                _oturum.KullaniciTipi = "Doktor";
                Form2 form2 = new Form2(_var, _oturum);
                this.Hide();
                form2.ShowDialog();
                this.Hide();
                form2 = null;
                this.Show();
            }
            else
            {
                MessageBox.Show("Hatalı Giriş");
            }

        }

        private void button2_MouseClick(object sender, MouseEventArgs e)
        {

            tcno = textBox3.Text;
            passw = textBox4.Text;

            if (_var.GirisYap(tcno, passw, "Hasta"))
            {
                _oturum.GirisYapanTc = tcno;
                _oturum.KullaniciTipi = "Hasta";
                var formH = new FormH1(_var, _oturum);
                this.Hide();
                formH.ShowDialog();
                formH = null;
                this.Show();
            }
            else
            {
                MessageBox.Show("Hatalı Giriş");
            }
        }      
    }
}
