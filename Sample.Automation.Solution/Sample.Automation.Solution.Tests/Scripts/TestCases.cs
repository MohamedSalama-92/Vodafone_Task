using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.Solution.API;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Sample.Automation.Solution.Application.Pages;
using Sample.Automation.Solution.Application.Pages.Home;

namespace Sample.Automation.Solution.Tests.Scripts
{
    public class TestCases : TestMain
    {
        private HomePage _homePage;
        private RegisterPage _registerPage;
        private SearchAPI _searchAPI;
        private readonly Random _random = new Random();
        string password;

        [SetUp]
        public void RegisterationSetup()
        {
            _homePage = new HomePage(ActiveBrowser.WebDriverInstance);
            _registerPage = new RegisterPage(ActiveBrowser.WebDriverInstance);
            _searchAPI = new SearchAPI();
            password = "Test@123";
        }

        [Test,Order(1)]
        public void Register_New_Account()
        {
            
            _homePage.PressOnSigninButton();
            _registerPage.ClickOnCreateAccountButton(email);
            _registerPage.FillRegistrationInformation();
            Assert.IsTrue(_registerPage.ValidateRegistrationCompleted());
        }

        [Test, Order(2)]
        public void Login_With_The_Registered_Account()
        {
            _homePage.PressOnSigninButton();
            _homePage.EnterValidEmailAndPassword(email,password);
            Assert.IsTrue(_registerPage.ValidateRegistrationCompleted());
        }

        [Test,Order(3)]
        public void Checkout_Item_From_store()
        {
            _homePage.PressOnSigninButton();
            _homePage.EnterValidEmailAndPassword(email, password);
            _homePage.UserPickItemFromStore();
            Assert.IsTrue(_homePage.ValidateOrderCompletedSuccessfully());
        }

        [Test, Order(4)]
        public void Validate_Posts_By_ID()
        {
            Assert.IsTrue(_searchAPI.ValidatePostByID());
        }
        [Test, Order(5)]
        public void Validate_All_Post()
        {
            Assert.IsTrue(_searchAPI.ValidateAllPosts());
        }
    }
}
