using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[DisallowConcurrentExecution]
public class JobWorker : IInterruptableJob
{
    private bool _stop;
    public static IJobWorke jobHolder;

    public void Interrupt()
    {
        _stop = true;
    }

    void IJob.Execute(IJobExecutionContext context)
    {
        if (!_stop)
        {
            jobHolder.circleWork();
        }
    }
}

public interface IJobWorke
{
    void circleWork();
}
