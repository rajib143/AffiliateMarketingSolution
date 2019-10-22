using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfiliateAPIConsumeJob.Utility
{
  public sealed class DatabaseConnection
    {
        private static volatile SqlConnection instance;`
        private static volatile LootLoOnlineDatabaseEntities intityinstance;
        private static object syncRoot = new object();
        private static string connectionString = ConfigurationManager.AppSettings["connectionString"];

        private DBConnection() { }

        public static SqlConnection Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null || instance.State == ConnectionState.Closed)
                        {
                            instance = new SqlConnection(connectionString);
                            instance.Open();
                        }
                    }
                }

                return instance;
            }
        }

        public static LootLoOnlineDatabaseEntities Entityinstance
        {
            get
            {
                if (intityinstance == null)
                {
                    lock (syncRoot)
                    {
                        if (intityinstance == null)
                        {
                            intityinstance = new LootLoOnlineDatabaseEntities();

                        }
                    }
                }

                return intityinstance;
            }
        }
    }
}
