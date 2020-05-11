﻿// SOLUTION : KFlearning
// PROJECT  : KFlearning.Core
// FILENAME : NativeConstants.cs
// AUTHOR   : Fahmi Noor Fiqri, Kodesiana.com
// WEBSITE  : https://kodesiana.com
// REPO     : https://github.com/Kodesiana or https://github.com/fahminlb33
// 
// This file is part of KFlearning, see LICENSE.
// See this code in repository URL above!

namespace KFlearning.Core.Native
{
    internal static class NativeConstants
    {
        public const string UacRegistryKey = "Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System";
        public const string UacRegistryValue = "EnableLUA";

        public static uint STANDARD_RIGHTS_READ = 0x00020000;
        public static uint TOKEN_QUERY = 0x0008;
        public static uint TOKEN_READ = (STANDARD_RIGHTS_READ | TOKEN_QUERY);
    }
}