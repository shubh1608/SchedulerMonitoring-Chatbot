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
