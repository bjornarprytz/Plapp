using Dna;
using Plapp.Core;
using Plapp.Peripherals;
using Plapp.Persist;
using Xamarin.Forms;

[assembly: ExportFont("SourceSansPro-Black.otf", Alias = "Black")]
[assembly: ExportFont("SourceSansPro-BlackIt.otf", Alias = "BlackIt")]
[assembly: ExportFont("SourceSansPro-Bold.otf", Alias = "Bold")]
[assembly: ExportFont("SourceSansPro-BoldIt.otf", Alias = "BoldId")]
[assembly: ExportFont("SourceSansPro-ExtraLight.otf", Alias = "ExtraLight")]
[assembly: ExportFont("SourceSansPro-ExtraLightIt.otf", Alias = "ExtraLightIt")]
[assembly: ExportFont("SourceSansPro-It.otf", Alias = "It")]
[assembly: ExportFont("SourceSansPro-Light.otf", Alias = "Light")]
[assembly: ExportFont("SourceSansPro-LightIt.otf", Alias = "LightIt")]
[assembly: ExportFont("SourceSansPro-Regular.otf", Alias = "Regular")]
[assembly: ExportFont("SourceSansPro-Semibold.otf", Alias = "Semibold")]
[assembly: ExportFont("SourceSansPro-SemiboldIt.otf", Alias = "SemiboldIt")]

namespace Plapp
{
    public partial class App : Application
    {
        public App()
        {

            InitializeComponent();
        }

        protected override async void OnStart()
        {
            Framework.Construct<DefaultFrameworkConstruction>()
                .LoadConfiguration()
                .UsePlappDataStore()
                .AddViewModels()
                .AddCamera()
                .AddNavigation()
                .Build();

            MainPage = new NavigationPage(
                IoC.Get<IViewFactory>()
                 .CreateView<IApplicationViewModel>());

            await IoC.Get<IPlappDataStore>().EnsureStorageReadyAsync();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
