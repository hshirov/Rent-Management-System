namespace Services.Common
{
    public static class StringManipulation
    {
        public static string NormalizeName(string name)
        {
            return System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(name.ToLower());
        }
    }
}
