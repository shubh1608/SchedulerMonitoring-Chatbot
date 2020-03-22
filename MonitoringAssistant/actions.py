# This files contains your custom actions which can be used to run
# custom Python code.
#
# See this guide on how to implement these action:
# https://rasa.com/docs/rasa/core/actions/#custom-actions/
from typing import Any, Text, Dict, List
from rasa_sdk import Action, Tracker
from rasa_sdk.executor import CollectingDispatcher
from rasa_sdk.events import SlotSet
import json

class ActionJobsStatus(Action):

    def name(self):
        return 'action_jobstatus'

    def run(self, dispatcher, tracker,domain):
        # call web api for fetching all jobs general status
        # process reponse using json.loads() in bot style
        # return the response
        dispatcher.utter_message('All good except one!')

class ActionJobDetails(Action):

    def name(self):
        return 'action_jobdetails'

    def run(self, dispatcher, tracker, domain):
        # take jobname param from tracker
        # call web api with jobname param
        # parse json response
        # convert it in to bot style
        # return the response
        dispatcher.utter_message('job-b seems to be in trouble!')

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