using System.Reactive.Disposables;
using Plapp.Core;
using Plapp.UI.Converters;
using ReactiveUI;
using ReactiveUI.XamForms;
using Xamarin.Forms;

namespace Plapp.UI.Pages
{
    public class TopicDetailsPage : BaseContentPage<ITopicViewModel>
    {
        private readonly Button _addPhotoButton = new();

        private readonly Entry _title = new();
        private readonly Image _image = new();
        
        private readonly CollectionView _dataSeries = new ();
        
        public TopicDetailsPage()
        {
            Content = new StackLayout
            {
                Children =
                {
                    _title,
                    _image,
                    _addPhotoButton
                }
            };
        }

        protected override void DoBindings(CompositeDisposable bindingsDisposable)
        {
            this.Bind(ViewModel, topic => topic.Title, page => page._title.Text)
                .DisposeWith(bindingsDisposable);
            
            this.BindCommand(ViewModel, topic => topic.AddImageCommand, page => page._addPhotoButton)
                .DisposeWith(bindingsDisposable);

            this.OneWayBind(ViewModel, topic => topic.ImageUri, page => page._image.Source, StringTo.ImageSource)
                .DisposeWith(bindingsDisposable);
            
            this.OneWayBind(ViewModel, topic => topic.DataSeries, page => page._dataSeries.ItemsSource)
                .DisposeWith(bindingsDisposable);
        }

    }
}