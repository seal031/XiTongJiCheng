using Quartz;
using HwMenJin;

public class JobWorker : IInterruptableJob
{
    private bool _stop;

    public void Interrupt()
    {
        _stop = true;
    }

    void IJob.Execute(IJobExecutionContext context)
    {
        if (!_stop)
        {
            KafkaWorker.startGetMessage();
        }
    }
}
