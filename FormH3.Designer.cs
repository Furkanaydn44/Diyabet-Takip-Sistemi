namespace PatientDBProject
{
    partial class FormH3
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
            dateTimePicker1 = new DateTimePicker();
            label2 = new Label();
            textBox1 = new TextBox();
            label3 = new Label();
            label4 = new Label();
            dateTimePicker2 = new DateTimePicker();
            button1 = new Button();
            comboBox1 = new ComboBox();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.Firebrick;
            panel1.Controls.Add(label1);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(844, 85);
            panel1.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 36F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.Control;
            label1.Location = new Point(12, 18);
            label1.Name = "label1";
            label1.Size = new Size(493, 54);
            label1.TabIndex = 0;
            label1.Text = "KAN ŞEKERİ ÖLÇÜM";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(305, 112);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(200, 23);
            dateTimePicker1.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(316, 150);
            label2.Name = "label2";
            label2.Size = new Size(179, 21);
            label2.TabIndex = 3;
            label2.Text = "Kan Şeker Ölçüm Değeri";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(305, 174);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(200, 23);
            textBox1.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(352, 209);
            label3.Name = "label3";
            label3.Size = new Size(94, 21);
            label3.TabIndex = 6;
            label3.Text = "Ölçüm Saati";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(364, 283);
            label4.Name = "label4";
            label4.Size = new Size(73, 21);
            label4.TabIndex = 8;
            label4.Text = "Girdi Tipi";
            // 
            // dateTimePicker2
            // 
            dateTimePicker2.Location = new Point(356, 239);
            dateTimePicker2.Name = "dateTimePicker2";
            dateTimePicker2.Size = new Size(90, 23);
            dateTimePicker2.TabIndex = 9;
            // 
            // button1
            // 
            button1.Location = new Point(362, 356);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 10;
            button1.Text = "Onayla";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Sabah", "Öğle", "İkindi", "Akşam", "Gece", "Diğer" });
            comboBox1.Location = new Point(340, 307);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(121, 23);
            comboBox1.TabIndex = 11;
            // 
            // FormH3
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(comboBox1);
            Controls.Add(button1);
            Controls.Add(dateTimePicker2);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(textBox1);
            Controls.Add(label2);
            Controls.Add(dateTimePicker1);
            Controls.Add(panel1);
            Name = "FormH3";
            Text = "FormH3";
            FormClosed += FormH3_FormClosed;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private DateTimePicker dateTimePicker1;
        private Label label2;
        private TextBox textBox1;
        private Label label3;
        private Label label4;
        private DateTimePicker dateTimePicker2;
        private Button button1;
        private ComboBox comboBox1;
    }
}