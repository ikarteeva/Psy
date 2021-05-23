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

    public partial class ClientList : MetroFramework.Forms.MetroForm
    {
        Context db;

        public bool udalen = false;
        int count = 5;
        int page = 1;
        int pages;
        String sort = "ID";
        String asсdesс = "asc";
        int idclient;
        ApplicationList al1;
        string alname = null;

        public ClientList(ApplicationList al)
        {
            InitializeComponent();

            al1 = al;
            alname = al.Name;

            comboBox4.Items.Clear();
            comboBox3.Items.Clear();
            comboBox3.Items.Clear();

            comboBox4.Items.Add("");
            comboBox3.Items.Add("");
            comboBox2.Items.Add("");

            metroComboBox3.Items.Clear();

            db = new Context();
            db.info.Load();

            foreach (var r in db.info.Local.ToBindingList())
            {
                if (r.idguide == 3)
                {
                    comboBox4.Items.Add(r.Name);
                }

                if (r.idguide == 1)
                {
                    comboBox3.Items.Add(r.Name);
                }
            }

        }

        private void ClientList_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            count = Convert.ToInt16(comboBox1.Text);
            FindClients();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                udalen = true;
            }
            else {
                udalen = false;
                 }
        }

        private void metroButton5_Click(object sender, EventArgs e)
        {
            if (page == pages)
            { MessageBox.Show("Дальше записей нет!"); }
            else { page = page + 1; }

            FindClients();
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            if (page == 1)
            { MessageBox.Show("Дальше записей нет!"); }
            else { page = page - 1; }

            FindClients();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            metroTextBox1.Clear();
            metroTextBox2.Clear();
            metroTextBox3.Clear();
            metroTextBox4.Clear();

            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;

            checkBox1.Checked = false;
            metroCheckBox1.Checked = false;
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;

        }

        private void metroButton2_Click(object sender, EventArgs e)
        {

            metroComboBox3.Items.Clear();

            FindClients();

            int plittle = 1;

            while (plittle < pages + 1)
            {
                metroComboBox3.Items.Add(plittle);
                plittle++;
            }

        }


        public void FindClients()
        {
            Clients client = new Clients();

            if (metroTextBox1.Text != "")
            {
                String[] words = metroTextBox1.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                client.SecondName = words[0];
                client.FirstName = words[1];
                client.MiddleName = words[2];
            }
            else
            {
                client.SecondName = null;
                client.FirstName = null;
                client.MiddleName = null;
            }

            if (metroTextBox2.Text != "")
            {
                client.Telephone = metroTextBox2.Text;
            }
            else
            {
                client.Telephone = null;
            }

            if (metroTextBox3.Text != "")
            {
                client.ID = Convert.ToInt16(metroTextBox3.Text);
            }
            else
            {
                client.ID = 0;
            }

            if (comboBox4.Text != "")
            {
                client.ReasonType = comboBox4.Text;
            }
            else
            {
                client.ReasonType = null;
            }

            string method;

            if (comboBox3.Text != "")
            {
                method = comboBox3.Text;
            }
            else
            {
                method = null;
            }

            if (metroTextBox4.Text != "")
            {
                client.Email = metroTextBox4.Text;
            }
            else
            {
                client.Email = null;
            }


            DateTime? date1 = null;
            DateTime? date2 = null;


            if (metroCheckBox1.Checked)
            {
                date1 = dateTimePicker1.Value;
                date2 = dateTimePicker2.Value;
            }

            int countrecord = 0;

            dataGridView1.Rows.Clear();

            List<Clients> clients = new List<Clients>();
            clients = ClientsMethods.Finding(client, method, date1, date2, sort, asсdesс, udalen, page, count, ref countrecord);
            pages = Convert.ToInt32(Math.Ceiling((double)countrecord / count));



            int i = 0;

           
                foreach (var s in clients)
                {

                    dataGridView1.Rows.Add();

                    dataGridView1.Rows[i].Cells[0].Value = s.ID;
                    dataGridView1.Rows[i].Cells[1].Value = s.SecondName + ' ' + s.FirstName + ' ' + s.MiddleName;
                    dataGridView1.Rows[i].Cells[2].Value = s.Telephone;
                    dataGridView1.Rows[i].Cells[3].Value = s.Email;

                    int age = ClientsMethods.ClientAge(Convert.ToDateTime(s.DayofBirth));

                    dataGridView1.Rows[i].Cells[4].Value = age;


                    int count = ClientsMethods.CountApplication(s.ID);
                    string lastdata = ClientsMethods.LastDataApp(s.ID);
                    decimal ? countmoney = ClientsMethods.CountMoney(s.ID);

                    dataGridView1.Rows[i].Cells[5].Value = count;
                    dataGridView1.Rows[i].Cells[6].Value = lastdata;
                    dataGridView1.Rows[i].Cells[7].Value = countmoney;
                    dataGridView1.Rows[i].Cells[8].Value = "Выбрать";
                    i++;
                }
        }

        private void metroComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            page = Convert.ToInt16(metroComboBox3.SelectedItem);
            FindClients();
        }

        private void metroTile1_Click(object sender, EventArgs e)
        {
            if (metroTile1.Text == "А-Я")
            {
                metroTile1.Text = "Я-А";
                asсdesс = "desс";
            }
            else
            {
                metroTile1.Text = "А-Я";
                asсdesс = "asc";
            }

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 1)
            {
                sort = "ID";
            }
            else if(comboBox2.SelectedIndex == 2)
            {
                sort = "FIO";
            }
            else if (comboBox2.SelectedIndex == 3)
            {
                sort = "Phone";
            }
            else if (comboBox2.SelectedIndex == 4)
            {
                sort = "Email";
            }
            else
            {
                sort = "ID";
            }
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            db.Client.Load();

            Clients client = new Clients();

            if (metroTextBox1.Text != "")
            {
                String[] words = metroTextBox1.Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                client.SecondName = words[0];
                client.FirstName = words[1];
                client.MiddleName = words[2];
            }
            else
            {
                client.SecondName = null;
                client.FirstName = null;
                client.MiddleName = null;
            }

            if (metroTextBox2.Text != "")
            {
                client.Telephone = metroTextBox2.Text;
            }
            else
            {
                client.Telephone = null;
            }

            if (metroTextBox3.Text != "")
            {
                client.ID = Convert.ToInt16(metroTextBox3.Text);
            }
            else
            {
                client.ID = 0;
            }

            if (comboBox4.Text != "")
            {
                client.ReasonType = comboBox4.Text;
            }
            else
            {
                client.ReasonType = null;
            }

            db.Client.Add(client);
            db.SaveChanges();

            MessageBox.Show("Клиент успешно сохранен!");


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int id;

            if (e.RowIndex > -1 && e.ColumnIndex < 5)
            {
                id = Convert.ToInt16(dataGridView1[Column1.Index, e.RowIndex].Value.ToString());
                Client form1 = new Client(id);
                form1.ShowDialog();
            }

            else if (e.RowIndex > -1 && e.ColumnIndex == 5)
            {
                id = Convert.ToInt16(dataGridView1[Column1.Index, e.RowIndex].Value.ToString());
                ApplicationList form2 = new ApplicationList(id);
                form2.ShowDialog();
            }

            else if (e.RowIndex > -1 && e.ColumnIndex == 6)
            {
                id = Convert.ToInt16(dataGridView1[Column1.Index, e.RowIndex].Value.ToString());
                string date = dataGridView1[Column7.Index, e.RowIndex].Value.ToString();
                int id1 = 0;

                db.Client.Load();
                db.Application.Load();

                foreach (Clients c in db.Client.Local.ToBindingList())
                {
                    if (c.ID == id)
                    {
                        foreach (Applications a in db.Application.Local.ToBindingList())
                        {
                            if (c.ID == a.idclient)
                            {
                                if(Convert.ToDateTime(a.Date) == Convert.ToDateTime(date))
                                {
                                    id1 = a.ID;
                                }

                            }
                        }
                    }
                }

                if(id1 == 0)
                {
                    MessageBox.Show("Нет обращений!");
                }
                else
                {
                    Appeal form3 = new Appeal(id1);
                    form3.ShowDialog();
                }

            }

            else if (e.RowIndex > -1 && e.ColumnIndex == 8)
            {
                id = Convert.ToInt16(dataGridView1[Column1.Index, e.RowIndex].Value.ToString());
                string FIO = dataGridView1[Column2.Index, e.RowIndex].Value.ToString();

                FormCollection fc = Application.OpenForms;

                int flag = 0;

                foreach (Form frm in fc)
                {
                    if (frm.Name == alname)
                    {
                        flag = 1;
                        break;
                    }
                }

                if (flag == 0)
                {
                    ApplicationList al2 = new ApplicationList(id);
                    al2.ShowDialog();
                }
                else
                {
                    al1.chClient(FIO, id);
                    al1.Show();
                    Close();
                }
                
            }



        }

        private void metroPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
