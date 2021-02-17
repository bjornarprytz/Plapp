using MaterialDesign.Icons;
using Plapp.Core;
using Xamarin.CommunityToolkit.Markup;
using Xamarin.Forms;

namespace Plapp
{
    public class DataSeriesTemplate : BaseDataTemplateSelector<IDataSeriesViewModel>
    {
        protected override DataTemplate OnSelectTypedTemplate(IDataSeriesViewModel viewModel, BindableObject container)
        {
            return new DataTemplate(
                () => new ContentView
                {
                    Content = new Grid
                    {
                        ColumnDefinitions =
                        {
                            new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) },
                            new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) },
                            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                        },

                        RowDefinitions =
                        {
                            new RowDefinition{},
                            new RowDefinition{},
                        },

                        Children =
                        {
                            // TODO: Replace this with TagTemplate or something
                            new Label().Row(0)
                                .Bind(nameof(viewModel.Id)), // Should be TagId
                            new Label().Row(1)
                                .Bind(nameof(viewModel.Latest.Date), converter: new DateTimeToDateStringConverter()),
                            new Label().Row(1).Column(1)
                                .Bind(nameof(viewModel.Tag.Unit)),
                            new Button().Column(2)
                                .MaterialIcon(MaterialIcon.Add)
                                
                        }
                    }
                });
        }
    }
}
