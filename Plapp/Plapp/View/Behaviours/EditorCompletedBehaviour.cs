using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Plapp
{
    public class EditorCompletedBehaviour : Behavior<Editor>
    {
		public static readonly BindableProperty CommandProperty =
			BindableProperty.Create("Command", typeof(ICommand), typeof(EditorCompletedBehaviour), null);
		public static readonly BindableProperty InputConverterProperty =
				BindableProperty.Create("Converter", typeof(IValueConverter), typeof(EditorCompletedBehaviour), null);

		public ICommand Command
		{
			get { return (ICommand)GetValue(CommandProperty); }
			set { SetValue(CommandProperty, value); }
		}

		public IValueConverter Converter
		{
			get { return (IValueConverter)GetValue(InputConverterProperty); }
			set { SetValue(InputConverterProperty, value); }
		}

		public Editor AssociatedObject { get; private set; }

		protected override void OnAttachedTo(Editor bindable)
		{
			base.OnAttachedTo(bindable);
			AssociatedObject = bindable;
			bindable.BindingContextChanged += OnBindingContextChanged;
			bindable.Completed += OnEditorCompleted;
		}

        private void OnEditorCompleted(object sender, EventArgs e)
        {
			if (Command == null)
			{
				return;
			}

			object parameter = Converter?.Convert(e, typeof(object), null, null);
			if (Command.CanExecute(parameter))
			{
				Command.Execute(parameter);
			}
		}

        protected override void OnDetachingFrom(Editor bindable)
		{
			base.OnDetachingFrom(bindable);
			bindable.BindingContextChanged -= OnBindingContextChanged;
			bindable.Completed -= OnEditorCompleted;
			AssociatedObject = null;
		}

		void OnBindingContextChanged(object sender, EventArgs e)
		{
			OnBindingContextChanged();
		}

		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();
			BindingContext = AssociatedObject.BindingContext;
		}
	}
}
