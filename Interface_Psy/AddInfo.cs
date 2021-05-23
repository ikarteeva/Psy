using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Interface_Psy
{
    public partial class AddInfo : MetroFramework.Forms.MetroForm
    {
        Context db;

        int addid;
        public AddInfo(int id)
        {
            db = new Context();
            db.info.Load();

            addid = id;
            InitializeComponent();
        }

        private void AddInfo_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Infoes newinf = new Infoes();
            newinf.Name = metroTextBox1.Text;
            newinf.idguide = addid;
            newinf.Meaning = metroTextBox2.Text;

            db.info.Add(newinf);
            db.SaveChanges();

            MessageBox.Show("Сохранение прошло успешно!");
        }
    }
}
