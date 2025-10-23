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
    public partial class Form7 : Form
    {
        private string doktortc;
        private VeriTabaniIslemleri VIT;
        private List<uyarikayit> kayit = new List<uyarikayit>();
        

        public Form7(Kullanici dr,VeriTabaniIslemleri VIT_)
        {
            InitializeComponent();
            doktortc = dr.TcKimlikNo;
            VIT = VIT_;

            DataTable dt = new DataTable();
            dt.Columns.Add("Uyari Tarihi",typeof(DateTime));
            dt.Columns.Add("Uyari ID", typeof(int));
            dt.Columns.Add("Uyari Tipi", typeof(string));
            dt.Columns.Add("Uyarı Mesajı", typeof(string));
            dt.Columns.Add("Hasta TC Kimlik", typeof(string));

            VIT.GuncelUyarilarGoster(doktortc, kayit);

            foreach (var a in kayit)
            {
                bool mevcutMu = dt.AsEnumerable()
                          .Any(row => row.Field<int?>("Uyari ID") == a.uyari_id);
                if (!mevcutMu)
                {
                    dt.Rows.Add(a.uyari_time, a.uyari_id, a.uyari_tip, a.uyari_mesaj,a.uyari_hasta);
                }
            }
            dataGridView1.DataSource = dt;

        }

        private void Form7_FormClosed(object sender, FormClosedEventArgs e)
        {
            GC.Collect();
            this.Dispose();
        }

       
    }
    public class uyarikayit
    {
        public string uyari_hasta;
        public string uyari_mesaj;
        public string uyari_tip;
        public DateTime uyari_time;
        public int uyari_id;
    }
}
