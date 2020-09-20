using Nancy.TinyIoc;
using System;
using Xamarin.Forms;

namespace Plapp
{
    public static class IoC
    {
        private static TinyIoCContainer _container => TinyIoCContainer.Current;

        public static void Setup()
        {

            _container.Register(new MainPage());
            _container.Register(new TopicPage());

            _container.Register<ITopicService>(new TopicService());
            _container.Register<INavigator>(new Navigator());
            _container.Register<IViewFactory>(new ViewFactory());

            _container.Register<IDiaryViewModel>(new DiaryViewModel());
            _container.Register<ITopicViewModel>(new TopicViewModel());
        }

        public static T Get<T>()
            where T : class
        {
            return _container.Resolve<T>();
        }

        public static T Resolve<T>(Type type)
            where T : class
        {
            return _container.Resolve(type) as T;
        }

        public static object Resolve(Type type)
        {
            return _container.Resolve(type);
        }
    }
}
