namespace Interface_Psy
{
    partial class FAQ
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
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.button3 = new System.Windows.Forms.Button();
            this.metroTile3 = new MetroFramework.Controls.MetroTile();
            this.button2 = new System.Windows.Forms.Button();
            this.metroTile2 = new MetroFramework.Controls.MetroTile();
            this.metroTile1 = new MetroFramework.Controls.MetroTile();
            this.metroTextBox2 = new MetroFramework.Controls.MetroTextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.metroTextBox1 = new MetroFramework.Controls.MetroTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.metroPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroPanel1
            // 
            this.metroPanel1.Controls.Add(this.button3);
            this.metroPanel1.Controls.Add(this.metroTile3);
            this.metroPanel1.Controls.Add(this.button2);
            this.metroPanel1.Controls.Add(this.metroTile2);
            this.metroPanel1.Controls.Add(this.metroTile1);
            this.metroPanel1.Controls.Add(this.metroTextBox2);
            this.metroPanel1.Controls.Add(this.listBox1);
            this.metroPanel1.Controls.Add(this.button1);
            this.metroPanel1.Controls.Add(this.metroTextBox1);
            this.metroPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(20, 60);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(264, 492);
            this.metroPanel1.TabIndex = 0;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.SystemColors.Info;
            this.button3.Location = new System.Drawing.Point(6, 91);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(111, 23);
            this.button3.TabIndex = 13;
            this.button3.Text = "Очистить поиск";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // metroTile3
            // 
            this.metroTile3.Location = new System.Drawing.Point(158, 12);
            this.metroTile3.Name = "metroTile3";
            this.metroTile3.Size = new System.Drawing.Size(93, 23);
            this.metroTile3.Style = MetroFramework.MetroColorStyle.Green;
            this.metroTile3.TabIndex = 12;
            this.metroTile3.Text = "Обновить";
            this.metroTile3.Click += new System.EventHandler(this.metroTile3_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.button2.Location = new System.Drawing.Point(176, 282);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 11;
            this.button2.Text = "Удалить";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // metroTile2
            // 
            this.metroTile2.Location = new System.Drawing.Point(7, 423);
            this.metroTile2.Name = "metroTile2";
            this.metroTile2.Size = new System.Drawing.Size(91, 23);
            this.metroTile2.Style = MetroFramework.MetroColorStyle.Lime;
            this.metroTile2.TabIndex = 10;
            this.metroTile2.Text = "Добавить";
            this.metroTile2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.metroTile2.Click += new System.EventHandler(this.metroTile2_Click);
            // 
            // metroTile1
            // 
            this.metroTile1.Location = new System.Drawing.Point(194, 423);
            this.metroTile1.Name = "metroTile1";
            this.metroTile1.Size = new System.Drawing.Size(57, 23);
            this.metroTile1.Style = MetroFramework.MetroColorStyle.Lime;
            this.metroTile1.TabIndex = 9;
            this.metroTile1.Text = "Ок";
            this.metroTile1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // metroTextBox2
            // 
            this.metroTextBox2.Location = new System.Drawing.Point(7, 311);
            this.metroTextBox2.Multiline = true;
            this.metroTextBox2.Name = "metroTextBox2";
            this.metroTextBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.metroTextBox2.Size = new System.Drawing.Size(244, 103);
            this.metroTextBox2.TabIndex = 8;
            this.metroTextBox2.Text = "Тут отображается информация по выбранному элементу";
            this.metroTextBox2.Click += new System.EventHandler(this.metroTextBox2_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(7, 120);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(244, 147);
            this.listBox1.TabIndex = 7;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Info;
            this.button1.Location = new System.Drawing.Point(176, 62);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Искать";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // metroTextBox1
            // 
            this.metroTextBox1.Location = new System.Drawing.Point(6, 62);
            this.metroTextBox1.Name = "metroTextBox1";
            this.metroTextBox1.Size = new System.Drawing.Size(163, 23);
            this.metroTextBox1.TabIndex = 5;
            this.metroTextBox1.Text = "Название";
            this.metroTextBox1.Click += new System.EventHandler(this.metroTextBox1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(154, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // FAQ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 572);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.metroPanel1);
            this.Name = "FAQ";
            this.Style = MetroFramework.MetroColorStyle.Lime;
            this.Text = "Справочник";
            this.Load += new System.EventHandler(this.FAQ_Load);
            this.metroPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroTile metroTile1;
        private MetroFramework.Controls.MetroTextBox metroTextBox2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button1;
        private MetroFramework.Controls.MetroTextBox metroTextBox1;
        private MetroFramework.Controls.MetroTile metroTile2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private MetroFramework.Controls.MetroTile metroTile3;
        private System.Windows.Forms.Button button3;
    }
}