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
    public partial class FAQ : MetroFramework.Forms.MetroForm

    {
        Context db;
        int id3;
        public FAQ(int id)
        {
            InitializeComponent();
            id3 = id;

            if(id3 == 3)
            {
                label1.Text = "Причин посещения";
            }
            else if(id3 == 2)
            {
                label1.Text = "Причин отмены";
            }
            else
            {
                label1.Text = "Техник";
            }

            db = new Context();
            db.info.Load();
            listBox1.Items.Clear();

            BindingList<Infoes> inf = db.info.Local.ToBindingList();

                    foreach (Infoes i in inf)
                    {
                        if (id3 == i.idguide && i.DateRemove == null)
                        {
                            listBox1.Items.Add(i.Name);
                        }
                    }

        }

        private void FAQ_Load(object sender, EventArgs e)
        {

        }

        private void metroTextBox2_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            metroTextBox2.Clear();

            BindingList<Infoes> inf = db.info.Local.ToBindingList();

            foreach (Infoes i in inf)
            {
                if (listBox1.SelectedItem.Equals(i.Name))
                {
                    metroTextBox2.Text = i.Meaning;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Infoes infs = new Infoes();
            foreach (Infoes i in db.info.Local.ToBindingList())
            {
                if (i.Name == listBox1.SelectedItem.ToString())
                {
                    i.DateRemove = DateTime.Now;
                    i.Edit();
                }
            }

            MessageBox.Show("Удаление прошло успешно!");

        }

        private void metroTile2_Click(object sender, EventArgs e)
        {
            AddInfo frm = new AddInfo(id3);
            frm.ShowDialog();
        }

        private void metroTile3_Click(object sender, EventArgs e)
        {
            db = new Context();
            db.info.Load();
            listBox1.Items.Clear();

            BindingList<Infoes> inf = db.info.Local.ToBindingList();

            foreach (Infoes i in inf)
            {
                if (id3 == i.idguide && i.DateRemove == null)
                {
                    listBox1.Items.Add(i.Name);
                }
            }
        }

        private void metroTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string text = metroTextBox1.Text;
            var v = db.info.Where(x => x.DateRemove == null && x.Name == text).ToList().OrderBy(u => u.Name);
            listBox1.Items.Clear();
            foreach (var p in v)
            {
                listBox1.Items.Add(p.Name);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            db = new Context();
            db.info.Load();
            listBox1.Items.Clear();

            BindingList<Infoes> inf = db.info.Local.ToBindingList();

            foreach (Infoes i in inf)
            {
                if (id3 == i.idguide && i.DateRemove == null)
                {
                    listBox1.Items.Add(i.Name);
                }
            }

        }
    }
}
