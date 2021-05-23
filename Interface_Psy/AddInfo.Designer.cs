namespace Interface_Psy
{
    partial class AddInfo
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
            this.metroTextBox1 = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroTextBox2 = new MetroFramework.Controls.MetroTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // metroTextBox1
            // 
            this.metroTextBox1.Location = new System.Drawing.Point(115, 63);
            this.metroTextBox1.Name = "metroTextBox1";
            this.metroTextBox1.Size = new System.Drawing.Size(205, 23);
            this.metroTextBox1.TabIndex = 0;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(23, 63);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(71, 19);
            this.metroLabel1.TabIndex = 1;
            this.metroLabel1.Text = "Название:";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(23, 105);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(75, 19);
            this.metroLabel2.TabIndex = 2;
            this.metroLabel2.Text = "Описание:";
            // 
            // metroTextBox2
            // 
            this.metroTextBox2.Location = new System.Drawing.Point(23, 127);
            this.metroTextBox2.Multiline = true;
            this.metroTextBox2.Name = "metroTextBox2";
            this.metroTextBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.metroTextBox2.Size = new System.Drawing.Size(297, 151);
            this.metroTextBox2.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Info;
            this.button1.Location = new System.Drawing.Point(243, 289);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Сохранить";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // AddInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(343, 335);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.metroTextBox2);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.metroTextBox1);
            this.Name = "AddInfo";
            this.Style = MetroFramework.MetroColorStyle.Lime;
            this.Text = "Добавить запись";
            this.Load += new System.EventHandler(this.AddInfo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroTextBox metroTextBox1;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroTextBox metroTextBox2;
        private System.Windows.Forms.Button button1;
    }
}