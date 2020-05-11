// SOLUTION : KFlearning
// PROJECT  : KFlearning.Core
// FILENAME : UpdateDefinition.cs
// AUTHOR   : Fahmi Noor Fiqri, Kodesiana.com
// WEBSITE  : https://kodesiana.com
// REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
// This file is part of KFlearning, see LICENSE.
// See this code in repository URL above!

using System;

namespace KFlearning.Core.API
{
    public class UpdateDefinition
    {
        public DateTime UpdateDate { get; set; }
        public string TagName { get; set; }
        public string ReleaseName { get; set; }
        public string Description { get; set; }
        public string DownloadUrl { get; set; }
        public string ReleaseUrl { get; set; }
    }
}