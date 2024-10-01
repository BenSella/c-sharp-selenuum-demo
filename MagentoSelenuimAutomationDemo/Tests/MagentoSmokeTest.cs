
using OpenQA.Selenium;
using SeleniumAutomationLibrary.Utils;
using Microsoft.Extensions.Configuration;
using MagentoSelenuimAutomationDemo.Tests.PageFactories;
using Allure.Net.Commons;
using MagentoSelenuimAutomationDemo.Objects;
using MagentoSelenuimAutomationDemo.Utils.Caching;
using NUnit.Allure.Attributes;
using NUnit.Allure.Core;
using Allure.NUnit;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;

namespace MagentoSelenuimAutomationDemo.Tests
{
    [Allure.NUnit.AllureNUnit]
    [TestFixture]
    internal class magentoSmokeTest
    {
        private AutomationInfrastructureHelper _automationHelper;
        private AllureInfrastructure _allure;
        private IWebDriver _driver;
        private const string NODATA = "no data found in appsettings.json";
        MegentoHomePage _homepage;
        private IConfiguration _configuration;
        static bool testPass = false;
        static bool testWarining = false;
        static string jsonFileLocation = string.Empty;
        
        [SetUp]
        public void Setup()
        {
            jsonFileLocation = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            var builder = new ConfigurationBuilder()
                .SetBasePath(jsonFileLocation) // Set the base path to the current directory
                .AddJsonFile("cachingData.json", optional: false, reloadOnChange: true) // Add caching.json file
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true); // Add appsettings.json file

            _configuration = builder.Build();

            _automationHelper = new AutomationInfrastructureHelper();
            _allure = new AllureInfrastructure();
            _driver = _automationHelper.GetDriver(headless: false);
            _homepage = new MegentoHomePage(_driver);
            testPass = false;
            testWarining = false;
        }

        [TearDown]
        public void TearDown()
        {
            // Close the WebDriver after test completion
            if (_driver != null)
            {
                _driver.Quit();
            }
        }

        [Test, Order(1)]
        [AllureTag("Smoke", "Magento")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureFeature("Magento Demo Site Testing, main page connection, test header, body and footer are visible")]
        public void Test1()
        {
            MegentoHomePage homepage = new MegentoHomePage(_driver);
            try
            {
                NavigateToHomePage();
                _automationHelper.WaitForElementToBeVisible(_driver, homepage.Magento2DemoStoreHEadline());
                _automationHelper.WaitForElementToBeVisible(_driver, homepage.HeaderVisible());
                _automationHelper.WaitForElementToBeVisible(_driver, homepage.BodyVisible());
                _automationHelper.WaitForElementToBeVisible(_driver, homepage.FooterVisible());
                _allure.ExecuteAndReportPass("Test 1 passed: connected to url,header, body and footer are visible.", null);//,false,"");

            }
            catch (Exception ex)
            {
                string sceenshotLocation = _automationHelper.CaptureScreenshot(_driver);
                _allure.ExecuteAndReportFailed($"Test 1 failure: Could not locate element\nerror message: {ex}.", sceenshotLocation);//,true, sceenshotLocation);
                Assert.Fail("Test 1 failure: Could not locate element");
            }
            TearDown();
        }

        [Test, Order(2)]

        [AllureTag("Smoke", "Magento")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureFeature("Magento Demo Site Testing, header navigation links (clickable links to outside url's")]
        public void Test2()
        {
            //MagentoHomePage homepage = new MagentoHomePage(_driver);

            try
            {
                NavigateToHomePage();
                CloseAdd();

                ExaminUrlLink(_homepage.Magento2ExtenssionsLink(), _configuration["Urls:Megento2ExtentionsUrl"] ?? NODATA);

                //check header Magento 2 Extensions 
                ExaminUrlLink(_homepage.ShopifyAppsLink(), _configuration["urls:ShopiffyAppUrl"] ?? NODATA);

                //check header Shopify Apps element
                ExaminUrlLink(_homepage.ServicesLink(), _configuration["Urls:ServiceUrl"] ?? NODATA);

                //check header Special Deals element
                ExaminUrlLink(_homepage.SpecialDealLink(), _configuration["Urls:SpecialDealsUrl"] ?? NODATA);

                //check header Blog element 
                ExaminUrlLink(_homepage.BlogLink(), _configuration["Urls:BlogsURl"] ?? NODATA);

                //check header ContactUs element
                ExaminUrlLink(_homepage.ContactUS(), _configuration["Urls:ContactUs"] ?? NODATA);

                //check header LogIn element
                ExaminUrlLink(_homepage.LigIn(), _configuration["Urls:LogIn"] ?? NODATA);
                if (testWarining)
                {
                    Assert.Warn("Test 2, some of the Externa urls were not the same as expected\ncheck test suits for more details");
                }

            }

            catch (Exception ex)
            {
                string sceenshotLocation = _automationHelper.CaptureScreenshot(_driver);
                _allure.ExecuteAndReportFailed($"Test 2 failure: Could not locate element\nerror message: {ex}.", sceenshotLocation);//, true, sceenshotLocation);
                Assert.Fail("Test 1 failure: Could not locate element");
            }
        }

