using System;

namespace JobExecution.Domain
{
    public class JobExecutionStatistics
    {
        public string Name { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int RunTime { get; set; }

        public int ScheduledInterval { get; set; }
    }
}
