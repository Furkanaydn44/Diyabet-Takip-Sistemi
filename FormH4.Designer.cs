namespace PatientDBProject
{
    partial class FormH4
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
            richTextBox1 = new RichTextBox();
            button1 = new Button();
            dateTimePicker1 = new DateTimePicker();
            button2 = new Button();
            button3 = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.Firebrick;
            panel1.Controls.Add(label1);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1163, 96);
            panel1.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 36F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.ForeColor = SystemColors.Control;
            label1.Location = new Point(12, 23);
            label1.Name = "label1";
            label1.Size = new Size(411, 54);
            label1.TabIndex = 0;
            label1.Text = "GÜN RAPORLARI";
            // 
            // richTextBox1
            // 
            richTextBox1.BackColor = SystemColors.ControlLight;
            richTextBox1.BorderStyle = BorderStyle.FixedSingle;
            richTextBox1.Location = new Point(30, 133);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new Size(785, 420);
            richTextBox1.TabIndex = 3;
            richTextBox1.Text = "";
            richTextBox1.WordWrap = false;
            // 
            // button1
            // 
            button1.Location = new Point(849, 169);
            button1.Margin = new Padding(10);
            button1.Name = "button1";
            button1.Size = new Size(228, 23);
            button1.TabIndex = 4;
            button1.Text = "Kan Şekeri Raporu Oluştur";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(849, 133);
            dateTimePicker1.Margin = new Padding(3, 3, 3, 10);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(228, 23);
            dateTimePicker1.TabIndex = 5;
            // 
            // button2
            // 
            button2.Location = new Point(849, 205);
            button2.Name = "button2";
            button2.Size = new Size(228, 23);
            button2.TabIndex = 6;
            button2.Text = "İnsülin Raporu Oluştur";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Enabled = false;
            button3.Location = new Point(849, 530);
            button3.Margin = new Padding(10);
            button3.Name = "button3";
            button3.Size = new Size(228, 23);
            button3.TabIndex = 7;
            button3.Text = "Kan Şekeri Grafiği";
            button3.UseVisualStyleBackColor = true;
            button3.Visible = false;
            button3.Click += button3_Click;
            // 
            // FormH4
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1109, 583);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(dateTimePicker1);
            Controls.Add(button1);
            Controls.Add(richTextBox1);
            Controls.Add(panel1);
            Name = "FormH4";
            Text = "FormH4";
            FormClosed += FormH4_FormClosed;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private RichTextBox richTextBox1;
        private Button button1;
        private DateTimePicker dateTimePicker1;
        private Button button2;
        private Button button3;
    }
}