        [Test, Order(3)]

        [AllureTag("Smoke", "Magento")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureFeature("Magento Demo Site Testing, login/ register testing")]
        public void Test3()
        {
            LogInPage login = new LogInPage(_driver);
            RegisterPage registerPage = new RegisterPage(_driver);
            try
            {
                NavigateToHomePage();
                CloseAdd();
                _automationHelper.WaitForElementToBeClickable(_driver, _homepage.LigIn());
                _automationHelper.SwitchToNewWindow(_driver, _homepage.LigIn());

                //login page
                _automationHelper.WaitForElementToBeClickable(_driver, login.RegisterUser());
                _driver.FindElement(login.RegisterUser()).Click();

                //test 1 = register 
                //register page - fill name
                string name = _configuration["Registration:UserName"] ?? NODATA;
                _driver.FindElement(registerPage.UserName()).SendKeys(name);

                //register page - fill name
                string familyName = _configuration["Registration:FamilyName"] ?? NODATA;
                _driver.FindElement(registerPage.LastName()).SendKeys(familyName);

                //tax VAT Number
                _driver.FindElement(registerPage.TaxVatNumber()).SendKeys(_configuration["Registration:TaxVatNumber"] ?? NODATA);

                //checkbox allow retome shopping assistance checkbox
                _driver.FindElement(registerPage.AllowRemoteShoppingAssistanceBox()).Click();

                //who are you select dropdown
                _driver.FindElement(registerPage.WhoAreYouDropTable()).Click();

                //select describe you option
                _automationHelper.WaitForElementToBeClickable(_driver, registerPage.WhoAreYouSelectProgrammer());
                _driver.FindElement(registerPage.WhoAreYouSelectProgrammer()).Click();

                //add a new increacing int number to existing email adress for a new registration under same mail
                int emailAdressNumber = ExtractLastNumberFromCachingJson();
                string emailAdress = _configuration["Registration:EmailStart"] +
                                _configuration["Registration:EmailRegistration"] +
                                emailAdressNumber +
                                _configuration["Registration:EmailEnd"] ?? NODATA;

                //fill email
                _driver.FindElement(registerPage.FillEmail()).SendKeys(emailAdress);

                //fill password
                string strongPassword = _configuration["Registration:StrongPassword"] ?? NODATA;
                _driver.FindElement(registerPage.FillPassword()).SendKeys(strongPassword);

                //confirm password
                _driver.FindElement(registerPage.FillConfirmPassword()).SendKeys(strongPassword);

                //click on createAccount
                _driver.FindElement(registerPage.CreateAnAccountButton()).Click();

                // check if user name and login were successful 

                //test 

                string message = _driver.FindElement(login.ReadMessage()).Text;
                if (!string.IsNullOrEmpty(message))
                {
                    if (message.Equals(_configuration["SiteMessgeges:CantSaveCustomer"] ?? NODATA))
                    {
                        string sceenshotLocation = _automationHelper.CaptureScreenshot(_driver);
                        _allure.ExecuteAndReportWarning($"Test 3 failure: error login Messege", sceenshotLocation);
                        testWarining = true;
                    }
                }

                //go to my account
                _automationHelper.WaitForElementToBeClickable(_driver, login.MyAccountButton());
                _driver.FindElement(login.MyAccountButton()).Click();
                _automationHelper.WaitForElementToBeClickable(_driver, login.MyAccountDetails());
                string nameFromAccount, emaillFromAccount;
                UserMail userMail = ExtractUserNameFamilyNMail(login.MyAccountDetails());
                string nameNFamily = name + " " + familyName;
                // check user name + family name given
                ConfirmUserDetails(3, nameNFamily, emailAdress, userMail.NameFamily ?? "no name found", userMail.Email ?? "no name found");

                if (testWarining)
                {
                    Assert.Warn(message);
                }
            }
            catch (Exception ex)
            {
                string sceenshotLocation = _automationHelper.CaptureScreenshot(_driver);
                _allure.ExecuteAndReportFailed($"Test 3 failure: Could not locate element\nerror message: {ex}.", sceenshotLocation);//, true, sceenshotLocation);//, true, sceenshotLocation);
                Assert.Fail("Test 3 failure: Could not locate key elements");
            }

        }

