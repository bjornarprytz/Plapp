using Plapp.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plapp
{
    public static class PlappViews
    {
        public static IViewFactory Configure()
        {
            return new ViewFactory()
                    .ChainBindPage<IApplicationViewModel, MainPage>()
                    .ChainBindPage<ITopicViewModel, TopicPage>()
                    .ChainBindPopup<ICreateViewModel<ITagViewModel>, CreateTagPopup>()
                    .ChainBindPopup<ICreateMultipleViewModel<IDataPointViewModel>, CreateDataPointsPopup>();
        }

        private static IViewFactory ChainBindPage<TViewModel, TView>(this IViewFactory viewFactory)
            where TViewModel : IIOViewModel
            where TView : BaseContentPage<TViewModel>
        {
            viewFactory.BindPage<TViewModel, TView>();

            return viewFactory;
        }

        private static IViewFactory ChainBindPopup<TViewModel, TView>(this IViewFactory viewFactory)
            where TViewModel : ITaskViewModel, IIOViewModel
            where TView : BasePopupPage<TViewModel>
        {
            viewFactory.BindPopup<TViewModel, TView>();

            return viewFactory;
        }
    }
}
