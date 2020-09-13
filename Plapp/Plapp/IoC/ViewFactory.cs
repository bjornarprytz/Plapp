using System;
using Xamarin.Forms;

namespace Plapp
{
    public static class ViewFactory
    {
        public static Page Resolve<VM>(Action<VM> setStateAction = null)
            where VM : class, IViewModel
        {
            var viewModel = ViewModelLocator.Get<VM>();
            var view = ViewLocator.ResolvePage<VM>();

            if (setStateAction != null)
                viewModel.SetState(setStateAction);

            view.BindingContext = viewModel;
            
            return view;
        }

        public static Page Resolve<VM>(VM viewModel, Action<VM> setStateAction = null)
            where VM : class, IViewModel
        {
            var view = ViewLocator.ResolvePage<VM>();

            if (setStateAction != null)
                viewModel.SetState(setStateAction);

            view.BindingContext = viewModel;

            return view;
        }
    }
}
