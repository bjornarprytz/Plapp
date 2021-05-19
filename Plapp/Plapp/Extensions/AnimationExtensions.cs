using System.Threading.Tasks;
using Xamarin.Forms;

namespace Plapp
{
    public static class AnimationExtensions
    {
        public static Task<bool[]> SillyAnimation<T>(this T element)
            where T : VisualElement
        {
            return Task.Run<bool[]>(
                async () =>
                {
                    while (true)
                    {
                        await Task.WhenAll(
                            element.RotateTo(360, length: 3000, easing: Easing.Linear),
                            element.ScaleTo(1.69, length: 3000, easing: Easing.CubicOut)
                            );
                        element.Rotation = 0;

                        await Task.WhenAll(
                            element.RotateTo(360, length: 3000, easing: Easing.Linear),
                            element.ScaleTo(0.69, length: 3000, easing: Easing.CubicOut)
                            );

                    }
                });
        }
    }
}
