using Xamarin.Forms;

namespace Plapp
{
    public static class InputViewExtensions
    {
        public static T Keyboard<T>(this T inputView, Keyboard keyboard)
            where T : InputView
        {
            inputView.Keyboard = keyboard;

            return inputView;
        }

        public static T Numeric<T>(this T inputView)
            where T : InputView
        {
            inputView.Keyboard = Xamarin.Forms.Keyboard.Numeric;

            return inputView;
        }
    }
}
