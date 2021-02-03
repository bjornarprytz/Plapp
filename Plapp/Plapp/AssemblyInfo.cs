using Plapp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

// Fonts
[assembly: ExportFont("SourceSansPro-Black.otf", Alias = "Black")]
[assembly: ExportFont("SourceSansPro-Bold.otf", Alias = "Bold")]
[assembly: ExportFont("SourceSansPro-It.otf", Alias = "It")]
[assembly: ExportFont("SourceSansPro-Light.otf", Alias = "Light")]
[assembly: ExportFont("SourceSansPro-Regular.otf", Alias = "Regular")]

// Icons
[assembly: ExportFont("MaterialIconsOutlined-Regular.otf", Alias = Fonts.MIOutline)]
[assembly: ExportFont("MaterialIcons-Regular.ttf", Alias = Fonts.MI)]
[assembly: ExportFont("MaterialIconsRound-Regular.otf", Alias = Fonts.MIRound)]
[assembly: ExportFont("MaterialIconsSharp-Regular.otf", Alias = Fonts.MISharp)]
[assembly: ExportFont("MaterialIconsTwoTone-Regular.otf", Alias = Fonts.MITwoTone)]