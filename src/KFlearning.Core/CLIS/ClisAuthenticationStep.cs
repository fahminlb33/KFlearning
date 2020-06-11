using OpenQA.Selenium;
using System;
using System.Threading;

namespace KFlearning.Core.CLIS
{

    public class ClisAuthenticationStep : IClisActionStep
    {
        public const int RetryCount = 3;
        public bool Fill(IWebDriver driver, ClisMetadata data, Action<ClisProgress> onProgressDelegate, CancellationToken cancellation)
        {
            int tries = RetryCount;
            while (tries > 0)
            {
                driver.Url = "http://labkom.ilkom.unpak.ac.id/login.php";
                driver.Navigate();

                var usernameElement = driver.FindElement(By.Name("username"));
                usernameElement.Clear();
                usernameElement.SendKeys(data.Username);

                var passwordElement = driver.FindElement(By.Name("password"));
                passwordElement.Clear();
                passwordElement.SendKeys(data.Password);

                var submitElement = driver.FindElement(By.TagName("button"));
                submitElement.Click();

                tries--;
                if (!driver.Url.Contains("login"))
                {
                    Report(onProgressDelegate, true, tries);
                    return true;
                }
                else
                {
                    Report(onProgressDelegate, false, tries);
                }
            }

            return false;
        }

        private void Report(Action<ClisProgress> onProgressDelegate, bool success, int retries)
        {
            onProgressDelegate(new ClisProgress
            {
                SuccessCount = success ? 1 : 0,
                FailedCount = retries,
                Total = 3,
                Message = success ? "Login success." : "Login failed, retrying..."
            });
        }
    }
}
