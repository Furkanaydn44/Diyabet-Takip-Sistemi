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
    public partial class Form5 : Form
    {
        private VeriTabaniIslemleri var;
        private List<string> hastalar_tc = new List<string>();
        private string doktor_id;
        private string h_isim;
        private string h_soyisim;
        private string h_epos;
        private string h_cins;
        private DateTime h_dt;
        private string gonderilen_tc;

        public Form5(Kullanici doktor, VeriTabaniIslemleri _var)
        {

            InitializeComponent();

            doktor_id = doktor.TcKimlikNo;
            var = _var;

            hastalar_tc = var.HastalariGetir(doktor_id);

        }

        private void Form5_FormClosed(object sender, FormClosedEventArgs e)
        {
            GC.Collect();
            this.Dispose();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            DataTable table = new DataTable();

            table.Columns.Add("TCno", typeof(string));
            table.Columns.Add("İsim", typeof(string));
            table.Columns.Add("Soyisim", typeof(string));
            table.Columns.Add("E-posta", typeof(string));
            table.Columns.Add("Cinsiyet", typeof(string));
            table.Columns.Add("Dogum Tarihi", typeof(DateTime));
            //belirtiler ve şeker gelcek
            foreach (string s in hastalar_tc)
            {
                var.KullaniciGetir(s, ref h_isim, ref h_soyisim, ref h_epos, ref h_dt, ref h_cins);
                table.Rows.Add(s, h_isim, h_soyisim, h_epos, h_cins, h_dt);
            }

            dataGridView1.DataSource = table;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            try
            {
                var selectedRow = dataGridView1.CurrentRow;
                gonderilen_tc = selectedRow.Cells[0].Value.ToString();
                var rapor = new Form6(var, gonderilen_tc);
                rapor.ShowDialog();
                rapor = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lütfen bütün satırı seçiniz.");
            }        
            
        }
    }
}
