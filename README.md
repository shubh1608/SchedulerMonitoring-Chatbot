# Scheduler Monitoring Chatbot

#### Project Status: [Active]

## Project Intro/Objective
I am working on creating a contextual assistant which can help DevOps, Developers and other Stakeholders in monitoring the background process running in schedulers. While working for one of the clients, we encountered an issue where the jobs were stuck for months resulting in pile up of items which eventually needed manual intervention for processing. 

Idea is to build a contextual assistant which is intelligent enough to understand your queries in natural language and respond accordingly.

### Methods Used
* Machine Learning
* Deep Learning
* Natural Language Processing
* Rasa(contextual assistant building framework)
* Rasa-x(UI support for training assistant)
* Rest APIs
* Windows Service

### Technologies
* Python
* C#
* .Net Framework
* Web APIs
* Topshelf
* SQL Server 


## Project Description
   ### Problem Statement
      * We encountered a major production issue while working for one of my clients, where a job which was responsible for processing notifications were stuck for 2-3 months, eventually the job needs to be restarted manually and thousands of notifications were processed with human intervention. 
      * There was no way for us to get notified as we did not have any monitoring capability. We were asked to take care of this problem, we researched and found out that jobs getting stuck is one of the common issues which is the case irrespective of whatever scheduling library you are using, internally it can happen due to failures in acquiring new connections from the thread pool which is not predictable in nature.
      
   ### Solution
      * We were asked to provide an rest based end point which can be called to see the health metrics of all the jobs running in the server. This involves designing of the whole system, which started with defining the statistics which needs to be captured and using which we can infer the jobs status(GOOD, WARNING, ATTENTION).
      * We did provided Rest APIs which can tell us the status of jobs with other detailed statistics also.
      * But i wanted the solution to be more convenient to use and easily approachable.
      * So i decided to build a contextual chatbot/assistant which can understand your natural queries and respond accordingly.
      
   ### Components
      1. Windows Service
         * Actually it's not a window service, but a console application which i am using as a Window Service. Thanks to Topshelf library which sits on top of console application and provides the ease of console app and background running nature of window service. Refer their documentation for more details - https://topshelf.readthedocs.io/en/latest/
         * Using Quartz dot net scheduling library which is running 2 Jobs in background, this is for simulating the actual environment.https://www.quartz-scheduler.net/documentation/index.html
         * Using Quartz job listeners to capture the Job execution statistics, and these statistics are stored in SQL Server database
         
      2. Web API( .Net framework for creating Restful web services)
         * Implemented Rest APIs which can be called to fetch the status and execution details for checking/monitoring the health of background jobs.
         * They won't just fetch the details from database, they will first fetch the data, process it, calculate other metrics(Number of occurence, number of misfires, average running time) using which their status(GOOD, WARNING, ATTENTION) are derived.
         
      3. Contextual Assistant/ Chatbot
         * I am using Rasa which is one of the best open source framework for creating a chatbot, you wont need to setup your own natural language processing pipeline from the scratch. You just have to focus on solving your business problems.Refer more details here - https://rasa.com/docs/getting-started/
         * Chatbot is already out there, you just need to figure out how to use it. But yes it will definitely need more training.
         * Training can be done using Rasa Interactive environment, which is console based UI for training a bot.
         * Chatbot capabilities as of now
            1. It can fetch the status of all the jobs running(GOOD/WARNING/ATTENTION).
            2. It can fetch you particular job execution details(occurences, misfires, average runtime, frequency).
            3. If you think you want to restart your scheduler service, you can tell a bot to do a restart.


## Needs of this project
- Web Backend Developers
- NLP Engineers
- data processing/cleaning
- writeup/reporting


## Getting Started
1. Clone this repo (for help see this [tutorial](https://help.github.com/articles/cloning-a-repository/)).
2. Raw Data is being kept [here](Repo folder containing raw data) within this repo.


## Contact
* Shubham Patel, email: shubhampatel1608@gmail.com, mobile: 8103856241
