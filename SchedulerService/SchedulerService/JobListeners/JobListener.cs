using JobExecution.Database;
using JobExecution.Domain;
using Quartz;
using System;
using System.Configuration;
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
            repository = new JobExecutionStatisticsRepository(ConfigurationManager.ConnectionStrings["SchedulerMonitoring.DB"].ConnectionString);
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
            var trg = context.Trigger as ISimpleTrigger;
            var stats = new JobExecutionStatistics {
                Name = context.JobDetail.Key.Name,
                StartTime = context.Trigger.StartTimeUtc.DateTime,
                EndTime = context.Trigger.StartTimeUtc.DateTime.AddMilliseconds(context.JobRunTime.Milliseconds),
                RunTime = context.JobRunTime.Milliseconds,
                ScheduledInterval = Int32.Parse(trg.RepeatInterval.TotalSeconds.ToString())
            };
            repository.Insert(stats);
            return Task.CompletedTask;
        }
    }
}
