using System;
using System.Configuration;
using Parsing.Utilities.Parsers;

namespace Sample.Automation.Solution.Application
{
    public static class AppConfigs
    {
        //Application Settings
        //public static string directory = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;
        
        //Application Settings       
        public static string Browser;

        public static int WaitingShortTime;
        public static int WaitingMediumTime;
        public static int WaitingLongTime;

     

        public static bool IsApplicationConfigsInitialized = false;

        public static void ReadApplicationConfigs(string pathOfCurrentContext = null)
        {

            string automationDirectory = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;
            ObjectRepository = automationDirectory + TextDataParser.GetValue("ObjectRepository");
            WaitingShortTime = Int32.Parse(ConfigurationManager.AppSettings["WaitingShortTime"]);
            WaitingMediumTime = Int32.Parse(ConfigurationManager.AppSettings["WaitingMediumTime"]);
            WaitingLongTime = Int32.Parse(ConfigurationManager.AppSettings["WaitingLongTime"]);

          
        }//end method ReadConfigs

       

        public static string ObjectRepository { get; set; } //= directory + ConfigurationManager.AppSettings["ObjectRepository"];
    }//end class



}
