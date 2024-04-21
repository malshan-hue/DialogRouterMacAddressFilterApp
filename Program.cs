using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace DialogRouterConfigApp
{
    class Program
    {
        static void Main(string[] args)
        {

            string url = "http://192.168.8.1/main.html#index_status";

            string username = "user";
            string password = "o9ZMQMjT";
            string comment = "TroughAutomation";
            List<string> filteredMacAddresses = new List<string>();
            List<string> newMacAddresses = new List<string>();

            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(url);
                System.Threading.Thread.Sleep(2000);

                IWebElement loginLink = driver.FindElement(By.Id("loginlink"));
                loginLink.Click();
                System.Threading.Thread.Sleep(2000);

                IWebElement usernameField = driver.FindElement(By.Id("txtUsr"));
                usernameField.SendKeys(username);
                System.Threading.Thread.Sleep(2000);

                IWebElement passwordField = driver.FindElement(By.Id("txtPwd"));
                passwordField.SendKeys(password);
                System.Threading.Thread.Sleep(2000);

                IWebElement loginButton = driver.FindElement(By.Id("btnLogin"));
                loginButton.Click();
                System.Threading.Thread.Sleep(2000);

                IWebElement firewallLink = driver.FindElement(By.CssSelector("a[data-trans='firewall']"));
                firewallLink.Click();
                System.Threading.Thread.Sleep(2000);

                IWebElement tableBodyFirewall = driver.FindElement(By.CssSelector("table tbody"));
                IList<IWebElement> rowsFirewall = tableBodyFirewall.FindElements(By.TagName("tr"));

                foreach (IWebElement row in rowsFirewall)
                {
                    IList<IWebElement> cells = row.FindElements(By.TagName("td"));
                    if (cells.Count >= 2) 
                    {
                        filteredMacAddresses.Add(cells[1].Text);
                    }
                }
                System.Threading.Thread.Sleep(2000);

                IWebElement deviceSettingsLink = driver.FindElement(By.CssSelector("a[data-trans='device_setting']"));
                deviceSettingsLink.Click();
                System.Threading.Thread.Sleep(2000);

                IWebElement tableBodyDevices = driver.FindElement(By.XPath("//table/tbody")); 
                IList<IWebElement> rowDevices = tableBodyDevices.FindElements(By.TagName("tr"));

                foreach (IWebElement row in rowDevices)
                {
                    IList<IWebElement> cells = row.FindElements(By.TagName("td"));
                    if (cells.Count >= 6)
                    {
                        newMacAddresses.Add(cells[5].Text);
                    }
                }

                List<string> notFilteredMacAddresses = newMacAddresses.Except(filteredMacAddresses).ToList();

                driver.FindElement(By.CssSelector("a[data-trans='firewall']")).Click();
                System.Threading.Thread.Sleep(5000);

                foreach (string notFilteredMacAddress in notFilteredMacAddresses)
                {
                    IWebElement addressField = driver.FindElement(By.Id("txtMacAddress"));
                    addressField.SendKeys(notFilteredMacAddress);
                    System.Threading.Thread.Sleep(2000);

                    IWebElement commentField = driver.FindElement(By.Id("txtComment"));
                    commentField.SendKeys(comment);
                    System.Threading.Thread.Sleep(2000);

                    IList<IWebElement> applyButtons = driver.FindElements(By.CssSelector("input[data-trans='apply']"));

                    if (applyButtons.Count >= 2)
                    {
                        applyButtons[1].Click();
                        System.Threading.Thread.Sleep(2000);

                        IWebElement yesBtn = driver.FindElement(By.Id("yesbtn"));
                        yesBtn.Click();
                        System.Threading.Thread.Sleep(2000);
                    }

                }

                //driver.FindElement(By.CssSelector("a[data-trans='system_settings']")).Click();
                //System.Threading.Thread.Sleep(5000);

                //driver.FindElement(By.CssSelector("a[data-trans='reboot'']")).Click();
                //System.Threading.Thread.Sleep(5000);

                //driver.FindElement(By.CssSelector("a[data-trans='restart_button']")).Click();
                //System.Threading.Thread.Sleep(5000);

                //driver.FindElement(By.Id("yesbtn")).Click();
                //System.Threading.Thread.Sleep(5000);

                driver.Quit();
            }
        }
    }
}
