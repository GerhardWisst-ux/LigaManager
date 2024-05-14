using System;

namespace LigaManagement.Models
{
    public static class ExtensionMethos
    {
        public static bool IsDate(this string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                DateTime dt;
                return (DateTime.TryParse(input, out dt));
            }
            else
            {
                return false;
            }
        }
    }
}
