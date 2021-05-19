using Xamarin.Essentials;

namespace Plapp
{
    public static class DeviceHelpers
    {
        public static double HalfWidth => DeviceDisplay.MainDisplayInfo.Width / 2;
        public static double HalfHeight => DeviceDisplay.MainDisplayInfo.Height / 2;
        public static double Height => DeviceDisplay.MainDisplayInfo.Height;
        public static double Width => DeviceDisplay.MainDisplayInfo.Width;
    }
}
