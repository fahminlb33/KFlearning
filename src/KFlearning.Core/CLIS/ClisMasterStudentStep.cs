using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;

namespace KFlearning.Core.CLIS
{
    public class ClisMasterStudentStep : IClisActionStep
    {
        private int _successCount, _failedCount, _total;

        public bool Fill(IWebDriver driver, ClisMetadata data, Action<ClisProgress> onProgressDelegate, CancellationToken cancellation)
        {
            _successCount = 0;
            _failedCount = 0;
            _total = data.Records.Count;

            foreach (var entry in data.Records)
            {
                cancellation.ThrowIfCancellationRequested();

                try
                {
                    driver.Url = "http://labkom.ilkom.unpak.ac.id/?m=master&p=form_student";
                    driver.Navigate();

                    var npmElement = driver.FindElement(By.XPath("//*[@name=\"npm\"]"));
                    npmElement.Clear();
                    npmElement.SendKeys(entry.Npm);

                    var namaElement = driver.FindElement(By.XPath("//*[@name=\"nama\"]"));
                    namaElement.Clear();
                    namaElement.SendKeys(entry.Name);

                    var kelasElement = new SelectElement(driver.FindElement(By.XPath("//*[@name=\"kelas\"]")));
                    kelasElement.SelectByValue(data.Class);

                    var tahunElement = driver.FindElement(By.XPath("//*[@name=\"tahun_angkatan\"]"));
                    tahunElement.Clear();
                    tahunElement.SendKeys(data.Year);

                    var submitElement = driver.FindElement(By.XPath("//*[@name=\"save\"]"));
                    submitElement.Click();

                    if (driver.PageSource.Contains("Failed!") || driver.PageSource.Contains("Warning!"))
                    {
                        _failedCount++;

                        var errorText = driver.FindElement(By.XPath("//div/div[@role=\"alert\"]")).Text;
                        Report(onProgressDelegate, false, entry, errorText);

                        if (!data.ContinueOnError) break;
                    }
                    else
                    {
                        _successCount++;
                        Report(onProgressDelegate, true, entry);
                    }
                }
                catch (Exception)
                {
                    _failedCount++;
                    Report(onProgressDelegate, false, entry);

                    if (!data.ContinueOnError) break;
                }
            }

            return _failedCount == 0;
        }

        private void Report(Action<ClisProgress> onProgressDelegate, bool success, ClisRecord record, string message = null)
        {
            onProgressDelegate(new ClisProgress
            {
                SuccessCount = _successCount,
                FailedCount = _failedCount,
                Total = _total,
                Message = message == null ? (success ? $"Processed entry: {record.Name}." : $"Entry failed: {record.Name}") : message
            });
        }
    }
}
