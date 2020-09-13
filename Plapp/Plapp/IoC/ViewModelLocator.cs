using Nancy.TinyIoc;

namespace Plapp
{
    public static class ViewModelLocator
    {
        private static TinyIoCContainer _container;

        public static void Setup()
        {
            _container = new TinyIoCContainer();

            _container.Register<IDiaryViewModel, DiaryViewModel>();
        }

        public static T Get<T>()
            where T : class
        {
            return _container.Resolve<T>();
        }
    }
}
