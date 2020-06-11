using OpenQA.Selenium;
using System;
using System.Threading;

namespace KFlearning.Core.CLIS
{
    public class ClisLecturerStudentStep : IClisActionStep
    {
        private int _successCount, _failedCount, _total;

        public bool Fill(IWebDriver driver, ClisMetadata data, Action<ClisProgress> onProgressDelegate, CancellationToken cancellation)
        {
            _successCount = 0;
            _failedCount = 0;
            _total = data.Records.Count;

            driver.Url = $"http://labkom.ilkom.unpak.ac.id/index.php?m=master&p=form_list_student&id={data.ClassId}";
            driver.Navigate();

            var asprakElement = driver.FindElement(By.XPath("//*[@id=\"atas\"]/div/div[2]/div/div[2]/div[2]/div[1]"));
            if (asprakElement == null || string.IsNullOrWhiteSpace(asprakElement.Text))
            {
                Report(onProgressDelegate, false, null, "Lecturer not detected on page.");
                return false;
            }

            Report(onProgressDelegate, false, null, $"Lecturer detected: {asprakElement.Text.Trim()}");
            foreach (var entry in data.Records)
            {
                cancellation.ThrowIfCancellationRequested();

                try
                {
                    var npmElement = driver.FindElement(By.XPath("//*[@name=\"npm\"]"));
                    npmElement.Clear();
                    npmElement.SendKeys(entry.Npm);

                    var submitElement = driver.FindElement(By.XPath("//*[@name=\"simpan\"]"));
                    submitElement.Click();

                    if (driver.PageSource.Contains("Failed!"))
                    {
                        _failedCount++;

                        var errorText = driver.FindElement(By.XPath("//*[@id=\"tampil\"]/div[1]/div")).Text;
                        Report(onProgressDelegate, false, entry, $"Entry {entry.Name} error, {errorText}");

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
