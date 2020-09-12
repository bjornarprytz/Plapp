using Nancy.TinyIoc;
using Plapp.ViewModel;

namespace Plapp
{
    public static class IoC
    {
        private static TinyIoCContainer _container;

        public static void Setup()
        {
            _container = new TinyIoCContainer();
               
            _container.Register<ITopicViewModelFactory, TopicViewModelFactory>();

            // TODO: Register everything
        }

        public static T Get<T>()
            where T : class
        {
            return _container.Resolve<T>();
        }
    }
}
