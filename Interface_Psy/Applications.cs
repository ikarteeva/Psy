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
    public class Applications
    {
        public int ID { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string PsyComment { get; set; }
        public decimal ? Cost{ get; set; }
        public int StateOfHealth { get; set; }
        public string status { get; set; }
        public int idclient { get; set; }
        public string ReasonOfCancel { get; set; }
        public string DateRemove { get; set; }
        public void Edit()
        {
            using (Context context = new Context())
            {
                context.Entry(this).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }

    public static class ApplicationsMethods
    {

        public static List<Applications> Finding(Applications application, int ? idclient, string reason, string method, DateTime? date1, DateTime? date2, String sort, String asсdesс, bool udalen, int page, int count, ref int countrecord)
        {

            List<Applications> applicationList = new List<Applications>();

            using (Context db = new Context())
            {
                var applications = from a in db.Application
                                   join c in db.Client on a.idclient equals c.ID
                                   join ai in db.mia on a.ID equals ai.idapplication into mj
                                   from submia in mj.DefaultIfEmpty()
                                   join i in db.info on submia.idmethod equals i.ID into ij
                                   from subinfo in ij.DefaultIfEmpty()

                              select new
                              {
                                  ID = a.ID,
                                  IDclient = c.ID,
                                  DateRemoveA = a.DateRemove,
                                  Reason = c.ReasonType,
                                  DateA = a.Date,
                                  Status = a.status,
                                  Method = subinfo.Name,
                                  Health = a.StateOfHealth,
                                  DateRemoveM = submia.DateRemove,
                                  Cost = a.Cost,
                                  Time = a.Time
                              };

                if (udalen)
                {
                    applications = applications.Where(x => x.DateRemoveA == null);
                }

                if(idclient != 0)
                {
                    applications = applications.Where(x => x.IDclient == idclient);
                }

                if (application.StateOfHealth != 0)
                {
                    applications = applications.Where(x => x.Health == application.StateOfHealth);
                }

                if (reason != null)
                {
                    applications = applications.Where(x => x.Reason == reason);
                }

                if (application.ID != 0)
                {
                    applications = applications.Where(x => x.ID == application.ID);
                }

                if (method != null)
                {
                    applications = applications.Where(x => x.Method == method && x.DateRemoveM == null);
                }

                if (application.Cost != null)
                {
                    applications = applications.Where(x => x.Cost == application.Cost);
                }

                if (date1 != null && date2 != null)
                {

                    List<int> idlist = new List<int>();

                    date1 = Convert.ToDateTime(Convert.ToDateTime(date1).ToShortDateString()).AddDays(-1);
                    date2 = Convert.ToDateTime(Convert.ToDateTime(date2).ToShortDateString()).AddDays(1);


                    foreach (var c in applications)
                    {

                        if (Convert.ToDateTime(c.DateA) > date1 && Convert.ToDateTime(c.DateA) < date2)
                        {
                            idlist.Add(c.ID);
                        }

                    }

                    applications = applications.Where(x => idlist.Contains(x.ID));
                }


                var query2 = applications.GroupBy(s => new { s.ID, s.IDclient, s.DateA, s.Time, s.Status, s.Cost, s.DateRemoveA}, (key, group) => new
                {
                    ID = key.ID,
                    IDclient = key.IDclient,
                    Date = key.DateA,
                    DateRemove = key.DateRemoveA,
                    Time = key.Time,
                    Status = key.Status,
                    Cost = key.Cost
                });

                if (sort != null) // Сортировка, если нужно
                {
                    query2 = Utilit.OrderByDynamic(query2, sort, asсdesс);
                }

                countrecord = query2.GroupBy(u => u.ID).Count();

                query2 = query2.Skip((page - 1) * count).Take(count); // Формирование страниц и кол-во записей на странице


                foreach (var p in query2)
                {
                    applicationList.Add(new Applications { ID = p.ID, idclient = p.IDclient, Cost = p.Cost, Date = p.Date, DateRemove = p.DateRemove, Time = p.Time, status = p.Status});
                }

                return applicationList;

            }
        }

    }
}
