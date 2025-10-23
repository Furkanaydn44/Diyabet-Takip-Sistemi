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
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String imageloc = "";

            try 
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "jpg files(*.jpg)|*.jpg| PNG files|*.png| All Files(*.*)|*.*|";

                if(dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    imageloc = dialog.FileName;

                    pictureBox2.ImageLocation = imageloc;
                }
            }
            catch(Exception) 
            {

                MessageBox.Show("Hatalı Konum veya Dosya. \n Belirtilen yol bulunamadı.","Hata",MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

        }
    }
}
