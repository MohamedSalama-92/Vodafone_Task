using NUnit.Framework;
using System.Reflection;
using Reporting.Utilities.Reporting;
using Sample.Automation.Solution.Application;
using Web.Automation.Web.Action;
using Web.Automation.Web.Component;
using XWorx.Logging.Utilities.Logging;
using System;

namespace Sample.Automation.Solution.Tests
{
    public class TestMain
    {
        protected TestReport AutomationReport;
        public string email;
        public string number;
        private readonly Random _random = new Random();

        /// <summary>
        /// Implement the logic that needs to run once before all the tests.
        /// </summary>
        [OneTimeSetUp]
        protected void OneTimeSetup()
        {
            AutomatedLogger.Log("TestMain: OneTimeSetup");

            InitializeConfigs(); 

            //Initialize the report and attach the html reporter to it
            AutomationReport = new TestReport(TestConfigs.ReportingDirectory);
            number = _random.Next(0, 100).ToString();
            email = "mohamed.test" + number + "@test.com";

        }

        /// <summary>
        /// Implement logic that has to run before executing each scenario.
        /// </summary>
        [SetUp]
        public void SetupTest()
        {
            AutomatedLogger.Log("TestMain : SetupTest");

           

            //Setup the Test Commons
            OpenApplication();

            ////Simple Assert at the beginning of each test. It checks the loading of the entry page of the test.
            //IsEntryPageLoaded();
            //Create a new test entry for this test in the report
            AutomationReport.CreateTest();
        }

        //list your automated browsers that you are going to work on here
        public static AutomatedBrowser ActiveBrowser;

        public void InitializeConfigs()
        {
            //Read the Test Configs(including the ones in app.config)
            string nameOfTestingDirectory = Assembly.GetExecutingAssembly().GetName().Name;

            //Get the current context path
            TestConfigs.SetCurrentContextPath(nameOfTestingDirectory);

            //Read the Test Configs (including the ones in app.config)    
            TestConfigs.Init();
            //This is a workaround to overcome the problem of placing all the configs under the test project
            string applicationConfigs = TestConfigs.AutomationDirectory.Replace(".Automation.Test", ".Automation.Application");
            //Read Application Configs
            AppConfigs.ReadApplicationConfigs(applicationConfigs);
            ////TODO: Virtual
            ////Read the automated app configs
            //TestConfigs.ReadApplicationConfigs();
        }

        /// <summary>
        /// Open the application url specified in the configs.
        /// Must be called after initializing the configs.
        /// </summary>
        public void OpenApplication()
        {
            AutomatedLogger.Log("OpenApplication: Go to the application url provided in the configs");

            //Start a new Browser : Initialize
            ActiveBrowser = new AutomatedBrowser(TestConfigs.Browser, isGridEnabled: false);

            //Open Website in the browser started by Selenium
            if (!string.IsNullOrEmpty(TestConfigs.Url))
            {
                AutomatedActions.NavigationActions.NavigateToUrl(ActiveBrowser.WebDriverInstance,TestConfigs.Url);
                AutomatedActions.WindowActions.Maximize(ActiveBrowser.WebDriverInstance);
            }

            AutomatedLogger.Log("Exiting OpenApplication");

        }//end method Common Setup

        public void IsEntryPageLoaded()
        {
            //bool isPageLoaded = AutomatedActions.WaitActions.WaitForJSandJQueryToLoad();
            bool isPageLoaded = AutomatedActions.WaitActions.WaitForJStoLoad(ActiveBrowser.WebDriverInstance);
            AutomationReport.AssertAndReportStatus(isPageLoaded, "Loading the test's entry page", "Entry page loaded", "Failed to load the entry page");
        }

        [TearDown]
        public void TeardownTest()
        {
            //Close the Browser
            ActiveBrowser.TearDown();           

            AutomatedLogger.Log("TestMain: TeardownTest");
            
            AutomatedLogger.Log("TestMain: Teardown: Copy Output directory to test.");

        }
        [OneTimeTearDown ]
        
        public void RunAfterAnyTestsInInEntireAssembly()
        {
            //Finalize and generate the report
            AutomationReport.GenerateTestReport();
           // AutomationReport.Flush();
        }

      
    }//end class

}//namespace
