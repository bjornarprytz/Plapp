using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plapp.UI.Pages;

namespace Plapp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        public Dictionary<string, Type> Routes { get; private set; } = new Dictionary<string, Type>();

        public AppShell()
        {
            InitializeComponent();

            RegisterRoutes();
            BindingContext = this;
        }

        void RegisterRoutes()
        {
            // TODO: Register routes
            Routes.Add("topic", typeof(TopicDetailsPage));
            Routes.Add("data-series", typeof(DataSeriesDetailsPage));
    
            foreach (var item in Routes)
            {
                Routing.RegisterRoute(item.Key, item.Value);
            }
        }
    }
}