using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Isam.Esent.Collections.Generic;

namespace wow
{
    class ActiveStateLog : IObserver
    {
        private ActivityWatcher activityWatcher;
        private string pathToAppDataFolder;

        [Serializable]
        public struct logEntry_t
        {
            public ActivityWatcher.activityState_t newState;
            public TimeSpan timeInState;
        }

        public ActiveStateLog(ActivityWatcher _activityWatcher)
        {
            activityWatcher = _activityWatcher;
            activityWatcher.Attach(this);
            pathToAppDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        }

        public void Update(ISubject subject)
        {
            logEntry_t logEntry = new logEntry_t();
            logEntry.newState = activityWatcher.ActivityState;
            logEntry.timeInState = activityWatcher.TimeInState;
            this.addLogEntry(logEntry);
        }

        private void addLogEntry(logEntry_t logEntry)
        {
            string folderPath = Path.Combine(pathToAppDataFolder, "testapp", "activityLog");
            using (PersistentDictionary<DateTime, logEntry_t> dictionary = new PersistentDictionary<DateTime, logEntry_t>(folderPath))
            {
                dictionary.Add(DateTime.Now, logEntry);
            }


            

        }



    }
}
