using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace AutoTest1402
{
    [TestFixture]
    public class Test
    {
        public static IWebDriver driver;

        [SetUp]
        public void BeforeTest()
        {
            Console.WriteLine("Open Chrome");
            var chProfile = new ChromeOptions();
            chProfile.AddArguments("--test-type, --disable-extensions", "--start-maximized");
            driver = new ChromeDriver(chProfile);
        }

        [TearDown]
        public void AfterTest()
        {
            driver.Quit();
            Console.WriteLine("Selenium is stopped.", "StateS");
        }

        [Test]
        public void SimpleTest()
        {

            driver.Navigate().GoToUrl("http://google.com/");
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//input[@type='text']")).SendKeys("mokilev");
            driver.FindElement(By.XPath("//input[@name='btnK']")).Click();
            Thread.Sleep(2000);

            bool result = ElementExists("//span[contains(text(),'Did you mean: mogilev')]");
            
            if (result)
            {
                Assert.IsTrue(result, "Pass");
            }
            else
            {
                Assert.IsFalse(result, "Fail");
            }
        }
        public bool ElementExists(string strXPath)
        {
            int retry = 3;
            do
            {
                try
                {
                    var element = driver.FindElement(By.XPath(strXPath)); ;
                    return element != null && element.Displayed;
                }
                catch (OpenQA.Selenium.NoSuchElementException)
                {
                    return false;
                }
                catch (OpenQA.Selenium.StaleElementReferenceException)
                {
                    if (retry == 0)
                    {
                        throw;
                    }
                }
                retry--;
            } while (retry >= 0);

            return false;
        }
    }
    
}
