namespace Services.Common
{
    /// <summary>
    /// String Manipulation
    /// </summary>
    public static class StringManipulation
    {
        /// <summary>
        /// Capitalize the first letter in a string and make the rest lowercase
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string NormalizeName(string name)
        {
            return System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(name.ToLower());
        }
    }
}
