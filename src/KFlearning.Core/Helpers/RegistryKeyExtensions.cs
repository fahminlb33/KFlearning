﻿using System;
using Microsoft.Win32;

namespace KFlearning.Core.Helpers
{
    public static class RegistryKeyExtensions
    {
        public static int GetIntValue(this RegistryKey key, string name, int defaultValue = 0)
        {
            if (string.IsNullOrEmpty(name))
            {
                return defaultValue;
            }

            return key == null ? defaultValue : Convert.ToInt32(key.GetValue(name, defaultValue));
        }

        public static string GetStringValue(this RegistryKey key, string name, string defaultValue = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                return defaultValue;
            }

            return key == null ? defaultValue : key.GetValue(name, defaultValue)?.ToString();
        }
    }
}
