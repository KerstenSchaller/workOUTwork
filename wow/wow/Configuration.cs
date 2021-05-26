using Microsoft.Isam.Esent.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Drawing;
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
                addConfigEntry("secondsToIdle", 3 * 60 * 1000);
                addConfigEntry("secondsToInactive", 5 * 60 * 1000);
                addConfigEntry("MinutesNoBreakWarning", 1);
            }
        }

        public Icon getApplicationIcon() 
        {
            Bitmap bmp = new Bitmap(System.Reflection.Assembly.GetEntryAssembly().GetManifestResourceStream("wow.systray_icon_32.png"));
            IntPtr Hicon = bmp.GetHicon();
            Icon newIcon = Icon.FromHandle(Hicon);
            return newIcon;
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

        public string getValueString(string key) 
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
                return getValueString("dataBaseNameActiveStateLog");
            }
        }

        public int secondsToIdle
        {
            get
            {
                return Int32.Parse(getValueString("secondsToIdle"));
            }
        }

        public int secondsToInactive
        {
            get
            {
                return Int32.Parse(getValueString("secondsToInactive"));

            }
        }


        public int getNoBreakWarningTimeMinutes()
        {
            return Int32.Parse(getValueString("MinutesNoBreakWarning"));
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
