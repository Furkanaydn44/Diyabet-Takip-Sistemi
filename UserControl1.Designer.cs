namespace PatientDBProject
{
    partial class UserControl1
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            label1 = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            pictureBox1 = new PictureBox();
            textBox3 = new TextBox();
            textBox4 = new TextBox();
            checkBox1 = new CheckBox();
            checkBox2 = new CheckBox();
            dateTimePicker1 = new DateTimePicker();
            pictureBox2 = new PictureBox();
            button1 = new Button();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.HotTrack;
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(804, 99);
            panel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(130, 42);
            label1.Name = "label1";
            label1.Size = new Size(171, 26);
            label1.TabIndex = 0;
            label1.Text = "HASTA KAYIT";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(38, 165);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(189, 23);
            textBox1.TabIndex = 1;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(273, 165);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(196, 23);
            textBox2.TabIndex = 2;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.hasta;
            pictureBox1.Location = new Point(14, 19);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(87, 77);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(38, 245);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(224, 23);
            textBox3.TabIndex = 3;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(38, 333);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(224, 23);
            textBox4.TabIndex = 4;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(513, 169);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(54, 19);
            checkBox1.TabIndex = 5;
            checkBox1.Text = "Erkek";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(608, 169);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(56, 19);
            checkBox2.TabIndex = 6;
            checkBox2.Text = "Kadın";
            checkBox2.UseVisualStyleBackColor = true;
            checkBox2.CheckedChanged += checkBox2_CheckedChanged;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(38, 411);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(224, 23);
            dateTimePicker1.TabIndex = 7;
            // 
            // pictureBox2
            // 
            pictureBox2.Location = new Point(467, 205);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(197, 229);
            pictureBox2.TabIndex = 8;
            pictureBox2.TabStop = false;
            // 
            // button1
            // 
            button1.Location = new Point(386, 411);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 9;
            button1.Text = "Yükle";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(38, 147);
            label2.Name = "label2";
            label2.Size = new Size(29, 15);
            label2.TabIndex = 10;
            label2.Text = "İsim";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(273, 147);
            label3.Name = "label3";
            label3.Size = new Size(48, 15);
            label3.TabIndex = 11;
            label3.Text = "Soyisim";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(38, 227);
            label4.Name = "label4";
            label4.Size = new Size(111, 15);
            label4.TabIndex = 12;
            label4.Text = "TC Kimlik Numarası";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(38, 315);
            label5.Name = "label5";
            label5.Size = new Size(47, 15);
            label5.TabIndex = 13;
            label5.Text = "E-Posta";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(38, 393);
            label6.Name = "label6";
            label6.Size = new Size(79, 15);
            label6.TabIndex = 14;
            label6.Text = "Doğum Tarihi";
            // 
            // UserControl1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(button1);
            Controls.Add(pictureBox2);
            Controls.Add(dateTimePicker1);
            Controls.Add(checkBox2);
            Controls.Add(checkBox1);
            Controls.Add(textBox4);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(panel1);
            Name = "UserControl1";
            Size = new Size(800, 500);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private PictureBox pictureBox1;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox textBox4;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private DateTimePicker dateTimePicker1;
        private PictureBox pictureBox2;
        private Button button1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
    }
}
