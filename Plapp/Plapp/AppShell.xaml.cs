using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plapp.UI.Pages;
using Plapp.ViewModels;

namespace Plapp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        private Dictionary<string, Type> Routes { get; } = new ();

        public AppShell()
        {
            InitializeComponent();

            RegisterRoutes();
            BindingContext = this;
        }

        void RegisterRoutes()
        {
            Routes.Add("topic", typeof(TopicDetailsPage));
            Routes.Add("data-series", typeof(DataSeriesDetailsPage));
            Routes.Add("tag", typeof(CreateTagViewModel));
    
            foreach (var item in Routes)
            {
                Routing.RegisterRoute(item.Key, item.Value);
            }
        }
    }
}