using Microsoft.Isam.Esent.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace wow
{
    static class Configuration
    {

        static string applicationName = "workOUTwork"; // not saved to dictionary
        static string dataBaseNameConfig = "configuration";

        static string dataBaseNameActiveStateLog = "activeStateLog";

        static List<Configurable> configurableObjects = new List<Configurable>();

        public static void appendConfigurableObject(Configurable _configurableObject ) 
        {
            configurableObjects.Add(_configurableObject);
            foreach(ConfigParameter param in _configurableObject.getParameters()) 
            {
                string id = param.getID();
                addConfigEntry(id, param.getValue());
                string valuestring = Configuration.getValueString(id);
                param.setValue(valuestring);
            }
        }

        public static Icon getApplicationIcon() 
        {
            Bitmap bmp = new Bitmap(System.Reflection.Assembly.GetEntryAssembly().GetManifestResourceStream("wow.systray_icon_32.png"));
            IntPtr Hicon = bmp.GetHicon();
            Icon newIcon = Icon.FromHandle(Hicon);
            return newIcon;
        }

        public static void addConfigEntry(string key, string configEntryValue)
        {
            using (PersistentDictionary<string, string> dictionary = new PersistentDictionary<string, string>(getConfigLogPath()))
            {
                if (!dictionary.ContainsKey(key))
                {
                    dictionary.Add(key, configEntryValue);
                }
            }
        }

        public static string getValueString(string key) 
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
        public static void addConfigEntry(string key, int configEntryValue)
        {
            addConfigEntry(key, Convert.ToString(configEntryValue));
        }
        public static void addConfigEntry(string key, float configEntryValue)
        {
            addConfigEntry(key, Convert.ToString(configEntryValue));
        }

        public static Dictionary<string, string> getAllEntrys()
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
        
        public static void changeConfig(string name, string value)
        {
            using (PersistentDictionary<string, string> dictionary = new PersistentDictionary<string, string>(getConfigLogPath()))
            {
                if (dictionary.ContainsKey(name))
                {
                    dictionary.Remove(name);
                    dictionary.Add(name, value);
                }
            }

            foreach(Configurable conf in configurableObjects) 
            {
                foreach (ConfigParameter param in conf.getParameters()) 
                {
                    if(param.getID() == name) 
                    {
                        param.setValue(value);
                    }
                }
            }
        }



        public static string ApplicationName 
        { 
            get { return applicationName; } 
        }
        public static string DataBaseNameActiveStateLog
        {
            get
            {
                return getValueString("dataBaseNameActiveStateLog");
            }
        }

        public static int secondsToIdle
        {
            get
            {
                return Int32.Parse(getValueString("secondsToIdle"));
            }
        }

        public static int secondsToInactive
        {
            get
            {
                return Int32.Parse(getValueString("secondsToInactive"));

            }
        }


        public static int getNoBreakWarningTimeMinutes()
        {
            return Int32.Parse(getValueString("MinutesNoBreakWarning"));
        }

        public static string getActiveStateLogPath() 
        {
            string baseDataPath = getDataBasePath();
            string logpath = Path.Combine( baseDataPath, dataBaseNameActiveStateLog);
            return logpath;      
        }

        public static string getConfigLogPath()
        {
            string baseDataPath = getDataBasePath();
            string logpath = Path.Combine(baseDataPath, dataBaseNameConfig);
            return logpath;
        }

        public static string getDataBasePath() 
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return Path.Combine(appDataPath, applicationName) ;
        }

    }

    public abstract class Configurable
    {
        protected List<ConfigParameter> parameters = new List<ConfigParameter>();
        public string ID = null;

        public Configurable(string _id)
        {
            ID = _id;
            
        }

        protected void setParameters(List<ConfigParameter> _parameters)
        {
            parameters = _parameters;
            Configuration.appendConfigurableObject(this);
        }

        public List<ConfigParameter> getParameters() { return parameters; }

        public void UpdateParameters(List<ConfigParameter> _parameters)
        {
            parameters = _parameters;
        }

    }


    public abstract class ConfigParameter
    {
        protected string id;
        protected string value;

        public string getValue()
        {
            return value;
        }


        public string getID()
        {
            return id;
        }

        public abstract void setValue(string _value);
    }

    public class ConfigStringParameter : ConfigParameter
    {
        public ConfigStringParameter(string _id, string _value)
        {
            base.id = _id;
            base.value = _value;
        }

        public override void setValue(string _value)
        {
            base.value = _value;
        }
    }

    public class ConfigFloatParameter : ConfigParameter
    {
        float value;
        public ConfigFloatParameter(string _id, float _value)
        {
            base.id = _id;
            base.value = _value.ToString();
            this.value = _value;
        }

        public override void setValue(string _value)
        {
            base.value = _value;
            this.value = float.Parse(_value);
        }
    }

    public class ConfigIntParameter : ConfigParameter
    {
        int value;
        public ConfigIntParameter(string _id, int _value)
        {
            base.id = _id;
            base.value = _value.ToString();
            this.value = _value;
        }

        public override void setValue(string _value)
        {
            this.value = Int32.Parse(_value);
        }

        public int getValue() { return this.value; }
    }
}
