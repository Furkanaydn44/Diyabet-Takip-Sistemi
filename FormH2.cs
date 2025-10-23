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
    public partial class FormH2 : Form
    {
        private Kullanici hasta;
        private Kullanici doktor;
        private VeriTabaniIslemleri var;
        private string doktortc;
        private bool diyetbool;
        private bool egzbool;
        private int yuzde_diyet;
        private int yuzde_egzersiz;


        public FormH2(Kullanici _hasta, VeriTabaniIslemleri _var)
        {
            InitializeComponent();
            var = _var;
            hasta = _hasta;



            chart1.Titles.Add("Diyet Grafiği");
            chart2.Titles.Add("Egzersiz Grafiği");

            chart_guncelle();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((!checkBox1.Checked && !checkBox2.Checked) || (!checkBox3.Checked && !checkBox4.Checked))
            {
                MessageBox.Show("Diyet/Egzersiz Girişi Yapılmadı.");
            }

            if (checkBox1.Checked)
            {
                diyetbool = true;
            }
            else if (checkBox2.Checked)
            {
                diyetbool = false;
            }
            else if (checkBox3.Checked)
            {
                egzbool = true;
            }
            else if (checkBox4.Checked)
            {
                egzbool = false;
            }


            var.Uygulandimi(hasta.TcKimlikNo, diyetbool, egzbool);
            this.Dispose();

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

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                checkBox4.Checked = false;
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                checkBox3.Checked = false;
            }
        }

        private void chart_guncelle()
        {
            var.HastaDiyetUygulamaYuzdesi(hasta.TcKimlikNo, ref yuzde_diyet);
            chart1.Series["s1"].Points.Clear();
            chart1.Series["s1"].Points.AddXY("Uygulandı", yuzde_diyet);
            chart1.Series["s1"].Points.AddXY("Uygulanmadı", 100 - yuzde_diyet);
            var.HastaEgzersizUygulamaYuzdesi(hasta.TcKimlikNo, ref yuzde_egzersiz);
            chart2.Series["s2"].Points.Clear();
            chart2.Series["s2"].Points.AddXY("Uygulandı", yuzde_egzersiz);
            chart2.Series["s2"].Points.AddXY("Uygulanmadı", 100 - yuzde_egzersiz);
        }

        private void FormH2_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            GC.Collect();
        }
    }
}
