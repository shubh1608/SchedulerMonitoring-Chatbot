CREATE TABLE JobExecutionStatistics (
    [Name] VARCHAR(50) NOT NULL,
	StartTime DATETIME NOT NULL,
	EndTime DATETIME NOT NULL,
	RunTime int NOT NULL,
	ScheduledInterval int NOT NULL
);