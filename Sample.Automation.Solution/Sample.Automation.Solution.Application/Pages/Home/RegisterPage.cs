using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using Web.Automation.Web.Action;
using Web.Automation.Web.Component;
using static Web.Automation.Web.Action.AutomatedActions.SelectActions;

namespace Sample.Automation.Solution.Application.Pages.Home
{
    public class RegisterPage
    {
        private readonly string _jsonPagePath = AppConfigs.ObjectRepository + @"Home\Home.json";
        private readonly AutomatedElement _formAuthentication;
        readonly IWebDriver driver;
        WebDriverWait wait;

        private By registrationEmailtxtbx = By.Id("email_create");
        private By createAccountBtn = By.Id("SubmitCreate");
        private By gender = By.Id("id_gender1");
        private By firstName = By.Id("customer_firstname");
        private By lastName = By.Id("customer_lastname");
        private By password = By.Id("passwd");
        private By newsLetterChckbx = By.Id("newsletter");
        private By optinChckbx = By.Id("optin");
        private By address = By.Id("address1");
        private By city = By.Id("city");
        private By postalCode = By.Id("postcode");
        private By phoneNumber = By.Id("phone_mobile");
        private By refrence = By.Id("alias");
        private By registerBtn = By.Id("submitAccount");
        private By logOutBtn = By.CssSelector("a[title='Log me out']");
        private By womenSection = By.CssSelector("a[title='Women']");

        public RegisterPage(IWebDriver driver)
        {
            this.driver = driver;
            //var pageElement = ElementParser.Initialize_Page_Elements(driver, _jsonPagePath);
            //_formAuthentication = pageElement["formAuthentication"];
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        }

        public void ClickOnCreateAccountButton(String Restration_Email)
        {
            driver.FindElement(registrationEmailtxtbx).SendKeys(Restration_Email);
            driver.FindElement(createAccountBtn).Click();
        }

        public void FillRegistrationInformation()
        {

            wait.Until(ExpectedConditions.ElementIsVisible(gender));

            driver.FindElement(gender).Click();
            driver.FindElement(firstName).SendKeys("First Name");
            driver.FindElement(lastName).SendKeys("Last Name");
            driver.FindElement(password).SendKeys("Test@123");


            SelectElement days = new SelectElement(driver.FindElement(By.Id("days")));
            days.SelectByIndex(2);


            SelectElement months = new SelectElement(driver.FindElement(By.Id("months")));
            months.SelectByIndex(6);

            SelectElement years = new SelectElement(driver.FindElement(By.Id("years")));
            years.SelectByIndex(5);

            driver.FindElement(newsLetterChckbx).Click();
            driver.FindElement(optinChckbx).Click();

            driver.FindElement(address).SendKeys("My Address");
            driver.FindElement(city).SendKeys("My City");


            SelectElement state = new SelectElement(driver.FindElement(By.Id("id_state")));
            state.SelectByIndex(2);

            driver.FindElement(postalCode).SendKeys("11835");
            driver.FindElement(phoneNumber).SendKeys("+200000000000");
            driver.FindElement(refrence).Clear();
            driver.FindElement(refrence).SendKeys("+200000000000");
            driver.FindElement(registerBtn).Click();
        }

        public bool ValidateRegistrationCompleted()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(womenSection));

            if (driver.FindElement(logOutBtn).Displayed)
            {
                return true;
            }
            else
                return false;
        }
    }
}
