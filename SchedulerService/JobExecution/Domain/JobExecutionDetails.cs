using System;

namespace JobExecution.Domain
{
    public class JobExecutionDetails
    {
        public string Name { get; set; }

        public JobStatus Status { get; set; }

        public int ScheduledInterval { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int AverageJobRunTime { get; set; }

        public int NumberOfOccurence { get; set; }

        public int NumberOfMisFires { get; set; }
    }
}
