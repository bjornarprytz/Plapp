namespace Plapp.ViewModels
{
    public static class StringExtensions
    {
        public static bool IsMissing(this string str) => string.IsNullOrEmpty(str);
        public static bool IsPresent(this string str) => !str.IsMissing();
    }
}