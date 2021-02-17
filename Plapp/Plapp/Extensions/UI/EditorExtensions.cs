using Xamarin.Forms;

namespace Plapp
{
    public static class EditorExtensions
    {
        public static T AutoSize<T>(this T editor, EditorAutoSizeOption autoSizeOption)
            where T : Editor
        {
            editor.AutoSize = autoSizeOption;

            return editor;
        }
    }
}
