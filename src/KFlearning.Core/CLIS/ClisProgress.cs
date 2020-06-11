using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFlearning.Core.CLIS
{
    public class ClisProgress
    {
        public int ProgressPercentage => (int)((double)(SuccessCount + FailedCount) / Total * 100);
        public string Message { get; set; }
        public int SuccessCount { get; set; }
        public int FailedCount { get; set; }
        public int Total { get; set; }
    }
}
