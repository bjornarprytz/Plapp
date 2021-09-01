using System;
using System.Reactive.Disposables;
using Plapp.Core;
using Plapp.UI.Converters;
using Plapp.UI.Extensions;
using ReactiveUI;
using ReactiveUI.XamForms;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;

namespace Plapp.UI.Pages
{
    public class TopicDetailsPage : BaseContentPage<ITopicViewModel>
    {
        private readonly Button _addImageButton = new();
        private readonly Button _addDataSeriesButton = new();

        private readonly Entry _title = new();
        private readonly Editor _description = new();
        private readonly Image _image = new();
        
        private readonly CollectionView _dataSeries = new ();
        
        public TopicDetailsPage()
        {
            Content = new StackLayout
            {
                Children =
                {
                    new Grid
                    {
                        Children =
                        {
                            _addImageButton
                                .TextColor(Color.Blue),
                            _image
                                .Aspect(Aspect.AspectFill),
                        }
                    },
                    _title
                        .TextColor(Color.Black),
                    _description
                        .TextColor(Color.Black),
                }
            };
        }

        protected override void DoBindings(CompositeDisposable bindingsDisposable)
        {
            this.Bind(ViewModel, topic => topic.Title, page => page._title.Text)
                .DisposeWith(bindingsDisposable);

            this.Bind(ViewModel, topic => topic.Description, page => page._description.Text)
                .DisposeWith(bindingsDisposable);
            
            this.BindCommand(ViewModel, topic => topic.AddImageCommand, page => page._addImageButton)
                .DisposeWith(bindingsDisposable);

            this.OneWayBind(ViewModel, topic => topic.ImageUri, page => page._image.IsVisible, imageUri => imageUri != null)
                .DisposeWith(bindingsDisposable);
            this.OneWayBind(ViewModel, topic => topic.ImageUri, page => page._addImageButton.IsVisible, imageUri => imageUri == null)
                .DisposeWith(bindingsDisposable);
            
            this.OneWayBind(ViewModel, topic => topic.ImageUri, page => page._image.Source, StringTo.ImageSource)
                .DisposeWith(bindingsDisposable);
            
            this.OneWayBind(ViewModel, topic => topic.DataSeries, page => page._dataSeries.ItemsSource)
                .DisposeWith(bindingsDisposable);
        }

    }
}