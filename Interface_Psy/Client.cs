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
    public partial class Client : MetroFramework.Forms.MetroForm
    {
        public Clients client1 = new Clients();

        Context db;
        public Client(int id)
        {
            InitializeComponent();
            db = new Context();
            db.Client.Load();

            foreach (Clients p in db.Client.Local.ToBindingList())
            {
                if (p.ID == id)
                {
                    client1 = p;
                    metroLabel4.Text = p.SecondName;
                    metroLabel5.Text = p.FirstName;
                    metroLabel6.Text = p.MiddleName;
                    metroLabel17.Text = p.DayofBirth;
                    metroLabel13.Text = p.Sex;
                    metroLabel15.Text = p.Nationality;
                    metroLabel9.Text = p.Telephone;
                    metroLabel11.Text = p.Email;
                    metroTextBox1.Text = p.OtherContactInformation;
                    metroTextBox2.Text = p.Education;
                    metroLabel21.Text = p.Marriage;
                    metroLabel23.Text = p.Child;
                    metroTextBox3.Text = p.HowFindOut;
                    metroTextBox4.Text = p.ReasonForVisiting;
                    metroLabel27.Text = p.ReasonType;

                }

            }
        }

        private void Client_Load(object sender, EventArgs e)
        {

        }

        private void metroPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddChClient form1 = new AddChClient(client1);
            form1.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            client1.DateRemove = Convert.ToString(DateTime.Now);
            client1.Edit();
            MessageBox.Show("Удаление прошло успешно!");
        }
    }
}
