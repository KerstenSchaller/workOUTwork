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
            string baseDataPath = getDataBasePath();
            string logpath = Path.Combine( baseDataPath, dataBaseNameActiveStateLog);
            return logpath;      
        }



        public static string getDataBasePath() 
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return Path.Combine(appDataPath, applicationName) ;
        }


    }
}
