using Quartz;
using Quartz.Impl;
using SchedulerService.Jobs;
using System.Collections.Specialized;

namespace SchedulerService
{
    public class Service
    {
        private readonly IScheduler scheduler;

        public Service()
        {
            NameValueCollection props = new NameValueCollection
            {
                { "quartz.serializer.type", "binary" },
                { "quartz.scheduler.instanceName", "MyScheduler" },
                { "quartz.jobStore.type", "Quartz.Simpl.RAMJobStore, Quartz" },
                { "quartz.threadPool.threadCount", "3" }
            };
            StdSchedulerFactory factory = new StdSchedulerFactory(props);
            scheduler = factory.GetScheduler().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public void Start()
        {
            scheduler.Start().ConfigureAwait(false).GetAwaiter().GetResult();
            ScheduleJobs();
        }

        public void ScheduleJobs()
        {
            IJobDetail jobA = JobBuilder.Create<JobA>()
            .WithIdentity("jobA", "group1")
            .Build();

            ITrigger triggerA = TriggerBuilder.Create()
                .WithIdentity("triggerA", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(60)
                    .RepeatForever())
                .Build();

            IJobDetail jobB = JobBuilder.Create<JobB>()
            .WithIdentity("jobB", "group1")
            .Build();

            ITrigger triggerB = TriggerBuilder.Create()
                .WithIdentity("triggerB", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(300)
                    .RepeatForever())
                .Build();

            // Telling quartz to schedule the job using our trigger
            scheduler.ScheduleJob(jobA, triggerA).ConfigureAwait(false).GetAwaiter().GetResult();
            scheduler.ScheduleJob(jobB, triggerB).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public void Stop()
        {

        }
    }
}
