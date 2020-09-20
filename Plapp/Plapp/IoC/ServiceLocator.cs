using Nancy.TinyIoc;
using Xamarin.Forms;

namespace Plapp
{
    public static class ServiceLocator
    {
        private static TinyIoCContainer _container;

        public static void Setup()
        {
            _container = TinyIoCContainer.Current;
               
            _container.Register<ITopicService>(new TopicService());
            _container.Register<INavigator>(new Navigator());
        }

        public static T Get<T>()
            where T : class
        {
            return _container.Resolve<T>();
        }
    }
}
