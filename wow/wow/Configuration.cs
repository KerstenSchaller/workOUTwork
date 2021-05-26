using Microsoft.Isam.Esent.Collections.Generic;
using System;
using System.Collections.Generic;
using System.IO;

namespace wow
{
    class Configuration
    {

        static string applicationName = "workOUTwork"; // not saved to dictionary
        static string dataBaseNameConfig = "configuration";

        static string dataBaseNameActiveStateLog = "activeStateLog";

        public Configuration()
        {
            initializeConfigDatabase();
        }

        public void initializeConfigDatabase()
        {
            int count = 0;
            using (PersistentDictionary<string, string> dictionary = new PersistentDictionary<string, string>(getConfigLogPath()))
            {
                count = dictionary.Count;
            }
            if (count == 0)
            {
                addConfigEntry("dataBaseNameActiveStateLog", "activeStateLog");
                addConfigEntry("secondsToIdle", 3);
                addConfigEntry("secondsToInactive", 5);
            }
        }

        private void addConfigEntry(string key, string configEntryValue)
        {
            using (PersistentDictionary<string, string> dictionary = new PersistentDictionary<string, string>(getConfigLogPath()))
            {
                if (!dictionary.ContainsKey(key))
                {
                    dictionary.Add(key, configEntryValue);
                }
            }
        }

        public string getValue(string key) 
        {
            using (PersistentDictionary<string, string> dictionary = new PersistentDictionary<string, string>(getConfigLogPath()))
            {
                if (dictionary.ContainsKey(key))
                {
                    return dictionary[key];
                }
                else 
                {
                    return "";
                }
            }
        }
        private void addConfigEntry(string key, int configEntryValue)
        {
            addConfigEntry(key, Convert.ToString(configEntryValue));
        }
        private void addConfigEntry(string key, float configEntryValue)
        {
            addConfigEntry(key, Convert.ToString(configEntryValue));
        }

        public Dictionary<string, string> getAllEntrys()
        {
            using (PersistentDictionary<string, string> dictionary = new PersistentDictionary<string, string>(getConfigLogPath()))
            {
                Dictionary<string, string> returnDict = new Dictionary<string, string>();
                foreach (KeyValuePair<string, string> entry in dictionary)
                {
                    returnDict.Add(entry.Key, entry.Value);
                }
                return returnDict;
            }
        }
        
        public void changeConfig(string name, string value)
        {
            using (PersistentDictionary<string, string> dictionary = new PersistentDictionary<string, string>(getConfigLogPath()))
            {
                if (dictionary.ContainsKey(name))
                {
                    dictionary.Remove(name);
                    dictionary.Add(name, value);
                }
            }
        }

        public string ApplicationName 
        { 
            get { return applicationName; } 
        }
        public string DataBaseNameActiveStateLog 
        { 
            get
            {
                using (PersistentDictionary<string, string> dictionary = new PersistentDictionary<string, string>(getConfigLogPath()))
                {                    
                    return dictionary["dataBaseNameActiveStateLog"];
                }
            } 
        }

        public int secondsToIdle
        {
            get
            {
                using (PersistentDictionary<string, string> dictionary = new PersistentDictionary<string, string>(getConfigLogPath()))
                {
                    return Int32.Parse(dictionary["secondsToIdle"]);
                }
            }
        }

        public int secondsToInactive
        {
            get
            {
                using (PersistentDictionary<string, string> dictionary = new PersistentDictionary<string, string>(getConfigLogPath()))
                {
                    return Int32.Parse(dictionary["secondsToInactive"]);
                }
            }
        }

        public string getActiveStateLogPath() 
        {
            string baseDataPath = getDataBasePath();
            string logpath = Path.Combine( baseDataPath, dataBaseNameActiveStateLog);
            return logpath;      
        }

        public string getConfigLogPath()
        {
            string baseDataPath = getDataBasePath();
            string logpath = Path.Combine(baseDataPath, dataBaseNameConfig);
            return logpath;
        }

        public string getDataBasePath() 
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return Path.Combine(appDataPath, applicationName) ;
        }


    }
}
