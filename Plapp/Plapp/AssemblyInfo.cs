using Plapp.UI.Constants;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

// Fonts
[assembly: ExportFont("SourceSansPro-Black.otf", Alias = Fonts.Black)]
[assembly: ExportFont("SourceSansPro-Bold.otf", Alias = Fonts.Bold)]
[assembly: ExportFont("SourceSansPro-It.otf", Alias = Fonts.Itaclic)]
[assembly: ExportFont("SourceSansPro-Light.otf", Alias = Fonts.Light)]
[assembly: ExportFont("SourceSansPro-Regular.otf", Alias = Fonts.Regular)]

// Icons
[assembly: ExportFont("MaterialIconsOutlined-Regular.otf", Alias = Fonts.MIOutline)]
[assembly: ExportFont("MaterialIcons-Regular.ttf", Alias = Fonts.MI)]
[assembly: ExportFont("MaterialIconsRound-Regular.otf", Alias = Fonts.MIRound)]
[assembly: ExportFont("MaterialIconsSharp-Regular.otf", Alias = Fonts.MISharp)]
[assembly: ExportFont("MaterialIconsTwoTone-Regular.otf", Alias = Fonts.MITwoTone)]