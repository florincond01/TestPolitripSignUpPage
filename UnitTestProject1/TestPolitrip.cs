using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace UnitTestProject1
{
    [TestClass]
    public class TestPolitrip
    {
        private IWebDriver driver;

        [TestInitialize]
        public void Setup()
        {
            driver = new ChromeDriver();
        }

        [TestCleanup]
        public void TearDown()
        {
            driver.Quit();
            driver.Dispose();
        }
        [TestMethod]
        public void CorrectSignUp()
        {
            
            driver.Navigate().GoToUrl("https://politrip.com/account/sign-up");
            driver.Manage().Window.Maximize();
            driver.FindElement(By.Name("first-name")).SendKeys("Test");
            driver.FindElement(By.Name("last-name")).SendKeys("Test");
            driver.FindElement(By.Name("email")).SendKeys("TestF@test.com");
            driver.FindElement(By.Name("password")).SendKeys("Test1234");
            driver.FindElement(By.Id("sign-up-confirm-password-input")).SendKeys("Test1234");
            //test daca pot scrie in dropdown
            driver.FindElement(By.Id("sign-up-heard-input")).SendKeys("Test.123");
            IWebElement element = driver.FindElement(By.Id("sign-up-heard-input"));
            SelectElement oSelect = new SelectElement(element);
            oSelect.SelectByIndex(1);
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            jse.ExecuteScript("scroll(0,250)");
            System.Threading.Thread.Sleep(5000);
            //driver.FindElement(By.CssSelector("body")).SendKeys(Keys.Control + Keys.End);
            IWebElement button = driver.FindElement(By.Id(" qa_loader-button"));
            button.Click();
            System.Threading.Thread.Sleep(5000);
            driver.FindElement(By.Id("qa_signup-participant")).Click();

        }

        [TestMethod]
        public void GivenWhiteSpaces_ReturnsCorrectErrorMessage()
        {

            string isempty = "This field can not be empty";           
            driver.Navigate().GoToUrl("https://politrip.com/account/sign-up");
            driver.Manage().Window.Maximize();
            driver.FindElement(By.Name("first-name")).SendKeys("");
            driver.FindElement(By.Name("last-name")).SendKeys("");
            driver.FindElement(By.Name("email")).SendKeys("");
            driver.FindElement(By.Name("password")).SendKeys("");
            driver.FindElement(By.Id("sign-up-confirm-password-input")).SendKeys("");
            IWebElement element = driver.FindElement(By.Id("sign-up-heard-input"));
            SelectElement oSelect = new SelectElement(element);
            oSelect.SelectByIndex(1);
            string errormessage = driver.FindElement(By.ClassName("form-input-hint")).Text;
            Assert.AreEqual(isempty, errormessage);
            
        }

        [TestMethod]
        public void EmailAlreadyRegistered()
        {

            string error= "An user with this email is already registered";
            driver.Navigate().GoToUrl("https://politrip.com/account/sign-up");
            driver.Manage().Window.Maximize();
            driver.FindElement(By.Name("first-name")).SendKeys("Test");
            driver.FindElement(By.Name("last-name")).SendKeys("Test");
            driver.FindElement(By.Name("email")).SendKeys("TestF@test.com");
            driver.FindElement(By.Name("password")).SendKeys("Test1234");
            driver.FindElement(By.Id("sign-up-confirm-password-input")).SendKeys("Test1234");   
            IWebElement element = driver.FindElement(By.Id("sign-up-heard-input"));
            SelectElement oSelect = new SelectElement(element);
            oSelect.SelectByIndex(1);
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            jse.ExecuteScript("scroll(0,250)");
            IWebElement button = driver.FindElement(By.Id(" qa_loader-button"));
            button.Click();
            System.Threading.Thread.Sleep(5000);
            string actualerror = driver.FindElement(By.Id("sign-up-error-div")).Text;
            Assert.AreEqual(actualerror, error);
            
        }

        [TestMethod]
        [DataRow("Prenume1", "Nume1")]
        [DataRow("Prenume!", "Nume!")]
        //Test cu caractere germane
        [DataRow("PrenumeÄ", "NumeÄ")]
        public void IncorrectName(string prenume, string nume)
        {

            string error = "Wrong characters or format";
            driver.Navigate().GoToUrl("https://politrip.com/account/sign-up");
            driver.Manage().Window.Maximize();
            driver.FindElement(By.Name("first-name")).SendKeys(prenume);
            driver.FindElement(By.Name("last-name")).SendKeys(nume);
            driver.FindElement(By.Name("email")).Click();
            System.Threading.Thread.Sleep(2000);
            string actualerror = driver.FindElement(By.ClassName("form-input-hint")).Text;
            Assert.AreEqual(actualerror, error);
   
        }

        [TestMethod]
        [DataRow("email@")]
       // [DataRow("email@a")] - it works but it shouldnt
        [DataRow("email@.com")]
        // [DataRow("email@1.11")] -it works but it shouldnt
        [DataRow("email@!@#.com")]
        public void IncorrectEmail(string email)
        {

            string error = "Please enter a valid email address";
            driver.Navigate().GoToUrl("https://politrip.com/account/sign-up");
            driver.Manage().Window.Maximize();
            driver.FindElement(By.Name("first-name")).SendKeys("Test");
            driver.FindElement(By.Name("last-name")).SendKeys("Test");
            driver.FindElement(By.Name("password")).SendKeys("Test1234");
            driver.FindElement(By.Id("sign-up-confirm-password-input")).SendKeys("Test1234");
            driver.FindElement(By.Name("email")).SendKeys(email);
            driver.FindElement(By.Name("first-name")).Click();
            System.Threading.Thread.Sleep(2000);
            string actualerror = driver.FindElement(By.ClassName("form-input-hint")).Text;
            Assert.AreEqual(actualerror, error);

        }

        [TestMethod]
        [DataRow("Password")]
        [DataRow("password1")]
        [DataRow("PASSWORD1")]
        public void IncorrectPassword(string password)
        {

            string error = "Password must contain at least 8 characters, 1 uppercase letter, 1 lowercase letter and 1 digit";
            driver.Navigate().GoToUrl("https://politrip.com/account/sign-up");
            driver.Manage().Window.Maximize();
            driver.FindElement(By.Name("first-name")).SendKeys("Test");
            driver.FindElement(By.Name("last-name")).SendKeys("Test");
            driver.FindElement(By.Name("email")).SendKeys("Testt@test.com");
            driver.FindElement(By.Id("sign-up-confirm-password-input")).SendKeys("Test1234");
            driver.FindElement(By.Name("password")).SendKeys(password);
            driver.FindElement(By.Name("first-name")).Click();
            System.Threading.Thread.Sleep(2000);
            string actualerror = driver.FindElement(By.ClassName("form-input-hint")).Text;
            Assert.AreEqual(actualerror, error);

        }

        [TestMethod]
        [DataRow("Password")]
        public void ConfirmPassword(string confirmpassword)
        {

            string error = "Passwords must match";
            driver.Navigate().GoToUrl("https://politrip.com/account/sign-up");
            driver.Manage().Window.Maximize();
            driver.FindElement(By.Name("first-name")).SendKeys("Test");
            driver.FindElement(By.Name("last-name")).SendKeys("Test");
            driver.FindElement(By.Name("email")).SendKeys("Testt@test.com");
            driver.FindElement(By.Id("sign-up-confirm-password-input")).SendKeys("Test1234");
            driver.FindElement(By.Name("password")).SendKeys("Password1");
            driver.FindElement(By.Id("sign-up-confirm-password-input")).SendKeys(confirmpassword);
            driver.FindElement(By.Name("password")).Click();
            System.Threading.Thread.Sleep(2000);
            string actualerror = driver.FindElement(By.ClassName("form-input-hint")).Text;
            Assert.AreEqual(actualerror, error);

        }

        [TestMethod]
        public void GoogleLogin()
        {

            driver.Navigate().GoToUrl("https://politrip.com/account/sign-up");
            driver.Manage().Window.Maximize();
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            jse.ExecuteScript("scroll(0,400)");
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.XPath("//span[@class='social-label'][contains(text(),'Google')]")).Click();
            System.Threading.Thread.Sleep(5000);

        }

        [TestMethod]
        public void FacebookLogin()
        {

            driver.Navigate().GoToUrl("https://politrip.com/account/sign-up");
            driver.Manage().Window.Maximize();
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            jse.ExecuteScript("scroll(0,400)");
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.XPath("//span[@class='social-label'][contains(text(),'Facebook')]")).Click();
            System.Threading.Thread.Sleep(5000);

        }

        [TestMethod]
        public void InstagramLogin()
        {

            driver.Navigate().GoToUrl("https://politrip.com/account/sign-up");
            driver.Manage().Window.Maximize();
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            jse.ExecuteScript("scroll(0,400)");
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.XPath("//span[@class='social-label'][contains(text(),'Instagram')]")).Click();
            System.Threading.Thread.Sleep(5000);

        }

        [TestMethod]
        public void VKLogin()
        {

            driver.Navigate().GoToUrl("https://politrip.com/account/sign-up");
            driver.Manage().Window.Maximize();
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            jse.ExecuteScript("scroll(0,400)");
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.XPath("//span[@class='social-label'][contains(text(),'VK')]")).Click();
            System.Threading.Thread.Sleep(5000);

        }

        [TestMethod]
        public void AlreadyHaveAnAccount()
        {

            driver.Navigate().GoToUrl("https://politrip.com/account/sign-up");
            driver.Manage().Window.Maximize();
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            jse.ExecuteScript("scroll(0,400)");
            System.Threading.Thread.Sleep(2000);
            driver.FindElement(By.ClassName("goto-container")).Click();
            System.Threading.Thread.Sleep(5000);

        }

    }
}
