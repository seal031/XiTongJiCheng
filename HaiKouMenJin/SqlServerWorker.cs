using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Quartz;
using Quartz.Impl;

namespace HaiKouMenJin
{
    public class SqlServerWorker:IForm
    {
        string connStr;
        SqlConnection conn;

        IScheduler scheduler;
        IJobDetail job;
        JobKey jobKey;

        public SqlServerWorker()
        {
            try
            {
                connStr = ConfigWorker.GetConfigValue("connectString");
                conn = new SqlConnection(connStr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 开始定时轮询
        /// </summary>
        public void startWork()
        {

        }
        /// <summary>
        /// 停止定时轮询
        /// </summary>
        public void stopWork()
        {

        }
        ///定时任务
        private void initJob()
        {
            try
            {
                if (scheduler == null && job == null)
                {
                    scheduler = StdSchedulerFactory.GetDefaultScheduler();
                    JobWorker.form = this;
                    job = JobBuilder.Create<JobWorker>().WithIdentity("connectJob", "jobs").Build();
                    ITrigger trigger = TriggerBuilder.Create().WithIdentity("connectTrigger", "triggers").StartAt(DateTimeOffset.Now.AddSeconds(1))
                        .WithSimpleSchedule(x => x.WithIntervalInSeconds(5).RepeatForever()).Build();
                    scheduler.ScheduleJob(job, trigger);//把作业，触发器加入调度器。  
                    jobKey = job.Key;
                }
                scheduler.Start();
            }
            catch (Exception ex)
            {
                FileWorker.LogHelper.WriteLog("定时任务异常：" + ex.Message);
            }
        }

        public async void cyclicWork()
        {
            await getDevice();
        }

        private async Task getDevice()
        {
            if (conn != null)
            {
                if (conn.State == ConnectionState.Open)
                {
                    //select per_ser,hotstamp,Lastname,Deptname from vwPersonal
                    SqlCommand cmd = new SqlCommand("select DoorID,dev_addr,DoorName from vwCemDoor", conn);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    var result = await cmd.ExecuteReaderAsync();
                    
                }
            }
        }

        private void getPerson()
        {

        }
    }
}
