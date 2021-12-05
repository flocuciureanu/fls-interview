using System.Text.RegularExpressions;

namespace FirstLineSoftwareInterviewTask.Common.Core.ExtensionMethods
{
    public static class StringExtensionMethods
    {
        public static bool IsValidEmailAddress(this string emailAddress)
        {
            var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            var match = regex.Match(emailAddress);
            return match.Success;
        }
    }
}