        [Test, Order(4)]
        [AllureTag("Smoke", "Magento")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureFeature("Magento Demo Site Testing, login")]
        public void Test4()
        {
            try
            {
                LogInPage login = new LogInPage(_driver);
                RegisterPage registerPage = new RegisterPage(_driver);
                NavigateToHomePage();
                CloseAdd();

                string email = _configuration["Caching:LastSuccessfulRegistrationMail"] ?? NODATA;
                
                //go to login
                LogInAction(login);

                //verify login message
                _automationHelper.WaitForElementToBeClickable(_driver, login.MyAccountButton());
                string message = _driver.FindElement(login.ReadMessage()).Text;
                if (!string.IsNullOrEmpty(message))
                {
                    if (message.Equals(_configuration["SiteMessgeges:LogInErrorMessae"] ?? NODATA))
                    {
                        string sceenshotLocation = _automationHelper.CaptureScreenshot(_driver);
                        _allure.ExecuteAndReportWarning($"Test 4 failure: error login Messege", sceenshotLocation);//, true, sceenshotLocation);//, true, sceenshotLocation);
                        testWarining = true;
                        //add screenshot to allure 
                    }
                }
                //verify login successfully
                string nameFromAppsettings = _configuration["Registration:UserName"] + " " + _configuration["Registration:FamilyName"] ?? NODATA;
                _driver.FindElement(login.MyAccountButton()).Click();
                _automationHelper.WaitForElementToBeVisible(_driver, login.MyAccountDetails());
                UserMail userMail = ExtractUserNameFamilyNMail(login.ContantInformation());
                ConfirmUserDetails(4, nameFromAppsettings, email, userMail.NameFamily ?? "no data found", userMail.Email ?? "no data found");
                if (testWarining) 
                {
                    Assert.Warn(message);
                }
            }
            catch (Exception ex)
            {
                string sceenshotLocation = _automationHelper.CaptureScreenshot(_driver);
                _allure.ExecuteAndReportFailed($"Test 4 failure: Could not locate element\nerror message: {ex}.", sceenshotLocation);//, true, sceenshotLocation);//, true, sceenshotLocation);
                Assert.Fail("Test 4 failure: Could not locate key elements");
            }
        }

        [Test, Order(5)]

        [AllureTag("Smoke", "Magento")]
        [AllureSeverity(SeverityLevel.critical)]
        [AllureFeature("Magento Demo Site Testing, logOut")]
        public void Test5()
        {
            try
            {
                LogInPage login = new LogInPage(_driver);
                RegisterPage registerPage = new RegisterPage(_driver);
                NavigateToHomePage();
                CloseAdd();
                //login
                LogInAction(login);
                
                //click logout
                _automationHelper.WaitForElementToBeClickable(_driver, login.LoginIcon());
                _driver.FindElement(login.LoginIcon()).Click();
                _automationHelper.WaitForElementToBeClickable(_driver, login.LogOutButton());
                _driver.FindElement(login.LogOutButton()).Click();
                //read logout message
                _automationHelper.WaitForElementToBeVisible(_driver, login.LogOutMessage());
                string text = _driver.FindElement(login.LogOutMessage()).Text;
                if (string.IsNullOrEmpty(text) && !text.Equals(_configuration["SiteMessgeges:SignedOut"] ?? NODATA))
                {
                    string sceenshotLocation = _automationHelper.CaptureScreenshot(_driver);
                    _allure.ExecuteAndReportWarning($"Test 5 failure: error logOut Messege", sceenshotLocation);
                    testWarining = true;
                }
                if (testWarining)
                {
                    Assert.Warn($"Test 5 failure: error logOut Messege");
                }

            }
            catch (Exception ex)
            {
                string sceenshotLocation = _automationHelper.CaptureScreenshot(_driver);
                _allure.ExecuteAndReportFailed($"Test 5 failure: Could not locate element\nerror message: {ex}.", sceenshotLocation);
                Assert.Fail($"Test 5 failure: Could not locate element");
            }
        }

