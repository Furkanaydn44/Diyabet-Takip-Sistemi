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
    public partial class FormH4 : Form
    {
        private VeriTabaniIslemleri repo;
        private Kullanici hasta;
        private string doktortc;
        private StringBuilder str;
        List<KanSekeriOlcumleri> gunlukSekerOlcumler = new List<KanSekeriOlcumleri>();

        public FormH4(Kullanici _hasta, VeriTabaniIslemleri _var)
        {
            InitializeComponent();
            hasta = _hasta;
            repo = _var;
            repo.DoktorTCKontrol(ref doktortc, hasta.TcKimlikNo);

            str = new StringBuilder();
        }

        private void FormH4_FormClosed(object sender, FormClosedEventArgs e)
        {
            GC.Collect();
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            gunlukSekerOlcumler.Clear();
            DateTime Value = dateTimePicker1.Value;
            str.Clear();
            var bugununOlcumleri = repo.GetGunlukOlcumler(hasta.TcKimlikNo, Value);
            if (bugununOlcumleri == null || !bugununOlcumleri.Any())
            {
                repo.KaydetUyari(new UyariKayitlari
                {
                    doktor_id = doktortc,
                    hasta_id = hasta.TcKimlikNo,
                    ilgili_olcum_id = -1,
                    uyari_tipi = "Ölçüm Eksik",
                    mesaj = $"Hasta {hasta.TcKimlikNo} tüm gün boyunca hiç kan şekeri ölçümü yapmadı.",
                    olusturulma_tarihi = DateTime.Now
                });
                str.AppendLine("Uyarı: Tüm gün ölçüm yapılmadı! Doktora bildirildi.");
            }
            else
            {
                str.AppendLine($"Bugün {bugununOlcumleri.Count} ölçüm yapıldı. Tüm gün , eksik ölçüm uyarısı tetiklenmedi.");
            }

            repo.GunlukKanSekeriRaporuGoster(hasta.TcKimlikNo, Value, str,gunlukSekerOlcumler);

            richTextBox1.Text = str.ToString();

            button3.Enabled = true;
            button3.Visible = true;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            DateTime Value = dateTimePicker1.Value.Date;
            str.Clear();
            repo.HastaTarihInsulin(hasta.TcKimlikNo, Value, str);
            richTextBox1.Text = str.ToString();

            button3.Enabled = false;
            button3.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var gui = new FormH5(gunlukSekerOlcumler);
            gui.ShowDialog();
            gui = null;
        }
    }
}
