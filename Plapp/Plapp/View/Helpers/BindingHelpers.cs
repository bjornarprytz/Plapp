using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Plapp
{
    public static class BindingHelpers
    {
        public static string BuildPath(params string[] pathNodes)
        {
            var sb = new StringBuilder();

            sb.AppendJoin('.', pathNodes);

            return sb.ToString();
        }

    }
}
