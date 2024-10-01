using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagentoSelenuimAutomationDemo.Tests.PageFactories
{
    internal class LogInPage
    {
        private IWebDriver _driver;
        internal LogInPage(IWebDriver driver)
        {
            _driver = driver;
        }
        public By RegisterUser()
        {
            return By.XPath("//a[@class='action create primary']");
        }
        public By MyAccountButton()
        {
            return By.XPath("(//a[contains(text(), 'My Account')])[2]");
            
        }
        public By MyAccountDetails()
        {
            return By.XPath("(//div[@class='box-content'])[1]");
        }
        public By ContantInformation()
        {
            return By.XPath("//div[@class= 'box-content']//p");
        }
        public By FillEmail()
        {
            return By.XPath("//input[@id='email']");
        }
        public By FillPassword()
        {
            return By.XPath("(//input[@id='pass'])[1]");
        }
        public By SighnIn()
        {
            return By.XPath("(//button[@type='submit'])[4]");
        }
        public By ReadMessage()
        {
            return By.XPath("//div[@data-bind='html: $parent.prepareMessageForHtml(message.text)']");
        }
        public By LoginIcon()
        {
            return By.XPath("//div//span[@id='userIcon']");
        }
        public By LogOutButton()
        {
            return By.XPath("//a[text()='Logout']");
        }
        public By LogOutMessage()
        {
            return By.XPath("//div//h1[@class='page-title']");
        }
        public By LogOutPageMessage()
        {
            return By.XPath("//script[@type='text/javascript']");
        }
    }
}
