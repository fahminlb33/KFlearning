using OpenQA.Selenium;
using System;
using System.Linq;
using System.Threading;

namespace KFlearning.Core.CLIS
{
    public class ClisFinalScoreStep : IClisActionStep
    {
        private int _successCount, _failedCount, _total;

        public bool Fill(IWebDriver driver, ClisMetadata data, Action<ClisProgress> onProgressDelegate, CancellationToken cancellation)
        {
            _successCount = 0;
            _failedCount = 0;
            _total = data.Records.Count;

            driver.Url = $"http://labkom.ilkom.unpak.ac.id/index.php?m=transaction&p=form_final_test&id={data.ClassId}";
            driver.Navigate();

            var asprakElement = driver.FindElement(By.XPath("//*[@id=\"atas\"]/div/div[2]/div/div[2]/div[2]/div[1]"));
            if (asprakElement == null || string.IsNullOrWhiteSpace(asprakElement.Text))
            {
                Report(onProgressDelegate, false, null, "Lecturer not detected on page.");
                return false;
            }

            Report(onProgressDelegate, false, null, $"Lecturer detected: {asprakElement.Text.Trim()}");

            var tableRowElements = driver.FindElements(By.XPath(".//tr"));
            foreach (var row in tableRowElements)
            {
                cancellation.ThrowIfCancellationRequested();

                try
                {
                    var npm = row.FindElement(By.XPath(".//td[2]")).Text.Trim();
                    if (npm.Length != 9) continue;

                    var record = data.Records.FirstOrDefault(x => x.Npm == npm);
                    if (record == null)
                    {
                        _failedCount++;
                        Report(onProgressDelegate, false, null, $"Entry not found for {npm}");

                        if (!data.ContinueOnError) break;
                    }
                    else
                    {
                        var nilaiElement = row.FindElement(By.XPath(".//td[4]/input"));
                        nilaiElement.Clear();
                        nilaiElement.SendKeys(((int)record.Score).ToString());
                        _successCount++;

                        Report(onProgressDelegate, true, record);
                    }
                }
                catch (Exception)
                {
                    _failedCount++;
                    Report(onProgressDelegate, false, null, "Failed to process a row.");

                    if (!data.ContinueOnError) break;
                }
            }

            if (_failedCount == 0 || data.ContinueOnError)
            {
                var submitElement = driver.FindElement(By.XPath("//*[@name=\"simpan\"]"));
                submitElement.Click();
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
