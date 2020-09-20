using Nancy.TinyIoc;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Plapp
{
    public static class ViewLocator
    {
        private static TinyIoCContainer _container;

        public static void Setup()
        {
            _container = TinyIoCContainer.Current;

            _container.Register(new MainPage());
            _container.Register(new TopicPage());
        }

        public static Page ResolvePage<VM>() 
            where VM : IViewModel => Map(typeof(VM));

        public static Page ResolvePage(Type viewModelType) => Map(viewModelType);
        

        private static Page Map(Type viewModelType)
        => (viewModelType) switch
        {
            ITopicViewModel => _container.Resolve<TopicPage>(),
            IDiaryViewModel => _container.Resolve<MainPage>(),
            _ => _container.Resolve<MainPage>()
        };
    }
}
