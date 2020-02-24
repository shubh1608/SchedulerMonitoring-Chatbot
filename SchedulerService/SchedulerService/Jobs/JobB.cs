using Quartz;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SchedulerService.Jobs
{
    public class JobB : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            //generating random number between 20 to 50
            var randomNumber = new Random().Next(20, 50);
           
            //this job will sleep randomly for seconds between 20 to 50.
            //Why putting it to sleep? just for simulating real scenario when it has something to process.
            Thread.Sleep(randomNumber * 1000);

            return Task.CompletedTask;
        }
    }
}
