using System.Collections.Generic;

namespace KFlearning.Core.CLIS
{
    public class ClisMetadata
    {
        public bool ContinueOnError { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Class { get; set; }
        public string ClassId { get; set; }
        public string Year { get; set; }
        public IList<ClisRecord> Records { get; set; }
    }
}
