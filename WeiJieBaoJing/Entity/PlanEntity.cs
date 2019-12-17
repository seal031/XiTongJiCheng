using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeiJieBaoJing.Entity
{
    public class PlanEntity
    {
        public static List<PlanEntity> plans = new List<PlanEntity>();

        public string deviceName { get; set; }
        public string deviceState { get; set; }//标识撤防布防 01布防、02撤防
        public List<PlanTime> Mon { get; set; }
        public List<PlanTime> Tue { get; set; }
        public List<PlanTime> Wed { get; set; }
        public List<PlanTime> Thu { get; set; }
        public List<PlanTime> Fri { get; set; }
        public List<PlanTime> Sat { get; set; }
        public List<PlanTime> Sun { get; set; }
        public string startTM { get; set; }
        public string endTM { get; set; }

        public static void insertPlan(PlanMessage command)
        {
            foreach (PlanMessage.Equ device in command.equs)
            {
                PlanEntity plan = new Entity.PlanEntity();
                plan.deviceName = device.equName;
                plan.deviceState = device.equStatus;
                plan.startTM = command.plan.startTM;
                plan.endTM = command.plan.endTM;
                plan.Mon = getPlanTimeFromString(command.plan.Mon);
                plan.Tue = getPlanTimeFromString(command.plan.Tue);
                plan.Wed = getPlanTimeFromString(command.plan.Wed);
                plan.Thu = getPlanTimeFromString(command.plan.Thu);
                plan.Fri = getPlanTimeFromString(command.plan.Fri);
                plan.Sat = getPlanTimeFromString(command.plan.Sat);
                plan.Sun = getPlanTimeFromString(command.plan.Sun);
                PlanEntity currentPlan = plans.FirstOrDefault(p => p.deviceName == device.equName);
                if (plan.endTM != "2000-01-01")//约定endTM为2000-01-01时，为删除设备计划
                {
                    if (currentPlan != null)//如果已有针对该设备的计划，则更新，否则新增
                    {
                        currentPlan.deviceState = plan.deviceState;
                        currentPlan.startTM = plan.startTM;
                        currentPlan.endTM = plan.endTM;
                        currentPlan.Mon = plan.Mon;
                        currentPlan.Tue = plan.Tue;
                        currentPlan.Wed = plan.Wed;
                        currentPlan.Thu = plan.Thu;
                        currentPlan.Fri = plan.Fri;
                        currentPlan.Sat = plan.Sat;
                        currentPlan.Sun = plan.Sun;
                        FileWorker.PrintLog("修改设备计划" + device.equName);
                        FileWorker.WriteLog("修改设备计划" + device.equName);
                    }
                    else
                    {
                        plans.Add(plan);
                        FileWorker.PrintLog("新增设备计划" + device.equName);
                        FileWorker.WriteLog("新增设备计划" + device.equName);
                    }
                }
                else
                {
                    PlanEntity deletePlan = plans.FirstOrDefault(p => p.deviceName == device.equName);
                    if (deletePlan != null)
                    {
                        plans.Remove(deletePlan);
                        FileWorker.PrintLog("删除设备计划"+ device.equName);
                        FileWorker.WriteLog("删除设备计划" + device.equName);
                    }
                }
            }
        }

        public static bool IsInPlan(string deviceName)
        {
            try
            {
                PlanEntity plan = plans.FirstOrDefault(p => p.deviceName == deviceName);
                if (plan != null)
                {
                    if (plan.deviceState.ToString() == "02")//状态为02时为撤防状态，不需要判断日期时间，直接返回false
                    {
                        return false;
                    }
                    switch (DateTime.Now.DayOfWeek)
                    {
                        case DayOfWeek.Sunday:
                            return inPlan(plan.startTM, plan.endTM, plan.Sun);
                        case DayOfWeek.Monday:
                            return inPlan(plan.startTM, plan.endTM, plan.Mon);
                        case DayOfWeek.Tuesday:
                            return inPlan(plan.startTM, plan.endTM, plan.Tue);
                        case DayOfWeek.Wednesday:
                            return inPlan(plan.startTM, plan.endTM, plan.Wed);
                        case DayOfWeek.Thursday:
                            return inPlan(plan.startTM, plan.endTM, plan.Thu);
                        case DayOfWeek.Friday:
                            return inPlan(plan.startTM, plan.endTM, plan.Fri);
                        case DayOfWeek.Saturday:
                            return inPlan(plan.startTM, plan.endTM, plan.Sat);
                        default:
                            return true;
                    }
                }
                else//没做计划的就正常布防
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                FileWorker.PrintLog(ex.Message);
                FileWorker.WriteLog(ex.Message);
                return false;
            }
        }

        private static bool inPlan(string startTM,string endTM,List<PlanTime> plans)
        {
            DateTime startDate = DateTime.MinValue;
            DateTime endDate = DateTime.MaxValue;
            if(string.IsNullOrEmpty(startTM))
            {
                startDate = DateTime.Parse(startTM);
            }
            if (string.IsNullOrEmpty(endTM))
            {
                endDate = DateTime.Parse(endTM);
            }
            DateTime datetimeNowZero = DateTime.Parse(DateTime.Now.ToLongDateString());
            if (datetimeNowZero < startDate || datetimeNowZero > endDate)//先判断日期
            {
                return false;
            }
            foreach (PlanTime plan in plans)
            {
                DateTime startTime = DateTime.Parse(plan.startTime);
                DateTime endTime = DateTime.Parse(plan.endTime);
                if (DateTime.Now >= startTime && DateTime.Now <= endTime)
                {
                    return true;
                }
            }
            return false;
        }

        private static List<PlanTime> getPlanTimeFromString(List<string> listTime)
        {
            List<PlanTime> list = new List<Entity.PlanEntity.PlanTime>();
            foreach (string time in listTime)
            {
                PlanTime pt = new Entity.PlanEntity.PlanTime();
                string[] array = time.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
                if (array.Length != 2)
                {
                    //pt.startTime = string.Empty;
                    //pt.endTime = string.Empty;
                    pt.startTime = "00:00:00";
                    pt.endTime = "24:00:00";
                }
                else
                {
                    pt.startTime = array[0];
                    pt.endTime = array[1];
                }
                list.Add(pt);
            }
            return list;
        }

        public class PlanTime
        {
            public string startTime { get; set; }
            public string endTime { get; set; }
        }
    }
}