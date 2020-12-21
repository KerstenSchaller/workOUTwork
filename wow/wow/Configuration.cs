using System;
using System.IO;

namespace wow
{
    static class Configuration
    {
        static string applicationName = "workOUTwork";
        static string dataBaseNameActiveStateLog = "activeStateLog";
     
        public static string ApplicationName 
        { 
            get { return applicationName; } 
        }
        public static string DataBaseNameActiveStateLog 
        { 
            get { return dataBaseNameActiveStateLog; } 
        }

        public static string getActiveStateLogPath() 
        {
            string pathToAppDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string logpath = Path.Combine(pathToAppDataFolder, applicationName , dataBaseNameActiveStateLog);
            return logpath;
        }
    }
}
