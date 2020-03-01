using JobExecution.Database;
using JobExecution.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ProcessingAPI.Service
{
    public class JobInfoService
    {
        private JobExecutionStatisticsRepository _repository { get; set; }
        private int Threshold { get; set; }

        public JobInfoService(IConfiguration configuration)
        {
            _repository = new JobExecutionStatisticsRepository(configuration.GetSection("ConnectionStrings").GetSection("SchedulerMonitoring.DB").Value);
            Threshold = Int16.Parse(configuration.GetSection("Threshold").Value);
        }

        public List<JobInfo> GetJobStatus()
        {
            //load records from repository
            //pass to find status
            //return result from find status to controller
            var jobExecutionStatsList = _repository.Get();
            var jobInfoList = FindStatus(jobExecutionStatsList);
            return jobInfoList;
        }

        private List<JobInfo> FindStatus(List<JobExecutionStatistics> jobExecutionStatistics)
        {
            var jobInfoList = new List<JobInfo>();
            var grp = jobExecutionStatistics.GroupBy(s => s.Name);
            foreach (var g in grp)
            {
                var jobInfo = CalculateJobRunHistory(g.ToList());
                jobInfoList.Add(jobInfo);
            }
            return jobInfoList;
        }

        private JobInfo CalculateJobRunHistory(List<JobExecutionStatistics> jobExecutionStatistics)
        {
            return new JobInfo();
        }
    }
}
