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
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;


namespace Interface_Psy
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        Context db;

        DateTime nowmonday;

        DateTime? datea1;
        DateTime? datea2;
        int flaga;
        bool udalena;

        public Form1()
        {
            InitializeComponent();

            dataGridView1.AutoGenerateColumns = false;

            db = new Context();
            string opa = db.Database.Connection.Database;
            db.Application.Load();
            db.Client.Load();


            datea1 = null;
            datea2 = null;
            flaga = 0;

            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            dataGridView3.Rows.Clear();
            dataGridView4.Rows.Clear();
            chart1.Series.Clear();
            chart2.Series.Clear();
            chart1.Titles.Clear();
            chart2.Titles.Clear();


            DateTime firstmonday = DateTime.Now.StartOfWeek(DayOfWeek.Monday);

            nowmonday = firstmonday;

            metroLabel1.Text = firstmonday.ToString("dd.MM.yyyy");

            DateTime firstsunday = firstmonday.AddDays(6);

            metroLabel4.Text = firstsunday.ToString("dd.MM.yyyy");


            var apply1 = db.Application.OrderBy(p => p.Date).ThenBy(p => p.Time);

            int o = 0;
            foreach (Applications a in apply1)
            {
                if(Convert.ToDateTime(a.Date) >= firstmonday && Convert.ToDateTime(a.Date) <= firstsunday && a.DateRemove == null)
                {
                    dataGridView1.Rows.Add();

                    if (Convert.ToDateTime(a.Date) == firstmonday)
                    {
                        dataGridView1.Rows[o].Cells[0].Value = "Понедельник" + " " + a.Date;
                    }
                    else if(Convert.ToDateTime(a.Date).AddDays(-1) == firstmonday)
                    {
                        dataGridView1.Rows[o].Cells[0].Value = "Вторник" + " " + a.Date;
                    }
                    else if (Convert.ToDateTime(a.Date).AddDays(-2) == firstmonday)
                    {
                        dataGridView1.Rows[o].Cells[0].Value = "Среда" + " " + a.Date;
                    }
                    else if (Convert.ToDateTime(a.Date).AddDays(-3) == firstmonday)
                    {
                        dataGridView1.Rows[o].Cells[0].Value = "Четверг" + " " + a.Date;
                    }
                    else if (Convert.ToDateTime(a.Date).AddDays(-4) == firstmonday)
                    {
                        dataGridView1.Rows[o].Cells[0].Value = "Пятница" + " " + a.Date;
                    }
                    else if (Convert.ToDateTime(a.Date).AddDays(-5) == firstmonday)
                    {
                        dataGridView1.Rows[o].Cells[0].Value = "Суббота" + " " + a.Date;
                    }
                    else if (Convert.ToDateTime(a.Date).AddDays(-6) == firstmonday)
                    {
                        dataGridView1.Rows[o].Cells[0].Value = "Воскресенье" + " " + a.Date;
                    }

                    dataGridView1.Rows[o].Cells[1].Value = a.Time;
                    foreach (Clients c in db.Client.Local.ToBindingList())
                    {
                        if (a.idclient == c.ID)
                        { dataGridView1.Rows[o].Cells[2].Value = c.SecondName + ' ' + c.FirstName; }
                    }
                        o++;
                }
            }

            dataGridView1.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12);
            dataGridView1.Columns[0].Width = 140;
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].Width = 170;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;


            BindingList<Applications> Apply = db.Application.Local.ToBindingList();
            int s = 0;

            foreach (Applications p in Apply)
            {
                if (p.DateRemove == null)
                {
                    dataGridView2.Rows.Add();

                    dataGridView2.Rows[s].Cells[0].Value = p.Date;
                    dataGridView2.Rows[s].Cells[1].Value = p.Time;

                    int id = p.idclient;

                    foreach (Clients c in db.Client.Local.ToBindingList())
                    {
                        if (id == c.ID)
                        {
                            dataGridView2.Rows[s].Cells[2].Value = c.SecondName + ' ' + c.FirstName;
                        }
                    }

                    dataGridView2.Rows[s].Cells[3].Value = p.PsyComment;
                    dataGridView2.Rows[s].Cells[4].Value = p.ID;

                    s++;
                }
            }

            s = 0;

            foreach (Clients p in db.Client.Local.ToBindingList())
            {
                if (p.DateRemove == null)
                {

                    dataGridView3.Rows.Add();

                    dataGridView3.Rows[s].Cells[0].Value = p.SecondName;
                    dataGridView3.Rows[s].Cells[1].Value = p.FirstName;
                    dataGridView3.Rows[s].Cells[2].Value = p.MiddleName;
                    dataGridView3.Rows[s].Cells[3].Value = p.Telephone;
                    dataGridView3.Rows[s].Cells[4].Value = p.Email;
                    dataGridView3.Rows[s].Cells[5].Value = p.DayofBirth;
                    dataGridView3.Rows[s].Cells[6].Value = p.ID;
                    s++;
                }


            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }




        bool IsTheSameCellValue(int column, int row)
        {
            DataGridViewCell cell1 = dataGridView1[column, row];
            DataGridViewCell cell2 = dataGridView1[column, row - 1];
            if (cell1.Value == null || cell2.Value == null)
            {
                return false;
            }
            return cell1.Value.ToString() == cell2.Value.ToString();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex == 0)
                return;
            if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
            {
                e.Value = "";
                e.FormattingApplied = true;
            }
        }

        private void dataGridView1_CellPainting_1(object sender, DataGridViewCellPaintingEventArgs e)
        {
            e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;
            if (e.RowIndex < 1 || e.ColumnIndex < 0)
                return;
            if (IsTheSameCellValue(e.ColumnIndex, e.RowIndex))
            {
                e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
            }
            else
            {
                e.AdvancedBorderStyle.Top = dataGridView1.AdvancedCellBorderStyle.Top;
            }
        }






        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                int id = Convert.ToInt16(dataGridView2[ColumnID.Index, e.RowIndex].Value.ToString());
            Appeal form1 = new Appeal(id);
            form1.ShowDialog();
            }
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                int id = Convert.ToInt16(dataGridView3[Column11.Index, e.RowIndex].Value.ToString());
                Client form1 = new Client(id);
                form1.ShowDialog();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Clients client = new Clients();
            AddChClient form1 = new AddChClient(client);
            form1.ShowDialog();
        }

        private void metroTile1_Click(object sender, EventArgs e)
        {

            db = new Context();
            string opa = db.Database.Connection.Database;
            db.Application.Load();
            db.Client.Load();

            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            dataGridView3.Rows.Clear();

            DateTime firstmonday = DateTime.Now.StartOfWeek(DayOfWeek.Monday);

            nowmonday = firstmonday;

            metroLabel1.Text = firstmonday.ToString("dd.MM.yyyy");

            DateTime firstsunday = firstmonday.AddDays(6);

            metroLabel4.Text = firstsunday.ToString("dd.MM.yyyy");


            var apply1 = db.Application.OrderBy(p => p.Date).ThenBy(p => p.Time);

            int o = 0;
            foreach (Applications a in apply1)
            {
                if (Convert.ToDateTime(a.Date) >= firstmonday && Convert.ToDateTime(a.Date) <= firstsunday && a.DateRemove == null)
                {
                    dataGridView1.Rows.Add();

                    if (Convert.ToDateTime(a.Date) == firstmonday)
                    {
                        dataGridView1.Rows[o].Cells[0].Value = "Понедельник" + " " + a.Date;
                    }
                    else if (Convert.ToDateTime(a.Date).AddDays(-1) == firstmonday)
                    {
                        dataGridView1.Rows[o].Cells[0].Value = "Вторник" + " " + a.Date;
                    }
                    else if (Convert.ToDateTime(a.Date).AddDays(-2) == firstmonday)
                    {
                        dataGridView1.Rows[o].Cells[0].Value = "Среда" + " " + a.Date;
                    }
                    else if (Convert.ToDateTime(a.Date).AddDays(-3) == firstmonday)
                    {
                        dataGridView1.Rows[o].Cells[0].Value = "Четверг" + " " + a.Date;
                    }
                    else if (Convert.ToDateTime(a.Date).AddDays(-4) == firstmonday)
                    {
                        dataGridView1.Rows[o].Cells[0].Value = "Пятница" + " " + a.Date;
                    }
                    else if (Convert.ToDateTime(a.Date).AddDays(-5) == firstmonday)
                    {
                        dataGridView1.Rows[o].Cells[0].Value = "Суббота" + " " + a.Date;
                    }
                    else if (Convert.ToDateTime(a.Date).AddDays(-6) == firstmonday)
                    {
                        dataGridView1.Rows[o].Cells[0].Value = "Воскресенье" + " " + a.Date;
                    }

                    dataGridView1.Rows[o].Cells[1].Value = a.Time;
                    foreach (Clients c in db.Client.Local.ToBindingList())
                    {
                        if (a.idclient == c.ID)
                        { dataGridView1.Rows[o].Cells[2].Value = c.SecondName + ' ' + c.FirstName; }
                    }
                    o++;
                }
            }

            dataGridView1.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12);
            dataGridView1.Columns[0].Width = 140;
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].Width = 170;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;


            BindingList<Applications> Apply = db.Application.Local.ToBindingList();
            int s = 0;

            foreach (Applications p in Apply)
            {
                if (p.DateRemove == null)
                {
                    dataGridView2.Rows.Add();

                    dataGridView2.Rows[s].Cells[0].Value = p.Date;
                    dataGridView2.Rows[s].Cells[1].Value = p.Time;

                    int id = p.idclient;

                    foreach (Clients c in db.Client.Local.ToBindingList())
                    {
                        if (id == c.ID)
                        {
                            dataGridView2.Rows[s].Cells[2].Value = c.SecondName + ' ' + c.FirstName;
                        }
                    }

                    dataGridView2.Rows[s].Cells[3].Value = p.PsyComment;
                    dataGridView2.Rows[s].Cells[4].Value = p.ID;

                    s++;
                }
            }

            s = 0;

            foreach (Clients p in db.Client.Local.ToBindingList())
            {
                if (p.DateRemove == null)
                {

                    dataGridView3.Rows.Add();

                    dataGridView3.Rows[s].Cells[0].Value = p.SecondName;
                    dataGridView3.Rows[s].Cells[1].Value = p.FirstName;
                    dataGridView3.Rows[s].Cells[2].Value = p.MiddleName;
                    dataGridView3.Rows[s].Cells[3].Value = p.Telephone;
                    dataGridView3.Rows[s].Cells[4].Value = p.Email;
                    dataGridView3.Rows[s].Cells[5].Value = p.DayofBirth;
                    dataGridView3.Rows[s].Cells[6].Value = p.ID;
                    s++;
                }


            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Applications appeal = new Applications();
            AddChAppeal form1 = new AddChAppeal(appeal);
            form1.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            dataGridView1.Rows.Clear();

            DateTime newmonday = nowmonday.AddDays(-7);

            metroLabel1.Text = newmonday.ToString("dd.MM.yyyy");

            DateTime nowsunday = newmonday.AddDays(6);

            metroLabel4.Text = nowsunday.ToString("dd.MM.yyyy");

            nowmonday = newmonday;

            var apply1 = db.Application.OrderBy(p => p.Date).ThenBy(p => p.Time);

            int o = 0;
            foreach (Applications a in apply1)
            {
                if (Convert.ToDateTime(a.Date) >= newmonday && Convert.ToDateTime(a.Date) <= nowsunday && a.DateRemove == null)
                {
                    dataGridView1.Rows.Add();

                    if (Convert.ToDateTime(a.Date) == newmonday)
                    {
                        dataGridView1.Rows[o].Cells[0].Value = "Понедельник" + " " + a.Date;
                    }
                    else if (Convert.ToDateTime(a.Date).AddDays(-1) == newmonday)
                    {
                        dataGridView1.Rows[o].Cells[0].Value = "Вторник" + " " + a.Date;
                    }
                    else if (Convert.ToDateTime(a.Date).AddDays(-2) == newmonday)
                    {
                        dataGridView1.Rows[o].Cells[0].Value = "Среда" + " " + a.Date;
                    }
                    else if (Convert.ToDateTime(a.Date).AddDays(-3) == newmonday)
                    {
                        dataGridView1.Rows[o].Cells[0].Value = "Четверг" + " " + a.Date;
                    }
                    else if (Convert.ToDateTime(a.Date).AddDays(-4) == newmonday)
                    {
                        dataGridView1.Rows[o].Cells[0].Value = "Пятница" + " " + a.Date;
                    }
                    else if (Convert.ToDateTime(a.Date).AddDays(-5) == newmonday)
                    {
                        dataGridView1.Rows[o].Cells[0].Value = "Суббота" + " " + a.Date;
                    }
                    else if (Convert.ToDateTime(a.Date).AddDays(-6) == newmonday)
                    {
                        dataGridView1.Rows[o].Cells[0].Value = "Воскресенье" + " " + a.Date;
                    }

                    dataGridView1.Rows[o].Cells[1].Value = a.Time;
                    foreach (Clients c in db.Client.Local.ToBindingList())
                    {
                        if (a.idclient == c.ID)
                        { dataGridView1.Rows[o].Cells[2].Value = c.SecondName + ' ' + c.FirstName; }
                    }
                    o++;
                }
            }

            dataGridView1.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12);
            dataGridView1.Columns[0].Width = 140;
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].Width = 170;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;


        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();

            DateTime newmonday = nowmonday.AddDays(7);

            metroLabel1.Text = newmonday.ToString("dd.MM.yyyy");

            DateTime nowsunday = newmonday.AddDays(6);

            metroLabel4.Text = nowsunday.ToString("dd.MM.yyyy");

            nowmonday = newmonday;

            var apply1 = db.Application.OrderBy(p => p.Date).ThenBy(p => p.Time);

            int o = 0;
            foreach (Applications a in apply1)
            {
                if (Convert.ToDateTime(a.Date) >= newmonday && Convert.ToDateTime(a.Date) <= nowsunday && a.DateRemove == null)
                {
                    dataGridView1.Rows.Add();

                    if (Convert.ToDateTime(a.Date) == newmonday)
                    {
                        dataGridView1.Rows[o].Cells[0].Value = "Понедельник" + " " + a.Date;
                    }
                    else if (Convert.ToDateTime(a.Date).AddDays(-1) == newmonday)
                    {
                        dataGridView1.Rows[o].Cells[0].Value = "Вторник" + " " + a.Date;
                    }
                    else if (Convert.ToDateTime(a.Date).AddDays(-2) == newmonday)
                    {
                        dataGridView1.Rows[o].Cells[0].Value = "Среда" + " " + a.Date;
                    }
                    else if (Convert.ToDateTime(a.Date).AddDays(-3) == newmonday)
                    {
                        dataGridView1.Rows[o].Cells[0].Value = "Четверг" + " " + a.Date;
                    }
                    else if (Convert.ToDateTime(a.Date).AddDays(-4) == newmonday)
                    {
                        dataGridView1.Rows[o].Cells[0].Value = "Пятница" + " " + a.Date;
                    }
                    else if (Convert.ToDateTime(a.Date).AddDays(-5) == newmonday)
                    {
                        dataGridView1.Rows[o].Cells[0].Value = "Суббота" + " " + a.Date;
                    }
                    else if (Convert.ToDateTime(a.Date).AddDays(-6) == newmonday)
                    {
                        dataGridView1.Rows[o].Cells[0].Value = "Воскресенье" + " " + a.Date;
                    }

                    dataGridView1.Rows[o].Cells[1].Value = a.Time;
                    foreach (Clients c in db.Client.Local.ToBindingList())
                    {
                        if (a.idclient == c.ID)
                        { dataGridView1.Rows[o].Cells[2].Value = c.SecondName + ' ' + c.FirstName; }
                    }
                    o++;
                }
            }

            dataGridView1.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12);
            dataGridView1.Columns[0].Width = 140;
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].Width = 170;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;





        }

        private void справочникПричинОбращенияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FAQ form1 = new FAQ(3);
            form1.ShowDialog();
        }

        private void справочникИспользуемыхТехникToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FAQ form1 = new FAQ(1);
            form1.ShowDialog();
        }

        private void справочникТиповПричинОтменыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FAQ form1 = new FAQ(2);
            form1.ShowDialog();
        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void metroCheckBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void metroCheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        public void Filtr(int p1, int p2, int p3, int p4)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            List<Analysis> an = AnalysisMethods.Finding(flaga, datea1, datea2, udalena);

            chart1.Series.Clear();
            chart2.Series.Clear();
            chart1.Titles.Clear();
            chart2.Titles.Clear();

            int i = 0;
            dataGridView4.Rows.Clear();

            int counttable = 0;
            decimal? sumtable = 0;

            foreach (var a in an)
            {
                dataGridView4.Rows.Add();
                dataGridView4.Rows[i].Cells[0].Value = a.qualifier;
                dataGridView4.Rows[i].Cells[1].Value = a.sum;
                dataGridView4.Rows[i].Cells[2].Value = a.count;

                counttable = counttable + a.count;
                sumtable = sumtable + a.sum;
                i++;
            }

            dataGridView4.Rows.Add();
            dataGridView4.Rows[i].Cells[0].Value = "Итого";
            dataGridView4.Rows[i].Cells[1].Value = sumtable;
            dataGridView4.Rows[i].Cells[2].Value = counttable;


            chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SeaGreen;
            chart1.Titles.Add("Количество");
            chart2.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.EarthTones;
            chart2.Titles.Add("Сумма");

            foreach (var a in an)
            {
                Series series = chart1.Series.Add(a.qualifier);
                series.Points.Add(a.count);

                Series series1 = chart2.Series.Add(a.qualifier);
                series1.Points.Add(Convert.ToDouble(a.sum));

            }



        }

        private void button6_Click(object sender, EventArgs e)
        {
            ApplicationList al = new ApplicationList(0);
            ClientList cl = new ClientList(al);
            cl.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ApplicationList al = new ApplicationList(0);
            al.Show();
        }

        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = openFileDialog1.FileName;
            Context.chbd(filename);
            MessageBox.Show("База данных открыта");

            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            dataGridView3.Rows.Clear();

        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            db.SaveChanges();
            MessageBox.Show("База данных успешно сохранена!");
        }

        private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(metroComboBox1.SelectedIndex == 0 || metroComboBox1.SelectedIndex == 1)
            {
                metroLabel5.Visible = false;
                dateTimePicker1.Visible = false;
                dateTimePicker2.Visible = false;
                datea1 = null;
                datea2 = null;
            }
            else if (metroComboBox1.SelectedIndex == 2)
            {
                metroLabel5.Visible = false;
                dateTimePicker1.Visible = false;
                dateTimePicker2.Visible = false;
                datea1 = DateTime.Now;
                datea2 = DateTime.Now.AddDays(-30);
            }
            else if (metroComboBox1.SelectedIndex == 3)
            {
                metroLabel5.Visible = true;
                dateTimePicker1.Visible = true;
                dateTimePicker2.Visible = true;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if(dateTimePicker1.Value < dateTimePicker2.Value)
            {
                datea1 = dateTimePicker2.Value;
                datea2 = dateTimePicker1.Value;
            }
        }

        private void metroComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (metroComboBox2.SelectedIndex == 0 || metroComboBox2.SelectedIndex == 4)
            {
                flaga = 0;
            }
            else if (metroComboBox2.SelectedIndex == 1)
            {
                flaga = 1;
            }
            else if (metroComboBox2.SelectedIndex == 2)
            {
                flaga = 2;
            }
            else if (metroComboBox2.SelectedIndex == 3)
            {
                flaga = 3;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                udalena = true;
            }
            else { udalena = false; }
        }

        private void сделатьРезервнуюКопиюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string savename;

            string dirinfway = "..\\..\\..\\backup";
            DirectoryInfo dirinf = new DirectoryInfo(dirinfway);
            string papka = dirinf.GetFiles().Length.ToString();

            string[] s = Context.bd.Split('\\');

            if (s.Count() == 0)
            {
                savename = Context.bd;
            }
            else
            {
                savename = s[s.Count() - 1];
            }

            if (papka == "10")
            {
                var fileinf = dirinf.GetFiles().OrderBy(x => x.LastWriteTime);
                fileinf.First().Delete();

                string filename = DateTime.Now.ToString();
                filename = dirinfway + "\\" + filename.Replace(":","_") + ".db";

                File.Copy(savename, filename);
       
                MessageBox.Show("Резервная копия успешно сохранена!");
            }
            else
            {
                string filename = DateTime.Now.ToString();
                filename = dirinfway + "\\" +  filename.Replace(":", "_") + ".db";

                File.Copy(savename, filename);

                MessageBox.Show("Резервная копия успешно сохранена!");
            }

        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string copyname;
            string[] s = Context.bd.Split('\\');

            if (s.Count() < 2)
            {
                copyname = Context.bd + ".db";
            }
            else
            {
                copyname = s[s.Count() - 1];
            }

            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = saveFileDialog1.FileName + ".db";

            string basecatalog = AppDomain.CurrentDomain.BaseDirectory;

            File.Copy(copyname, filename);

            MessageBox.Show("База данных успешно сохранена!");

            Context.chbd(filename);
        }
    }
}
