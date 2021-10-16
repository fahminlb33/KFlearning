namespace KFlearning.Core.Helpers
{
    public static class StringExtensions
    {
        public static string TrimLongText(this string path, int maxLength = 40)
        {
            if (string.IsNullOrEmpty(path))
            {
                return string.Empty;
            }

            return path.Length <= maxLength ? path : path.Substring(0, maxLength) + "...";
        }
    }
}
