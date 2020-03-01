using JobExecution.Domain;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace JobExecution.Database
{
    public class JobExecutionStatisticsRepository
    {
        private string _connectionString { get; set; }

        public JobExecutionStatisticsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Insert(JobExecutionStatistics stats)
        {
            string query = "INSERT INTO dbo.JobExecutionStatistics (Name, StartTime, EndTime, RunTime, ScheduledInterval) " +
                "VALUES(@name, @startTime, @endTime, @runTime, @scheduledInterval)";
            using (SqlConnection cn = new SqlConnection(_connectionString))
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

        public List<JobExecutionStatistics> Get()
        {
            var jobStatsList = new List<JobExecutionStatistics>();
            string query = "SELECT * FROM dbo.JobExecutionStatistics";
            using (SqlConnection cn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, cn))
            {
                cn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var stats = new JobExecutionStatistics {
                            Name = reader["Name"].ToString(),
                            StartTime = Convert.ToDateTime(reader["StartTime"].ToString()),
                            EndTime = Convert.ToDateTime(reader["EndTime"].ToString()),
                            RunTime = Int32.Parse(reader["RunTime"].ToString()),
                            ScheduledInterval = Int32.Parse(reader["ScheduledInterval"].ToString())
                        };
                        jobStatsList.Add(stats);
                    }
                }
                cn.Close();
            }
            return jobStatsList;
        }

        public List<JobExecutionStatistics> GetByName(string jobName)
        {
            var jobStatsList = new List<JobExecutionStatistics>();
            string query = "select * from [SchedulerMonitoring].[dbo].[JobExecutionStatistics] where Name=@jobName";
            using (SqlConnection cn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, cn))
            {
                cn.Open();
                cmd.Parameters.Add("@jobName", SqlDbType.VarChar, 50).Value = jobName;
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var stats = new JobExecutionStatistics
                        {
                            Name = reader["Name"].ToString(),
                            StartTime = Convert.ToDateTime(reader["StartTime"].ToString()),
                            EndTime = Convert.ToDateTime(reader["EndTime"].ToString()),
                            RunTime = Int32.Parse(reader["RunTime"].ToString()),
                            ScheduledInterval = Int32.Parse(reader["ScheduledInterval"].ToString())
                        };
                        jobStatsList.Add(stats);
                    }
                }
                cn.Close();
            }
            return jobStatsList;
        }
    }
}
