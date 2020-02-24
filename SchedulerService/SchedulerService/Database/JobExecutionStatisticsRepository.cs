using SchedulerService.Domain;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SchedulerService.Database
{
    public class JobExecutionStatisticsRepository
    {
        public void Insert(JobExecutionStatistics stats)
        {
            string query = "INSERT INTO dbo.JobExecutionStatistics (Name, StartTime, EndTime, JobRunTime, ScheduledInterval) " +
                "VALUES(@name, @startTime, @endTime, @runTime, @scheduledInterval)";
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings["SchedulerMonitoring.DB"]))
            using (SqlCommand cmd = new SqlCommand(query, cn))
            {
                cmd.Parameters.Add("@name", SqlDbType.VarChar, 50).Value = stats.Name;
                cmd.Parameters.Add("@startTime", SqlDbType.DateTime).Value = stats.StartTime;
                cmd.Parameters.Add("@endTime", SqlDbType.DateTime).Value = stats.EndTime;
                cmd.Parameters.Add("@runTime", SqlDbType.Int).Value = stats.RunTime;
                cmd.Parameters.Add("@scheduledInterval", SqlDbType.Int).Value = stats.ScheduledInterval;

                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
        }
    }
}
