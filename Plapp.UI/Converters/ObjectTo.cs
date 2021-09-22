using System;

namespace Plapp.UI.Converters
{
    public class ObjectTo
    {
        public static T Enum<T>(object arg)
            where T : Enum
        {
            if (arg is string s)
            {
                return StringTo.Enum<T>(s);
            }

            return default;
        }
    }
}