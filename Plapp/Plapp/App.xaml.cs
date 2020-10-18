using Dna;
using Plapp.Core;
using Plapp.Peripherals;
using Plapp.Persist;
using Xamarin.Forms;

[assembly: ExportFont("SourceSansPro-Black.otf", Alias = "Black")]
[assembly: ExportFont("SourceSansPro-Bold.otf", Alias = "Bold")]
[assembly: ExportFont("SourceSansPro-It.otf", Alias = "It")]
[assembly: ExportFont("SourceSansPro-Light.otf", Alias = "Light")]
[assembly: ExportFont("SourceSansPro-Regular.otf", Alias = "Regular")]

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
                .AddIcons()
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
