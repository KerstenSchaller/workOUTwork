using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Isam.Esent.Collections.Generic;

namespace wow
{

         class ActiveStateLog : SubjectImplementation, IObserver
         {
            private string pathToAppDataFolder;
            private string logpath;

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
                string folderPath = Path.Combine(pathToAppDataFolder, "testapp", "activityLog");
                using (PersistentDictionary<DateTime, logEntry_t> dictionary = new PersistentDictionary<DateTime, logEntry_t>(folderPath))
                {
                    return dictionary.Count;
                }
            }
        }

        public KeyValuePair<DateTime, logEntry_t> getLastEntry()
        {
            string folderPath = Path.Combine(pathToAppDataFolder, "testapp", "activityLog");
            using (PersistentDictionary<DateTime, logEntry_t> dictionary = new PersistentDictionary<DateTime, logEntry_t>(folderPath))
            {
                //if (dictionary.Count > 0)
                { 
                    return dictionary.Last();
                }


            }
        }

        public Dictionary<DateTime, logEntry_t> getAllEntrys() 
        {
            string folderPath = Path.Combine(pathToAppDataFolder, "testapp", "activityLog");
            using (PersistentDictionary<DateTime, logEntry_t> dictionary = new PersistentDictionary<DateTime, logEntry_t>(folderPath)) 
            {
                Dictionary<DateTime, logEntry_t> returnDict = new Dictionary<DateTime, logEntry_t>();
                foreach (KeyValuePair<DateTime, logEntry_t> entry in dictionary) 
                {
                    returnDict.Add(entry.Key,entry.Value);
                }
                return returnDict;

            }

        }

            public ActiveStateLog(string _logpath)
            {
                this.logpath = _logpath;
                pathToAppDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            }

            public void Update(ISubject subject)
            {
                ActivityWatcher activityWatcher = (ActivityWatcher)subject;
                logEntry_t logEntry = new logEntry_t();
                logEntry.newState = activityWatcher.ActivityState;
                logEntry.timeInState = activityWatcher.TimeInLastState;
                this.addLogEntry(logEntry);
            }

            private void addLogEntry(logEntry_t logEntry)
            {
                string folderPath = Path.Combine(pathToAppDataFolder, "testapp", "activityLog");
                using (PersistentDictionary<DateTime, logEntry_t> dictionary = new PersistentDictionary<DateTime, logEntry_t>(folderPath))
                {
                    dictionary.Add(DateTime.Now, logEntry);
                }
                this.Notify();
            }



        }

    }

    
