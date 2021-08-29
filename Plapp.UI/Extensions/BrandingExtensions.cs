using Xamarin.Forms;

namespace Plapp.UI.Extensions
{
    public static partial class BrandingExtensions
    {
        public static T BackgroundColor<T>(this T visualElement, Color color) 
            where T : VisualElement 
        { 
            visualElement.BackgroundColor = color; 
            return visualElement; 
        }
        
        public static T BorderColor<T>(this T frame, Color color) 
            where T : Frame 
        { 
            frame.BorderColor = color; 
            return frame; 
        }


        public static T Color<T>(this T boxView, Color color) 
            where T : BoxView 
        { 
            boxView.Color = color; 
            return boxView; 
        }

        public static Label TextColor(this Label label, Color color) 
        { 
            label.TextColor = color; 
            return label; 
        }

        public static Button TextColor(this Button button, Color color) 
        { 
            button.TextColor = color; 
            return button; 
        }

        public static Entry TextColor(this Entry entry, Color color) 
        { 
            entry.TextColor = color; 
            return entry; 
        }
    }
}