using JobExecution.Database;
using JobExecution.Domain;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProcessingAPI.Service
{
    public class JobInfoService
    {
        private JobExecutionStatisticsRepository _repository { get; set; }
        private int Threshold { get; set; }

        public JobInfoService(IConfiguration configuration)
        {
            _repository = new JobExecutionStatisticsRepository(configuration.GetSection("ConnectionStrings").GetSection("SchedulerMonitoring.DB").Value);
            Threshold = short.Parse(configuration.GetSection("Threshold").Value);
        }

        public List<JobInfo> GetJobStatus()
        {
            var jobExecutionStatsList = _repository.Get();
            var jobInfoList = FindStatus(jobExecutionStatsList);
            return jobInfoList;
        }

        public JobExecutionDetails GetJobExecutionDetails(string jobName)
        {
            var jobExecutionList = _repository.GetByName(jobName);
            var jobInfo= CalculateJobRunHistory(jobExecutionList);
            var windowFrameInterval = Threshold * jobInfo.ScheduledInterval;
            int avgRunTime = 0, numOfOccurence = 0, numberOfMisFires = 0;
            CalculateMetrics(windowFrameInterval, jobExecutionList, ref avgRunTime, ref numOfOccurence, ref numberOfMisFires);

            var jobDetails = new JobExecutionDetails {
                Name = jobInfo.Name,
                Status = jobInfo.Status,
                ScheduledInterval = jobInfo.ScheduledInterval,
                StartTime = DateTime.Now.AddSeconds(-windowFrameInterval),
                EndTime = DateTime.Now,
                NumberOfOccurence = numOfOccurence,
                AverageJobRunTime = avgRunTime,
                NumberOfMisFires = numberOfMisFires
            };

            return jobDetails;
        }

        private void CalculateMetrics(int secondsBefore, List<JobExecutionStatistics> jobExecutionStatistics, ref int avgRunTime, ref int numOfOccurence, ref int numOfMisFires)
        {
            var windowRecords = jobExecutionStatistics.Where(j => j.StartTime > DateTime.UtcNow.AddSeconds(-secondsBefore));
            avgRunTime = Convert.ToInt16(windowRecords.Average(j => j.RunTime));
            numOfOccurence = FindRunsInTimePeriod(-secondsBefore, jobExecutionStatistics);
            numOfMisFires = Math.Abs(Threshold - numOfOccurence);
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
            var jobInfo = new JobInfo
            {
                Name = jobExecutionStatistics.First().Name,
                ScheduledInterval = jobExecutionStatistics.First().ScheduledInterval
            };

            var windowInterval = Threshold * jobExecutionStatistics.First().ScheduledInterval;
            if (FindRunsInTimePeriod(windowInterval, jobExecutionStatistics) <= 0)
            {
                jobInfo.Status = JobStatus.ATTENTION;
            }
            else if (FindRunsInTimePeriod(windowInterval, jobExecutionStatistics) < (Threshold/2))
            {
                jobInfo.Status = JobStatus.WARNING;
            }
            else {
                jobInfo.Status = JobStatus.GOOD;
            }
            return jobInfo;
        }

        private int FindRunsInTimePeriod(int secondsBefore, List<JobExecutionStatistics> jobExecutionStatistics)
        {
            var cnt = jobExecutionStatistics.Where(j => j.StartTime > DateTime.UtcNow.AddSeconds(-secondsBefore)).Count();
            return cnt;
        }
    }
}
