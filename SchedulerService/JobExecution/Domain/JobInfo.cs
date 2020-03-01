using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobExecution.Domain
{
    public class JobInfo
    {
        public string Name { get; set; }

        public JobStatus Status { get; set; }

        public int ScheduledInterval { get; set; }
    }

    public enum JobStatus
    {
        GOOD,
        WARNING,
        ATTENTION
    }
}
