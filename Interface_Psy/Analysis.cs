using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_Psy
{
    public class Analysis
    {
        public string qualifier { get; set; }
        public decimal ? sum { get; set; }
        public int count { get; set; }
    }

    public static class AnalysisMethods
    {

        public static List<Analysis> Finding(int flag, DateTime? date1, DateTime? date2, bool udalen)
        {
            List<Analysis> analysisList = new List<Analysis>();
            Dictionary<string, int> countanalys = new Dictionary<string, int>();
            Dictionary<string, decimal ?> sumanalys = new Dictionary<string, decimal ?>();
            int counta = 0;
            decimal? suma = 0;

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
                                       Time = a.Time,
                                       Sex = c.Sex
                                   };

                if (udalen)
                {
                    applications = applications.Where(x => x.DateRemoveA == null);
                }


                if (date1 != null && date2 != null)
                {

                    List<int> idlist = new List<int>();

                    date1 = Convert.ToDateTime(Convert.ToDateTime(date1).ToShortDateString()).AddDays(-1);
                    date2 = Convert.ToDateTime(Convert.ToDateTime(date2).ToShortDateString()).AddDays(1);

                    foreach (var c in applications)
                    {

                        if (Convert.ToDateTime(c.DateA) < date1 && Convert.ToDateTime(c.DateA) > date2)
                        {
                            idlist.Add(c.ID);
                        }

                    }

                    applications = applications.Where(x => idlist.Contains(x.ID));
                }


                List<int> listid = new List<int>();


                if (flag == 3)
                {
                    foreach (var c in applications)
                    {
                        int cc = countanalys.Keys.Count();
                        int cs = sumanalys.Keys.Count();

                        if (c.Method != null)
                        {
                            string f = "0";

                            if (cc == 0)
                            {
                                countanalys.Add(c.Method, 1);

                            }
                            else
                            {
                                foreach (var key in countanalys.Keys)
                                {
                                    if (c.Method == key)
                                    {
                                        f = key;
                                    }
                                }


                                if (f != "0")
                                {
                                    countanalys[f] = countanalys[f] + 1;
                                }
                                else
                                {
                                    countanalys.Add(c.Method, 1);
                                }
                            }


                            f = "0";

                            if (cs == 0)
                            {
                                sumanalys.Add(c.Method, c.Cost);

                            }
                            else
                            {
                                foreach (var key in sumanalys.Keys)
                                {
                                    if (c.Method == key)
                                    {
                                        f = key;
                                    }
                                }

                                if (f != "0")
                                {
                                    sumanalys[f] = sumanalys[f] + c.Cost;
                                }
                                else
                                {
                                    sumanalys.Add(c.Method, c.Cost);
                                }

                            }



                        }
                    }
                }


                var application1 = applications.GroupBy(s => new { s.ID, s.Cost, s.Reason, s.Sex}, (key, group) => new
                {
                    ID = key.ID,
                    Sex = key.Sex,
                    Cost = key.Cost,
                    Reason = key.Reason
                });

                if (flag == 0)
                {
                    foreach (var c in application1)
                    {
                            counta++;
                            suma = suma + c.Cost;
                    }
                }

                else if (flag == 1)
                {
                    foreach (var c in application1)
                    {
                        int cc = countanalys.Keys.Count();
                        int cs = sumanalys.Keys.Count();

                        string f = "0";

                        if (cc == 0)
                        {
                            countanalys.Add(c.Sex, 1);

                        }
                        else
                        {
                            foreach (var key in countanalys.Keys)
                            {
                                if (c.Sex == key)
                                {
                                    f = key;
                                }
                            }

                           if (f != "0")
                            {
                                countanalys[f] = countanalys[f] + 1;
                            }
                            else
                            {
                                countanalys.Add(c.Sex, 1);
                            }
                        }


                        f = "0";

                        if (cs == 0)
                        {
                            sumanalys.Add(c.Sex, c.Cost);
         
                        }
                        else
                        {
                            foreach (var key in sumanalys.Keys)
                            {
                                if (c.Sex == key)
                                {
                                    f = key;
                                }
                            }


                            if (f != "0")
                            {
                                sumanalys[f] = sumanalys[f] + c.Cost;
                            }
                            else
                            {
                                sumanalys.Add(c.Sex, c.Cost);
                            }

                        }


                    }
                }

                else if (flag == 2)
                {
                    foreach (var c in application1)
                    {
                        int cc = countanalys.Keys.Count();
                        int cs = sumanalys.Keys.Count();

                        if (c.Reason != null)
                        {
                            string f = "0";

                            if (cc == 0)
                            {
                                countanalys.Add(c.Reason, 1);
                            }
                            else
                            {
                                foreach (var key in countanalys.Keys)
                                {
                                    if (c.Reason == key)
                                    {
                                        f = key;
                                    }
                                }

                                if (f != "0")
                                {
                                    countanalys[f] = countanalys[f] + 1;
                                }
                                else
                                {
                                    countanalys.Add(c.Reason, 1);
                                }
                            }


                            f = "0";

                            if (cs == 0)
                            {
                                sumanalys.Add(c.Reason, c.Cost);
     
                            }
                            else
                            {
                                foreach (var key in sumanalys.Keys)
                                {
                                    if (c.Reason == key)
                                    {
                                        f = key;
                                    }
                                }


                                if (f != "0")
                                {
                                    sumanalys[f] = sumanalys[f] + c.Cost;
                                }
                                else
                                {
                                    sumanalys.Add(c.Reason, c.Cost);
                                }

                            }

                        }
                    }
                }

                if (flag == 0)
                {
                    Analysis an = new Analysis();
                    an.qualifier = "По всем заявкам";
                    an.count = counta;
                    an.sum = suma;

                    analysisList.Add(an);
                }

                foreach (var keyValue in countanalys)
                {
                    foreach (var key in sumanalys.Keys)
                    {
                        if(key == keyValue.Key)
                        {
                            Analysis an = new Analysis();
                            an.qualifier = keyValue.Key;
                            an.count = keyValue.Value;
                            an.sum = sumanalys[key].Value;

                            analysisList.Add(an);
                        }
                    }
                }

                return analysisList;

            }
        }

    }
}
