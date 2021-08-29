using System.Reactive.Disposables;
using Plapp.Core;
using Plapp.UI.Converters;
using ReactiveUI;
using Xamarin.Forms;

namespace Plapp.UI.ContentViews
{
    public class TopicThumbnail : BaseContentView<ITopicViewModel>
    {
        private readonly Label _title = new();
        private readonly Image _image = new() { Aspect = Aspect.AspectFit };
        
        private readonly TapGestureRecognizer _tapGesture = new();
        
        public TopicThumbnail()
        {
            Content = new Frame()
            {
                CornerRadius = 10,
                Padding = 5,
                
                BackgroundColor = Color.Purple,
                BorderColor = Color.Aquamarine,
                
                Content = new StackLayout
                {
                    GestureRecognizers = { _tapGesture },   
                    
                    Children =
                    {
                        _title,
                        _image
                    }
                }
            };
        }
        
        protected override void DoBindings(CompositeDisposable bindingsDisposable)
        {
            this.BindCommand(ViewModel, topic => topic.OpenCommand, thumbnail => thumbnail._tapGesture)
                .DisposeWith(bindingsDisposable);

            this.OneWayBind(ViewModel, topic => topic.Title, thumbnail => thumbnail._title.Text)
                .DisposeWith(bindingsDisposable);

            this.OneWayBind(ViewModel, topic => topic.ImageUri, thumbnail => thumbnail._image.Source, StringTo.ImageSource)
                .DisposeWith(bindingsDisposable);
        }
    }
}