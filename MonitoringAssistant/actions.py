from typing import Any, Text, Dict, List
from rasa_sdk import Action, Tracker
from rasa_sdk.executor import CollectingDispatcher
from rasa_sdk.events import SlotSet
import json
import requests
import pandas as pd

class ActionJobsStatus(Action):

    def name(self):
        return 'action_jobstatus'

    def run(self, dispatcher, tracker,domain):
        dispatcher.utter_message('Wait, let me fetch the results.')
        text_resp = get('http://localhost:58316/api/jobinfo')
        df = pd.read_json(text_resp)
        df['status'] = df['status'].map(convert_status)
        dispatcher.utter_message('Please find job status below.')
        dispatcher.utter_message(df.to_string())

class ActionJobDetails(Action):

    def name(self):
        return 'action_jobdetails'

    def run(self, dispatcher, tracker, domain):
        jobname = tracker.get_slot('jobname')
        text_resp = get('http://localhost:58316/api/jobinfo/{0}'.format(jobname))
        l = []
        l.append(json.loads(text_resp))
        df = pd.read_json(json.dumps(l))
        df.drop(columns = ['startTime', 'endTime'], inplace = True)
        df['status'] = df['status'].map(convert_status)
        dispatcher.utter_message(df.to_string())

class ActionValidateJobName(Action):

    def name(self):
        return 'action_validate_jobname'

    def run(self, dispatcher, tracker, domain):
        # before fetching the job details, call this action for validation
        # it will check if this job exists in scheduler or not
        # if not simply return false with a message that this job does not exist
        # if yes than call above fetch details action method
        #how to do?
        # call web api for validation, it will just return true or false
        # based on that we can decide here to call other actions or not
        jobname = tracker.get_slot('jobname')
        if False:
            dispatcher.utter_message('Job not found in scheduler, could you please check job name.')
        else:
            return [SlotSet('jobname', jobname )]

class ActionFetchServerStatus(Action):

    def name(self):
        return 'action_fetch_serverdetails'

    def run(self, dispatcher, tracker, domain):
        #to-do: actual implementation of scripts
        dispatcher.utter_message('CPU usage: 85%, Disk usage: 95%')

class ActionRestartService(Action):

    def name(self):
        return 'action_restart_service'

    def run(self, dispatcher, tracker, domain):
        #to-do: actual implementation of scripts
        # need to find out how to run scripts in elevated permission.
        dispatcher.utter_message('Service restarted successfully.')

def get(url):
    resp = requests.get(url)
    if resp.status_code != 200:
        return "Something went wrong while fetching data from api."
    else:
        return resp.text

def convert_status(i):
    if i == 0:
        return "GOOD"
    elif i == 1:
        return "WARNING"
    else:
        return "ATTENTION"

# result = get('http://localhost:58316/api/jobinfo/jobA')
# print(result)