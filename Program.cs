using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationTask1
{
    internal class Program
    {

        //create refences for Chrome browser
        IWebDriver driver = new ChromeDriver();

        string expectedMessage = "CHECKOUT: COMPLETE!";
        string expectFirstUpdate = "Sauce Labs Backpack";
        string expectSecondUpdate = "2";
        string expectNextUpdate = "Sauce Labs Bolt T-Shirt";

        static void Main(string[] args)
        {

        }
        [SetUp]
        public void Initialize()
        {
            // go to the page 
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            driver.Manage().Window.Maximize();
        }
        [Test]
        public void ExecuteTest()
        {

            // find user field and input username
            IWebElement userName = driver.FindElement(By.Id("user-name"));
            userName.SendKeys("standard_user");

            // find password field and input password
            IWebElement password = driver.FindElement(By.Id("password"));
            password.SendKeys("secret_sauce");

            //find login button and click
            IWebElement login = driver.FindElement(By.Id("login-button"));
            login.Click();

            // add item from the list
            IWebElement firstItem = driver.FindElement(By.Id("add-to-cart-sauce-labs-backpack"));
            firstItem.Click();
                        
            //verify that the cart is update correctly
            IWebElement update= driver.FindElement(By.ClassName("inventory_item_name"));
            string actualFirstUpdate = update.Text;
            Assert.AreEqual(actualFirstUpdate, expectFirstUpdate);

            //open item s details page
            var detail = driver.FindElement(By.LinkText("Sauce Labs Bolt T-Shirt"));
            detail.Click();

            // add second item to the cart
            var secondItem = driver.FindElement(By.Id("add-to-cart-sauce-labs-bolt-t-shirt"));
            secondItem.Click();

            //open the cart 
            IWebElement cart = driver.FindElement(By.Id("shopping_cart_container"));
            cart.Click();

            //verify there are two items
            IWebElement secondUpdate = driver.FindElement(By.ClassName("shopping_cart_badge"));
            var actualSecondUpdate = secondUpdate.GetCssValue("2");
            
            //remove first item from the cart
            IWebElement remove = driver.FindElement(By.Id("remove-sauce-labs-backpack"));
            remove.Click();

            //verify 1 item is present
            IWebElement nextUpdate = driver.FindElement(By.ClassName("inventory_item_name"));
            string actualNextUpdate = nextUpdate.Text;
            Assert.AreEqual(actualNextUpdate, expectNextUpdate);

            //checkout page
            IWebElement checkout = driver.FindElement(By.Id("checkout"));
            checkout.Click();


            //complete the checkout form
            //input First Name
            driver.FindElement(By.Id("first-name")).SendKeys("Petar");

            //input Last Name
            driver.FindElement(By.Id("last-name")).SendKeys("Petrovic");

            //input postal code
            driver.FindElement(By.Id("postal-code")).SendKeys("21000");

            //complete the order
            IWebElement cont = driver.FindElement(By.Id("continue"));
            cont.Click();
            IWebElement finish = driver.FindElement(By.Id("finish"));
            finish.Click();
            
            //verify that the order is completed with the displayed message
            IWebElement message = driver.FindElement(By.ClassName("title"));
            string actualMessage = message.Text;
            Assert.AreEqual(actualMessage, expectedMessage);

           
        }
        [TearDown]
        public void FinishTest()
        {
            //close browser
            driver.Quit();
        }

    }
}
