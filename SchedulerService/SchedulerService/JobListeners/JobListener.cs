using Quartz;
using SchedulerService.Database;
using SchedulerService.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SchedulerService.JobListeners
{
    public class JobListener : IJobListener
    {
        public string Name => "JobListener";
        private JobExecutionStatisticsRepository repository;

        public JobListener()
        {
            repository = new JobExecutionStatisticsRepository();
        }

        public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.CompletedTask;
        }

        public Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            return Task.CompletedTask;
        }

        public Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = default(CancellationToken))
        {
            var stats = new JobExecutionStatistics {
                Name = context.JobDetail.Key.Name,
                StartTime = context.Trigger.StartTimeUtc.DateTime,
                EndTime = context.Trigger.EndTimeUtc.Value.DateTime,
                RunTime = context.JobRunTime.Milliseconds,
                ScheduledInterval = (context.NextFireTimeUtc.Value.DateTime.Millisecond - context.PreviousFireTimeUtc.Value.DateTime.Millisecond)/60
            };
            repository.Insert(stats);
            return Task.CompletedTask;
        }
    }
}
