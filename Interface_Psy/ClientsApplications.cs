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
    public partial class ClientsApplications : MetroFramework.Forms.MetroForm
    {
        Context db;
        int idcl;
        public ClientsApplications(int id)
        {
            InitializeComponent();

            idcl = id;

            db = new Context();
            db.Client.Load();
            db.Application.Load();
            dataGridView1.Rows.Clear();

            foreach (Clients c in db.Client.Local.ToBindingList())
            {
                if (c.ID == id)
                {
                    metroLabel1.Text = c.SecondName + " " + c.FirstName;

                    int schet = 0;

                    foreach (Applications a in db.Application.Local.ToBindingList())
                    {
                        if (c.ID == a.idclient)
                        {
                            dataGridView1.Rows.Add();
                            dataGridView1.Rows[schet].Cells[0].Value = a.ID;
                            dataGridView1.Rows[schet].Cells[1].Value = a.Date;
                            dataGridView1.Rows[schet].Cells[2].Value = a.Time;
                            dataGridView1.Rows[schet].Cells[3].Value = a.status;
                            schet++;
                        }
                    }
                }
            }

        }

        private void ClientsApplications_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                int id = Convert.ToInt16(dataGridView1[Column1.Index, e.RowIndex].Value.ToString());
                Appeal form1 = new Appeal(id);
                form1.ShowDialog();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Applications appeal = new Applications();
            appeal.idclient = idcl;
            AddChAppeal form1 = new AddChAppeal(appeal);
            form1.ShowDialog();
        }
    }
}
