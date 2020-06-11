using OpenQA.Selenium;
using System;
using System.Threading;

namespace KFlearning.Core.CLIS
{
    public interface IClisActionStep
    {
        bool Fill(IWebDriver driver, ClisMetadata data, Action<ClisProgress> onProgressDelegate, CancellationToken cancellation);
    }
}
