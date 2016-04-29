using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using log4net;
using System.Reflection;
using System.Net.Http;
using System.Configuration;

namespace SeleniumService
{
    public class HomeController : ApiController
    {
        private IWebDriver _driver;
        private StringBuilder _verificationErrors;
        private string _baseUrl;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public HttpResponseMessage Start()
        {
            try
            {                
                log.Info("Starting Selenium ...");
                var userName = ConfigurationManager.AppSettings["UserName"];
                var password = ConfigurationManager.AppSettings["Password"];
                _driver = new FirefoxDriver();
                _baseUrl = ConfigurationManager.AppSettings["CarLinkUrl"];// "http://my.voxcarlink.com/";
                _verificationErrors = new StringBuilder();


                _driver.Navigate().GoToUrl(_baseUrl + "/login");
                _driver.FindElement(By.Name("login")).Clear();
                _driver.FindElement(By.Name("login")).SendKeys(userName);
                _driver.FindElement(By.Id("password")).Clear();
                _driver.FindElement(By.Id("password")).SendKeys(password);
                _driver.FindElement(By.CssSelector("button[type=\"submit\"]")).Click();
                _driver.FindElement(By.XPath("//div[@id='car_2319']/div[2]/div[4]/a/div")).Click();

                log.Info("Start button clicked ...");
                Thread.Sleep(8000);

                _driver.Navigate().GoToUrl(_baseUrl + "/logout");

                log.Info("Logging out ...");

                _driver.Quit();
                log.Info("Firefox succussfully closed");
            }
            catch(Exception ex)
            {
                log.Error(ex);
                return Request.CreateErrorResponse(System.Net.HttpStatusCode.InternalServerError, ex);
            }

            return Request.CreateResponse(System.Net.HttpStatusCode.OK, Request);
            
        }
    }
}
