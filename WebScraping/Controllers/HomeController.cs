using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.ObjectModel;

namespace WebScraping.Controllers
{
    public class HomeController : Controller
    {
        private IWebDriver driver;
        public ActionResult Index()
        {
            var list = new List<string>();

            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.reverbnation.com/");

            var discoverButton = ((ChromeDriver)driver).FindElementById("desktop_header_discover_menu_li");
            discoverButton.Click();

            var collections = FindElements(By.ClassName("card__contents"));
            foreach (var collection in collections)
                list.Add(collection.Text);

            return View(list);
        }

        private IReadOnlyCollection<IWebElement>FindElements(By by)
        {
            Stopwatch w = Stopwatch.StartNew();

            while(w.ElapsedMilliseconds < 5 * 1000)
            {
                var elements = driver.FindElements(by);

                if (elements.Count > 0)
                    return elements;

                Thread.Sleep(10);
            }
            return new ReadOnlyCollection<IWebElement>(new List<IWebElement>());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}