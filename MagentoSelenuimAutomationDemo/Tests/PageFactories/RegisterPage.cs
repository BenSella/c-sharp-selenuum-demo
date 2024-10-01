using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagentoSelenuimAutomationDemo.Tests.PageFactories
{
    internal class RegisterPage
    {
        private IWebDriver _driver;
        internal RegisterPage(IWebDriver driver)
        {
            _driver = driver;
        }
        // register user name
        public By UserName()
        {
            return By.XPath("//div//input[@id='firstname']");
        }

        public By LastName()
        {
            return By.XPath("//div/input[@id='lastname']");
        }
        public By TaxVatNumber()
        {
            return By.XPath("//div/input[@id='taxvat']");
        }
        public By AllowRemoteShoppingAssistanceBox()
        {
            return By.XPath("//input[@type='checkbox']");
        }
        public By WhoAreYouDropTable()
        {
            return By.XPath("//select[@id='who_are_you']");
        }
        public By WhoAreYouSelectProgrammer()
        {
            return By.XPath("//option[@value='17']");
        }
        public By FillEmail()
        {
            return By.XPath("(//input[@type='email'])[1]");
        }
        public By FillPassword()
        {
            return By.XPath("(//input[@type='password'])[1]");
        }
        public By FillConfirmPassword()
        {
            return By.XPath("//input[@id='password-confirmation']");
        }
        public By CreateAnAccountButton()
        {
            return By.XPath("//button[@class='action submit primary']");
        }
    }
}
