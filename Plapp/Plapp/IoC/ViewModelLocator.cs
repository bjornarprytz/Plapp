using Nancy.TinyIoc;
using Plapp.ViewModel.Topc;
using System;

namespace Plapp
{
    public static class ViewModelLocator
    {
        private static TinyIoCContainer _container;

        public static void Setup()
        {
            _container = new TinyIoCContainer();

            _container.Register<IDiaryViewModel>(new DiaryViewModel());
            _container.Register<ITopicViewModel>(new TopicViewModel());
        }

        public static VM Get<VM>()
            where VM : class, IViewModel
        {
            return _container.Resolve<VM>();
        }
    }
}
