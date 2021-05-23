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
    public partial class ApplicationList : MetroFramework.Forms.MetroForm
    {
        Context db;

        int idclient = 0;
        public bool udalen = false;
        int count = 5;
        int page = 1;
        int pages;
        String sort = "ID";
        String asсdesс = "asc";
        public ApplicationList(int id)
        {
            InitializeComponent();

            idclient = id;

            comboBox8.Items.Clear();
            comboBox9.Items.Clear();

            comboBox8.Items.Add("");
            comboBox9.Items.Add("");

            metroComboBox3.Items.Clear();


            db = new Context();
            db.info.Load();
            db.Client.Load();

            if (idclient != 0)
            {
                foreach (var c in db.Client.Local.ToBindingList())
                {
                    if(c.ID == idclient)
                    {
                        metroLabel5.Text = c.SecondName + " " + c.FirstName + " " + c.MiddleName;
                    }
                }
            }

            foreach (var r in db.info.Local.ToBindingList())
            {
                if (r.idguide == 3)
                {
                    comboBox9.Items.Add(r.Name);
                }

                if (r.idguide == 1)
                {
                    comboBox8.Items.Add(r.Name);
                }
            }

        }

        private void ApplicationList_Load(object sender, EventArgs e)
        {

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            metroLabel5.Text = " ";
            idclient = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            count = Convert.ToInt16(comboBox1.Text);
            FindApplications();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                udalen = true;
            }
            else
            {
                udalen = false;
            }
        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            if (page == pages)
            { MessageBox.Show("Дальше записей нет!"); }
            else { page = page + 1; }

            FindApplications();
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            if (page == 1)
            { MessageBox.Show("Дальше записей нет!"); }
            else { page = page - 1; }

            FindApplications();
        }

        private void metroButton7_Click(object sender, EventArgs e)
        {
            metroTextBox6.Clear();
            metroTextBox4.Clear();

            comboBox9.SelectedIndex = 0;
            comboBox8.SelectedIndex = 0;
            comboBox6.SelectedIndex = 0;

            checkBox2.Checked = false;
            metroCheckBox2.Checked = false;
            dateTimePicker4.Value = DateTime.Now;
            dateTimePicker3.Value = DateTime.Now;
        }

        private void metroButton6_Click(object sender, EventArgs e)
        {
            metroComboBox3.Items.Clear();

            FindApplications();

            int plittle = 1;

            while (plittle < pages + 1)
            {
                metroComboBox3.Items.Add(plittle);
                plittle++;
            }
        }

        public void FindApplications()
        {
            Applications application = new Applications();

            if (metroTextBox6.Text != "")
            {
                application.ID = Convert.ToInt16(metroTextBox6.Text);
            }
            else
            {
                application.ID = 0;
            }

            if (metroTextBox4.Text != "")
            {
                application.Cost = Convert.ToDecimal(metroTextBox4.Text);
            }
            else
            {
                application.Cost = null;
            }

            string reason;

            if (comboBox8.Text != "")
            {
                reason = comboBox8.Text;
            }
            else
            {
                reason = null;
            }

            string method;

            if (comboBox9.Text != "")
            {
                method = comboBox9.Text;
            }
            else
            {
                method = null;
            }

            if (comboBox6.Text != "")
            {
                application.StateOfHealth = Convert.ToInt16(comboBox6.Text);
            }
            else
            {
                application.StateOfHealth = 0;
            }


            DateTime? date1 = null;
            DateTime? date2 = null;


            if (metroCheckBox2.Checked)
            {
                date1 = dateTimePicker4.Value;
                date2 = dateTimePicker3.Value;
            }

            int countrecord = 0;

            dataGridView1.Rows.Clear();

            List<Applications> applications = new List<Applications>();
            applications = ApplicationsMethods.Finding(application, idclient, reason, method, date1, date2, sort, asсdesс, udalen, page, count, ref countrecord);
            pages = Convert.ToInt32(Math.Ceiling((double)countrecord / count));

            int i = 0;
            decimal ? summa = 0;


            foreach (var s in applications)
            {

                dataGridView1.Rows.Add();

                dataGridView1.Rows[i].Cells[0].Value = s.ID;

                db.Client.Load();

                foreach(var c in db.Client.Local.ToBindingList())
                {
                    if(c.ID == s.idclient)
                    {
                        dataGridView1.Rows[i].Cells[1].Value = c.SecondName + ' ' + c.FirstName + ' ' + c.MiddleName;
                    }
                }

                dataGridView1.Rows[i].Cells[2].Value = s.Date;
                dataGridView1.Rows[i].Cells[3].Value = s.Time;
                dataGridView1.Rows[i].Cells[4].Value = s.status;
                dataGridView1.Rows[i].Cells[5].Value = s.Cost;

                summa = summa + s.Cost;
                i++;
            }

            metroLabel8.Text = Convert.ToString(summa);



        }

        private void metroLabel8_Click(object sender, EventArgs e)
        {

        }

        private void metroComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            page = Convert.ToInt16(metroComboBox3.SelectedItem);
            FindApplications();
        }

        private void metroTile2_Click(object sender, EventArgs e)
        {
            if (metroTile2.Text == "А-Я")
            {
                metroTile2.Text = "Я-А";
                asсdesс = "desс";
            }
            else
            {
                metroTile2.Text = "А-Я";
                asсdesс = "asc";
            }
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox7.SelectedIndex == 1)
            {
                sort = "ID";
            }
            else if (comboBox7.SelectedIndex == 2)
            {
                sort = "IDclient";
            }
            else if (comboBox7.SelectedIndex == 3)
            {
                sort = "Date";
            }
            else if (comboBox7.SelectedIndex == 4)
            {
                sort = "Time";
            }
            else if (comboBox7.SelectedIndex == 4)
            {
                sort = "Status";
            }
            else if (comboBox7.SelectedIndex == 4)
            {
                sort = "Cost";
            }
            else
            {
                sort = "ID";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (metroTextBox7.Text != "")
            {
                string phone = metroTextBox7.Text;

                if (metroTextBox1.Text != "")
                {
                    try
                    {
                        String[] fio = metroTextBox1.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        db.Client.Load();
                        string fio1 = fio[0];
                        string fio2 = fio[1];
                        string fio3 = fio[2];

                        var v = db.Client.Where(x => x.FirstName == fio2 && x.SecondName == fio1 && x.MiddleName == fio3  && x.Telephone == phone).FirstOrDefault();

                        if (v == null)
                        {
                            MessageBox.Show("Ни одного клиента не найдено!");
                        }
                        else
                        {
                            MessageBox.Show("Клиент успешно найден!");
                            metroLabel5.Text = v.SecondName + " " + v.FirstName + " " + v.MiddleName;
                            idclient = v.ID;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Неправильно введено ФИО!");
                    }


                }
                else { MessageBox.Show("Вы не ввели ФИО!"); }
            }
            else
            {
                MessageBox.Show("Вы не ввели номер телефона!");
            }

        }

        public void chClient(string FIO, int id)
        {
            idclient = id;
            metroLabel5.Text = FIO;
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            ClientList cl = new ClientList(this);
            cl.Show();
            Hide();

        }
    }
}
