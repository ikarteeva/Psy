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
    public partial class AddChAppeal : MetroFramework.Forms.MetroForm
    {

        Context db;

        public Applications appeal1;

        public AddChAppeal(Applications appeal)
        {

            remember.Clear();

            InitializeComponent();

            appeal1 = appeal;

            db = new Context();
            db.Application.Load();
            db.Client.Load();
            db.guide.Load();
            db.mia.Load();
            db.info.Load();

            if (appeal1.idclient > 0)
            {
                foreach (Clients c in db.Client.Local.ToBindingList())
                {
                    if (appeal1.idclient == c.ID)
                    {
                        metroLabel7.Text = c.SecondName + ' ' + c.FirstName + ' ' + c.MiddleName;
                        idclient = c.ID;
                    }
                }
            }

            if (appeal1.ID > 0)
            {
                BindingList<Applications> Apply = db.Application.Local.ToBindingList();

                        foreach (Clients c in db.Client.Local.ToBindingList())
                        {
                            if (appeal1.idclient == c.ID)
                            {
                                metroLabel7.Text = c.SecondName + ' ' + c.FirstName + ' ' + c.MiddleName;
                        idclient = c.ID;
                            }
                        }

                foreach (MethodInApplications m in db.mia.Local.ToBindingList())
                {
                    if(m.idapplication == appeal1.ID)
                    {
                        foreach (Infoes i in db.info.Local.ToBindingList())
                        {
                            if(m.idmethod == i.ID)
                            {
                                remember.Add(i.Name);
                            }
                        }
                    }
                }

                metroComboBox1.SelectedItem = appeal1.status;
                metroTextBox3.Text = appeal1.ReasonOfCancel;
                dateTimePicker1.Text = appeal1.Date.ToString();
                dateTimePicker2.Text = appeal1.Time;
                metroTextBox1.Text = appeal1.PsyComment;
                metroTextBox2.Text = Convert.ToString(appeal1.Cost);
                if(appeal1.StateOfHealth == 1)
                {
                    radioButton1.Select();
                }
                else if(appeal1.StateOfHealth == 2)
                {
                    radioButton2.Select();
                }
                else if (appeal1.StateOfHealth == 3)
                {
                    radioButton3.Select();
                }
                else if (appeal1.StateOfHealth == 4)
                {
                    radioButton4.Select();
                }
                else if (appeal1.StateOfHealth == 5)
                {
                    radioButton5.Select();
                }

            }




            metroTextBox3.Enabled = false;

            BindingList<Infoes> Info1 = db.info.Local.ToBindingList();


            foreach (Infoes p in Info1)
            {


                if (p.idguide == 1)
                {
                    int flag = 0;

                    foreach (var c in metroComboBox2.Items)
                    {
                        if (c.ToString() == p.Group)
                        {
                            flag = 1;
                        }

                    }
                    if (flag == 0)
                    { metroComboBox2.Items.Add(p.Group); }
                }
            }

        }

        public int idclient = -1;
        private void AddChAppeal_Load(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void metroLabel8_Click(object sender, EventArgs e)
        {

        }

        public void ChName(int id, string name)
        {
            idclient = id;
            this.metroLabel7.Text = name;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddChAppeal form2 = this;
            why f = new why(form2);
            f.Show();
        }

        private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (metroComboBox1.SelectedIndex == 1)
            {
                metroTextBox3.Enabled = true;
            }
            else
            {
                metroTextBox3.Enabled = false;
            }
        }

        private void metroComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkedListBox1.Items.Clear();

            string group = metroComboBox2.SelectedItem.ToString();

            BindingList<Infoes> Info1 = db.info.Local.ToBindingList();


            foreach (Infoes p in Info1)
            {
                if (p.idguide == 1 && p.Group == group)
                {
                    checkedListBox1.Items.Add(p.Name);
                }
            }



            int sc = 0;


                foreach (var c in checkedListBox1.Items.OfType<String>().ToList())
                {

                    foreach (var r in remember)
                    {
                    if (c == r)
                    { checkedListBox1.SetItemChecked(sc, true);}
                    }

                sc++;

            }



        }


        private void checkedListBox1_CheckedChanged(object sender, EventArgs e)
        {
        }

        List<string> remember = new List<string>();
        List<string> notremember = new List<string>();
        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            notremember.Clear();

            foreach (var d in checkedListBox1.Items.OfType<String>().ToList())
            {
                notremember.Add(d);
            }

            foreach (var n in notremember)
            {
                remember.Remove(n);
            }

            foreach (var c in checkedListBox1.CheckedItems.OfType<String>().ToList())
            {
                int flag = 0;
                foreach (var pr in remember)
                {
                    if (c == pr)
                    { flag = 1; }
                }

                if (flag == 0)

                { remember.Add(c); }
            }
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            notremember.Clear();

            foreach (var d in checkedListBox1.Items.OfType<String>().ToList())
            {
                notremember.Add(d);
            }

            foreach (var n in notremember)
            {
                remember.Remove(n);
            }

            foreach (var c in checkedListBox1.CheckedItems.OfType<String>().ToList())
            {
                int flag = 0;
                foreach (var pr in remember)
                {
                    if (c == pr)
                    { flag = 1; }
                }

                if (flag == 0)

                { remember.Add(c); }
            }
        
            MessageBox.Show("Список техник успешно сохранен!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int ida = 0;
            int flagok = 0;

            BindingList<Applications> Apply = db.Application.Local.ToBindingList();

            int flag = 0;
            foreach (Applications p in Apply)
            {
                if (dateTimePicker2.Text == p.Time && dateTimePicker1.Text == p.Date.ToString())
                {
                    if (p.ID == appeal1.ID)
                    { }
                    else
                    {    MessageBox.Show("Это время занято!");
                        flag = 1;
                    }
                }
            }

            if(flag == 0)
            {
                appeal1.idclient = idclient;
                appeal1.status = metroComboBox1.SelectedItem.ToString();
                appeal1.ReasonOfCancel = metroTextBox3.Text;
                appeal1.Date = dateTimePicker1.Text;
                appeal1.Time = dateTimePicker2.Text;
                appeal1.PsyComment = metroTextBox1.Text;
                appeal1.Cost = Convert.ToInt16(metroTextBox2.Text);

                if (radioButton1.Checked)
                {
                    appeal1.StateOfHealth = 1;
                }
                else if (radioButton2.Checked)
                {
                    appeal1.StateOfHealth = 2;
                }
                else if (radioButton3.Checked)
                {
                    appeal1.StateOfHealth = 3;
                }
                else if (radioButton4.Checked)
                {
                    appeal1.StateOfHealth = 4;
                }
                else if (radioButton5.Checked)
                {
                    appeal1.StateOfHealth = 5;
                }

                if (appeal1.ID == 0)
                {
                    db.Application.Add(appeal1);
                    db.SaveChanges();

                    ida = appeal1.ID;
                }
                else
                {
                    flagok = 1;
                    appeal1.Edit();
                    ida = appeal1.ID; 

                }

                notremember.Clear();

                foreach (var d in checkedListBox1.Items.OfType<String>().ToList())
                {
                    notremember.Add(d);
                }

                foreach (var n in notremember)
                {
                    remember.Remove(n);
                }

                foreach (var c in checkedListBox1.CheckedItems.OfType<String>().ToList())
                {
                    flag = 0;
                    foreach (var pr in remember)
                    {
                        if (c == pr)
                        { flag = 1; }
                    }

                    if (flag == 0)

                    { remember.Add(c); }
                }

                if (flagok == 1)

                {
                    List<int> delete = new List<int>();

                    foreach (MethodInApplications m in db.mia.Local.ToBindingList())
                    {
                        if (m.idapplication == appeal1.ID)
                        {
                            delete.Add(m.ID);
                        }
                    }

                    foreach (var d in delete)
                    {
                        MethodInApplications del = db.mia.Where(x => x.ID == d).FirstOrDefault();
                        db.mia.Remove(del);
                        db.SaveChanges();
                    }

                }

                    foreach (var f in remember)
                    {
                        BindingList<Infoes> Info1 = db.info.Local.ToBindingList();

                        int nomer = 0;

                        foreach (Infoes p in Info1)
                        {
                            if (f == p.Name)
                            {
                                nomer = p.ID;
                            }
                        }

                        MethodInApplications MiA = new MethodInApplications();
                        MiA.idapplication = ida;
                        MiA.idmethod = nomer;

                        db.mia.Add(MiA);
                    }

                    db.SaveChanges();


                MessageBox.Show("Обращение успешно сохранено");

            }
        }
    }
}
