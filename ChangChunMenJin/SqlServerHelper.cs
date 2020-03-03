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
        private static string imagePath = string.Empty;
        static SqlServerHelper()
        {
            SqlConnectionStringBuilder scsb = new SqlConnectionStringBuilder();
            scsb.DataSource = ConfigWorker.GetConfigValue("sqlserverIp");
            scsb.IntegratedSecurity = true;
            scsb.InitialCatalog = ConfigWorker.GetConfigValue("sqlserverDbName");
            scsb.UserID = ConfigWorker.GetConfigValue("sqlserverUser");
            scsb.Password = ConfigWorker.GetConfigValue("sqlserverPassword");
            imagePath = ConfigWorker.GetConfigValue("imagePath");
            sqlconStr = scsb.ConnectionString;
        }
        public static List<PersonEntity> getPersons()
        {
            List<PersonEntity> persons = new List<PersonEntity>();
            try
            {
                using (SqlConnection con = new SqlConnection(sqlconStr))
                {
                    SqlCommand cmd = new SqlCommand("select p.[PERSID],[PERSNO],[LASTNAME],[FIRSTNAME],[COMPANYID],c.CARDNO from bsuser.PERSONS p left join bsuser.CARDS c on p.PERSID=c.PERSID", con);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        PersonEntity person = new PersonEntity();
                        person.body.stcArea = dr["FIRSTNAME"].ToString();
                        person.body.stcIdNumber = dr["PERSNO"].ToString();
                        person.body.stcStaffCardId = dr["PERSID"].ToString();
                        person.body.stcStaffCode = dr["COMPANYID"].ToString();
                        person.body.stcStaffIcId = dr["CARDNO"].ToString();
                        person.body.stcStaffName = dr["LASTNAME"].ToString();
                        person.body.stcPhotoPath = imagePath + person.body.stcStaffCardId + ".jpg";
                        persons.Add(person);
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                FileWorker.LogHelper.WriteLog("获取数据库人员信息出现异常："+ex.Message);
            }
            return persons;
        }
    }
    
}
