using System.Reactive.Disposables;
using Microcharts.Forms;
using Plapp.Core;
using Plapp.UI.Converters;
using Plapp.UI.Extensions;
using ReactiveUI;
using Xamarin.Forms;

namespace Plapp.UI.ContentViews
{
    public class TopicSummary : BaseContentView<ITopicViewModel>
    {
        private readonly Label _title = new();
        private readonly Image _image = new();
        private readonly DataSummary _dataSummary = new();
        
        private readonly TapGestureRecognizer _tapGesture = new();
        
        public TopicSummary()
        {  
            Content = new StackLayout
            {
                GestureRecognizers = { _tapGesture },   
                
                Children =
                {
                    _image.Aspect(Aspect.AspectFit),
                    _title.NamedFontSize(NamedSize.Header),
                    _dataSummary,
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

            this.OneWayBind(ViewModel, topic => topic, summary => summary._dataSummary.BindingContext)
                .DisposeWith(bindingsDisposable);
        }
    }
}