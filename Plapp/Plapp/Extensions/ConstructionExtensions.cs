﻿using Plapp.Core;
using Dna;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;
using Plugin.Iconize;
using Plugin.Iconize.Fonts;

namespace Plapp
{
    public static class ConstructionExtensions
    {
        public static FrameworkConstruction AddViewModels(this FrameworkConstruction construction)
        {
            construction.Services.AddSingleton<IApplicationViewModel, ApplicationViewModel>();

            return construction;
        }
        
        public static FrameworkConstruction AddIcons(this FrameworkConstruction construction)
        {
            Iconize.With(new MaterialDesignIconsModule());

            return construction;
        }

        public static FrameworkConstruction AddNavigation(this FrameworkConstruction construction)
        {
            construction.Services.AddSingleton(new MainPage());
            construction.Services.AddSingleton(new TopicPage());

            construction.Services.AddSingleton(provider =>
                new ViewFactory()
                    .ChainBind<IApplicationViewModel, MainPage>()
                    .ChainBind<ITopicViewModel, TopicPage>()
            );

            construction.Services.AddSingleton<INavigator>(new Navigator());

            construction.Services.AddSingleton(provider => Application.Current.MainPage.Navigation);

            return construction;
        }


        private static IViewFactory ChainBind<TViewModel, TView>(this IViewFactory viewFactory)
            where TViewModel : class, IViewModel
            where TView : Page
        {
            viewFactory.Bind<TViewModel, TView>();

            return viewFactory;
        }
    }
}
