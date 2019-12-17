using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ChangChunMenJin
{
    public class SqlServerHelper
    {
        private static string sqlconStr;
        static SqlServerHelper()
        {
            SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();
            scsb.DataSource = ConfigWorker.GetConfigValue("sqlserverIp");
            scsb.IntegratedSecurity = true;
            scsb.InitialCatalog = ConfigWorker.GetConfigValue("sqlserverDbName");
            scsb.UserID = ConfigWorker.GetConfigValue("sqlserverUser");
            scsb.Password = ConfigWorker.GetConfigValue("sqlserverPassword");
            sqlconStr = scsb.ConnectionString;
        }
        public static List<DbPerson> getPersons()
        {
            List<DbPerson> persons = new List<ChangChunMenJin.DbPerson>();
            using (SqlConnection con = new SqlConnection(sqlconStr))
            {
                SqlCommand cmd = new SqlCommand("select * from persons", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    
                }
            }
            return persons;
        }
    }

    public class DbPerson
    {

    }
}
