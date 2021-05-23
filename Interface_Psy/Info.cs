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
    public class Infoes
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Meaning { get; set; }
        public int idguide { get; set; }
        public string Group { get; set; }
        public DateTime? DateRemove { get; set; }

        public void Edit()
        {
            using (Context context = new Context())
            {
                context.Entry(this).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
