using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using Web.Automation.Web.Action;
using Web.Automation.Web.Component;

namespace Sample.Automation.Solution.Application.Pages.Home
{
    public class HomePage
    {
        private readonly string _jsonPagePath = AppConfigs.ObjectRepository + @"Home\Home.json";
        private readonly AutomatedElement _formAuthentication;
        readonly IWebDriver driver;
        WebDriverWait wait;

        private By signInBtn = By.CssSelector("a[title='Log in to your customer account']");
        private By emailtxtbx = By.Id("email");
        private By passwordtxtbx = By.Id("passwd");
        private By loginBtn = By.Id("SubmitLogin");

        By WomenSection = By.CssSelector("a[title='Women']");
        By BlousesSection = By.CssSelector("a[title='Blouses']");
        By BlouseItem = By.CssSelector("span[class='available-now']");
        By AddtoCartBtn = By.CssSelector("a[title='Add to cart']");
        By CheckOutBtn = By.CssSelector("a[title='Proceed to checkout']");
        By ProceedtoCheckOutBtn = By.CssSelector("a[title='Proceed to checkout']:nth-child(1)");
        By ProceedaddressBtn = By.CssSelector("button[name='processAddress']");
        By ShippingBtn = By.CssSelector("input[id='cgv']");
        By ProceedShippingBtn = By.CssSelector("button[name='processCarrier']");
        By PayByCheckBtn = By.CssSelector("a[title='Pay by check.']");
        By confirmBtn = By.XPath("//p[@id='cart_navigation']/button");
        By OrderSuccessMsg = By.CssSelector("p[class='alert alert-success']");
        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
            //var pageElement = ElementParser.Initialize_Page_Elements(driver, _jsonPagePath);
            //_formAuthentication = pageElement["formAuthentication"];
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        }

        public void PressOnSigninButton()
        {
            driver.FindElement(signInBtn).Click();
        }

        public void EnterValidEmailAndPassword(string username, string password)
        {
            driver.FindElement(emailtxtbx).SendKeys(username);
            driver.FindElement(passwordtxtbx).SendKeys(password);
            driver.FindElement(loginBtn).Click();
        }

        public void UserPickItemFromStore()
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(driver.FindElement(WomenSection)).Build().Perform();

            wait.Until(ExpectedConditions.ElementIsVisible(BlousesSection));

            driver.FindElement(BlousesSection).Click();

            wait.Until(ExpectedConditions.ElementIsVisible(BlouseItem));

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollBy(0,500)");

            //actions.MoveToElement(driver.FindElement(BlouseItem)).Build().Perform();
            driver.FindElement(BlouseItem).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(AddtoCartBtn));
            driver.FindElement(AddtoCartBtn).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(CheckOutBtn));
            driver.FindElement(CheckOutBtn).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(ProceedtoCheckOutBtn));
            driver.FindElement(ProceedtoCheckOutBtn).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(ProceedaddressBtn));
            driver.FindElement(ProceedaddressBtn).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(ProceedShippingBtn));
            driver.FindElement(ShippingBtn).Click();
            driver.FindElement(ProceedShippingBtn).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(PayByCheckBtn));
            driver.FindElement(PayByCheckBtn).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(confirmBtn));
            driver.FindElement(confirmBtn).Click();
        }
        public bool ValidateOrderCompletedSuccessfully()
        {
            if (driver.FindElement(OrderSuccessMsg).Text.Equals("Your order on My Store is complete."))
            {
                return true;
            }
            else
                return false;
        }
    }
}
