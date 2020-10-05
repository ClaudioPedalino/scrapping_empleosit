using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using scrapper.console.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace scrapper.console
{
    class Program
    {
        static void Main(string[] args)
        {
            var dirverPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            //Ui__________________________
            Console.WriteLine("Elegí la opción:\n01.Buscar ofertas por una palabra clave\n02.Para obtener los detalles de los banners sin detalles");
            var opt = Convert.ToInt32(Console.ReadLine());

            switch (opt)
            {
                case 1:
                    Console.WriteLine("Por qué palabra calve querés buscar?");
                    var searchBy = Console.ReadLine();
                    SearchOffers(dirverPath, searchBy);
                    break;
                case 2:
                    Console.WriteLine("..vamos a buscar los detalles de las ofertas que aún no lo tengan..");
                    GetDetails(dirverPath);
                    break;
                default:
                    break;
            }

            Console.WriteLine("End!");
        }

        private static void GetDetails(string dirverPath)
        {
            var offersToSearchDetails = new List<Offer>();
            using (var dataContext = new DataContext())
            {
                offersToSearchDetails = dataContext.Offers.Where(x => string.IsNullOrWhiteSpace(x.OfferDetail)).ToList();
            }

            IWebDriver driver = new ChromeDriver(dirverPath);
            var offersToUpdate = new List<Offer>();
            foreach (var offer in offersToSearchDetails)
            {
                driver.Navigate().GoToUrl(offer.Link);

                var detail = driver.FindElement(By.ClassName("displayField"));
                var spans = driver.FindElements(By.TagName("span"));
                foreach (var s in spans)
                {
                    offer.OfferDetail += s.Text;
                };
                offersToUpdate.Add(offer);
            }

            using (var dataContext = new DataContext())
            {
                dataContext.Offers.UpdateRange(offersToUpdate);
                dataContext.SaveChanges();
            }

            Close(driver);
        }

        private static void SearchOffers(string dirverPath, string searchBy)
        {
            using (IWebDriver driver = new ChromeDriver(dirverPath))
            {
                //Setup Driver
                driver.Navigate().GoToUrl("https://www.empleosit.com.ar/");
                driver.Manage().Window.Maximize();

                //Search Offers
                Search(driver, searchBy);

                SetupMaxPageSize(driver);
                List<Offer> offersToSave = GetOffers(driver);

                //Save Offers
                SaveOffersInDb(offersToSave);
                Close(driver);
            }
        }

        private static void Close(IWebDriver driver)
        {
            driver.Quit();
            Environment.Exit(0);
        }

        private static void SaveOffersInDb(List<Offer> offersToSave)
        {
            using (var dataContext = new DataContext())
            {
                dataContext.Offers.AddRange(offersToSave);
                dataContext.SaveChanges();
            }
        }

        private static List<Offer> GetOffers(IWebDriver driver)
        {
            var offersList = driver.FindElements(By.ClassName("listing-right"));
            var offersToSave = new List<Offer>();
            foreach (var item in offersList)
            {

                var offer = new Offer()
                {
                    OfferId = Guid.NewGuid(),
                    Title = item.FindElement(By.ClassName("listing-title")).Text,
                    BannerText = item.FindElement(By.ClassName("show-brief")).Text,
                    Link = item.FindElement(By.ClassName("viewDetails")).FindElement(By.TagName("a")).GetAttribute("href"),
                    Location = item.FindElement(By.ClassName("location-ico")).Text,
                    PublishedDate = GetPublishedDate(item.FindElement(By.ClassName("posted-ico")).Text),
                    Company = item.FindElement(By.ClassName("company-ico")).Text
                };


                offersToSave.Add(offer);
            }

            return offersToSave;
        }

        private static void SetupMaxPageSize(IWebDriver driver)
        {
            var cboPages = driver.FindElement(By.Id("listings_per_page_form"));
            cboPages.Click();
            Thread.Sleep(3000);
            cboPages.FindElement(By.ClassName("sbOptions"))
                    .FindElements(By.TagName("li"))
                    .Last()
                    .Click();
        }

        private static void Search(IWebDriver driver, string searchBy)
        {
            IWebElement textbox = driver.FindElement(By.Id("keywords"));
            textbox.SendKeys(searchBy);
            textbox.Submit();
        }

        private static DateTime GetPublishedDate(string input)
        {
            string[] splitted = input.Split('/');
            var date = new DateTime(Convert.ToInt32(splitted[2]), Convert.ToInt32(splitted[1]), Convert.ToInt32(splitted[0]));
            return date;
        }
    }
}
