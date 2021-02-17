using MaterialDesign.Icons;
using Xamarin.CommunityToolkit.Markup;
using Xamarin.Forms;


namespace Plapp
{
    public static class Components
    {
        public static Button FloatingActionButton()
            => new Button()
            .MaterialIcon(MaterialIcon.Add)
            .Circle(80);

        public static Grid PhotoFrame(string imageBinding, string isVisibleBinding, string takePhotoCommandBinding)
            => new Grid
            {
                Children =
                    {
                        new Image()
                            .Bind(imageBinding),

                        new Button()
                            .MaterialIcon(MaterialIcon.AddAPhoto, IconSize.Huge)
                            .Bind(VisualElement.IsVisibleProperty, isVisibleBinding)
                            .BindCommand(takePhotoCommandBinding)
                    }
            };

    }
}
