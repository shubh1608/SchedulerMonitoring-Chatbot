## happy path
* greet
  - utter_greet

## say goodbye
* goodbye
  - utter_goodbye
  
## interactive_story_1
* greet
    - utter_greet

## interactive_story_1
* greet
    - utter_greet
* jobstatus
    - action_jobstatus
* jobdetails{"jobname": "job-a"}
    - slot{"jobname": "job-a"}
    - action_validate_jobname
    - slot{"jobname": "job-a"}
    - action_jobdetails
* goodbye
    - utter_goodbye

## interactive_story_1
* greet
    - utter_greet
* jobstatus
    - action_jobstatus
* jobdetails{"jobname": "job-a"}
    - slot{"jobname": "job-a"}
    - action_validate_jobname
    - slot{"jobname": "job-a"}
    - action_jobdetails
* serverstatus
    - action_fetch_serverdetails
* servicerestart
    - action_restart_service
* goodbye
    - utter_goodbye

## interactive_story_1
* greet
    - utter_greet
* jobstatus
    - action_jobstatus
* jobdetails{"jobname": "job-d"}
    - slot{"jobname": "job-d"}
    - action_validate_jobname
    - slot{"jobname": "job-d"}
    - action_jobdetails
* serverstatus
    - action_fetch_serverdetails
* servicerestart
    - action_restart_service
* goodbye
    - utter_goodbye

## interactive_story_1
* greet
    - utter_greet
* jobstatus
    - action_jobstatus
* jobdetails{"jobname": "job-c"}
    - slot{"jobname": "job-c"}
    - action_validate_jobname
    - slot{"jobname": "job-c"}
    - action_jobdetails
* serverstatus
    - action_fetch_serverdetails
* servicerestart
    - action_restart_service
* goodbye
    - utter_goodbye
