using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using ObjectsComparer;
using Quartz;
using Quartz.Impl;

namespace WhCameraStateWathcer
{
    public partial class Form1 : Form
    {
        MySqlConnection con;
        MySqlCommand cmd;
        string connStr;

        List<DbEntity> dbEntityList = new List<DbEntity>();
        ObjectsComparer.Comparer<DbEntity> comparer = new ObjectsComparer.Comparer<DbEntity>();
        IEnumerable<Difference> differences;

        IScheduler scheduler;
        IJobDetail job;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (loadLocalConfig())
            {

            }
        }
        private bool loadLocalConfig()
        {
            try
            {
                connStr = ConfigWorker.GetConfigValue("mysqlConStr");
                con = new MySqlConnection(connStr);
                cmd = new MySqlCommand("select * from tb", con);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("读取本地配置及初始化失败：" + ex.Message);
                FileWorker.LogHelper.WriteLog("读取本地配置及初始化失败：" + ex.Message);
                return false;
            }
        }

        private void startJob()
        {
            try
            {
                if (scheduler == null && job == null)
                {
                    scheduler = StdSchedulerFactory.GetDefaultScheduler();
                    JobWorker.form = this;
                    job = JobBuilder.Create<JobWorker>().WithIdentity("fireJob", "jobs").Build();
                    ITrigger trigger = TriggerBuilder.Create().WithIdentity("fireTrigger", "triggers").StartAt(DateTimeOffset.Now.AddSeconds(1))
                        .WithSimpleSchedule(x => x.WithIntervalInSeconds(30).RepeatForever()).Build();
                    scheduler.ScheduleJob(job, trigger);//把作业，触发器加入调度器。  
                }
                scheduler.Start();
            }
            catch (Exception ex)
            {
                FileWorker.LogHelper.WriteLog("定时任务异常：" + ex.Message);
            }
        }

        /////////业务逻辑 ///////

        public void compareData()
        {
            DataSet ds = getData();
            List<DbEntity> currentList = dataToEntity(ds);
            if (dbEntityList.Count == 0)
            {
                dbEntityList = currentList;
            }
            else
            {
                foreach (DbEntity currentEntity in currentList)
                {
                    var oldEntity = dbEntityList.FirstOrDefault(e => e.id == currentEntity.id);
                    if (oldEntity == null)
                    {
                        dbEntityList.Add(currentEntity);
                    }
                    else
                    {
                        var isEqual = comparer.Compare(currentEntity, oldEntity, out differences);
                        if (isEqual == false)
                        {

                        }
                    }
                }
            }
        }

        public DataSet getData()
        {
            DataSet ds = new DataSet();

            return ds;
        }

        private List<DbEntity> dataToEntity(DataSet ds)
        {
            List<DbEntity> list = new List<DbEntity>();

            return list;



        }
    }
}
