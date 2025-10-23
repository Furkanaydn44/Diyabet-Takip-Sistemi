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
    public partial class FormH5 : Form
    {
        private string zaman;
        private string olcum;

        public FormH5(List<KanSekeriOlcumleri> gunluk)
        {
            InitializeComponent();

            chart1.Series.Clear();
            chart1.ChartAreas.Clear();

            chart1.ChartAreas.Add("Alan1");
            var seri = new System.Windows.Forms.DataVisualization.Charting.Series("Kan Şekeri");
            seri.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            seri.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;

            foreach (var a in gunluk)
            {

                seri.Points.AddXY(a.olcum_tarihi, a.olcum_degeri_mg_dl);
            }

            chart1.Series.Add(seri);

            chart1.ChartAreas[0].AxisX.Title = "Ölçüm Saati";
            chart1.ChartAreas[0].AxisY.Title = "Kan Şekeri (mg/dL)";

            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "HH:mm";

        }

        private void FormH5_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            GC.Collect();
        }
    }
}
