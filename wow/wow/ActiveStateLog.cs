using System;
using System.Collections.Generic;
using Microsoft.Isam.Esent.Collections.Generic;

namespace wow
{
    class ActiveStateLog : SubjectImplementation, IObserver
    {
        private readonly string logpath;

        [Serializable]
        public struct logEntry_t
        {
            public ActivityWatcher.activityState_t newState;
            public TimeSpan timeInState;
        }

        public int Count
        {
            get
            {
                using (PersistentDictionary<DateTime, logEntry_t> dictionary = new PersistentDictionary<DateTime, logEntry_t>(logpath))
                {
                    return dictionary.Count;
                }
            }
        }

        public KeyValuePair<DateTime, logEntry_t> getLastEntry()
        {
            using (PersistentDictionary<DateTime, logEntry_t> dictionary = new PersistentDictionary<DateTime, logEntry_t>(logpath))
            {
                return dictionary.Last();
            }
        }

        public Dictionary<DateTime, logEntry_t> getAllEntrys()
        {
            using (PersistentDictionary<DateTime, logEntry_t> dictionary = new PersistentDictionary<DateTime, logEntry_t>(logpath))
            {
                Dictionary<DateTime, logEntry_t> returnDict = new Dictionary<DateTime, logEntry_t>();
                foreach (KeyValuePair<DateTime, logEntry_t> entry in dictionary)
                {
                    returnDict.Add(entry.Key, entry.Value);
                }
                return returnDict;

            }

        }

        public ActiveStateLog()
        {
            TopicBroker.subscribeTopic("ACTIVITY_STATE_CHANGE_EVENT", this);
            TopicBroker.publishTopic("ACTIVIVE_STATE_LOG_EVENT", this);
            Configuration config = new Configuration();
            logpath = config.getActiveStateLogPath();
        }

        public void Update(ISubject subject)
        {
            if (subject is ActivityWatcher activityWatcher) 
            { 
                //ActivityWatcher activityWatcher = (ActivityWatcher)subject;
                logEntry_t logEntry;
                logEntry.newState = activityWatcher.ActivityState;
                logEntry.timeInState = activityWatcher.TimeInLastState;
                this.addLogEntry(logEntry);
            }
        }

        private void addLogEntry(logEntry_t logEntry)
        {
            using (PersistentDictionary<DateTime, logEntry_t> dictionary = new PersistentDictionary<DateTime, logEntry_t>(logpath))
            {
                dictionary.Add(DateTime.Now, logEntry);
            }
            this.Notify();
        }



    }

}


