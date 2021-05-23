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
using System.Globalization;

namespace Interface_Psy
{
    public class Clients
    {
 
        public int ID { get; set; }
        public string DateRemove { get; set; }
        public string SecondName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string DayofBirth { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string OtherContactInformation { get; set; }
        public string Sex { get; set; }
        public string Nationality { get; set; }
        public string Child { get; set; }
        public string Education { get; set; }
        public string HowFindOut { get; set; }
        public string ReasonForVisiting { get; set; }
        public string ReasonType { get; set; }
        public string Marriage { get; set; }

        public void Edit()
        {
            using (Context context = new Context())
            {
                context.Entry(this).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

    }
    public static class ClientsMethods
    {

        public static int ClientAge(DateTime dob)
        {
            int age = (DateTime.Now.Year - dob.Year);
            if (DateTime.Now.DayOfYear < dob.DayOfYear)
            { age++; }

            return age;
        }

        public static int CountApplication(int id)
        {
            int count = 0;

            using (Context db = new Context())
            {
                var clients1 = db.Client.OrderBy(p => p.ID).ThenBy(p => p.SecondName).ToList();
                var apply1 = db.Application.ToList();

                foreach (var c in clients1)
                {

                    if (c.ID == id)
                    {

                        foreach (var a in apply1)
                        {
                            if (a.idclient == c.ID)
                            {
                                count++;
                            }

                        }

                    }
                }

                return (count);
            }
        }

        public static string LastDataApp(int id)
        {
            string lastdata = "Нет обращений";

            using (Context db = new Context())
            {
                var clients1 = db.Client.OrderBy(p => p.ID).ThenBy(p => p.SecondName).ToList();
                var apply1 = db.Application.OrderBy(p => p.Date).ToList();

                foreach (var c in clients1)
                {
                    if (c.ID == id)
                    {

                        foreach (var a in apply1)
                        {
                            if (a.idclient == c.ID)
                            {
                                lastdata = a.Date.ToString();
                            }

                        }

                    }
                }

                return (lastdata);
            }
        }

        public static decimal ? CountMoney(int id)
        {
            decimal ? countmoney = 0;

            using (Context db = new Context())
            {
                var clients1 = db.Client.OrderBy(p => p.ID).ThenBy(p => p.SecondName).ToList();
                var apply1 = db.Application.OrderBy(p => p.Date).ToList();

                foreach (var c in clients1)
                {
                    if (c.ID == id)
                    {

                        foreach (var a in apply1)
                        {
                            if (a.idclient == c.ID && a.status == "Проведено")
                            {
                                countmoney = countmoney + a.Cost;
                            }

                        }

                    }
                }

                return (countmoney);
            }
        }

        public static List<Clients> Finding(Clients client, string method, DateTime? date1, DateTime? date2, String sort, String asсdesс, bool udalen, int page, int count, ref int countrecord)
        {

            List<Clients> clientList = new List<Clients>();

            using (Context db = new Context())
            {
                var clients = from c in db.Client
                              join a in db.Application on c.ID equals a.idclient into gj
                              from subapplication in gj.DefaultIfEmpty()
                              join ai in db.mia on subapplication.ID equals ai.idapplication into mj
                              from submia in mj.DefaultIfEmpty()
                              join i in db.info on submia.idmethod equals i.ID into ij
                              from subinfo in ij.DefaultIfEmpty()

                              select new
                              {
                                  ID = c.ID,
                                  Phone = c.Telephone,
                                  Email = c.Email,
                                  DayofBirth = c.DayofBirth,
                                  FIO = c.SecondName + " " + c.FirstName + " " + c.MiddleName,
                                  DateRemoveC = c.DateRemove,
                                  Reason = c.ReasonType,
                                  DateA = subapplication.Date,
                                  Method = subinfo.Name,
                                  DateRemoveM = submia.DateRemove
                              };

                if (udalen)
                {
                    clients = clients.Where(x => x.DateRemoveC == null);
                }

                if (client.Telephone != null)
                {
                    clients = clients.Where(x => x.Phone == client.Telephone);
                }

                if (client.ReasonType != null)
                {
                    clients = clients.Where(x => x.Reason == client.ReasonType);
                }

                if (client.Email != null)
                {
                    clients = clients.Where(x => x.Email.Contains(client.Email));
                }

                if (client.SecondName != null && client.FirstName != null && client.MiddleName != null)
                {
                    string name = client.SecondName + " " + client.FirstName + " " + client.MiddleName;
                    clients = clients.Where(x => x.FIO == name);
                }

                if (client.ID != 0)
                {
                    clients = clients.Where(x => x.ID == client.ID);
                }

                if (method != null)
                {
                    clients = clients.Where(x => x.Method == method && x.DateRemoveM == null);
                }

                if (date1 != null && date2 != null)
                {

                    List<int> idlist = new List<int>();

                    date1 = Convert.ToDateTime(Convert.ToDateTime(date1).ToShortDateString()).AddDays(-1);
                    date2 = Convert.ToDateTime(Convert.ToDateTime(date2).ToShortDateString()).AddDays(1);

                    foreach (var c in clients)
                    {


                        if (Convert.ToDateTime(c.DateA) > date1 && Convert.ToDateTime(c.DateA) < date2)
                        {
                            idlist.Add(c.ID);
                        }

                    }

                    clients = clients.Where(x => idlist.Contains(x.ID));

                 }

                    var query2 = clients.GroupBy(s => new { s.ID, s.FIO, s.Phone, s.Email, s.DateRemoveC, s.DayofBirth}, (key, group) => new
                {
                    ID = key.ID,
                    FIO = key.FIO,
                    Phone = key.Phone,
                    Email = key.Email,
                    DayOfBirth = key.DayofBirth,
                    DateRemove = key.DateRemoveC
                });

                var query = clients.GroupBy(s => new { s.ID, s.DateA }, (key, group) => new
                {
                    ID1 = key.ID,
                    DateA = key.DateA
                });


                if (sort != null) // Сортировка, если нужно
                {
                    query2 = Utilit.OrderByDynamic(query2, sort, asсdesс);
                }

                countrecord = query2.GroupBy(u => u.ID).Count();

                query2 = query2.Skip((page - 1) * count).Take(count); // Формирование страниц и кол-во записей на странице

                foreach (var p in query2)
                {
                    String[] words = p.FIO.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    clientList.Add(new Clients { SecondName = words[0], FirstName = words[1], MiddleName = words[2], ID = p.ID, Telephone = p.Phone, Email = p.Email, DateRemove = Convert.ToString(p.DateRemove), DayofBirth = p.DayOfBirth });

                }

                    return clientList;
            }
        }

    }
}