        private void LogInAction(LogInPage login)
        {
            _automationHelper.WaitForElementToBeClickable(_driver, _homepage.LigIn());
            _automationHelper.SwitchToNewWindow(_driver, _homepage.LigIn());

            //getHashCode email and password from appsettings 
            string email = _configuration["Caching:LastSuccessfulRegistrationMail"] ?? NODATA;
            string passWord = _configuration["Registration:StrongPassword"] ?? NODATA;

            //enter email and password from appsettings
            _automationHelper.WaitForElementToBeClickable(_driver, login.FillEmail());
            _driver.FindElement(login.FillEmail()).SendKeys(email);
            _driver.FindElement(login.FillPassword()).SendKeys(passWord);
            _driver.FindElement(login.SighnIn()).Click();
        }

        private void NavigateToHomePage()
        {

            _driver.Navigate().GoToUrl(_homepage.Homepage());
        }
        private void CloseAdd()
        {
            _automationHelper.WaitForElementToBeVisible(_driver, _homepage.XButtonSpinWheelAdd());
            Thread.Sleep(1000);
            _homepage.ClickOnCloseWheelAdd();
        }
        private void ExaminUrlLink(By by, string urlFromConfiguration)
        {
            _automationHelper.WaitForElementToBeClickable(_driver, by);
            _automationHelper.SwitchToNewWindow(_driver, by);

            string fullUrl = _driver.Url;
            string baseUrl;
            int index = fullUrl.IndexOf("/customer/account/login");

            if (index > -1)
            {
                baseUrl = fullUrl.Substring(0, index + "/customer/account/login".Length);
            }
            else
            {
                baseUrl = fullUrl; // If the path is not found, use the full URL
            }

            if (ExaminUrl(urlFromConfiguration, baseUrl))
            {
                _allure.ExecuteAndReportPass($"Test 2, url compareson: from configuration {urlFromConfiguration} equlas to url extracted {baseUrl}", null);
            }
            else
            {
                testWarining = true;
                string errorMessage = ($"test 2, warning {urlFromConfiguration}is not equals to actual url {baseUrl}");
                _allure.ExecuteAndReportWarning(errorMessage, null);
            }

            _automationHelper.SwitchToOriginalWindow(_driver);
        }

        private int ExtractLastNumberFromCachingJson()
        {
            int emailCurrentNumbe = 0;
            try
            {
                emailCurrentNumbe = int.Parse(_configuration["Caching:EmailRegisterData" ?? NODATA]);
                emailCurrentNumbe++;
                CachingHelper.UpdateCachingFile(jsonFileLocation, "EmailRegisterData", emailCurrentNumbe.ToString(), "cachingData.json");

            }
            catch (Exception ex)
            {
                string sceenshotLocation = _automationHelper.CaptureScreenshot(_driver);
                _allure.ExecuteAndReportFailed($"could not convert to integer\nerror message:{ex}", sceenshotLocation);
            }
            return emailCurrentNumbe;

        }

        private void ConfirmUserDetails(int test, string nameNFamily, string emailAdress, string nameFromAccount, string emaillFromAccount)
        {
            if (nameNFamily.Equals(nameFromAccount))
            {
                _allure.ExecuteAndReportPass($"Test {test} : given user + family namme: {nameNFamily} match data extracted from site: {emaillFromAccount}.", null);
                // check email
                if (emailAdress.Equals(emaillFromAccount))
                {
                    //register last successful login mail
                    CachingHelper.UpdateCachingFile(jsonFileLocation, "LastSuccessfulRegistrationMail", emailAdress, "cachingData.json");
                    _allure.ExecuteAndReportPass($"Test {test} : given mail {emailAdress} match data extracted from site: {emaillFromAccount}.", null);
                }
                else
                {
                    string sceenshotLocation = _automationHelper.CaptureScreenshot(_driver);
                    _allure.ExecuteAndReportFailed($"Test {test} failure: given mail {emailAdress}not match from data extracted from site: {emaillFromAccount}.", sceenshotLocation);
                    Assert.Fail();
                }

            }
            else
            {
                string sceenshotLocation = _automationHelper.CaptureScreenshot(_driver);
                _allure.ExecuteAndReportFailed($"Test {test} failure: given name + family name {nameNFamily}not match from data extracted from site: {nameFromAccount}.", sceenshotLocation);
                Assert.Fail();
            }
        }

        private UserMail ExtractUserNameFamilyNMail(By by)
        {
            IWebElement accountDetails = _driver.FindElement(by);
            string fullText = accountDetails.Text;
            //extract data from fullText
            string[] lines = fullText.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            // check ser name and email and compare to what sent
            UserMail userMail = new UserMail
            {
                NameFamily = lines[0].Trim(),
                Email = lines[1].Trim(),

            };
            return userMail;

        }
        private bool ExaminUrl(string urlFromAppsettings, string urlFromDriver)
        {
            return (urlFromAppsettings.Equals(urlFromDriver));
        }
    }
}




