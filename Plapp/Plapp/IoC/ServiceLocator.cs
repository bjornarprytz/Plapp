using Nancy.TinyIoc;

namespace Plapp
{
    public static class ServiceLocator
    {
        private static TinyIoCContainer _container;

        public static void Setup()
        {
            _container = new TinyIoCContainer();
               
            _container.Register<ITopicService, TopicService>();
        }

        public static T Get<T>()
            where T : class
        {
            return _container.Resolve<T>();
        }
    }
}
