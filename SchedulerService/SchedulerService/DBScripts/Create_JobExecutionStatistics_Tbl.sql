CREATE TABLE JobExecutionStatistics (
    JobName VARCHAR(50) NOT NULL,
	StartTime DATETIME NOT NULL,
	EndTime DATETIME NOT NULL,
	JobRunTime int NOT NULL,
	ScheduledInterval int NOT NULL
);