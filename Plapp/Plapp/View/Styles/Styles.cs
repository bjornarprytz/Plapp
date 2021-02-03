using Xamarin.CommunityToolkit.Markup;
using Xamarin.Forms;

namespace Plapp
{
    public static class Styles
    {
        static Style<Button> buttons, filledButton;
        static Style<Label> labels;
        static Style<Editor> editors;
        static Style<ContentPage> pages;
        static Style<Span> link;


        public static ResourceDictionary Implicit => new ResourceDictionary 
        { 
            Buttons, 
            Labels,
            Editors,
            Pages
        };

        public static Style<Button> Buttons => buttons ??= new Style<Button>(
            (Button.FontSizeProperty, Device.GetNamedSize(NamedSize.Small, typeof(Button))),
            (Button.TextColorProperty, Colors.ForegroundVeryDark.ToColor()),
            (Button.BackgroundColorProperty, Colors.BackgroundLight.ToColor()),
            (Button.HorizontalOptionsProperty, LayoutOptions.Center),
            (Button.VerticalOptionsProperty, LayoutOptions.Center)
        ).ApplyToDerivedTypes(true);

        public static Style<Label> Labels => labels ??= new Style<Label>(
            (Label.FontSizeProperty, Device.GetNamedSize(NamedSize.Medium, typeof(Label))),
            (Label.FontFamilyProperty, Fonts.Light),
            (Label.TextColorProperty, Colors.ForegroundMain.ToColor())
        ).ApplyToDerivedTypes(true);


        public static Style<Editor> Editors => editors ??= new Style<Editor>(
            (Editor.FontSizeProperty, Device.GetNamedSize(NamedSize.Body, typeof(Editor))),
            (Editor.FontFamilyProperty, Fonts.Regular),
            (Editor.TextColorProperty, Colors.ForegroundMain.ToColor())
        ).ApplyToDerivedTypes(true);

        public static Style<ContentPage> Pages => pages ??= new Style<ContentPage>(
            (ContentPage.VisualProperty, "Material"),
            (ContentPage.BackgroundColorProperty, Colors.BackgroundDark.ToColor())
        ).ApplyToDerivedTypes(true);


        // Explicit style examples (TODO: remove when no longer needed for learning)
        public static Style<Button> FilledButton => filledButton ?? (filledButton = new Style<Button>(
            (Button.TextColorProperty, Colors.ForegroundVeryDark.ToColor()),
            (Button.BackgroundColorProperty, Colors.BackgroundLight.ToColor())
        )).BasedOn(Buttons);

        public static Style<Span> Link => link ??= new Style<Span>(
            (Span.TextColorProperty, Color.Blue),
            (Span.TextDecorationsProperty, TextDecorations.Underline)
        );
    }
}
