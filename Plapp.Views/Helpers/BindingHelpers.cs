using System.Text;

namespace Plapp.Views.Helpers
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
