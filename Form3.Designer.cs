namespace PatientDBProject
{
    partial class Form3
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            label1 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            button1 = new Button();
            dateTimePicker1 = new DateTimePicker();
            textBox4 = new TextBox();
            textBox3 = new TextBox();
            textBox2 = new TextBox();
            textBox1 = new TextBox();
            pictureBox2 = new PictureBox();
            checkBox2 = new CheckBox();
            checkBox1 = new CheckBox();
            button2 = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.HotTrack;
            panel1.Controls.Add(label1);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(844, 106);
            panel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 36F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.Control;
            label1.Location = new Point(19, 30);
            label1.Name = "label1";
            label1.Size = new Size(324, 54);
            label1.TabIndex = 3;
            label1.Text = "HASTA KAYIT";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(12, 383);
            label6.Name = "label6";
            label6.Size = new Size(79, 15);
            label6.TabIndex = 25;
            label6.Text = "Doğum Tarihi";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 305);
            label5.Name = "label5";
            label5.Size = new Size(47, 15);
            label5.TabIndex = 24;
            label5.Text = "E-Posta";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 217);
            label4.Name = "label4";
            label4.Size = new Size(111, 15);
            label4.TabIndex = 23;
            label4.Text = "TC Kimlik Numarası";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(247, 137);
            label3.Name = "label3";
            label3.Size = new Size(48, 15);
            label3.TabIndex = 22;
            label3.Text = "Soyisim";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 137);
            label2.Name = "label2";
            label2.Size = new Size(29, 15);
            label2.TabIndex = 21;
            label2.Text = "İsim";
            // 
            // button1
            // 
            button1.Location = new Point(546, 395);
            button1.Name = "button1";
            button1.Size = new Size(226, 29);
            button1.TabIndex = 20;
            button1.Text = "Resim Yükle";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.CustomFormat = "yyyy-MM-dd";
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.Location = new Point(12, 401);
            dateTimePicker1.MaxDate = new DateTime(2025, 12, 31, 0, 0, 0, 0);
            dateTimePicker1.MinDate = new DateTime(1900, 1, 1, 0, 0, 0, 0);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(224, 23);
            dateTimePicker1.TabIndex = 19;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(12, 323);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(224, 23);
            textBox4.TabIndex = 18;
            textBox4.Validating += textBox4_Validating;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(12, 235);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(224, 23);
            textBox3.TabIndex = 17;
            textBox3.Validating += textBox3_Validating;
            textBox3.Validated += textBox3_Validated;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(247, 155);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(196, 23);
            textBox2.TabIndex = 16;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 155);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(189, 23);
            textBox1.TabIndex = 15;
            // 
            // pictureBox2
            // 
            pictureBox2.BorderStyle = BorderStyle.FixedSingle;
            pictureBox2.Image = Properties.Resources.Unknown_person;
            pictureBox2.Location = new Point(546, 137);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(226, 252);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 26;
            pictureBox2.TabStop = false;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            checkBox2.Location = new Point(274, 323);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(68, 25);
            checkBox2.TabIndex = 28;
            checkBox2.Text = "Kadın";
            checkBox2.UseVisualStyleBackColor = true;
            checkBox2.CheckedChanged += checkBox2_CheckedChanged;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            checkBox1.Location = new Point(276, 235);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(67, 25);
            checkBox1.TabIndex = 27;
            checkBox1.Text = "Erkek";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // button2
            // 
            button2.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button2.ForeColor = SystemColors.ControlText;
            button2.Location = new Point(332, 383);
            button2.Name = "button2";
            button2.Size = new Size(132, 41);
            button2.TabIndex = 29;
            button2.Text = "Hastayı Kaydet";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // Form3
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(834, 461);
            Controls.Add(button2);
            Controls.Add(checkBox2);
            Controls.Add(checkBox1);
            Controls.Add(pictureBox2);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(button1);
            Controls.Add(dateTimePicker1);
            Controls.Add(textBox4);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(panel1);
            Name = "Form3";
            Text = "Form3";
            FormClosed += Form3_FormClosed;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Button button1;
        private DateTimePicker dateTimePicker1;
        private TextBox textBox4;
        private TextBox textBox3;
        private TextBox textBox2;
        private TextBox textBox1;
        private PictureBox pictureBox2;
        private CheckBox checkBox2;
        private CheckBox checkBox1;
        private Button button2;
    }
}