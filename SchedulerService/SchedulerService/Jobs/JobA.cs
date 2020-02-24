using Quartz;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SchedulerService.Jobs
{
    public class JobA : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            //generating random number between 2 to 6
            var randomNumber = new Random().Next(2, 6);
            
            //this job will sleep randomly for 2 to 6 seconds.
            //Why putting it to sleep? just for simulating real scenario when it has something to process.
            Thread.Sleep(randomNumber*1000);

            return Task.CompletedTask;
        }
    }
}
