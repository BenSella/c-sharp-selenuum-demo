using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagentoSelenuimAutomationDemo.Tests.PageFactories
{
    internal class MegentoHomePage
    {
        private IWebDriver _driver;
        internal MegentoHomePage(IWebDriver driver)
        {
            _driver = driver;
        }
        public string Homepage()
        {
            return "https://www.mageplaza.com/kb/magento-2-demo.html";
        }
        public By Magento2DemoStoreHEadline
()
        {
            return By.XPath("//div[@class='mb-2 col-sm-12 py-4 px-0']");
        }
        public By XButtonSpinWheelAdd()
        {
            return By.XPath("(//div[@data-avada-title='Close button'])[1]");
        }
        public By HeaderVisible()
        {
            return By.XPath("//header[@id='header']");
        }
        public By BodyVisible()
        {
            return By.XPath("//main[@id='content']");
        }
        public By FooterVisible()
        {
            return By.XPath("//footer[@class='bg-dark footer-top']");

        }
        //header elemetns 
        public By Magento2ExtenssionsLink()
        {
            return By.XPath("//a[@id='homeMegaMenu']");

        }
        public By ShopifyAppsLink()
        {
            return By.XPath("//li[@id='shopifyAppsLink']");

        }
        public By ServicesLink()
        {
            return By.XPath("//a[@id='homeMegaMenu-Services']");
        }
        public By SpecialDealLink()
        {
            return By.XPath("(//a[@class='font-size-18 font-size-md-down-1'])[2]");
        }
        public By ResourcesLink()
        {
            return By.XPath("(//a[@class='nav-item title-sub-menu-extend arrow font-size-18 font-size-md-down-1'])[2]");
        }
        public By ProgramsLink()
        {
            return By.XPath("(//a[@class='nav-item title-sub-menu-extend arrow font-size-18 font-size-md-down-1'])[3]");
        }
        public By BlogLink()
        {
            return By.XPath("//a[contains(text(), 'Blog')]");
        }
        public By ContactUS()
        {
            return By.XPath("//a[@id='contact-us']");
        }
        public By LigIn()
        {
            return By.XPath("//div[@class='nav-account d-block']");
        }
        public By WhatsNew()
        {
            return By.XPath("//div[@class='mpnews-menu customer-welcome']");
        }
        public By ShopingCart()
        {
            return By.XPath("//div[@data-block='minicart']");
        }

        public void ClickOnCloseWheelAdd()
        {
            _driver.FindElement(XButtonSpinWheelAdd()).Click();
        }
    }
}
