using Parsing.Utilities.Parsers;
using System;

namespace Sample.Automation.Solution.Tests
{
    class TestConfigs
    {
        //Application Settings
        public static string Url;
        public static string Browser;

        //Automation Configs
        public static string AutomationDirectory;

        //Files
        public static string TestDataFile;

        //Logs
        public static string LogDirectory;

        //Maximum number of retries if the test failed
        public const int MaxNumberOfRetries = 1;
        public static bool IsTestConfigsInitialized = false;

        //Reporting Directory
        public static string ReportingDirectory;

        public static void SetCurrentContextPath(string nameOfTestingDirectory)
        {
            
            AutomationDirectory = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;

            //AutomationDirectory = currentPath;
        }

        /// <summary>
        /// Read the configurations of the application under test
        /// </summary>
        public void ReadApplicationConfigs()
        {
        }//end method ReadApplicationConfigs

        /// <summary>
        /// Read the provided test configurations
        /// </summary>
        public static void ReadConfigs(string pathOfAutomationConfigs)
        {
            //Reporting Directory
            //Load the Automation Configs
           // TextDataParser.LoadAndSetValue("DownloadChromeDirectory", SetDownloadDirectoryPath(),pathOfAutomationConfigs);
            TextDataParser.LoadConfigurationValues(pathOfAutomationConfigs);

            Url = TextDataParser.GetValue("Url");
            Browser = TextDataParser.GetValue("Browser");
            //Files: Test Data, Messages
            TestDataFile = AutomationDirectory + TextDataParser.GetValue("TestDataFile");
            LogDirectory = AutomationDirectory + TextDataParser.GetValue("LoggingDirectory");
            ReportingDirectory = AutomationDirectory + TextDataParser.GetValue("ReportingDirectory");

        }//end method ReadConfigs


        /// <summary>
        /// Initialize the test configurations
        /// </summary>
        public static void Init()
        {
            if (!IsTestConfigsInitialized)
            {
                string configurationFile = AutomationDirectory + "\\automation.conf";
                //Read the automated app configs
                ReadConfigs(configurationFile);

                //AutomatedLogger.Init(LogDirectory);

                //AutomatedLogger.Log("Automation Configuration file: " + configurationFile);

                //Initialize your configs: messages, test data, logger, ...
                ExcelDataParser.Init(TestConfigs.TestDataFile);

                //TODO: this should be json data object
                //data = JsonDataParser.ParseJsonData(TestConfigs.TestDataFile);

                IsTestConfigsInitialized = true;

            }//endif

        }//end method


        /// <summary>
        /// Check if you are testing remotely or on a localhost
        /// </summary>
        /// <returns></returns>
        public static bool IsRemoteTesting()
        {
            bool isRemote = !Url.Contains("localhost");
            return isRemote;
        }
    }
}
