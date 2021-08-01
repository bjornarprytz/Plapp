using Xamarin.Forms;

namespace Plapp.Views.Extensions.UI
{
    public static class BindingExtensions
    {
        public static T BindItems<T>(this T collectionView, string path, DataTemplate template)
                where T : ItemsView
        {
            collectionView.SetBinding(ItemsView.ItemsSourceProperty, path);
            collectionView.ItemTemplate = template;

            return collectionView;
        }

        public static T BindContext<T>(this T bindableObject, string path = ".")
            where T : BindableObject
        {
            return bindableObject.Bind(BindableObject.BindingContextProperty, path);
        }

        public static T BindContext<T>(this T bindableObject, string path, IValueConverter valueConverter)
            where T : BindableObject
        {
            return bindableObject.Bind(BindableObject.BindingContextProperty, path, converter: valueConverter);
        }

        public static T BindTrigger<T>(this T visualElement, BindableProperty property, object propertySetterValue, object source, string path, object triggerValue)
            where T : VisualElement
        {
            var binding = new Binding
            {
                Source = source,
                Path = path
            };

            var trigger = new DataTrigger(typeof(T))
            {
                Binding = binding,
                Value = triggerValue,
            };

            trigger.Setters.Add(new Setter
            {
                Property = property,
                Value = propertySetterValue
            });

            visualElement.Triggers.Add(trigger);

            return visualElement;
        }
    }
}
