namespace Services.Common
{
    /// <summary>
    /// String Manipulation class
    /// </summary>
    public static class StringManipulation
    {
        /// <summary>
        /// Normalize Name Function
        /// </summary>
        /// <param name="name"></param>
        /// <returns>The specified string to title lower case</returns>
        public static string NormalizeName(string name)
        {
            return System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(name.ToLower());
        }
    }
}
