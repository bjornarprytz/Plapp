using Xamarin.Forms;

namespace Plapp.Views.Styles
{
    public static class Styles
    {
        static Style<Button> buttons, filledButton;
        static Style<Label> labels;
        static Style<Editor> editors;
        static Style<ContentPage> pages;


        public static ResourceDictionary Implicit => new ResourceDictionary 
        { 
            Buttons, 
            Labels,
            Editors,
            Pages
        };

        public static Style<Button> Buttons => buttons ??= new Style<Button>(
            (Button.FontSizeProperty, Device.GetNamedSize(NamedSize.Small, typeof(Button))),
            (Button.TextColorProperty, Palette.White),
            (Button.BackgroundColorProperty, Palette.Interactive),
            (Button.HorizontalOptionsProperty, LayoutOptions.Center),
            (Button.VerticalOptionsProperty, LayoutOptions.Center)
        ).ApplyToDerivedTypes(true);

        public static Style<Label> Labels => labels ??= new Style<Label>(
            (Label.FontSizeProperty, Device.GetNamedSize(NamedSize.Medium, typeof(Label))),
            (Label.FontFamilyProperty, Fonts.Light),
            (Label.TextColorProperty, Palette.Black)
        ).ApplyToDerivedTypes(true);


        public static Style<Editor> Editors => editors ??= new Style<Editor>(
            (Editor.FontSizeProperty, Device.GetNamedSize(NamedSize.Body, typeof(Editor))),
            (Editor.FontFamilyProperty, Fonts.Regular),
            (Editor.TextColorProperty, Palette.Black)
        ).ApplyToDerivedTypes(true);

        public static Style<ContentPage> Pages => pages ??= new Style<ContentPage>(
            (ContentPage.VisualProperty, "Material"),
            (ContentPage.BackgroundColorProperty, Palette.BackgroundDark)
        ).ApplyToDerivedTypes(true);


        // Explicit style examples (TODO: remove when no longer needed for learning)
        public static Style<Button> FilledButton => filledButton ?? (filledButton = new Style<Button>(
            (Button.TextColorProperty, Palette.White),
            (Button.BackgroundColorProperty, Palette.Interactive)
        )).BasedOn(Buttons);
    }
}
