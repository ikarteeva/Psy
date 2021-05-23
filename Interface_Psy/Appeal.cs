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
    public partial class Appeal: MetroFramework.Forms.MetroForm
    {
        Context db;
        public int ident;
        public Applications appeal;

        public Appeal(int id1)
        {
            InitializeComponent();
            db = new Context();
            db.Application.Load();
            db.Client.Load();
            db.mia.Load();
            db.info.Load();

            listBox1.Items.Clear();

            BindingList<MethodInApplications> MiA = db.mia.Local.ToBindingList();
            BindingList<Infoes> inf = db.info.Local.ToBindingList();

            foreach (MethodInApplications m in MiA)
            {
                if (m.idapplication == id1)
                {
                    foreach (Infoes i in inf)
                    {
                        if (m.idmethod == i.ID)
                        {
                            listBox1.Items.Add(i.Name);
                        }
                    }
                }
            }


            BindingList<Applications> Apply = db.Application.Local.ToBindingList();

            foreach (Applications p in Apply)
            {
                if (p.ID == id1)
                {
                    appeal = p;
                    foreach (Clients c in db.Client.Local.ToBindingList())
                    {
                        if (p.idclient == c.ID)
                        {
                            metroLabel7.Text = c.SecondName + ' ' + c.FirstName + ' ' + c.MiddleName;
                        }
                    }

                    metroLabel12.Text = p.status;
                    metroLabel3.Text = p.Date.ToString();
                    metroLabel4.Text = p.Time;
                    metroTextBox1.Text = p.PsyComment;
                    metroLabel10.Text = Convert.ToString(p.Cost);

                    int soshelth = p.StateOfHealth;

                    if (soshelth == 1)
                    {
                        radioButton1.Select();
                    }
                    else if (soshelth == 2)
                    {
                        radioButton2.Select();
                    }
                    else if (soshelth == 3)
                    {
                        radioButton3.Select();
                    }
                    else if (soshelth == 4)
                    {
                        radioButton4.Select();
                    }
                    else
                    {
                        radioButton5.Select();
                    }

                }

            }

        }

        private void Appeal_Load(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            AddChAppeal form1 = new AddChAppeal(appeal);
            form1.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            appeal.DateRemove = Convert.ToString(DateTime.Now);
            appeal.status = "Удалено";
            appeal.Edit();
            MessageBox.Show("Удаление прошло успешно!");
        }
    }
}
