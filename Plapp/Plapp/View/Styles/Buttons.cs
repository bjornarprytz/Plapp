using Xamarin.Forms;

namespace Plapp
{
    public class Buttons
    {
        protected Buttons() { }

        public static Button FloatingActionButton
            => new Button()
            .MaterialIcon(MaterialDesign.Icons.MaterialIcon.Add)
            .BackgroundColor(Colors.Attention)
            .Circle(80);
    }
}
