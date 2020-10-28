using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShaoGuanXiaoFangBaoJing
{
    [DisallowConcurrentExecution]
    public class JobWorker : IInterruptableJob
    {
        private bool _stop;
        public static Form1 form;

        public void Interrupt()
        {
            _stop = true;
        }

        void IJob.Execute(IJobExecutionContext context)
        {
            if (!_stop)
            {
                form.getData();
            }
        }
    }
}
