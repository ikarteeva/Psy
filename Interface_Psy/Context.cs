using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.Entity;
using System.IO;

namespace Interface_Psy
{
    public class Context : DbContext
    {
        public static string bd = "Data Source=..\\..\\..\\Psy.db";

        public Context() : base(new SQLiteConnection() { ConnectionString = bd }, true)
        {
        }

        public static void chbd(string namebd) 
        {
            bd = "Data Source ="+ namebd;

        }

        //Отражение таблиц базы данных на свойства с типом DbSet
        public DbSet<Applications> Application { get; set; }

        public DbSet<Clients> Client { get; set; }

        public DbSet<MethodInApplications> mia { get; set; }

        public DbSet<Guides> guide { get; set; }

        public DbSet<Infoes> info { get; set; }

    }
}
