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
    public partial class AddChClient : MetroFramework.Forms.MetroForm
    {
        public Clients client1;
        Context db;

        public AddChClient(Clients client)
        {
            InitializeComponent();
            client1 = client;
            db = new Context();
            db.info.Load();

            if (client1.ID > 0)
            {
                metroTextBox5.Text = client1.SecondName;
                metroTextBox6.Text = client1.FirstName;
                metroTextBox7.Text = client1.MiddleName;
                metroTextBox8.Text = client1.Nationality;
                metroTextBox9.Text = client1.Telephone;
                metroTextBox10.Text = client1.Email;
                metroTextBox1.Text = client1.OtherContactInformation;
                metroTextBox2.Text = client1.Education;
                metroTextBox3.Text = client1.HowFindOut;
                metroTextBox4.Text = client1.ReasonForVisiting;
                dateTimePicker1.Text = client1.DayofBirth;

                metroComboBox1.Text = client1.Sex;
                metroComboBox2.Text = client1.Marriage;
                metroComboBox3.Text = client1.Child;
                metroComboBox4.Text = client1.ReasonType;

            }

            foreach (Infoes c in db.info.Local.ToBindingList())
            {
                if (c.idguide == 3)
                {
                    metroComboBox4.Items.Add(c.Name);
                }
            }

        }

        private void AddChClient_Load(object sender, EventArgs e)
        {

        }

        private void metroLabel22_Click(object sender, EventArgs e)
        {

        }

        private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            metroComboBox2.Items.Clear();

            if (metroComboBox1.SelectedIndex == 0)
            {
                metroComboBox2.Items.Add("Холост");
                metroComboBox2.Items.Add("Женат");
                metroComboBox2.Items.Add("Разведен");
            }
            else
            {
                metroComboBox2.Items.Add("Не замужем");
                metroComboBox2.Items.Add("Замужем");
                metroComboBox2.Items.Add("Разведена");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            client1.SecondName = metroTextBox5.Text;
            client1.FirstName = metroTextBox6.Text;
            client1.MiddleName = metroTextBox7.Text;
            client1.Nationality = metroTextBox8.Text;
            client1.Telephone = metroTextBox9.Text;
            client1.Email = metroTextBox10.Text;
            client1.OtherContactInformation = metroTextBox1.Text;
            client1.Education = metroTextBox2.Text;
            client1.HowFindOut = metroTextBox3.Text;
            client1.ReasonForVisiting = metroTextBox4.Text;
            client1.DayofBirth = dateTimePicker1.Text;
            client1.Sex = metroComboBox1.Text;
            client1.Marriage = metroComboBox2.Text;
            client1.Child = metroComboBox3.Text;
            client1.ReasonType = metroComboBox4.Text;
            
            db = new Context();
            if (client1.ID == 0)
            {
                db.Client.Add(client1);
                db.SaveChanges();
            }

            else
            {
                client1.Edit();
            }

            MessageBox.Show("Клиент успешно сохранен!");

        }
    }
}
