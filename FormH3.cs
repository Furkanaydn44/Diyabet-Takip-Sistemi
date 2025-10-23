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
    public partial class FormH3 : Form
    {
        private VeriTabaniIslemleri var;
        private Kullanici hasta;
        private string doktortc;
        private int olcumseker;
        private string olcum_tip;
        private DateTime saat_dk_sn;
        private DateTime yil_ay_gun;
        private int yil;
        private int gun;
        private int ay;
        private int sn;
        private int dk;
        private int saat;




        public FormH3(Kullanici _hasta, VeriTabaniIslemleri _var)
        {
            InitializeComponent();

            hasta = _hasta;
            var = _var;

            dateTimePicker2.Format = DateTimePickerFormat.Time;
            dateTimePicker2.ShowUpDown = true;
            Controls.Add(dateTimePicker2);

            var.DoktorTCKontrol(ref doktortc, hasta.TcKimlikNo);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string giris_seker = textBox1.Text.Trim();

            if (giris_seker == "" || !giris_seker.All(char.IsDigit))
            {
                MessageBox.Show("Hatalı kan şekeri girdisi !");
            }
            else if (comboBox1.Text == "" || comboBox1.Text == null)
            {
                MessageBox.Show("Girdi tipi boş bırakılamaz !");
            }
            else
            {
                olcumseker = Convert.ToInt32(giris_seker);

                DateTime gunolcum = dateTimePicker1.Value;
                yil = gunolcum.Year;
                ay = gunolcum.Month;
                gun = gunolcum.Day;

                DateTime secilenZaman = dateTimePicker2.Value;
                saat = secilenZaman.Hour;
                sn = secilenZaman.Second;
                dk = secilenZaman.Minute;

                olcum_tip = comboBox1.Text;

                var.HesaplaVeOneriYap(hasta.TcKimlikNo,doktortc,olcumseker,olcum_tip,
                new DateTime(yil,ay,gun,saat,dk,sn));
         
            }


        }

        private void FormH3_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            GC.Collect();
        }
    }
}
